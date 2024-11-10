using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDefaultTargetIp : MonoBehaviour
{
    public string m_name="DefaultTarget";
    public UDPThreadSender m_sender;

    public void Awake()
    {
        m_sender.AddAlias(m_name, "127.0.0.1", 2506);
    }
    public void SetAddress(string ip)
    {
        m_sender.SetAliasIp(m_name,ip);
    }
    public void SetPort(string port)
    {
        int p;
        if (int.TryParse(port, out p))
            SetPort(p);

    }
    public void SetPort(int port)
    {

        m_sender.SetAliasPort(m_name,port);
        m_sender.ResetTheTargetFromAlias();

    }
}
