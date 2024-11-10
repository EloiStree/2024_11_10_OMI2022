using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class User32PercentPointRelative : IPercentPointGet
{
    public float m_xLeft2Right;
    public float m_yTop2Down;

    public void GetHorizontalDirection(out AxisDirectionHorizontal value)
    {
        value = AxisDirectionHorizontal.Left2Right;
    }

    public void GetHorizontalPercentValue(out double value)
    {
        value = m_xLeft2Right;
    }


    public void GetVerticalDirection(out AxisDirectionVertical value)
    {
        value = AxisDirectionVertical.Top2Down;
    }

    public void GetVerticalPercentValue(out double value)
    {
        value = m_yTop2Down;
    }

}
