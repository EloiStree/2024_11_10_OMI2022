//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Threading;
//using UnityEngine;
//using WindowsInput;
//using WindowsInput.Native;

//public class WindowVirtualKeyboard : AbstractInteractableKeyboard
//{

//    public bool m_isOnWritePlatform;
//    public bool m_checkHardWareKeyState;
//    public InputSimulator m_instance;
//    public InputSimulator WinKeyboard { get {  
//                if(m_instance==null)
//                m_instance =new InputSimulator();
//            return m_instance;
//        } }

//    public string m_pressionDebugMessage;
//    public string m_conversionDebugMessage;

//    public void Awake()
//    {
//        CheckIfRunable();
//    }
//    public void OnValidate()
//    {
//        CheckIfRunable();
//    }

//    private void CheckIfRunable()
//    {
//#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
//        m_isOnWritePlatform = true;
//#endif
//    }
    

//    public override bool GetRealStateOf(KeyboardTouch touch)
//    {
//        bool isConvertable;
//        VirtualKeyCode virtualKeyCode;
//        KeyBindingTable.ConvertTouchToWindowVirtualKeyCodes(touch, out virtualKeyCode, out isConvertable);
//        if (!isConvertable) {
//                m_conversionDebugMessage=("Real State | Not convertable:" + virtualKeyCode.ToString() + " of "+ touch);
//            return false;
//        }
//        if(m_checkHardWareKeyState)
//            return WinKeyboard.InputDeviceState.IsHardwareKeyDown(virtualKeyCode);
//        return WinKeyboard.InputDeviceState.IsKeyDown(virtualKeyCode);
//    }

//    public void StrokeASCII(int m_code)
//    {
//        throw new NotImplementedException();
//    }

//    public override bool GetRealStateOfCapsLock()
//    {
//        return WinKeyboard.InputDeviceState.IsTogglingKeyInEffect(VirtualKeyCode.CAPITAL);
//    }

//    public override bool GetRealStateOfNumLock()
//    {
//        return WinKeyboard.InputDeviceState.IsTogglingKeyInEffect(VirtualKeyCode.NUMLOCK);
//    }

//    public override bool GetRealStateOfScrollLock()
//    {
//        return WinKeyboard.InputDeviceState.IsTogglingKeyInEffect(VirtualKeyCode.SCROLL);
//    }

//    public override  void RealPressDown(KeyboardTouch touch)
//    {
//        bool isConvertable;
//        VirtualKeyCode virtualKeyCode;
//        KeyBindingTable.ConvertTouchToWindowVirtualKeyCodes(touch, out virtualKeyCode, out isConvertable);
//        if (!isConvertable)
//        {

//             m_conversionDebugMessage=("Press Down | Not convertable:" + touch.ToString());
//            return;
//        }
//        //if (MousePressing(touch, true)) { }
//        else {

//            m_pressionDebugMessage = ("Press Down |" + touch.ToString());
//            WinKeyboard.Keyboard.KeyDown(virtualKeyCode);

//        }
//    }

//    public override void RealPressUp(KeyboardTouch touch)
//    {
//        bool isConvertable;
//        VirtualKeyCode virtualKeyCode;
//        KeyBindingTable.ConvertTouchToWindowVirtualKeyCodes(touch, out virtualKeyCode, out isConvertable);
//        if (!isConvertable)
//        {
//            m_conversionDebugMessage="Press Up | Not convertable:" + touch.ToString();
//            return;
//        }
//        else
//        {
//            m_pressionDebugMessage = "Press Up |" + touch.ToString();
//            WinKeyboard.Keyboard.KeyUp(virtualKeyCode); }
//    }

//    private bool  MousePressing( KeyboardTouch touch, bool down)
//    {
//        if (touch == KeyboardTouch.MouseLeft || touch == KeyboardTouch.MouseMiddle || touch == KeyboardTouch.MouseRight
//             || touch == KeyboardTouch.MouseSupp1 || touch == KeyboardTouch.MouseSupp2) {

//                switch (touch)
//                {
//                case KeyboardTouch.MouseLeft:
//                    if (down)
//                        WinKeyboard.Mouse.LeftButtonDown();
//                    else
//                        WinKeyboard.Mouse.LeftButtonUp();
//                    break;
//                case KeyboardTouch.MouseRight:
//                    if (down)
//                        WinKeyboard.Mouse.RightButtonDown();
//                    else
//                        WinKeyboard.Mouse.RightButtonUp();
//                    break;
//                case KeyboardTouch.MouseMiddle:
//                case KeyboardTouch.MouseSupp1:
//                case KeyboardTouch.MouseSupp2:
//                case KeyboardTouch.MouseSupp3:
//                    throw new NotImplementedException("InputSimulator Don't seem to work with Mouse.XButton and Keyboard.Press (XBUTTON1)");

//                default:
//                    break;
//                }
//            return true;
//        }
//        return false;
//    }

//    public override void Stroke(char character)
//    {
//        WinKeyboard.Keyboard.TextEntry(character);
//    }
//    public override  void Stroke(string text)
//    {
//        WinKeyboard.Keyboard.TextEntry(text);
//    }
//    public void StrokeUnicode(int unicode) {
//        Stroke((char)unicode);
//    }
    
//}


//public abstract class AbstractInteractableKeyboard : AbstractKeyboardState, IKeyboardWrite {

