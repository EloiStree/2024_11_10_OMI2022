using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PixelToBooleanRegister : MonoBehaviour
{

    public List<PixelObserved> m_pixelsObserved = new List<PixelObserved>();

    public void Clear() {
        m_pixelsObserved.Clear();
    }
    public void Add(PixelObserved pixelObserver) {
        m_pixelsObserved.Add(pixelObserver);
    }
}

[System.Serializable]
public class PixelObserved
{
    public int m_displayIndex;
    public float m_leftRightPourcent = 0.5f;
    public float m_botTopPourcent = 0.5f;
    public Color m_wantedColor;
    public float m_pourcentPrecision = 0.4f;
}
[System.Serializable]
public class PixelObservedState
{
    public PixelObserved m_ref;
    public Color m_currentColor;
    public BooleanSwitchListener m_isTheSame;
    public UnityEvent m_isTrue;
    public UnityEvent m_isFalse;
    private PixelObserved pixelObserver;

    public PixelObservedState(PixelObserved pixelObserver)
    {
        this.pixelObserver = pixelObserver;
    }

    internal void SetWithColor(Color pixelColor)
    {
        m_currentColor = pixelColor;
         RefreshState();

    }
    public void RefreshState()
    {
        bool hasChange;
        bool value = Mathf.Abs(m_currentColor.r - m_ref.m_wantedColor.r) <m_ref.m_pourcentPrecision
            && Mathf.Abs(m_currentColor.g - m_ref.m_wantedColor.g) < m_ref.m_pourcentPrecision
            && Mathf.Abs(m_currentColor.b - m_ref.m_wantedColor.b) < m_ref.m_pourcentPrecision;
        m_isTheSame.SetValue(value, out hasChange);
        if (hasChange)
        {
            if (value)
                m_isTrue.Invoke();
            else
                m_isFalse.Invoke();

        }
    }
}