using static us.awise.biscuits.generated.Methods;

namespace us.awise.biscuits;

public sealed unsafe class BlockBuilder : IDisposable
{
    internal generated.BlockBuilder* handle;

    public BlockBuilder()
    {
        this.handle = create_block();
        if (handle == null)
        {
            throw BiscuitException.FromLastError();
        }
    }

    public void AddRule(ReadOnlySpan<byte> utf8)
    {
        using var chars = new CStringBuilder(utf8, stackalloc byte[CStringBuilder.STACK_SIZE]);
        fixed (sbyte* charPtr = chars.Buffer)
        {
            byte ret = block_builder_add_rule(this.handle, charPtr);
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
            byte ret = block_builder_add_fact(this.handle, charPtr);
            GC.KeepAlive(this);
            if (ret == 0)
            {
                throw BiscuitException.FromLastError();
            }
        }
    }

    public void AddCheck(ReadOnlySpan<byte> utf8)
    {
        using var chars = new CStringBuilder(utf8, stackalloc byte[CStringBuilder.STACK_SIZE]);
        fixed (sbyte* charPtr = chars.Buffer)
        {
            byte ret = block_builder_add_check(this.handle, charPtr);
            GC.KeepAlive(this);
            if (ret == 0)
            {
                throw BiscuitException.FromLastError();
            }
        }
    }

    ~BlockBuilder()
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
        generated.BlockBuilder* handle = this.handle;
        this.handle = null;
        if (handle != null)
        {
            block_builder_free(handle);
            GC.KeepAlive(this);
        }
    }
}
