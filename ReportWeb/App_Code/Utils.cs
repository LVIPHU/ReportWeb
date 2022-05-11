using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class Utils
{
    public static bool LimitedIn(int index, List<string> array)
    {
        return (index >= 0) && (index < array.Count);
    }

    public static bool Numerical(string text)
    {
        double number;
        return double.TryParse(text, out number);
    }
}