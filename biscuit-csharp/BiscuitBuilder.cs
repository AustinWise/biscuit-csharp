using System;
using System.Buffers;
using System.Security.Cryptography;
using static us.awise.biscuits.generated.Methods;

namespace us.awise.biscuits;

public sealed unsafe class BiscuitBuilder : IDisposable
{
    private const int SEED_SIZE = 32;

    private generated.BiscuitBuilder* handle;

    public BiscuitBuilder()
    {
        this.handle = biscuit_builder();
        if (this.handle == null)
        {
            throw BiscuitException.FromLastError();
        }
    }

    public void AddRule(ReadOnlySpan<byte> utf8)
    {
        using var chars = new CStringBuilder(utf8, stackalloc byte[CStringBuilder.STACK_SIZE]);
        fixed (sbyte* charPtr = chars.Buffer)
        {
            byte ret = biscuit_builder_add_rule(this.handle, charPtr);
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
            byte ret = biscuit_builder_add_fact(this.handle, charPtr);
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
            byte ret = biscuit_builder_add_check(this.handle, charPtr);
            GC.KeepAlive(this);
            if (ret == 0)
            {
                throw BiscuitException.FromLastError();
            }
        }
    }

    public Biscuit Build(KeyPair keyPair)
    {
        // TODO: figure out if we need to use the same seed as the key pair
        // TODO: maybe deduplicate this logic with that in KeyPair
#if NET
        Span<byte> buf = stackalloc byte[SEED_SIZE];
        RandomNumberGenerator.Fill(buf);
#else
        byte[] buf = ArrayPool<byte>.Shared.Rent(SEED_SIZE);
        using var ran = RandomNumberGenerator.Create();
        ran.GetBytes(buf, 0, SEED_SIZE);
#endif
        generated.Biscuit* ret;
        fixed (byte* bufPtr = buf)
        {
            ret = biscuit_builder_build(this.handle, keyPair.handle, bufPtr, SEED_SIZE);
            GC.KeepAlive(this);
            GC.KeepAlive(keyPair);
            if (handle == null)
            {
                throw BiscuitException.FromLastError();
            }
        }
#if !NET
        ArrayPool<byte>.Shared.Return(buf, clearArray: true);
#endif
        return new Biscuit(ret);
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
        // TODO: make this thread safe and handle resurrection.
        generated.BiscuitBuilder* handle = this.handle;
        this.handle = null;
        if (handle != null)
        {
            biscuit_builder_free(handle);
            GC.KeepAlive(this);
        }
    }
}
