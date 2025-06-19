using System.Text;

namespace us.awise.biscuits;

internal unsafe class CString
{
    public static string ToString(sbyte* strPtr)
    {
        if (strPtr == null)
        {
            throw new ArgumentNullException();
        }

        int count = 0;
        while (strPtr[count] != 0)
        {
            count++;
        }

        string str = Encoding.UTF8.GetString((byte*)strPtr, count);

        return str;
    }
}
