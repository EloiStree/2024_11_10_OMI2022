

using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using UnityEngine.Networking;

public class GoogleDocAndSheetUtility
{

    public static string m_googleDocFormat =
        "https://docs.google.com/document/export?format=txt&id={0}";
    public static string m_googleSheetFormat =
        "https://docs.google.com/spreadsheets/d/{0}/export?format=csv";

    public static void GetTextInGoogleSheet(in string id, out string textAsGoogleCsv)
    {
        if (id.Trim().Length > 0)
            textAsGoogleCsv = GetTextInWebPageWithCSharp(string.Format(m_googleDocFormat, id));
        else textAsGoogleCsv = "";
    }
    public static void GetTextInGoogleDoc(in string id, out string docAsText)
    {
        if (id.Trim().Length > 0)
            docAsText = GetTextInWebPageWithCSharp(string.Format(m_googleDocFormat, id));
        else docAsText = "";
    }
    public static string GetTextInWebPageWithCSharp(string address)
    {

        var request = (HttpWebRequest)WebRequest.Create(address);

        using (var response = (HttpWebResponse)request.GetResponse())
        {
            var encoding = Encoding.GetEncoding(response.CharacterSet);

            using (var responseStream = response.GetResponseStream())
            using (var reader = new StreamReader(responseStream, encoding))
                return reader.ReadToEnd();
        }
    }
    public static IEnumerator GetTextInGoogleSheet(string id, StringOutputRef textAsGoogleCsv)
    {
        textAsGoogleCsv.m_value = "";
        if (id.Trim().Length > 0)
            yield return GetTextInWebPageWithCoroutine(string.Format(m_googleSheetFormat, id), textAsGoogleCsv);
    }
    public static IEnumerator GetTextInGoogleDoc(string id, StringOutputRef docAsText)
    {
        docAsText.m_value = "";
        if (id.Trim().Length > 0)
            yield return GetTextInWebPageWithCoroutine(string.Format(m_googleDocFormat, id), docAsText);
    }

    public static IEnumerator GetTextInWebPageWithCoroutine(string address, StringOutputRef callback)
    {
        UnityWebRequest www = UnityWebRequest.Get(address);
        yield return www.SendWebRequest();
        if (callback == null)
            callback = new StringOutputRef();

        if (www.result != UnityWebRequest.Result.Success)
        { callback.m_value = ""; }
        else
        { }
        callback.m_value = www.downloadHandler.text;
    }
    public class StringOutputRef
    {
        public string m_value;

    }

    public static void ParseTextToChars(in string text, ref List<char> chars)
    {
        if (chars == null)
            chars = new List<char>();
        chars.Clear();
        foreach (char item in text.ToCharArray())
        {
            if (item != ' ' && item != '\r' && item != '\n')
                chars.Add(item);
        }
    }
    public static void ParseTextToLines(in string text, out string[] lines)
    {
        lines = text.Split(new char[] { '\n', '\r' });
    }
}