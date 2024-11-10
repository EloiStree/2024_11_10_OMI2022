using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Net.Configuration;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

public class OpenSerialPortAtStart : MonoBehaviour
{
   
    public SerialPortRequestCommunication m_portSetter;
    public UnityThreadPortCommunication m_communication;
    public MessageReceivedAsStringEvent m_receivedMessageAsString;
    public MessageReceivedEvent m_receivedMessage;
    void Awake()
    {
        m_communication = new UnityThreadPortCommunication(m_portSetter, false);
        m_communication.StartThread();
    }

    private void Update()
    {
        TimedMessage msg;
        while (m_communication.HasReceivedMessage()) {
            msg = m_communication.DequeueReceivedMessage();
            m_receivedMessage.Invoke(msg);
            m_receivedMessageAsString.Invoke(msg.m_message);
        }
         
    }

    void OnDestroy()
    {
        m_communication.StopThread();
    }
}

[System.Serializable]
public class MessageReceivedEvent : UnityEvent<TimedMessage> { }
[System.Serializable]
public class MessageReceivedAsStringEvent : UnityEvent<string> { }

public class UnityThreadPortCommunicationMono
{ 
    
public MessageReceivedEvent m_receivedMessageEvent = new MessageReceivedEvent();
public MessageReceivedAsStringEvent m_receivedMessageAsStringEvent = new MessageReceivedAsStringEvent();


}

public class SerialPortRequestCommunication {
    public uint m_portId = 3;
    public uint m_baudRate = 9600;
    public uint m_dataBite = 8;
    public Parity m_parity;
    public StopBits m_stopBits = StopBits.One;
    public Handshake m_handShake;

    public string GetPortName() { return "COM" + m_portId; }
    public uint GetPortId() { return m_portId; }

    public SerialPortRequestCommunication GetCopy()
    {
        return new SerialPortRequestCommunication()
        {
            m_baudRate = this.m_baudRate,
            m_portId = this.m_portId,
            m_dataBite = this.m_dataBite,
            m_parity = this.m_parity,
            m_handShake = this.m_handShake,
            m_stopBits = this.m_stopBits
        };
    }
}


    public class UnityThreadPortCommunication
{
    public Thread m_thread;
    public SerialPortRequestCommunication m_setter;
    public SerialPort m_serialPort;
    public bool m_requestThreadToStop=false;

    public UnityThreadPortCommunication(SerialPortRequestCommunication portSetter, bool autoStart) {
        m_setter = portSetter;
        if (autoStart)
            StartThread();
    }

    public bool IsStillConnected()
    {
        return m_serialPort != null && m_serialPort.IsOpen;
    }
    public bool IsStillRunnning()
    {
        return IsStillConnected() && !m_requestThreadToStop && m_thread!=null && m_thread.IsAlive ;
    }

    public SerialPortRequestCommunication GetSetterInformation() { return m_setter; }


    public void CloseAndGetReconnection(out UnityThreadPortCommunication reconnection, bool autoStart) {
        StopThread();
        reconnection =  new UnityThreadPortCommunication(m_setter, true);
        if (autoStart)
            StartThread();
    }

    public void StopThread() {

        m_requestThreadToStop = true;
        if(m_serialPort!=null && m_serialPort.IsOpen)
        m_serialPort.Close();
        if (m_thread != null && m_thread.IsAlive)
            m_thread.Abort();
    }

    public void StartThread()
    {
       

        m_thread = new Thread(ReadAndSend);
        m_thread.Priority = System.Threading.ThreadPriority.Lowest;
        m_thread.Start();
    }

    public Queue<TimedMessage> m_received= new Queue<TimedMessage>();
    public Queue<TimedMessage> m_toSend = new Queue<TimedMessage>();

    public void AddToSendMessage(string message)
    {
        m_toSend.Enqueue(new TimedMessage(message));
    }
    public void AddReceivedMessage(string message)
    {
        m_received.Enqueue(new TimedMessage(message));
    }

    public bool HasReceivedMessage() { return m_received.Count>0; }
    public TimedMessage DequeueReceivedMessage() { 
        return m_received.Dequeue(); }
    public TimedMessage DequeueToSendMessage() { return m_toSend.Dequeue(); }

    public void ReadAndSend() {

        bool connectionSucceed;
        SerialPortUtility.SetConnectionWithPort(m_setter,
            out m_serialPort, out connectionSucceed);
        if (!connectionSucceed)
        {

            Debug.Log("Fail to connect: " + m_setter.GetPortName());
            StopThread();
            return;
        }
        Thread.Sleep(1000);
        while (!m_requestThreadToStop)
        {

         
            try
            {
                if (m_serialPort != null &&  m_serialPort.IsOpen) { 
                    string message = m_serialPort.ReadLine().Trim();
                    if (!string.IsNullOrEmpty(message))
                    {
                        TimedMessage tm = new TimedMessage(message);
                        m_received.Enqueue(tm);
                        if(m_onReceivedMessage!=null)
                            m_onReceivedMessage(m_setter.GetPortId(), tm);
                    }
                }
            }
            catch (TimeoutException)
            {
                Debug.Log("Timeout: COM" + m_setter.GetPortId());
                StopThread();
            }
            catch (IOException)
            {
                Debug.Log("Connection Error: COM"+m_setter.GetPortId());
                StopThread();
            }
            while (m_serialPort != null && m_serialPort.IsOpen && m_toSend.Count > 0)
            {
                m_serialPort.WriteLine(DequeueToSendMessage().m_message);
            }


        }
        StopThread();
    }

    public void AddMessageListener(PortMessageEvent messageListener)
    {
            m_onReceivedMessage += messageListener;
    }
    public void RemoveMessageListener(PortMessageEvent messageListener)
    {
            m_onReceivedMessage -= messageListener;
    }
    private PortMessageEvent m_onReceivedMessage;
}


public delegate void PortMessageEvent(uint portId, TimedMessage message);

[System.Serializable]
public struct TimedMessage
{
    public long m_whenReceived;
    public string m_message;
    public TimedMessage(string message)
    {
        m_message = message;
        m_whenReceived = (long)(DateTimeOffset.UtcNow.ToUnixTimeMilliseconds());
    }
}


public class SerialPortUtility {

     public static List<string> GetListOfAvailaiblePort()
    {
        return SerialPort.GetPortNames().ToList();
    }


    public static void SetConnectionWithPort(SerialPortRequestCommunication setter, out SerialPort portConnection, out bool connectionEstablish)
    {
        portConnection = null;
        try
        {
            StringComparer stringComparer = StringComparer.OrdinalIgnoreCase;
            portConnection = new SerialPort();

            portConnection.PortName = (setter.GetPortName());
            portConnection.BaudRate = (int)(setter.m_baudRate);
            portConnection.Parity = (setter.m_parity);
            portConnection.DataBits = (int)(setter.m_dataBite);
            portConnection.StopBits = (setter.m_stopBits);
            portConnection.Handshake = (setter.m_handShake);

            // Set the read/write timeouts
            portConnection.ReadTimeout = 600;
            portConnection.WriteTimeout = 600;

            portConnection.Open();
            connectionEstablish = true;
        }
        catch (Exception e)
        {
            portConnection = null;
            connectionEstablish = false;
           // Debug.Log("Couldn't open serial port: " + e.Message);
        }
    }
}