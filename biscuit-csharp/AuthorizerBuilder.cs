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
            byte ret = authorizer_builder_add_check(this.handle, charPtr);
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
            byte ret = authorizer_builder_add_fact(this.handle, charPtr);
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
            byte ret = authorizer_builder_add_policy(this.handle, charPtr);
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
            byte ret = authorizer_builder_add_rule(this.handle, charPtr);
            GC.KeepAlive(this);
            if (ret == 0)
            {
                throw BiscuitException.FromLastError();
            }
        }
    }

    public Authorizer Build(Biscuit token)
    {
        // TODO: make thread safe
        generated.AuthorizerBuilder* handle = this.handle;
        this.handle = null;
        if (handle == null)
            throw new ObjectDisposedException(nameof(AuthorizerBuilder));

        // This consume the AuthorizerBuilder*
        var ret = authorizer_builder_build(handle, token.handle);
        GC.KeepAlive(this);
        GC.KeepAlive(token);
        if (ret == null)
        {
            throw BiscuitException.FromLastError();
        }

        GC.SuppressFinalize(this);
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
        // TODO: make this thread safe and handle resurrection.
        generated.AuthorizerBuilder* handle = this.handle;
        this.handle = null;
        if (handle != null)
        {
            authorizer_builder_free(handle);
            GC.KeepAlive(this);
        }
    }
}
