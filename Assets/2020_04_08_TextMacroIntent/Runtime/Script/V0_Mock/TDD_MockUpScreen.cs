using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDD_MockUpScreen : MonoBehaviour
{
    public MockUpScreen m_mockScreen;
    public float m_timeBetweenRandomClick=0.5f;

    void Start()
    {
        InvokeRepeating("RandomInput", 0, m_timeBetweenRandomClick);
    }

    void RandomInput()
    {
        m_mockScreen.SetGlobalCursorPosition(Random.value, Random.value);
    }
}
