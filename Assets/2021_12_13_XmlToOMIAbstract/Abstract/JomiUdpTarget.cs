using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IJomiUdpTarget
{
    void GetTargetIdName(out string idName);
    void GetTargetIP(out string ipAddress);
    void GetTargetPort(out string ipPort);
}

[System.Serializable]
public class JomiUdpTarget : IJomiUdpTarget
{
    public string m_idName;
    public string m_ipAddress;
    public string m_port;

    public JomiUdpTarget(string idName, string ipAddress, string port)
    {
        m_idName = idName;
        m_ipAddress = ipAddress;
        m_port = port;
    }

    public void GetTargetIdName(out string idName)
    {
        idName = m_idName;
    }

    public void GetTargetIP(out string ipAddress)
    {
        ipAddress = m_ipAddress;
    }

    public void GetTargetPort(out string ipPort)
    {
        ipPort = m_port;
    }
}