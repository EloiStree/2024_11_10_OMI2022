using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseScrollStacker : MonoBehaviour
{
    public string m_verticalStack = "MouseVerticalScroll";
    public string m_horizontalStack = "MouseHorizontalScroll";
    public float m_exitTimeVertical = 0.3f;
    public float m_exitTimeHorizontal = .8f;
    void Update()
    {
        if (Input.mouseScrollDelta.y > 0f)
            MorseStacker.StackOn(m_verticalStack, MorseKey.Short, m_exitTimeVertical);
        if (Input.mouseScrollDelta.y < 0f)
            MorseStacker.StackOn(m_verticalStack, MorseKey.Long, m_exitTimeVertical);
        if (Input.mouseScrollDelta.x > 0f)
            MorseStacker.StackOn(m_horizontalStack, MorseKey.Short, m_exitTimeHorizontal);
        if (Input.mouseScrollDelta.x < 0f)
            MorseStacker.StackOn(m_horizontalStack, MorseKey.Long, m_exitTimeHorizontal);
      

    }
}
