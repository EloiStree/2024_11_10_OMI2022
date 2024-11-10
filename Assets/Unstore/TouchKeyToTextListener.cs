using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchKeyToTextListener : MonoBehaviour
{
    public TextListenerMono m_textlistener;
    //public KeyboardTouch m_startListening= KeyboardTouch.Enter;
    //public KeyboardTouch m_pushText= KeyboardTouch.RightControl;

    public void ReceivedKey(KeyboardTouch touch) {

        //if (m_startListening == touch)
        //    m_textlistener.SwitchTheListenerState();
        //if (m_pushText == touch)
        //    m_textlistener.PushAndClear();
        char c = ' ';
        bool found = false;
        switch (touch)
        {
            case KeyboardTouch.A:
                c = 'a'; found = true;
                break;
            case KeyboardTouch.B:
                c = 'b'; found = true;
                break;
            case KeyboardTouch.C:
                c = 'c'; found = true;
                break;
            case KeyboardTouch.D:
                c = 'd'; found = true;
                break;
            case KeyboardTouch.E:
                c = 'e'; found = true;
                break;
            case KeyboardTouch.F:
                c = 'f'; found = true;
                break;
            case KeyboardTouch.G:
                c = 'g'; found = true;
                break;
            case KeyboardTouch.H:
                c = 'h'; found = true;
                break;
            case KeyboardTouch.I:
                c = 'i'; found = true;
                break;
            case KeyboardTouch.J:
                c = 'j'; found = true;
                break;
            case KeyboardTouch.K:
                c = 'k'; found = true;
                break;
            case KeyboardTouch.L:
                c = 'l'; found = true;
                break;
            case KeyboardTouch.M:
                c = 'm'; found = true;
                break;
            case KeyboardTouch.N:
                c = 'n'; found = true;
                break;
            case KeyboardTouch.O:
                c = 'o'; found = true;
                break;
            case KeyboardTouch.P:
                c = 'p'; found = true;
                break;
            case KeyboardTouch.Q:
                c = 'q'; found = true;
                break;
            case KeyboardTouch.R:
                c = 'r'; found = true;
                break;
            case KeyboardTouch.S:
                c = 's'; found = true;
                break;
            case KeyboardTouch.T:
                c = 't'; found = true;
                break;
            case KeyboardTouch.U:
                c = 'u'; found = true;
                break;
            case KeyboardTouch.V:
                c = 'v'; found = true;
                break;
            case KeyboardTouch.W:
                c = 'w'; found = true;
                break;
            case KeyboardTouch.X:
                c = 'x'; found = true;
                break;
            case KeyboardTouch.Y:
                c = 'y'; found = true;
                break;
            case KeyboardTouch.Z:
                c = 'z'; found = true;
                break;
            case KeyboardTouch.NumPad0:
                c = '0'; found = true;
                break;
            case KeyboardTouch.NumPad1:
                c = '1'; found = true;
                break;
            case KeyboardTouch.NumPad2:
                c = '2'; found = true;
                break;
            case KeyboardTouch.NumPad3:
                c = '3'; found = true;
                break;
            case KeyboardTouch.NumPad4:
                c = '4'; found = true;
                break;
            case KeyboardTouch.NumPad5:
                c = '5'; found = true;
                break;
            case KeyboardTouch.NumPad6:
                c = '6'; found = true;
                break;
            case KeyboardTouch.NumPad7:
                c = '7'; found = true;
                break;
            case KeyboardTouch.NumPad8:
                c = '8'; found = true;
                break;
            case KeyboardTouch.NumPad9:
                c = '9'; found = true;
                break;
            case KeyboardTouch.NumPadDecimal:
                c = '.'; found = true;
                break;
            case KeyboardTouch.NumPadDivide:
                c = '/'; found = true;
                break;
            case KeyboardTouch.NumPadMultiply:
                c = '*'; found = true;
                break;
            case KeyboardTouch.NumPadSubstract:
                c = '-'; found = true;
                break;
            case KeyboardTouch.NumPadAdd:
                c = '+'; found = true;
                break;
            case KeyboardTouch.NumPadPeriod:
                c = '.'; found = true;
                break;
            case KeyboardTouch.NumPadEquals:
                c = '='; found = true;
                break;
           
            case KeyboardTouch.MSub: 
                c = '-'; found = true;
                break;
            case KeyboardTouch.Alpha0:
                c = '0'; found = true;
                break;
            case KeyboardTouch.Alpha1:
                c = '1'; found = true;
                break;
            case KeyboardTouch.Alpha2:
                c = '2'; found = true;
                break;
            case KeyboardTouch.Alpha3:
                c = '3'; found = true;
                break;
            case KeyboardTouch.Alpha4:
                c = '4'; found = true;
                break;
            case KeyboardTouch.Alpha5:
                c = '5'; found = true;
                break;
            case KeyboardTouch.Alpha6:
                c = '6'; found = true;
                break;
            case KeyboardTouch.Alpha7:
                c = '7'; found = true;
                break;
            case KeyboardTouch.Alpha8:
                c = '8'; found = true;
                break;
            case KeyboardTouch.Alpha9:
                c = '9'; found = true;
                break;
     
            case KeyboardTouch.Space:
                c = ' '; found = true;
                break;
           
           
            default:
                break;
        }
        if(found)
            m_textlistener.Append(c);
    }
}
