using System.Runtime.InteropServices;
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
        generated.Biscuit* ret = biscuit_append_block(this.handle, bb.handle, kp.handle);
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
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        // TODO: make this thread safe and handle resurrection.
        generated.Biscuit* handle = this.handle;
        this.handle = null;
        if (handle != null)
        {
            biscuit_free(handle);
            GC.KeepAlive(this);
        }
    }
}
