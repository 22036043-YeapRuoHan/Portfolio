namespace RP.SOI.DotNet.Utils;

public static class StringExtMethods
{
    // Extension Method to Stretch a String
    public static string Stretch(this String str)
    {
        //TODO: Task 1: Implement this method
        string newstr = "";
        for (int i = 0; i < str.Length; i++)
        {
            newstr += str.Substring(i, 1) + " ";
        }
        return newstr.ToUpper();



    }

    // Extension Method to uppercase and lowercase 
    // alternate characters in a String
    public static string UpperLower(this String str)
    {
        string newstr = "";
        for (int i = 0; i < str.Length; i++)
        {
            if (i % 2 == 0)
                newstr += str.Substring(i, 1).ToUpper();
            else
                newstr += str.Substring(i, 1).ToLower();
        }
        return newstr;
    }
}