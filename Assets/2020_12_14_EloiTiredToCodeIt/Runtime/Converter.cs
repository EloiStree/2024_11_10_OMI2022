using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class Converter 
{

    public static void ParseToFloat(string value, out bool found, out float valueFound)
    {
        value = value.Replace(",", ".");
         found = float.TryParse(value, NumberStyles.Float,  CultureInfo.InvariantCulture.NumberFormat,out valueFound);
        //string [] t =value.Trim().Split(new char[] { ',', '.' });
        //if (t.Length == 1)
        //{
        //    int d;
        //    if (int.TryParse(t[0], out d)) { 
        //        valueFound = d;
        //        found = true;
        //        return;
        //    }

        //}
        //else if (t.Length == 2)
        //{
        //    int a,b;
        //    if (int.TryParse(t[0], out a) && int.TryParse(t[1], out b))
        //    {
        //        valueFound = a + (b / (10f * t[1].Length+1 ) );
        //        found = true;
        //        return;
        //    }

        //}

        //found = false;
        //valueFound = 0;
    }
    public static bool ParseToFloat(string value, out float valueFound)
    {
        bool f;
        ParseToFloat(value, out f, out valueFound);
        return f;
    }
}
