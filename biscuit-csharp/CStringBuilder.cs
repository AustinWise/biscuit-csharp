using System.Buffers;
using System.Runtime.InteropServices;
using System.Text;

namespace us.awise.biscuits;

internal ref struct CStringBuilder : IDisposable
{
    internal const int STACK_SIZE = 32;

    private Span<byte> _buf;
    private byte[]? _borrowedBuffer;

    internal CStringBuilder(ReadOnlySpan<byte> input, Span<byte> stackAllocatedBuffer)
    {
        if (input.Length + 1 > stackAllocatedBuffer.Length)
        {
            _buf = _borrowedBuffer = ArrayPool<byte>.Shared.Rent(input.Length + 1);
        }
        else
        {
            _buf = stackAllocatedBuffer;
        }
        input.CopyTo(_buf);
        _buf[input.Length] = 0;
    }

    internal unsafe CStringBuilder(ReadOnlySpan<char> input, Span<byte> stackAllocatedBuffer)
    {
        fixed (char* charPtr = input)
        {
            int inputAsUtfLength = Encoding.UTF8.GetByteCount(charPtr, input.Length);

            if (inputAsUtfLength + 1 > stackAllocatedBuffer.Length)
            {
                _buf = _borrowedBuffer = ArrayPool<byte>.Shared.Rent(input.Length + 1);
            }
            else
            {
                _buf = stackAllocatedBuffer;
            }

            fixed (byte* destPtr = _buf)
            {
                Encoding.UTF8.GetBytes(charPtr, input.Length, destPtr, _buf.Length);
            }

            _buf[inputAsUtfLength] = 0;
        }
    }

    public readonly ReadOnlySpan<sbyte> Buffer => MemoryMarshal.Cast<byte, sbyte>(_buf);

    public void Dispose()
    {
        byte[]? bufferToReturn = _borrowedBuffer;
        this = default; // prevent accidental reuse
        if (bufferToReturn != null)
        {
            ArrayPool<byte>.Shared.Return(bufferToReturn);
        }
    }
}
