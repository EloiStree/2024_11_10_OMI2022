using System;
using System.Text.RegularExpressions;

public class TimeStringParser
{
    public static Regex second = new Regex("[0-9]+\\.[0-9]+");
    public static Regex millisecond = new Regex("[0-9]+");
    public static Regex onlyDigit = new Regex("[^-0-9\\.]+");
    public static void GetMillisecond(string text, out bool hasFound, out double millisecond)
    {
        text= text.Trim();
        millisecond = 0;
        string followingText;
        double exactValue;
        ExtractValue(text, out hasFound, out exactValue, out followingText );
        if (hasFound) {

            if (followingText == "s" ||
                followingText == "sec" ||
                followingText == "second")
            {
                millisecond = exactValue * 1000.0;
            }
            else if (followingText == "m" ||
               followingText == "min" ||
               followingText == "minute")
            {
                millisecond = exactValue * 60000.0;
            }
            else if (followingText == "h" ||
              followingText == "hour")
            {
                millisecond = exactValue * 3600000.0;
            }
            else {
                millisecond = exactValue;
            }
        }
    }

    private static void ExtractValue(string text, out bool hasFound, out double extractValue, out string followingText)
    {
        text=text.Trim();
        string value = onlyDigit.Replace(text, "");
        followingText = text.Replace(value, "");
        hasFound = double.TryParse(value, out extractValue);
        if (hasFound)
        {
            hasFound = true;
            if (followingText.Length == 0 && value.IndexOf(".") >= 0)
                followingText = "s";
            if (followingText.Length == 0 && value.IndexOf("") < 0)
                followingText = "ms";
        }
     
    }
}