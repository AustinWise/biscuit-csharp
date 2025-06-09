namespace us.awise.biscuits;

// TODO: expose error kind
public class BiscuitException : Exception
{
    private BiscuitException(string message) : base(message) { }

    internal unsafe static BiscuitException FromLastError()
    {
        // This msgPtr is owned by a thread local variable, so we should NOT free it.
        sbyte* msgPtr = generated.Methods.error_message();
        string errorMessage = CString.ToString(msgPtr);
        return new BiscuitException(errorMessage);
    }
}