//    public void CheckThatKeypadIsOn()
//    {
//        bool isNumOn = GetRealStateOfNumLock();
//        if (!isNumOn)
//        {
//            PressDown(KeyboardTouch.NumLock);
//            PressUp(KeyboardTouch.NumLock);
//        }
//    }


//    public void PressDown(KeyboardTouch touch)
//    {
//        m_keyboardState.RealPressDown(touch);
//        RealPressDown(touch);
//    }
//    public void PressUp(KeyboardTouch touch)
//    {
//        m_keyboardState.RealPressUp(touch);
//        RealPressUp(touch);
//    }

//    public abstract void RealPressDown(KeyboardTouch touch);
//    public abstract void RealPressUp(KeyboardTouch touch);

//    public abstract void Stroke(char character);
//    public abstract void Stroke(string text);

    
//}

////DIVIVE THE CLASS IN TO Mono<-AbstractKeyboardStateAccess<-AsbtractKeyboardAccess
//public abstract class AbstractKeyboardState : MonoBehaviour, IKeyboardRead {

//    public KeyboardStorageState m_keyboardState;
//    public DateTime m_lastCheck;
//    public DateTime m_now;
//    public double m_timeDelta;
//    public KeyboardTouch[] m_listenedTouch = new KeyboardTouch[] { };
//    public RefreshTool m_refreshType = RefreshTool.UnityUpdate;


//    public ThreadInfo threadInfo = new ThreadInfo();
//    public class ThreadInfo{
//       public  Thread m_process=null;
//       public bool requestToBeKilled=false;
//        public int millisecondSleeping=10;
//       public System.Threading.ThreadPriority m_threadPrioty = System.Threading.ThreadPriority.AboveNormal;
//    }
//    public enum RefreshTool { UnityUpdate, Thread}


//    void OnEnable()
//    {
//        m_keyboardState.Reset();
//        RefreshToRealState();

//        if (m_refreshType == RefreshTool.Thread) {
//            threadInfo.m_process = new Thread(LaunchRefreshStateLoop);
//            threadInfo.m_process.Priority = threadInfo.m_threadPrioty;
//            threadInfo.m_process.Start();
//        }
//    }

//    void OnDisable() {
//        threadInfo.requestToBeKilled = true;
//    }
//    public void Reset()
//    {
//        m_listenedTouch = KeyBindingTable.GetAllTouches();


//    }
//    void Update() {

//        if (m_refreshType == RefreshTool.UnityUpdate) {
//            RefreshToRealState();
//        }

//    }
//    public void SaveTimeAsChecked() {
//        m_now = DateTime.Now;
//        m_timeDelta = (m_now - m_lastCheck).TotalSeconds;
//        m_lastCheck = m_now;
//    }

//    private void LaunchRefreshStateLoop() {
//        while (threadInfo.requestToBeKilled == false) {
//            RefreshToRealState();
//            Thread.Sleep(threadInfo.millisecondSleeping);
//        }
//    }

//    private void RefreshToRealState()
//    {
//        KeyboardTouch[] touches = m_listenedTouch;
//        for (int i = 0; i < touches.Length; i++)
//        {
//            if(GetRealStateOf(touches[i]))
//                m_keyboardState.RealPressDown(touches[i]);
//            else
//                m_keyboardState.RealPressUp(touches[i]);
//        }
//    }

//    public abstract bool GetRealStateOf(KeyboardTouch touch);
//    public abstract bool GetRealStateOfCapsLock();
//    public abstract bool GetRealStateOfNumLock();
//    public abstract bool GetRealStateOfScrollLock();

//    public bool IsTouchActive(KeyboardTouch keyboardTouch)
//    {
//      return   m_keyboardState.IsTouchActive(keyboardTouch);
//    }

//    public bool IsTouchUp(KeyboardTouch keyboardTouch)
//    {
//        return m_keyboardState.IsTouchUp(keyboardTouch, m_timeDelta);
//    }
//    public bool IsTouchDown(KeyboardTouch keyboardTouch)
//    {
//        return m_keyboardState.IsTouchDown(keyboardTouch,m_timeDelta);
//    }
//    public bool IsTouchUp(KeyboardTouch keyboardTouch, double time )
//    {
//        return m_keyboardState.IsTouchUp(keyboardTouch, m_timeDelta);
//    }
//    public bool IsTouchDown(KeyboardTouch keyboardTouch, double time)
//    {
//        return m_keyboardState.IsTouchDown(keyboardTouch, m_timeDelta);
//    }
//    public bool IsControlActive()
//    {
//        return m_keyboardState.IsControlActive();
//    }

//    public bool IsShiftActive()
//    {
//        return m_keyboardState.IsShiftActive();
//    }

//    public bool IsMetaActive()
//    {
//        return m_keyboardState.IsMetaActive();
//    }

//    public bool IsAltActive()
//    {
//        return m_keyboardState.IsAltActive();
//    }

//    public bool IsAltGrActive()
//    {
//        return m_keyboardState.IsAltGrActive();
//    }

//    public bool IsCapsLockOn()
//    {
//        return m_keyboardState.IsCapsLockOn();
//    }

//    public bool IsNumLockOn()
//    {
//        return m_keyboardState.IsNumLockOn();
//    }

//    public bool IsScrollLockOn()
//    {
//        return m_keyboardState.IsScrollLockOn();
//    }

//    public LibraryTo GetRepresentedPlatform()
//    {
//        return m_keyboardState.GetRepresentedPlatform();
//    }

//    public KeyboardTouch[] GetActiveTouchs()
//    {
//        return m_keyboardState.GetActiveTouchs();
//    }
//}


