
using System;

[System.Serializable]
public class KeyboardStorageState
{
    public EnumDictionary<KeyboardTouch> m_keyboardState = new EnumDictionary<KeyboardTouch>();
    public TimeDictionary<KeyboardTouch> m_downTime = new TimeDictionary<KeyboardTouch>();
    public TimeDictionary<KeyboardTouch> m_upTime = new TimeDictionary<KeyboardTouch>();



    public bool IsAltActive()
    {

        return IsTouchActive(KeyboardTouch.Alt) || IsTouchActive(KeyboardTouch.AltGr) || IsTouchActive(KeyboardTouch.LeftAlt) || IsTouchActive(KeyboardTouch.RightAlt);
    }

    public bool IsAltGrActive()
    {
        return IsTouchActive(KeyboardTouch.AltGr) || IsTouchActive(KeyboardTouch.RightAlt);
    }

    public bool IsControlActive()
    {
        return IsTouchActive(KeyboardTouch.Control) || IsTouchActive(KeyboardTouch.LeftControl) || IsTouchActive(KeyboardTouch.RightControl);
    }

    public bool IsMetaActive()
    {
        return IsTouchActive(KeyboardTouch.Meta) || IsTouchActive(KeyboardTouch.MetaLeft) || IsTouchActive(KeyboardTouch.MetaRight)
            || IsTouchActive(KeyboardTouch.LeftWindow) || IsTouchActive(KeyboardTouch.RightWindow)
             || IsTouchActive(KeyboardTouch.RightCommand) || IsTouchActive(KeyboardTouch.LeftCommand)
             ;
    }

    public bool IsShiftActive()
    {
        return IsTouchActive(KeyboardTouch.Shift) || IsTouchActive(KeyboardTouch.LeftShift) || IsTouchActive(KeyboardTouch.RightShift);
    }
    public bool IsTouchActive(KeyboardTouch keyboardTouch)
    {
        return m_keyboardState.GetState(keyboardTouch) == true;
    }


    public KeyboardTouch[] GetActiveTouchs()
    {
        return m_keyboardState.GetActiveElements().ToArray();
    }

    public void Reset()
    {
        m_keyboardState.Clear();
    }

    public void RealPressDown(KeyboardTouch touch)
    {
        bool hasChanged = m_keyboardState.GetState(touch) == false;
        if (hasChanged)
        {
            m_downTime.SetTimeRecorded(touch, DateTime.Now);
        }
        m_keyboardState.SetState(touch, true);
    }


    public void RealPressUp(KeyboardTouch touch)
    {
        bool hasChanged = m_keyboardState.GetState(touch) == true;
        if (hasChanged)
        {
            m_upTime.SetTimeRecorded(touch, DateTime.Now);
        }
        m_keyboardState.SetState(touch, false);
    }

}