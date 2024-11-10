using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KoFiCount : MonoBehaviour
{
    public long m_countPoint;
    public long m_countEstimateWinTime;
    public string m_userName;

    public KoFiCount(string userName) { 
    
    }
    public void OpenKoFiPage() {
        Application.OpenURL("https://ko-fi.com/"+m_userName);
    }


    public static void AddPoints(string userName, int points)
    {

    }
    public static void AddTime(string userName, int points)
    {

    }

}

public class KoFiPoints {
    public int m_points;
    public int m_saveEstimateSeconds;

}