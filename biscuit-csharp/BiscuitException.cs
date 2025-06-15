namespace us.awise.biscuits;

public class BiscuitException : Exception
{
    public ErrorKind Kind { get; }

    private BiscuitException(ErrorKind kind, string message)
        : base(message)
    {
        Kind = kind;
    }

    internal unsafe static BiscuitException FromLastError()
    {
        // This msgPtr is owned by a thread local variable, so we should NOT free it.
        sbyte* msgPtr = generated.Methods.error_message();
        // A unit test ensures the enums are equivalent.
        ErrorKind kind = (ErrorKind)generated.Methods.error_kind();
        string errorMessage = CString.ToString(msgPtr);
        return new BiscuitException(kind, errorMessage);
    }
}
