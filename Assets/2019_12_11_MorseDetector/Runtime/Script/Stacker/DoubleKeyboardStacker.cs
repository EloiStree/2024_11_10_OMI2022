using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleKeyboardStacker : MonoBehaviour
{
    public string m_stackName="Double Key LeftCmd-Space";
    public KeyCode m_keycodeShort= KeyCode.LeftControl;
    public KeyCode m_keycodeLong = KeyCode.Space;
    public float m_existTime=0.25f;
  
    void Update()
    {
        if (Input.GetKeyDown(m_keycodeShort))
            MorseStacker.StackOn(m_stackName, MorseKey.Short, m_existTime);
        if (Input.GetKeyDown(m_keycodeLong))
            MorseStacker.StackOn(m_stackName, MorseKey.Long, m_existTime);


    }
}
