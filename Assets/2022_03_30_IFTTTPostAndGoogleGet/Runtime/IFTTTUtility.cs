using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;



public class IFTTTUtility
{

    public class CSharp
    {
        public static void PostEvent(in string apiwebhookkey, in string iftttEventName)
        {
            PushURL(GetWebUrlToSend(in apiwebhookkey, in iftttEventName));
        }

        public static void PostJsonEvent(in string apiwebhookkey, in  string iftttEventName, in string value0, in string value1 = "", in  string value2 = "")
        {
            PushURLWithJsonTripleValue(GetWebUrlToSend(in apiwebhookkey, in iftttEventName), value0, value1, value2);
        }

        private static void PushURL(in string url)
        {
            Uri ourUri = new Uri(url);
            WebRequest myWebRequest = WebRequest.Create(url);
            WebResponse myWebResponse = myWebRequest.GetResponse();
            myWebResponse.Close();
            myWebRequest.Abort();
        }
        private static void PushURLWithJsonTripleValue(in string url, in string value0, in  string value1 = "", in string value2 = "")
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.ContentType = "application/json";
            request.Method = "POST";
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                string json = GetJsonTripleValue(value0, value1, value2);
                streamWriter.Write(json);
            }
            var response = (HttpWebResponse)request.GetResponse();
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
            }
            response.Close();
            request.Abort();
        }



    }
    public class Coroutine
    {
        public static IEnumerator PostEvent(string apiwebhookkey, string iftttEventName)
        {
            yield return PushURL(GetWebUrlToSend(in apiwebhookkey, in iftttEventName));
        }

        public static IEnumerator PostJsonEvent(string apiwebhookkey, string iftttEventName, string value0, string value1 = "", string value2 = "")
        {
            yield return PushURLWithJsonTripleValue(GetWebUrlToSend(in apiwebhookkey, in iftttEventName), value0, value1, value2);
        }

        private static IEnumerator PushURL(string url)
        {
            using (UnityWebRequest request = UnityWebRequest.Get(url))
            {
                yield return request.SendWebRequest();
            }
        }
        private static IEnumerator PushURLWithJsonTripleValue(string url, string value0, string value1 = "", string value2 = "")
        {
            string json = GetJsonTripleValue(value0, value1, value2);
            using (UnityWebRequest request = UnityWebRequest.Post(url, json))
            {
                byte[] jsonToSend = new UTF8Encoding().GetBytes(json);
                request.uploadHandler = new UploadHandlerRaw(jsonToSend);
                request.SetRequestHeader("Content-Type", "application/json");
                yield return request.SendWebRequest();
                //Debug.Log(request.result);
            }
        }

    }



    private static string GetWebUrlToSend(in string apiwebhookkey, in string iftttEventName)
    {
        return "https://maker.ifttt.com/trigger/" + iftttEventName + "/with/key/" + apiwebhookkey;
    }
    private static string GetJsonTripleValue(in string value0 = "", in string value1 = "", in string value2 = "")
    {
        return
            "{ \"value1\" : \"" + value0 + "\", \"value2\" : \"" + value1 + "\", \"value3\" : \"" + value2 + "\" }";
    }
    public static void OpenWebPageWhereToFindAPIKey()
    {
        Application.OpenURL("https://ifttt.com/maker_webhooks");
    }
    public static void OpenOfficialWebsite()
    {
        Application.OpenURL("https://ifttt.com/maker_webhooks");
    }
    public static void OpenUserAppletsOnOfficialWebsite()
    {
        Application.OpenURL("https://ifttt.com/maker_webhooks");
    }
}

