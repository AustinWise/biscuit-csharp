using static us.awise.biscuits.generated.Methods;

namespace us.awise.biscuits;

public sealed unsafe class Authorizer : IDisposable
{
    private generated.Authorizer* _handle;

    public static Authorizer Create(Biscuit token, Action<AuthorizerBuilder> builderCallback)
    {
        if (token is null)
            throw new ArgumentNullException(nameof(token));
        if (builderCallback is null)
            throw new ArgumentNullException(nameof(builderCallback));

        var ret = new Authorizer(); // create ahead of time so that we don't have to deal with OOM after creating the native handle
        lock (token)
        {
            if (token._handle == null)
            {
                throw new ObjectDisposedException(nameof(token));
            }
            generated.AuthorizerBuilder* builder = authorizer_builder();
            if (builder == null)
                throw BiscuitException.FromLastError();
            try
            {
                builderCallback(new AuthorizerBuilder(builder));
            }
            catch
            {
                authorizer_builder_free(builder);
                throw;
            }
            // This consume the AuthorizerBuilder*
            ret._handle = authorizer_builder_build(builder, token._handle);
            GC.KeepAlive(token);
        }
        if (ret._handle == null)
        {
            throw BiscuitException.FromLastError();
        }

        return ret;
    }

    private Authorizer()
    {
    }

    public void Authorize()
    {
        byte ret;
        lock (this)
        {
            if (_handle == null)
                throw new ObjectDisposedException(nameof(Authorizer));
            ret = authorizer_authorize(_handle);
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
            handle = _handle;
            _handle = null;
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
                if (_handle == null)
                    throw new ObjectDisposedException(nameof(Authorizer));
                cstr = authorizer_print(_handle);
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
