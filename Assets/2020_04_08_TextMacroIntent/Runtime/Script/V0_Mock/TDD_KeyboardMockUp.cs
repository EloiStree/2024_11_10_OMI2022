using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDD_KeyboardMockUp : MonoBehaviour
{
    public KeyboardMockUp m_keyboard;
    public float m_timeBetweenRandomClick = 0.5f;
    public char[] m_keys;
    void Start()
    {
        InvokeRepeating("RandomInput", 0, m_timeBetweenRandomClick);
    }

    void RandomInput()
    {
        m_keyboard.Press(""+m_keys[Random.Range(0, m_keys.Length - 1)], Random.value<0.5f) ;
    }

    private void Reset()
    {
        m_keys = "0123456789AZERTYUIOPQSDFGHJKLMWXCVBN".ToCharArray();
    }
}
