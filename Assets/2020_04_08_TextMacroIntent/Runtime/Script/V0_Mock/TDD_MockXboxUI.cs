using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDD_MockXboxUI : MonoBehaviour
{
    public MockXboxUI m_mockMouse;
    public float m_timeBetweenRandomClick = 0.1f;
    void Start()
    {
        InvokeRepeating("RandomInput", 0, m_timeBetweenRandomClick);
    }

    void RandomInput()
    {
        m_mockMouse.SetOn((MockXboxUI.XboxButton) Random.Range(0,14), Random.value < 0.5f ? true : false);
    }
}
