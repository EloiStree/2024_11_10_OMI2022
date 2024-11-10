using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseButtonStacker : MonoBehaviour
{
    public string m_stackName = "Mouse Left Right Stack";
    public float m_exitTime = 0.2f;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            MorseStacker.StackOn(m_stackName, MorseKey.Short, m_exitTime);
        if (Input.GetMouseButtonDown(1))
            MorseStacker.StackOn(m_stackName, MorseKey.Long, m_exitTime);

    }
}
