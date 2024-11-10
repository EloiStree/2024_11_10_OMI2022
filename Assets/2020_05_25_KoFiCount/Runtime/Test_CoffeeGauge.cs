using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Test_CoffeeGauge : MonoBehaviour
{
    public string m_userName = "eloistree";
    public string m_userId = "E1E21QCY5";
    public ExtractedInfoFromPublicProfile m_profile;
    public long m_count;
    public CountChange m_countEvent;
    [System.Serializable]
    public class CountChange : UnityEvent<long> { };


    public string m_textId;

    IEnumerator Start()
    {

        Refresh();
        WWW www = new WWW("https://ko-fi.com/widget/counterwidget/" + m_userId);
        yield return www;
        m_textId = www.text;
    }

    public void Refresh()
    {
       

        RefreshCount();
        RefreshProfile();
    }
    public void RefreshCount()
    {

        StartCoroutine(KoFiWidget.GetCoffeeCountFromId(m_userId, Count));
    }
    public void RefreshProfile()
    {

        StartCoroutine(KoFiExtractor.ExtractInfoFrom(m_userName, FoundProfile)); 
    }

    private void Count(KoFiWidget.CountCallBack count)
    {
        m_count = count.m_coffeeCount;
        m_countEvent.Invoke(count.m_coffeeCount);
    }

    private void FoundProfile(ExtractedInfoFromPublicProfile profile)
    {
        m_profile = profile;
    }

   
}
