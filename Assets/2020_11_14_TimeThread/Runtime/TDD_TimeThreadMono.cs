using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDD_TimeThreadMono : MonoBehaviour
{

    public TimeThreadMono m_timeThread;
    public float m_helloTime;
    public bool m_receivedTest;
    public int m_pingCount;
    private PingWhenItisTime m_switchButton;
    void Start()
    {
        PingWhenItisTime hello = new PingWhenItisTime(3f, Hello , PingThreadType.InUnityThread );
        m_switchButton = new PingWhenItisTime(6f, SwitchReceivedBool, PingThreadType.InTimeThread);
        m_timeThread.Add(hello);
        m_timeThread.Add(m_switchButton);

    }


    public void AddPing() {
        m_pingCount++;
    }

    private void Hello()
    {
        Debug.Log("Hello");
    }

    private void SwitchReceivedBool()
    {
        m_receivedTest = true;
    }

}
