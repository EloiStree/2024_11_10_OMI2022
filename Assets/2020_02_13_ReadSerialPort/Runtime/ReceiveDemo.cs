using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceiveDemo : MonoBehaviour
{
    public string m_lastMessageReceived;
    public int m_messageCount;

    public void LastMessageReceived(TimedMessage msg) {
        m_lastMessageReceived = msg.m_message;
        m_messageCount++;
    }
}
