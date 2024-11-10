using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using UnityEngine;
using UnityEngine.UI;

public class UI_DisplayIPAddressess : MonoBehaviour
{
    public Dropdown m_target;
    public float m_refreshTime = 5;
    void Start()
    {
        InvokeRepeating("TryToSetWithCurrentIP", 0, m_refreshTime);
    }

  
    public void TryToSetWithCurrentIP() { 


        string hostName = Dns.GetHostName();
        m_target.ClearOptions();
        m_target.AddOptions(Dns.GetHostEntry(hostName).AddressList.Where(k => k.GetAddressBytes().Length == 4).Select(k => k.ToString()).ToList());
        //m_target.AddOptions(Dns.GetHostEntry(hostName).AddressList.Select(k => k.ToString()).ToList());

    }
}
