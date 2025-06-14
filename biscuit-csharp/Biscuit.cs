using System.Runtime.InteropServices;
using static us.awise.biscuits.generated.Methods;

namespace us.awise.biscuits;

public sealed unsafe class Biscuit : IDisposable
{
    internal generated.Biscuit* _handle;

    internal Biscuit(generated.Biscuit* handle)
    {
        _handle = handle;
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

    public Biscuit AppendBlock(BlockBuilder bb, KeyPair kp)
    {
        if (bb is null)
            throw new ArgumentNullException(nameof(bb));
        if (kp is null)
            throw new ArgumentNullException(nameof(kp));

        generated.Biscuit* ret;
        // Lock in alphabetical order by class name
        lock (this)
            lock (bb)
                lock (kp)
                {
                    if (_handle == null)
                        throw new ObjectDisposedException(nameof(Biscuit));
                    if (bb._handle == null)
                        throw new ObjectDisposedException(nameof(BlockBuilder));
                    if (kp._handle == null)
                        throw new ObjectDisposedException(nameof(KeyPair));
                    ret = biscuit_append_block(_handle, bb._handle, kp._handle);
                }
        GC.KeepAlive(this);
        GC.KeepAlive(bb);
        GC.KeepAlive(kp);
        if (ret == null)
        {
            throw BiscuitException.FromLastError();
        }
        return new Biscuit(ret);
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
