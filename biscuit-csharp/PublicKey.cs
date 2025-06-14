using static us.awise.biscuits.generated.Methods;

namespace us.awise.biscuits;

public unsafe sealed class PublicKey : IDisposable
{
    private generated.PublicKey* handle;

    internal PublicKey(generated.PublicKey* handle)
    {
        this.handle = handle;
    }

    ~PublicKey()
    {
        Dispose(false);
    }

    public void Dispose()
    {
        if (handle == null)
            throw new ObjectDisposedException(nameof(PublicKey));
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        generated.PublicKey* handle;
        lock (this)
        {
            handle = this.handle;
            this.handle = null;
        }
        if (handle != null)
        {
            public_key_free(handle);
        }
    }
}
