using static us.awise.biscuits.generated.Methods;

namespace us.awise.biscuits;

public unsafe sealed class PublicKey : IEquatable<PublicKey>, IDisposable
{
    public const int SERIALIZED_SIZE = 32;

    private static int s_serialCounter;

    internal generated.PublicKey* _handle;
    private readonly int _serial;

    internal PublicKey(generated.PublicKey* handle)
    {
        _handle = handle;
        _serial = Interlocked.Increment(ref s_serialCounter);
    }

    public PublicKey(ReadOnlySpan<byte> serialized, SignatureAlgorithm algorithm)
    {
        if (serialized.Length != SERIALIZED_SIZE)
        {
            throw new ArgumentException(nameof(serialized), $"Expected as {SERIALIZED_SIZE}-byte buffer");
        }
        generated.SignatureAlgorithm genSignatureAlgorithm = algorithm.ToGenerated();

        fixed (byte* pBuf = serialized)
        {
            _handle = public_key_deserialize(pBuf, genSignatureAlgorithm);
        }
        if (_handle == null)
        {
            throw BiscuitException.FromLastError();
        }
        _serial = Interlocked.Increment(ref s_serialCounter);
    }

    /// <summary>
    /// Serializes the public key into the buffer.
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
                ret = public_key_serialize(_handle, pBuf);
            }
            if (ret != SERIALIZED_SIZE)
            {
                Environment.FailFast("public_key_serialize wrote an unexpected number of bytes");
            }
        }
    }

    public bool Equals(PublicKey? other)
    {
        if (other is null)
            throw new ArgumentNullException(nameof(other));

        PublicKey a = this;
        PublicKey b = other;

        // sort by serial for consistent locking order
        if (a._serial > b._serial)
        {
            PublicKey temp = a;
            a = b;
            b = temp;
        }

        lock (a)
            lock (b)
            {
                if (a._handle == null || b._handle == null)
                {
                    throw new ObjectDisposedException(nameof(PublicKey));
                }
                return 0 != public_key_equals(a._handle, b._handle);
            }
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as PublicKey);
    }

    public override int GetHashCode()
    {
        return _serial;
    }

    ~PublicKey()
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
        generated.PublicKey* handle;
        lock (this)
        {
            handle = _handle;
            _handle = null;
        }
        if (handle != null)
        {
            public_key_free(handle);
        }
    }
}
