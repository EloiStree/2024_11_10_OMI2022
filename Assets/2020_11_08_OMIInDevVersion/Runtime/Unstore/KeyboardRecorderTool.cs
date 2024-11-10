using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WindowsInput.Native;

public class KeyboardRecorderTool : MonoBehaviour
{
    public TimeThreadMono m_time;
    public ThreadWindowSimListener m_keyListener;


    public TimedCommandLines m_lastRecord = new TimedCommandLines();
    public List<KeyToDatetime> m_recFound= new List<KeyToDatetime>();
    public bool m_isRecording;

    void Start()
    {

        
        TimeThreadMono.LoopPing loop = new TimeThreadMono.LoopPing(20, PingThreadType.InTimeThread);
        loop.SetCallBack(CheckForChange);
        m_time.SubscribeLoop(loop);
    }
    public List<KeyboardTouch> m_previous = new List<KeyboardTouch>();
    public List<KeyboardTouch> m_current = new List<KeyboardTouch>();
    //public List<KeyboardTouch> newPressKey = new List<KeyboardTouch>();
    //public List<KeyboardTouch> newReleaseKey = new List<KeyboardTouch>();
    public KeyboardTouch[] m_dontCount= new KeyboardTouch[] { KeyboardTouch.Shift, KeyboardTouch.Alt, KeyboardTouch.Menu, KeyboardTouch.Control};

    public bool m_usePauseDebug;
    private void Update()
    {
        if (m_usePauseDebug) { 
            if (Input.GetKeyDown(KeyCode.Pause))
            {
                StartRecording();
            }
            if (Input.GetKeyUp(KeyCode.Pause))
            {
                StopRecording();
            }
        }
    }

    internal void PauseRecording()
    {
        m_isRecording = false;
    }


    private void CheckForChange()
    {

        try
        {



            m_current = m_keyListener.GetTouchActive();

            for (int i = 0; i < m_dontCount.Length; i++)
            {
                m_current.Remove(m_dontCount[i]);
            }

            for (int i = 0; i < m_current.Count; i++)
            {
                if (!m_previous.Contains(m_current[i]))
                {
                    //  newPressKey.Add(m_current[i]);
                    // Debug.Log("P:" + m_current[i]);

                    if (m_isRecording)
                    {
                        m_recFound.Add(new KeyToDatetime(true, m_current[i]));
                    }

                }
            }
            for (int i = 0; i < m_previous.Count; i++)
            {
                if (!m_current.Contains(m_previous[i]))
                {
                    //newReleaseKey.Add(m_previous[i]);
                    // Debug.Log("R:" + m_previous[i]);
                    if (m_isRecording)
                    {
                        m_recFound.Add(new KeyToDatetime(false, m_previous[i]));
                    }
                }
            }

            m_previous = m_current.ToList();
        }
        catch (Exception e)
        {
            ToCorrectLaterExeception.Ping();
        }
    }

    public void StartRecording()
    {
        m_recFound.Clear();
        m_isRecording = true;
    }
    public void ResumeRecording()
    {
        m_isRecording = true;
    }
    public void StopRecording()
    {
        m_lastRecord= CreateMacro(m_recFound); 
        m_isRecording = false;
    }
    public TimedCommandLines GetCurrent() {
        return CreateMacro(m_recFound);
    }

    private TimedCommandLines CreateMacro(List<KeyToDatetime> recFound)
    {
        if (recFound.Count == 0)
            return new TimedCommandLines();

        DateTime start = recFound[0].m_when;
        TimedCommandLines macro  = new TimedCommandLines();
        for (int i = 0; i < recFound.Count; i++)
        {
            ulong ms =(ulong) (recFound[i].m_when - start).TotalMilliseconds;
            string cmd = GetRawCommandFor(recFound[i]);
            if(cmd.Trim().Length>0)
                macro.AddKey(ms,cmd);
        }

        return macro;
    }

    private string GetRawCommandFor(KeyToDatetime recFound)
    {
        return GetRawCommandFor(recFound.m_isPress, recFound.m_key);
    }
        string format = "k{0}:{1}";
    private string GetRawCommandFor(bool isPressed, KeyboardTouch key)
    {
        bool found;
        JavaOpenMacroInput.JavaKeyEvent keyFound;
        GetJavaKey(key, out found, out keyFound);
        if(found)
        return string.Format(format, isPressed?'p':'r', keyFound.ToString() );
        return "";
    }

