using System.Text;

namespace us.awise.biscuits;

internal unsafe class CString
{
    public static string ToString(sbyte* strPtr)
    {
        sbyte* msgPtr = strPtr;
        if (msgPtr == null)
        {
            throw new ArgumentNullException();
        }

        int count = 0;
        while (msgPtr[count] != 0)
        {
            count++;
        }

        string str = Encoding.UTF8.GetString((byte*)msgPtr, count);

        return str;
    }
}
