using System.Buffers;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using static us.awise.biscuits.generated.Methods;

namespace us.awise.biscuits;

public sealed unsafe class Biscuit : IDisposable
{
    private const int SEED_SIZE = 32;

    public static Biscuit Create(KeyPair keyPair, Action<BiscuitBuilder> builderCallback)
    {
        if (keyPair == null)
            throw new ArgumentNullException(nameof(keyPair));
        if (builderCallback is null)
            throw new ArgumentNullException(nameof(builderCallback));

        // TODO: figure out if we need to use the same seed as the key pair
        // TODO: maybe deduplicate this logic with that in KeyPair
#if NET
        Span<byte> buf = stackalloc byte[SEED_SIZE];
        RandomNumberGenerator.Fill(buf);
#else
        byte[] buf = ArrayPool<byte>.Shared.Rent(SEED_SIZE);
        using var ran = RandomNumberGenerator.Create();
        ran.GetBytes(buf, 0, SEED_SIZE);
#endif
        var ret = new Biscuit(); // create ahead of time so that we don't have to deal with OOM after creating the native handle
        lock (keyPair)
        {
            if (keyPair._handle == null)
                throw new ObjectDisposedException(nameof(KeyPair));
            using var builder = new BiscuitBuilder();
            builderCallback(builder);
            fixed (byte* bufPtr = buf)
            {
                ret._handle = biscuit_builder_build(builder._handle, keyPair._handle, bufPtr, SEED_SIZE);
                GC.KeepAlive(builder);
                GC.KeepAlive(keyPair);
            }
        }
        if (ret._handle == null)
        {
            throw BiscuitException.FromLastError();
        }
#if !NET
        ArrayPool<byte>.Shared.Return(buf, clearArray: true);
#endif
        return ret;
    }

    internal generated.Biscuit* _handle;

    private Biscuit()
    {
    }

    public Biscuit(ReadOnlySpan<byte> serialized, PublicKey key)
    {
        lock (key)
        {
            if (key._handle == null)
            {
                throw new ObjectDisposedException(nameof(PublicKey));
            }

            fixed (byte* pBuf = serialized)
            {
                _handle = biscuit_from(pBuf, (nuint)serialized.Length, key._handle);
                if (_handle == null)
                {
                    throw BiscuitException.FromLastError();
                }
            }
        }
    }

    public Biscuit AppendBlock(KeyPair kp, Action<BlockBuilder> builderCallback)
    {
        if (builderCallback is null)
            throw new ArgumentNullException(nameof(builderCallback));
        if (kp is null)
            throw new ArgumentNullException(nameof(kp));

        var ret = new Biscuit(); // create ahead of time so that we don't have to deal with OOM after creating the native handle
        // Lock in alphabetical order by class name
        lock (this)
            lock (kp)
            {
                if (_handle == null)
                    throw new ObjectDisposedException(nameof(Biscuit));
                if (kp._handle == null)
                    throw new ObjectDisposedException(nameof(KeyPair));
                using var bb = new BlockBuilder();
                builderCallback(bb);
                ret._handle = biscuit_append_block(_handle, bb._handle, kp._handle);
                GC.KeepAlive(bb);
            }
        GC.KeepAlive(this);
        GC.KeepAlive(kp);
        if (ret._handle == null)
        {
            throw BiscuitException.FromLastError();
        }
        return ret;
    }

    public nuint CalculateSerializedSize()
    {
        lock (this)
        {
            if (_handle == null)
                throw new ObjectDisposedException(nameof(PublicKey));
            return biscuit_serialized_size(_handle);
        }
    }

    public void Serialize(Span<byte> buffer)
    {
        lock (this)
        {
            if (_handle == null)
                throw new ObjectDisposedException(nameof(PublicKey));

            nuint expectedSize = biscuit_serialized_size(_handle);
            if ((nuint)buffer.Length != expectedSize)
            {
                throw new ArgumentException(nameof(buffer), $"Expected as {expectedSize}-byte buffer");

            }

            nuint ret;
            fixed (byte* pBuf = buffer)
            {
                ret = biscuit_serialize(_handle, pBuf);
            }
            if (ret != expectedSize)
            {
                Environment.FailFast("biscuit_serialize wrote an unexpected number of bytes");
            }
        }
    }

    ~Biscuit()
    {
        Dispose(false);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        generated.Biscuit* handle;
        lock (this)
        {
            handle = _handle;
            _handle = null;
        }
        if (handle != null)
        {
            biscuit_free(handle);
        }
    }
}
