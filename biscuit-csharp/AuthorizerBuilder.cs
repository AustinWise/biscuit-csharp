using static us.awise.biscuits.generated.Methods;

namespace us.awise.biscuits;

public unsafe readonly struct AuthorizerBuilder
{
    private readonly generated.AuthorizerBuilder* _handle;

    internal AuthorizerBuilder(generated.AuthorizerBuilder* handle)
    {
        if (handle == null)
            throw new ArgumentNullException(nameof(handle));
        _handle = handle;
    }

    public readonly void AddCheck(ReadOnlySpan<byte> utf8)
    {
        using var chars = new CStringBuilder(utf8, stackalloc byte[CStringBuilder.STACK_SIZE]);
        fixed (sbyte* charPtr = chars.Buffer)
        {
            byte ret = authorizer_builder_add_check(_handle, charPtr);
            if (ret == 0)
            {
                throw BiscuitException.FromLastError();
            }
        }
    }

    public readonly void AddCheck(ReadOnlySpan<char> str)
    {
        using var chars = new CStringBuilder(str, stackalloc byte[CStringBuilder.STACK_SIZE]);
        fixed (sbyte* charPtr = chars.Buffer)
        {
            byte ret = authorizer_builder_add_check(_handle, charPtr);
            if (ret == 0)
            {
                throw BiscuitException.FromLastError();
            }
        }
    }

    public readonly void AddFact(ReadOnlySpan<byte> utf8)
    {
        using var chars = new CStringBuilder(utf8, stackalloc byte[CStringBuilder.STACK_SIZE]);
        fixed (sbyte* charPtr = chars.Buffer)
        {
            byte ret = authorizer_builder_add_fact(_handle, charPtr);
            if (ret == 0)
            {
                throw BiscuitException.FromLastError();
            }
        }
    }

    public readonly void AddFact(ReadOnlySpan<char> str)
    {
        using var chars = new CStringBuilder(str, stackalloc byte[CStringBuilder.STACK_SIZE]);
        fixed (sbyte* charPtr = chars.Buffer)
        {
            byte ret = authorizer_builder_add_fact(_handle, charPtr);
            if (ret == 0)
            {
                throw BiscuitException.FromLastError();
            }
        }
    }

    public readonly void AddPolicy(ReadOnlySpan<byte> utf8)
    {
        using var chars = new CStringBuilder(utf8, stackalloc byte[CStringBuilder.STACK_SIZE]);
        fixed (sbyte* charPtr = chars.Buffer)
        {
            byte ret = authorizer_builder_add_policy(_handle, charPtr);
            if (ret == 0)
            {
                throw BiscuitException.FromLastError();
            }
        }
    }

    public readonly void AddPolicy(ReadOnlySpan<char> str)
    {
        using var chars = new CStringBuilder(str, stackalloc byte[CStringBuilder.STACK_SIZE]);
        fixed (sbyte* charPtr = chars.Buffer)
        {
            byte ret = authorizer_builder_add_policy(_handle, charPtr);
            if (ret == 0)
            {
                throw BiscuitException.FromLastError();
            }
        }
    }

    public readonly void AddRule(ReadOnlySpan<byte> utf8)
    {
        using var chars = new CStringBuilder(utf8, stackalloc byte[CStringBuilder.STACK_SIZE]);
        fixed (sbyte* charPtr = chars.Buffer)
        {
            byte ret = authorizer_builder_add_rule(_handle, charPtr);
            if (ret == 0)
            {
                throw BiscuitException.FromLastError();
            }
        }
    }

    public readonly void AddRule(ReadOnlySpan<char> str)
    {
        using var chars = new CStringBuilder(str, stackalloc byte[CStringBuilder.STACK_SIZE]);
        fixed (sbyte* charPtr = chars.Buffer)
        {
            byte ret = authorizer_builder_add_rule(_handle, charPtr);
            if (ret == 0)
            {
                throw BiscuitException.FromLastError();
            }
        }
    }
}
