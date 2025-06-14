using static us.awise.biscuits.generated.Methods;

namespace us.awise.biscuits;

public sealed unsafe class AuthorizerBuilder : IDisposable
{
    private generated.AuthorizerBuilder* handle;

    public AuthorizerBuilder()
    {
        this.handle = authorizer_builder();
        if (this.handle == null)
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
                if (handle == null)
                    throw new ObjectDisposedException(nameof(AuthorizerBuilder));
                ret = authorizer_builder_add_check(this.handle, charPtr);
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
                ret = authorizer_builder_add_fact(this.handle, charPtr);
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
                if (handle == null)
                    throw new ObjectDisposedException(nameof(AuthorizerBuilder));
                ret = authorizer_builder_add_policy(this.handle, charPtr);
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
                if (handle == null)
                    throw new ObjectDisposedException(nameof(AuthorizerBuilder));
                ret = authorizer_builder_add_rule(this.handle, charPtr);
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
            handle = this.handle;
            this.handle = null;
        }
        if (handle == null)
            throw new ObjectDisposedException(nameof(AuthorizerBuilder));
        GC.SuppressFinalize(this);

        generated.Authorizer* ret;
        lock (token)
        {
            // This consume the AuthorizerBuilder*
            ret = authorizer_builder_build(handle, token.handle);
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
        if (handle == null)
            throw new ObjectDisposedException(nameof(AuthorizerBuilder));
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        generated.AuthorizerBuilder* handle;
        lock (this)
        {
            handle = this.handle;
            this.handle = null;
        }
        if (handle != null)
        {
            authorizer_builder_free(handle);
        }
    }
}
