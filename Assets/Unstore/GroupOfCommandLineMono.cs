using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupOfCommandLineMono : MonoBehaviour
{
    public CommandLineEvent m_sendOnRequest;
    public List<string> m_commandLinesToSend = new List<string>();


    public void PushCommands() {

        for (int i = 0; i < m_commandLinesToSend.Count; i++)
        {
            m_sendOnRequest.Invoke(new CommandLine( m_commandLinesToSend[i]));
        }
    }
    public void Clear()
    {

        m_commandLinesToSend.Clear();
    }
    public void Add(params string [] commandToSend)
    {
        for (int i = 0; i < commandToSend.Length; i++)
        {
            m_commandLinesToSend.Add(commandToSend[i]);
        }
    }
}
