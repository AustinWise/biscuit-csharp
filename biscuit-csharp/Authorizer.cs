using static us.awise.biscuits.generated.Methods;

namespace us.awise.biscuits;

public sealed unsafe class Authorizer : IDisposable
{
    private generated.Authorizer* handle;

    internal Authorizer(generated.Authorizer* handle)
    {
        this.handle = handle;
    }

    public void Authorize()
    {
        byte ret;
        lock (this)
        {
            if (handle == null)
                throw new ObjectDisposedException(nameof(Authorizer));
            ret = authorizer_authorize(handle);
            GC.KeepAlive(this);
        }
        if (ret == 0)
        {
            // TODO: maybe there is a way to return an option type instead of throwing?
            throw BiscuitException.FromLastError();
        }
    }

    ~Authorizer()
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
        generated.Authorizer* handle;
        lock (this)
        {
            handle = this.handle;
            this.handle = null;
        }
        if (handle != null)
        {
            authorizer_free(handle);
        }
    }

    public override string ToString()
    {
        sbyte* cstr = null;
        try
        {
            lock (this)
            {
                if (handle == null)
                    throw new ObjectDisposedException(nameof(Authorizer));
                cstr = authorizer_print(this.handle);
                GC.KeepAlive(this);
            }
            if (cstr == null)
            {
                throw BiscuitException.FromLastError();
            }
            return CString.ToString(cstr);
        }
        finally
        {
            if (cstr != null)
            {
                string_free(cstr);
            }
        }
    }
}
