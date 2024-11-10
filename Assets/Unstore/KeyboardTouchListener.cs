using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class KeyboardTouchListener : MonoBehaviour
{
    public KeyboardReadMono m_keyboardReader;
    public float m_timeBetweenCheckInSecond = 0.05f;
    public List<KeyboardTouch> m_previous = new List<KeyboardTouch>();
    public List<KeyboardTouch> m_current= new List<KeyboardTouch>();
    public TouchEvent m_pressed;
    public TouchEvent m_released; 
    public KeyboardTouch[] m_dontCount = new KeyboardTouch[] { KeyboardTouch.Shift, KeyboardTouch.Alt, KeyboardTouch.Menu, KeyboardTouch.Control };

    void Awake()
    {
        InvokeRepeating("CheckTouch", 0, m_timeBetweenCheckInSecond);
    }

    void CheckTouch()
    {
        m_current = m_keyboardReader.GetActiveTouches().ToList();
        for (int i = 0; i < m_dontCount.Length; i++)
        {
            m_current.Remove(m_dontCount[i]);
        }

        for (int i = 0; i < m_current.Count; i++)
        {
            if (!m_previous.Contains(m_current[i]))
            {
                m_pressed.Invoke(m_current[i]);
                //  newPressKey.Add(m_current[i]);
                // Debug.Log("P:" + m_current[i]);


            }
        }
        for (int i = 0; i < m_previous.Count; i++)
        {
            if (!m_current.Contains(m_previous[i]))
            {
                m_released.Invoke(m_previous[i]);
                //newReleaseKey.Add(m_previous[i]);
                // Debug.Log("R:" + m_previous[i]);

            }
        }

        m_previous = m_current;
    }

    [System.Serializable]
    public class TouchEvent : UnityEvent<KeyboardTouch> { }
    public bool Contain(KeyboardTouch mouseLeft)
    {
        return m_current.Contains(mouseLeft);
    }
}
