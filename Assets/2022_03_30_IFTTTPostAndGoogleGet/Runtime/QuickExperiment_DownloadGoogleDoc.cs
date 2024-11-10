using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using UnityEngine;

public class QuickExperiment_DownloadGoogleDoc : MonoBehaviour
{


    public string m_googleDocFormat= "https://docs.google.com/document/export?format=txt&id={0}";
    public string m_googleDocId= "1rtEOH6GRRRUX5nfffefgBXVaUFw_jxDnLus6onMt58c";


    [TextArea(0, 10)]
    public string m_googleDocCsv;


    public string m_googleSheetFormat= "https://docs.google.com/spreadsheets/d/{0}/export?format=csv";
    public string m_googleSheetId= "13oRM1GagNaub_jWWWkj3xfp-c7s72yonT5NAVBzoBVU";

    [TextArea(0,10)]
    public string m_googleSheetCsv;




    [ContextMenu("Refresh")]
    public void Refresh()
    {
        if (m_googleSheetId.Trim().Length > 0)
        m_googleSheetCsv = GetResponseText(string.Format(m_googleSheetFormat, m_googleSheetId));
        if (m_googleDocId.Trim().Length > 0)
            m_googleDocCsv = GetResponseText(string.Format(m_googleDocFormat, m_googleDocId));

    }
    public static string GetResponseText(string address)
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
}
