using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInformationInUnity : MonoBehaviour
{
    public MouseInformationAbstract m_toAffect;
    private int m_buttonMouseCount=5;
    void Start()
    {
        m_toAffect.SetButtonsCount(m_buttonMouseCount);
    }

    void Update()
    {
        for (int i = 0; i < m_buttonMouseCount; i++)
        {
            m_toAffect.SetButtonValue(i, Input.GetMouseButton(i));
        }

        Vector2 pos= Input.mousePosition;
        m_toAffect.SetPosition_D2T_L2R((int)pos.y, (int)pos.x);

    }
}
