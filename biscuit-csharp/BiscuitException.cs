namespace us.awise.biscuits;

public class BiscuitException : Exception
{
    public ErrorKind Kind { get; }

    public AuthorizationError? AuthorizationError { get; }

    private BiscuitException(ErrorKind kind, string message, AuthorizationError? authorizationError)
        : base(message)
    {
        Kind = kind;
        AuthorizationError = authorizationError;
    }

    internal unsafe static BiscuitException FromLastError(AuthorizationError? authorizationError = null)
    {
        // This msgPtr is owned by a thread local variable, so we should NOT free it.
        sbyte* msgPtr = generated.Methods.error_message();
        // A unit test ensures the enums are equivalent.
        ErrorKind kind = (ErrorKind)generated.Methods.error_kind();
        string errorMessage = CString.ToString(msgPtr);
        return new BiscuitException(kind, errorMessage, authorizationError);
    }
}
