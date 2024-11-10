using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDD_MockMusicUI : MonoBehaviour
{
    public MockMusicUI m_mockMusic;
    public float m_timeBetweenRandomClick = 0.5f;
    void Start()
    {
        InvokeRepeating("RandomInput", 0, m_timeBetweenRandomClick);
    }

    void RandomInput()
    {
        m_mockMusic.Tap((MockMusicUI.Note)Random.Range(0, 7));
    }

}
