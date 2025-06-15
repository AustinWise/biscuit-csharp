using System.Buffers;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using static us.awise.biscuits.generated.Methods;

namespace us.awise.biscuits;

public sealed unsafe class KeyPair : IDisposable
{
    public const int SERIALIZED_SIZE = 32;

    private const int SEED_SIZE = 32;
    private const int EXPECTED_PEM_SIZE = 256;

    public static KeyPair FromPem(ReadOnlySpan<char> pem)
    {
        using (var builder = new CStringBuilder(pem, stackalloc byte[EXPECTED_PEM_SIZE]))
            fixed (sbyte* buf = builder.Buffer)
            {
                generated.KeyPair* ret = key_pair_from_pem(buf);
                if (ret == null)
                    throw BiscuitException.FromLastError();
                return new KeyPair(ret);
            }
    }

    public static KeyPair FromPem(ReadOnlySpan<byte> utf8Pem)
    {
        using (var builder = new CStringBuilder(utf8Pem, stackalloc byte[EXPECTED_PEM_SIZE]))
            fixed (sbyte* buf = builder.Buffer)
            {
                generated.KeyPair* ret = key_pair_from_pem(buf);
                if (ret == null)
                    throw BiscuitException.FromLastError();
                return new KeyPair(ret);
            }
    }

    internal generated.KeyPair* _handle;

    public KeyPair(SignatureAlgorithm algorithm)
    {
        generated.SignatureAlgorithm genSignatureAlgorithm = algorithm.ToGenerated();

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
            _handle = key_pair_new(bufPtr, SEED_SIZE, genSignatureAlgorithm);
            if (_handle == null)
            {
                throw BiscuitException.FromLastError();
            }
        }
#if !NET
        ArrayPool<byte>.Shared.Return(buf, clearArray: true);
#endif
    }

    public KeyPair(ReadOnlySpan<byte> serialized, SignatureAlgorithm algorithm)
    {
        if (serialized.Length != SERIALIZED_SIZE)
        {
            throw new ArgumentException(nameof(serialized), $"Expected as {SERIALIZED_SIZE}-byte buffer");
        }
        generated.SignatureAlgorithm genSignatureAlgorithm = algorithm.ToGenerated();

        fixed (byte* pBuf = serialized)
        {
            _handle = key_pair_deserialize(pBuf, genSignatureAlgorithm);
        }
        if (_handle == null)
        {
            throw BiscuitException.FromLastError();
        }
    }

    private KeyPair(generated.KeyPair* handle)
    {
        _handle = handle;
    }

    public PublicKey GetPublicKey()
    {
        generated.PublicKey* ret;
        lock (this)
        {
            if (_handle == null)
                throw new ObjectDisposedException(nameof(KeyPair));
            ret = key_pair_public(_handle);
        }
        GC.KeepAlive(this);
        if (ret == null)
        {
            throw BiscuitException.FromLastError();
        }
        return new PublicKey(ret);
    }

    /// <summary>
    /// Serializes the key pair into the buffer.
    /// </summary>
    /// <param name="buffer">Should be <see cref="SERIALIZED_SIZE"/> bytes long</param>
    public void Serialize(Span<byte> buffer)
    {
        if (buffer.Length != SERIALIZED_SIZE)
        {
            throw new ArgumentException(nameof(buffer), $"Expected as {SERIALIZED_SIZE}-byte buffer");
        }

        lock (this)
        {
            if (_handle == null)
                throw new ObjectDisposedException(nameof(PublicKey));
            nuint ret;
            fixed (byte* pBuf = buffer)
            {
                ret = key_pair_serialize(_handle, pBuf);
            }
            if (ret != SERIALIZED_SIZE)
            {
                Environment.FailFast("key_pair_serialize wrote an unexpected number of bytes");
            }
        }
    }

    public string ToPem()
    {
        lock (this)
        {
            if (_handle == null)
                throw new ObjectDisposedException(nameof(PublicKey));

            sbyte* ret = null;
            try
            {
                ret = key_pair_to_pem(_handle);
                if (ret == null)
                    throw BiscuitException.FromLastError();
                return CString.ToString(ret);
            }
            finally
            {
                if (ret != null)
                    string_free(ret);
            }
        }
    }

    ~KeyPair()
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
        generated.KeyPair* handle;
        lock (this)
        {
            handle = _handle;
            _handle = null;
        }
        if (handle != null)
        {
            key_pair_free(handle);
        }
    }
}
