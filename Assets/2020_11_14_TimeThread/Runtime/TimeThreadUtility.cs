using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeThreadUtility 
{

    public static TimeThreadMono m_inTheSceneThread;
    public static TimeTheadFrequenceMono m_inTheSceneFrequence;


    public TimeThreadMono GetThreadInScene()
    {

        if (m_inTheSceneThread == null)
            m_inTheSceneThread = GameObject.FindObjectOfType<TimeThreadMono>();
        return m_inTheSceneThread;
    }

    public bool HasThreadInScene()
    {
        TimeThreadMono t = GetThreadInScene();
        return t != null;
    }
    public TimeTheadFrequenceMono GetThreadFrequenceInScene()
    {

        if (m_inTheSceneFrequence == null)
            m_inTheSceneFrequence = GameObject.FindObjectOfType<TimeTheadFrequenceMono>();
        return m_inTheSceneFrequence;
    }
    public bool HasThreadFrequenceInScene()
    {
        TimeTheadFrequenceMono t = GetThreadFrequenceInScene();
        return t != null;
    }
}
