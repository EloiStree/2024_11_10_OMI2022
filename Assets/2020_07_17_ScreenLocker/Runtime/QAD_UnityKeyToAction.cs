using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class QAD_UnityKeyToAction : MonoBehaviour
{
    public KeyCode m_keyToTrigger;
    public UnityEvent m_actionDown;
    public UnityEvent m_actionUp;
 
    void Update()
    {
        if (Input.GetKeyDown(m_keyToTrigger))
        {
            m_actionDown.Invoke();
        }
        if (Input.GetKeyUp(m_keyToTrigger))
        {
            m_actionUp.Invoke();
        }
    }
}
