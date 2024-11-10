using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

public class UDPThreadDispatcher : MonoBehaviour
{
    public int m_portId = 2504;
    public float m_timeBetweenUnityCheck=0.05f;
    public StringEvent m_messageReceived;
    public System.Threading.ThreadPriority m_threadPriority;

    public Queue<string> m_receivedMessages = new Queue<string>();
    public string m_lastReceived;
    private bool m_wantThreadAlive = true;
    private Thread m_threadListener=null;
    public UdpClient m_listener;
    public IPEndPoint m_ipEndPoint;
    public bool m_hasBeenKilled;

    public bool m_useAwakeInit;
    private void Awake()
    {
        InvokeRepeating("PushOnUnityThreadMessage", 0, m_timeBetweenUnityCheck);
        if (m_useAwakeInit) 
            ResetThread();
    }

    [ContextMenu("Reset Thread")]
    public void ResetThread()
    {
            if (m_threadListener!=null) 
                 m_threadListener.Abort();
            m_threadListener = new Thread(ChechUdpClientMessageInComing);
            m_threadListener.Priority = m_threadPriority;
            m_threadListener.Start();
        
       
    }

 


    public void OnDisable()
    {
        if (!m_hasBeenKilled)
        {
            Kill();
        }

    }
    private void OnDestroy()
    {
        if (!m_hasBeenKilled)
        {
            Kill();
        }
    }
    private void OnApplicationQuit()
    {
        if (!m_hasBeenKilled)
        {
            Kill();
        }
    }

    private void Kill()
    {
        if(m_listener!=null)
            m_listener.Close();
        if (m_threadListener != null)
            m_threadListener.Abort();
        m_wantThreadAlive = false;
        m_hasBeenKilled = true;
    }

   

    public void PushOnUnityThreadMessage() {
        while (m_receivedMessages.Count > 0) { 
            m_lastReceived = m_receivedMessages.Dequeue();
            m_messageReceived.Invoke(m_lastReceived);
        }
    }

    private void ChechUdpClientMessageInComing() {

        if (m_listener != null)
        {
                m_listener.Close();
        }
        if (m_listener == null) { 
            m_listener = new UdpClient(m_portId);
            m_ipEndPoint = new IPEndPoint(IPAddress.Any, 0);
        }
       

        while (m_wantThreadAlive) { 
            try
            {

                Byte[] receiveBytes = m_listener.Receive(ref m_ipEndPoint);
                string returnData = Encoding.UTF8.GetString(receiveBytes);
                m_receivedMessages.Enqueue(returnData);
                //RemoteIpEndPoint.Address.ToString() --  RemoteIpEndPoint.Port.ToString());
            }
            catch (Exception e)
            {
               Debug.Log(e.ToString());
                m_wantThreadAlive = false;
            }
        }
    }


    [System.Serializable]
    public class StringEvent : UnityEvent<string>{ 
    
    }
}