    private void GetJavaKey(KeyboardTouch key,out bool found, out JavaOpenMacroInput.JavaKeyEvent javaKey)
    {
        javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_NONCONVERT;

        switch (key)
        {
            case KeyboardTouch.LeftAlt:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_ALT;
                break;
            case KeyboardTouch.RightAlt:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_ALT_GRAPH;
                break;
            case KeyboardTouch.A:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_A;
                break;
            case KeyboardTouch.Return:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_ENTER;
                break;
            case KeyboardTouch.B:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_B;
                break;
            case KeyboardTouch.C:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_C;
                break;
            case KeyboardTouch.D:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_D;
                break;
            case KeyboardTouch.E:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_E;
                break;
            case KeyboardTouch.F:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_F;
                break;
            case KeyboardTouch.G:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_G;
                break;
            case KeyboardTouch.H:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_H;
                break;
            case KeyboardTouch.I:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_I;
                break;
            case KeyboardTouch.J:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_J;
                break;
            case KeyboardTouch.K:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_K;
                break;
            case KeyboardTouch.L:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_L;
                break;
            case KeyboardTouch.M:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_M;
                break;
            case KeyboardTouch.N:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_N;
                break;
            case KeyboardTouch.O:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_O;
                break;
            case KeyboardTouch.P:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_P;
                break;
            case KeyboardTouch.Q:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_Q;
                break;
            case KeyboardTouch.R:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_R;
                break;
            case KeyboardTouch.S:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_S;
                break;
            case KeyboardTouch.T:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_T;
                break;
            case KeyboardTouch.U:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_U;
                break;
            case KeyboardTouch.V:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_V;
                break;
            case KeyboardTouch.W:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_W;
                break;
            case KeyboardTouch.X:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_X;
                break;
            case KeyboardTouch.Y:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_Y;
                break;
            case KeyboardTouch.Z:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_Z;
                break;
            case KeyboardTouch.NumLock:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_NUM_LOCK;
                break;
            case KeyboardTouch.NumPad0:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_NUMPAD0;
                break;
            case KeyboardTouch.NumPad1:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_NUMPAD1;
                break;
            case KeyboardTouch.NumPad2:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_NUMPAD2;
                break;
            case KeyboardTouch.NumPad3:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_NUMPAD3;
                break;
            case KeyboardTouch.NumPad4:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_NUMPAD4;
                break;
            case KeyboardTouch.NumPad5:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_NUMPAD5;
                break;
            case KeyboardTouch.NumPad6:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_NUMPAD6;
                break;
            case KeyboardTouch.NumPad7:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_NUMPAD7;
                break;
            case KeyboardTouch.NumPad8:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_NUMPAD8;
                break;
            case KeyboardTouch.NumPad9:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_NUMPAD9;
                break;
            case KeyboardTouch.NumPadDecimal:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_DECIMAL;
                break;
            case KeyboardTouch.NumPadDivide:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_DIVIDE;
                break;
            case KeyboardTouch.NumPadMultiply:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_MULTIPLY;
                break;
            case KeyboardTouch.NumPadSubstract:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_SUBTRACT;
                break;
            case KeyboardTouch.NumPadAdd:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_ADD;
                break;
            case KeyboardTouch.NumPadEnter:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_ENTER;
                break;
            case KeyboardTouch.NumPadPeriod:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_PERIOD;
                break;
            case KeyboardTouch.NumPadEquals:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_EQUALS;
                break;
            case KeyboardTouch.NumPadClear:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_CLEAR;
                break;
            case KeyboardTouch.NumPadClearEntry:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_ENTER;
                break;
            case KeyboardTouch.ClearEntry:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_CLEAR;
                break;
            case KeyboardTouch.Alpha0:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_0;
                break;
            case KeyboardTouch.Alpha1:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_1;
                break;
            case KeyboardTouch.Alpha2:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_2;
                break;
            case KeyboardTouch.Alpha3:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_3;
                break;
            case KeyboardTouch.Alpha4:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_4;
                break;
            case KeyboardTouch.Alpha5:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_5;
                break;
            case KeyboardTouch.Alpha6:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_6;
                break;
            case KeyboardTouch.Alpha7:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_7;
                break;
            case KeyboardTouch.Alpha8:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_8;
                break;
            case KeyboardTouch.Alpha9:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_9;
                break;
            case KeyboardTouch.Up:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_UP;
                break;
            case KeyboardTouch.Enter:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_ENTER;
                break;
            case KeyboardTouch.Down:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_DOWN;
                break;
            case KeyboardTouch.Right:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_RIGHT;
                break;
            case KeyboardTouch.Left:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_LEFT;
                break;
            case KeyboardTouch.PageUp:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_PAGE_UP;
                break;
            case KeyboardTouch.PageDown:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_PAGE_DOWN;
                break;
            case KeyboardTouch.Home:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_HOME;
                break;
            case KeyboardTouch.End:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_END;
                break;
            case KeyboardTouch.Insert:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_INSERT;
                break;
            case KeyboardTouch.Delete:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_DELETE;
                break;
            case KeyboardTouch.F1:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_F1;
                break;
            case KeyboardTouch.F2:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_F2;
                break;
            case KeyboardTouch.F3:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_F3;
                break;
            case KeyboardTouch.F4:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_F4;
                break;
            case KeyboardTouch.F5:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_F5;
                break;
            case KeyboardTouch.F6:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_F6;
                break;
            case KeyboardTouch.F7:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_F7;
                break;
            case KeyboardTouch.F8:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_F8;
                break;
            case KeyboardTouch.F9:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_F9;
                break;
            case KeyboardTouch.F10:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_F10;
                break;
            case KeyboardTouch.F11:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_F11;
                break;
            case KeyboardTouch.F12:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_F12;
                break;
            case KeyboardTouch.F13:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_F13;
                break;
            case KeyboardTouch.F14:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_F14;
                break;
            case KeyboardTouch.F15:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_F15;
                break;
            case KeyboardTouch.F16:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_F16;
                break;
            case KeyboardTouch.F17:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_F17;
                break;
            case KeyboardTouch.F18:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_F18;
                break;
            case KeyboardTouch.F19:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_F19;
                break;
            case KeyboardTouch.F20:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_F20;
                break;
            case KeyboardTouch.F21:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_F21;
                break;
            case KeyboardTouch.F22:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_F22;
                break;
            case KeyboardTouch.F23:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_F23;
                break;
            case KeyboardTouch.F24:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_F24;
                break;
            case KeyboardTouch.WinUS_OEM_Comma:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_COMMA;
                break;
            case KeyboardTouch.WinUS_OEM_Period:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_PERIOD;
                break;
            case KeyboardTouch.WinUS_OEM_102_BackSlash:

                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_BACK_SLASH;
                break;
            case KeyboardTouch.WinUS_OEM_1_SemiColon:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_SEMICOLON;
                break;
            case KeyboardTouch.WinUS_OEM_2_Slash:

                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_SLASH;
                break;
            case KeyboardTouch.WinUS_OEM_4_LeftBracket:

                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_OPEN_BRACKET;
                break;
            case KeyboardTouch.WinUS_OEM_5_BackSlash:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_BACK_SLASH;
                break;
            case KeyboardTouch.WinUS_OEM_6_RightBlacket:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_CLOSE_BRACKET;
                break;
            case KeyboardTouch.WinUS_OEM_7_Quote:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_QUOTE;
                break;
            case KeyboardTouch.Space:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_SPACE;
                break;
            case KeyboardTouch.CapsLock:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_CAPS_LOCK;
                break;
            case KeyboardTouch.Backspace:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_BACK_SPACE;
                break;
            case KeyboardTouch.Tab:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_TAB;
                break;
            case KeyboardTouch.LeftShift:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_SHIFT;
                break;
            case KeyboardTouch.RightShift:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_SHIFT;
                break;
            case KeyboardTouch.LeftControl:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_CONTROL;
                break;
            case KeyboardTouch.RightControl:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_CONTROL;
                break;
           
            case KeyboardTouch.ContextMenu:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_CONTEXT_MENU;
                break;
            case KeyboardTouch.Break:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_PAUSE;
                break;
            case KeyboardTouch.ScrollLock:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_SCROLL_LOCK;
                break;
            case KeyboardTouch.PrintScreen:
                javaKey= JavaOpenMacroInput.JavaKeyEvent.VK_PRINTSCREEN;
                break;
            case KeyboardTouch.Help:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_HELP;
                break;
            case KeyboardTouch.EndOfText:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_END;
                break;
            case KeyboardTouch.Escape:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_ESCAPE;
                break;

            
            
            case KeyboardTouch.MetaLeft:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_META;
                break;
            case KeyboardTouch.MetaRight:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_META;
                break;
            case KeyboardTouch.LeftWindow:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_WINDOWS;
                break;
            case KeyboardTouch.RightWindow:
                javaKey = JavaOpenMacroInput.JavaKeyEvent.VK_WINDOWS;
                break;
            
            default:
                break;

        }
        found = javaKey != JavaOpenMacroInput.JavaKeyEvent.VK_NONCONVERT;
    }
}

/*
 
     case KeyboardTouch.MouseLeft:
            case KeyboardTouch.MouseRight:
            case KeyboardTouch.MouseMiddle:
            case KeyboardTouch.MouseSupp1:
            case KeyboardTouch.MouseSupp2:
            case KeyboardTouch.MouseSupp3:
                break;           
                case KeyboardTouch.LeftCommand:
            case KeyboardTouch.RightCommand:

            
     */

[System.Serializable]
public class KeyToDatetime
{

    public DateTime m_when;
    public KeyboardTouch m_key;
    public bool m_isPress;
    public KeyToDatetime(bool isPress , KeyboardTouch virtualKeyCode)
    {
        m_isPress = isPress;
        m_when = DateTime.Now;
        m_key = virtualKeyCode;
    }
}

public class ToCorrectLaterExeception : Exception
{
    internal static void Ping()
    {
        Debug.LogWarning("Hum, exception is known and need to be corrected asap");
    }
}