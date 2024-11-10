////using System.Collections;
////using System.Collections.Generic;
////using UnityEngine;

////public class KeyStrokeKeyboardState : AbstractKeyboardState
////{

////    public KeyboardTouchPressRequest m_lastRequest;
////    public double m_lastDown;
////    public double m_lastUp;

////    public void KeyStrokeRequest(KeyboardTouchPressRequest touchRequest) {
////        m_lastRequest = touchRequest;
////        m_lastDown = base.m_keyboardState.m_downTime.GetTimeInSeconds(touchRequest.m_touch);
////        m_lastUp = base.m_keyboardState.m_upTime.GetTimeInSeconds(touchRequest.m_touch);
////        if (touchRequest.m_pression == PressType.Down )
////        {

////            base.m_keyboardState.RealPressDown(touchRequest.m_touch);
////        }
////         if (touchRequest.m_pression == PressType.Up )
////        {
////            base.m_keyboardState.RealPressUp(touchRequest.m_touch);
////        }

////    }
    
////    public override bool GetRealStateOfCapsLock()
////    {
////        return base.m_keyboardState.IsCapsLockOn();
////    }

////    public override bool GetRealStateOfNumLock()
////    {
////        return base.m_keyboardState.IsNumLockOn();
////    }

////    public override bool GetRealStateOfScrollLock()
////    {
////        return base.m_keyboardState.IsScrollLockOn();
////    }

////    public override bool GetRealStateOf(KeyboardTouch touch)
////    {
////        return base.m_keyboardState.IsTouchActive(touch);
////    }
////}
