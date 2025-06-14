using static us.awise.biscuits.generated.Methods;

namespace us.awise.biscuits;

public sealed unsafe class BlockBuilder : IDisposable
{
    internal generated.BlockBuilder* _handle;

    public BlockBuilder()
    {
        _handle = create_block();
        if (_handle == null)
        {
            throw BiscuitException.FromLastError();
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
                    throw new ObjectDisposedException(nameof(BlockBuilder));
                ret = block_builder_add_rule(_handle, charPtr);
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
                if (_handle == null)
                    throw new ObjectDisposedException(nameof(BlockBuilder));
                ret = block_builder_add_fact(_handle, charPtr);
            }
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
            byte ret;
            lock (this)
            {
                if (_handle == null)
                    throw new ObjectDisposedException(nameof(BlockBuilder));
                ret = block_builder_add_check(_handle, charPtr);
            }
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
        generated.BlockBuilder* handle;
        lock (this)
        {
            handle = _handle;
            _handle = null;
        }
        if (handle != null)
        {
            block_builder_free(handle);
        }
    }
}
