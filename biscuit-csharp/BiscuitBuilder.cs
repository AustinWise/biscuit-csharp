using static us.awise.biscuits.generated.Methods;

namespace us.awise.biscuits;

public sealed unsafe class BiscuitBuilder : IDisposable
{
    internal generated.BiscuitBuilder* _handle;

    internal BiscuitBuilder()
    {
        _handle = biscuit_builder();
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
                    throw new ObjectDisposedException(nameof(BiscuitBuilder));
                ret = biscuit_builder_add_rule(_handle, charPtr);
            }
            GC.KeepAlive(this);
            if (ret == 0)
            {
                throw BiscuitException.FromLastError();
            }
        }
    }

    public void AddRule(ReadOnlySpan<char> str)
    {
        using var chars = new CStringBuilder(str, stackalloc byte[CStringBuilder.STACK_SIZE]);
        fixed (sbyte* charPtr = chars.Buffer)
        {
            byte ret;
            lock (this)
            {
                if (_handle == null)
                    throw new ObjectDisposedException(nameof(BiscuitBuilder));
                ret = biscuit_builder_add_rule(_handle, charPtr);
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
                    throw new ObjectDisposedException(nameof(BiscuitBuilder));
                ret = biscuit_builder_add_fact(_handle, charPtr);
            }
            GC.KeepAlive(this);
            if (ret == 0)
            {
                throw BiscuitException.FromLastError();
            }
        }
    }

    public void AddFact(ReadOnlySpan<char> str)
    {
        using var chars = new CStringBuilder(str, stackalloc byte[CStringBuilder.STACK_SIZE]);
        fixed (sbyte* charPtr = chars.Buffer)
        {
            byte ret;
            lock (this)
            {
                if (_handle == null)
                    throw new ObjectDisposedException(nameof(BiscuitBuilder));
                ret = biscuit_builder_add_fact(_handle, charPtr);
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
                    throw new ObjectDisposedException(nameof(BiscuitBuilder));
                ret = biscuit_builder_add_check(_handle, charPtr);
            }
            GC.KeepAlive(this);
            if (ret == 0)
            {
                throw BiscuitException.FromLastError();
            }
        }
    }

    public void AddCheck(ReadOnlySpan<char> str)
    {
        using var chars = new CStringBuilder(str, stackalloc byte[CStringBuilder.STACK_SIZE]);
        fixed (sbyte* charPtr = chars.Buffer)
        {
            byte ret;
            lock (this)
            {
                if (_handle == null)
                    throw new ObjectDisposedException(nameof(BiscuitBuilder));
                ret = biscuit_builder_add_check(_handle, charPtr);
            }
            GC.KeepAlive(this);
            if (ret == 0)
            {
                throw BiscuitException.FromLastError();
            }
        }
    }

    ~BiscuitBuilder()
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
        generated.BiscuitBuilder* handle;
        lock (this)
        {
            handle = _handle;
            _handle = null;
        }
        if (handle != null)
        {
            biscuit_builder_free(handle);
        }
    }
}
