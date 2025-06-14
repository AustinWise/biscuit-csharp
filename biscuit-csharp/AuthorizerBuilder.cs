using static us.awise.biscuits.generated.Methods;

namespace us.awise.biscuits;

public sealed unsafe class AuthorizerBuilder : IDisposable
{
    private generated.AuthorizerBuilder* _handle;

    public AuthorizerBuilder()
    {
        _handle = authorizer_builder();
        if (_handle == null)
        {
            throw BiscuitException.FromLastError();
        }
    }

    public void AddCheck(ReadOnlySpan<byte> utf8)
    {
        using var chars = new CStringBuilder(utf8, stackalloc byte[CStringBuilder.STACK_SIZE]);
        fixed (sbyte* charPtr = chars.Buffer)
        {
            byte ret;
            lock (this)
            {
                if (_handle == null)
                    throw new ObjectDisposedException(nameof(AuthorizerBuilder));
                ret = authorizer_builder_add_check(_handle, charPtr);
            }
            GC.KeepAlive(this);
            if (ret == 0)
            {
                throw BiscuitException.FromLastError();
            }
        }
    }

    public void AddFact(ReadOnlySpan<byte> utf8)
    {
        using var chars = new CStringBuilder(utf8, stackalloc byte[CStringBuilder.STACK_SIZE]);
        fixed (sbyte* charPtr = chars.Buffer)
        {
            byte ret;
            lock (this)
            {
                ret = authorizer_builder_add_fact(_handle, charPtr);
            }
            GC.KeepAlive(this);
            if (ret == 0)
            {
                throw BiscuitException.FromLastError();
            }
        }
    }

    public void AddPolicy(ReadOnlySpan<byte> utf8)
    {
        using var chars = new CStringBuilder(utf8, stackalloc byte[CStringBuilder.STACK_SIZE]);
        fixed (sbyte* charPtr = chars.Buffer)
        {
            byte ret;
            lock (this)
            {
                if (_handle == null)
                    throw new ObjectDisposedException(nameof(AuthorizerBuilder));
                ret = authorizer_builder_add_policy(_handle, charPtr);
            }
            GC.KeepAlive(this);
            if (ret == 0)
            {
                throw BiscuitException.FromLastError();
            }
        }
    }

    public void AddRule(ReadOnlySpan<byte> utf8)
    {
        using var chars = new CStringBuilder(utf8, stackalloc byte[CStringBuilder.STACK_SIZE]);
        fixed (sbyte* charPtr = chars.Buffer)
        {
            byte ret;
            lock (this)
            {
                if (_handle == null)
                    throw new ObjectDisposedException(nameof(AuthorizerBuilder));
                ret = authorizer_builder_add_rule(_handle, charPtr);
            }
            GC.KeepAlive(this);
            if (ret == 0)
            {
                throw BiscuitException.FromLastError();
            }
        }
    }

    public Authorizer Build(Biscuit token)
    {
        generated.AuthorizerBuilder* handle;
        lock (this)
        {
            handle = _handle;
            _handle = null;
        }
        if (handle == null)
            throw new ObjectDisposedException(nameof(AuthorizerBuilder));
        GC.SuppressFinalize(this);

        generated.Authorizer* ret;
        lock (token)
        {
            // This consume the AuthorizerBuilder*
            ret = authorizer_builder_build(handle, token._handle);
            GC.KeepAlive(token);
        }
        if (ret == null)
        {
            throw BiscuitException.FromLastError();
        }

        return new Authorizer(ret);
    }

    ~AuthorizerBuilder()
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
        generated.AuthorizerBuilder* handle;
        lock (this)
        {
            handle = _handle;
            _handle = null;
        }
        if (handle != null)
        {
            authorizer_builder_free(handle);
        }
    }
}
