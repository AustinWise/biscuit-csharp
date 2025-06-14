using System.Buffers;
using System.Security.Cryptography;
using static us.awise.biscuits.generated.Methods;

namespace us.awise.biscuits;

public sealed unsafe class KeyPair : IDisposable
{
    private const int SEED_SIZE = 32;

    internal generated.KeyPair* handle;

    public KeyPair()
    {
        // TODO: maybe deduplicate this logic with that in BiscuitBuilder
#if NET
        Span<byte> buf = stackalloc byte[SEED_SIZE];
        RandomNumberGenerator.Fill(buf);
#else
        byte[] buf = ArrayPool<byte>.Shared.Rent(SEED_SIZE);
        using var ran = RandomNumberGenerator.Create();
        ran.GetBytes(buf, 0, SEED_SIZE);
#endif
        fixed (byte* bufPtr = buf)
        {
            handle = key_pair_new(bufPtr, SEED_SIZE, generated.SignatureAlgorithm.Ed25519);
            if (handle == null)
            {
                throw BiscuitException.FromLastError();
            }
        }
#if !NET
        ArrayPool<byte>.Shared.Return(buf, clearArray: true);
#endif
    }

    private KeyPair(generated.KeyPair* handle)
    {
        this.handle = handle;
    }

    public PublicKey GetPublicKey()
    {
        generated.PublicKey* ret;
        lock (this)
        {
            if (handle == null)
                throw new ObjectDisposedException(nameof(KeyPair));
            ret = key_pair_public(handle);
        }
        GC.KeepAlive(this);
        if (ret == null)
        {
            throw BiscuitException.FromLastError();
        }
        return new PublicKey(ret);
    }

    ~KeyPair()
    {
        Dispose(false);
    }

    public void Dispose()
    {
        if (handle == null)
            throw new ObjectDisposedException(nameof(KeyPair));
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        generated.KeyPair* handle;
        lock (this)
        {
            handle = this.handle;
            this.handle = null;
        }
        if (handle != null)
        {
            key_pair_free(handle);
        }
    }
}
