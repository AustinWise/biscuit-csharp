using static us.awise.biscuits.generated.Methods;

namespace us.awise.biscuits;

public sealed unsafe class Biscuit : IDisposable
{
    internal generated.Biscuit* handle;

    internal Biscuit(generated.Biscuit* handle)
    {
        this.handle = handle;
    }

    public Biscuit AppendBlock(BlockBuilder bb, KeyPair kp)
    {
        if (bb is null)
            throw new ArgumentNullException(nameof(bb));
        if (kp is null)
            throw new ArgumentNullException(nameof(kp));

        generated.Biscuit* ret;
        // Lock in alphabetical order by class name
        lock (this)
            lock (bb)
                lock (kp)
                {
                    if (handle == null)
                        throw new ObjectDisposedException(nameof(Biscuit));
                    if (bb.handle == null)
                        throw new ObjectDisposedException(nameof(BlockBuilder));
                    if (kp.handle == null)
                        throw new ObjectDisposedException(nameof(KeyPair));
                    ret = biscuit_append_block(handle, bb.handle, kp.handle);
                }
        GC.KeepAlive(this);
        GC.KeepAlive(bb);
        GC.KeepAlive(kp);
        if (ret == null)
        {
            throw BiscuitException.FromLastError();
        }
        return new Biscuit(ret);
    }

    ~Biscuit()
    {
        Dispose(false);
    }

    public void Dispose()
    {
        if (handle == null)
            throw new ObjectDisposedException(nameof(Biscuit));
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        generated.Biscuit* handle;
        lock (this)
        {
            handle = this.handle;
            this.handle = null;
        }
        if (handle != null)
        {
            biscuit_free(handle);
        }
    }
}
