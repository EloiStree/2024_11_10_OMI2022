using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Experiment_SaveLotOfPoints : MonoBehaviour
{
    public MouseInformationAbstract m_mouse;
    public KeyboardTouchListener m_keylistener;

    public bool inverseTopBot=true;
    public bool inverseLeftRight;
    public BooleanSwitchListener m_leftClic= new BooleanSwitchListener();

    [TextArea(0,10)]
    public string m_mouseClick;

    

    private void Update()
    {
        bool hasChange;
        
        m_leftClic.SetValue(m_keylistener.Contain(KeyboardTouch.MouseLeft), out hasChange);
        if (hasChange && m_leftClic.GetValue() && m_keylistener.Contain(KeyboardTouch.MouseRight)) {
            SaveScreenPoint();

        }
    }
    private int m_count=0;
    public int m_timeBetweenClick=20;

    private void SaveScreenPoint()
    {
        float lr, bt;
        m_mouse.GetPourcent(out lr, out bt);
        m_count++;
        m_mouseClick += string.Format("{2}♦jomiraw:mm:{0:0.000}%:{1:0.000}%\n{3}♦jomiraw:ms:r\n",
           inverseLeftRight ? 1f - lr : lr, inverseTopBot?1f-bt:bt, m_count* m_timeBetweenClick , m_count*m_timeBetweenClick + m_timeBetweenClick);
    }
}
