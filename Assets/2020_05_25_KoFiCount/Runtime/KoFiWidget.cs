using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class KoFiWidget : MonoBehaviour
{
    public string m_userId = "E1E21QCY5";
    public int m_coffeeCount;

    public class CountCallBack
    {
        public bool m_infoLoaded;
        public bool m_errorHappened;
        public long m_coffeeCount;
        public long m_previousCoffeeCount;
        public OnCoffeeCountValueChange m_onCoffeeChangedListener;
        public OnCoffeeCountChange m_onCoffeeFailToLoad;
        public delegate void OnCoffeeCountValueChange(CountCallBack obj, int newCoffeValue);
        public delegate void OnCoffeeCountChange(CountCallBack obj);

        public void AddFailListener(OnCoffeeCountChange listener)
        {
            m_onCoffeeFailToLoad += listener;
        }
        public void RemoveFailListener(OnCoffeeCountChange listener)
        {
            m_onCoffeeFailToLoad -= listener;
        }
        public void AddValueChangeListener(OnCoffeeCountValueChange listener)
        {
            m_onCoffeeChangedListener += listener;
        }
        public void RemoveValueChangeListener(OnCoffeeCountValueChange listener)
        {
            m_onCoffeeChangedListener -= listener;
        }

        public bool HasChangeSinceLastCallBack() {
            return m_previousCoffeeCount != m_coffeeCount;
        }


        public void SetCoffeCount(int coffee) {
            m_infoLoaded = true;
            m_coffeeCount = coffee;
            if (m_onCoffeeChangedListener != null && HasChangeSinceLastCallBack()) {
                m_onCoffeeChangedListener(this, coffee);
            }
        }
        public void SetAsFailed()
        {
            m_infoLoaded = true;
            m_errorHappened = true;
            if (m_onCoffeeFailToLoad != null)
            {
                m_onCoffeeFailToLoad(this);
            }
        }
        public void Clear() {
            m_infoLoaded = false;
            m_errorHappened = false;
            m_previousCoffeeCount = m_coffeeCount;
            m_coffeeCount = 0;
        }

       
    }

    public delegate void CountCallbackEvent(CountCallBack count);
    public static IEnumerator GetCoffeeCountFromId(string id, CountCallbackEvent callback)
    {
        WWW www = new WWW("https://ko-fi.com/widget/counterwidget/" + id);
        yield return www;
        string text = www.text;
        CountCallBack info = new CountCallBack();
        LookForCoffeeInfoInHtmlAndPush(text, info);
        callback(info);
    }

    public static void GetCoffeeCountFromId(string id, CountCallBack callback)
    {
        GetCoffeeCountFromUrl("https://ko-fi.com/widget/counterwidget/" + id, callback);
    }
    public static void GetCoffeeCountFromUserName(string name, CountCallBack callback)
    {
        GetCoffeeCountFromUrl("https://ko-fi.com/" + name, callback);
    }
    public static void GetCoffeeCountFromUrl(string url, CountCallBack callback)
    {
        WebClient client = new WebClient();
        string downloadString = client.DownloadString(url);
        LookForCoffeeInfoInHtmlAndPush(downloadString, callback);
    }
    public static void LookForCoffeeInfoInHtmlAndPush(string htmlText, CountCallBack callback)
    {
        bool foundInfo;
        int coffee;
        LookForCoffeeInfoInHtml(htmlText, out foundInfo, out coffee);
        if (foundInfo)
            callback.SetCoffeCount(coffee);
        else callback.SetAsFailed();
    }
       
    public static void LookForCoffeeInfoInHtml(string htmlText,out bool found, out int coffeeValue) {
        coffeeValue = 0;
        found = false;
        if (string.IsNullOrEmpty(htmlText))
            return;
        string text = htmlText;
        int start = text.IndexOf("koficounter-value");
        if (start < 0) 
            return;
            
        text = text.Substring(start);
        int startBalise = text.IndexOf(">") + 1;
        int endBalise = text.IndexOf("<");
        if (startBalise < 0 || endBalise < 0) 
            return;
        string count = text.Substring(startBalise, endBalise - startBalise);
       
        found = int.TryParse(count, out coffeeValue);
    }
    
    public static IEnumerator LookForCoffeeInfo(string url, CountCallBack callback) {
        WWW www = new WWW(url);
        yield return www;
        if (string.IsNullOrEmpty(www.error))
        {
            LookForCoffeeInfoInHtmlAndPush(www.text, callback);
        }
        else callback.SetAsFailed();
    }

    //https://ko-fi.com/marcmakescomics

    

}
