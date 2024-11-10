using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public  abstract class KeyboardKeyListener : MonoBehaviour {


    public KeyboardStorageState m_keyboardTouchState;
    public List<KeyboardTouch> m_activeKeys;
    public List<KeyboardTouch> m_availaibleKeys;

    public OnKeyChange m_onKeyDown;
    public OnKeyChange m_onKeyUp;

    public bool m_isCapsLockOn;
    public bool m_isNumLockOn;
    public bool m_isScrollLock;

    public DateTime lastCheck;
    public DateTime now;

   

    [System.Serializable]
    public class OnKeyChange : UnityEvent<KeyboardTouch> {
    }
    

    protected virtual void Awake()
    {
        m_keyboardTouchState = new KeyboardStorageState();
        m_availaibleKeys = KeystrokeUtility.GetEnumList<KeyboardTouch>();
        m_keyboardTouchState.m_keyboardState.onStateChange += NotifyEvent;

    } 

    protected virtual void Update()
    {
        now = DateTime.Now;


        m_activeKeys = m_keyboardTouchState.m_keyboardState.GetActiveElements();
        foreach (KeyboardTouch vkc in m_availaibleKeys)
        {
            if (IsTouchActive(vkc))
                m_keyboardTouchState.RealPressDown(vkc);
            if (!IsTouchActive(vkc))
                m_keyboardTouchState.RealPressUp(vkc);
        }
        m_isCapsLockOn = IsCapsLockOn();
        m_isNumLockOn = IsNumLockOn(); 
        m_isScrollLock = IsScrollLockOn();


        lastCheck = now;




    }
    private void NotifyEvent(KeyboardTouch element, bool isOn)
    {
        if (isOn)
            m_onKeyDown.Invoke(element);
        else
            m_onKeyUp.Invoke(element);
    }
  

    void OnValidate()
    {
        m_availaibleKeys = KeystrokeUtility.GetEnumList<KeyboardTouch>();
    }
    private EnumDictionary<KeyboardTouch> GetKeysPressionState()
    {
        return m_keyboardTouchState.m_keyboardState;
    }
    public bool IsAltActive()
    {
        return GetKeysPressionState().GetState(KeyboardTouch.Alt);
    }

    public bool IsAltGrActive()
    {
        return GetKeysPressionState().GetState(KeyboardTouch.AltGr);
    }

    public bool IsControlActive()
    {
        return GetKeysPressionState().GetState(KeyboardTouch.Control);
    }

    public bool IsMetaActive()
    {
        return GetKeysPressionState().GetState(KeyboardTouch.Meta);
    }
    public bool IsShiftActive()
    {
        return GetKeysPressionState().GetState(KeyboardTouch.Shift);
    }



    public abstract bool IsCapsLockOn();
    public abstract bool IsNumLockOn();
    public abstract bool IsScrollLockOn();
    public abstract bool IsTouchActive(KeyboardTouch keyboardTouch);
  
    public KeyboardTouch[] GetActiveTouchs()
    {
        return m_activeKeys.ToArray();
    }

}
