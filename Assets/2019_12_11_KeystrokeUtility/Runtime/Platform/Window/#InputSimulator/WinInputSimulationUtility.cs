using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using WindowsInput.Native;

public class WinInputSimulationUtility : MonoBehaviour {

    public static VirtualKeyCode GetInputSimKeyCodeFromAlphaNum(char c, out bool isUpperCase)
    {

        isUpperCase = false;

        switch (c)
        {
            case 'A':
            case 'B':
            case 'C':
            case 'D':
            case 'E':
            case 'F':
            case 'G':
            case 'H':
            case 'I':
            case 'J':
            case 'K':
            case 'L':
            case 'M':
            case 'N':
            case 'O':
            case 'P':
            case 'Q':
            case 'R':
            case 'S':
            case 'T':
            case 'U':
            case 'V':
            case 'X':
            case 'Y':
            case 'Z':
            case 'W':
                isUpperCase = true;
                break;

        }


        switch (c)
        {
            case 'a':
            case 'A':
                return VirtualKeyCode.VK_A;

            case 'b':
            case 'B':
                return VirtualKeyCode.VK_B;

            case 'c':
            case 'C':
                return VirtualKeyCode.VK_C;


            case 'd':
            case 'D':
                return VirtualKeyCode.VK_D;


            case 'e':
            case 'E':
                return VirtualKeyCode.VK_E;


            case 'f':
            case 'F':
                return VirtualKeyCode.VK_F;


            case 'g':
            case 'G':
                return VirtualKeyCode.VK_G;


            case 'h':
            case 'H':
                return VirtualKeyCode.VK_H;


            case 'i':
            case 'I':
                return VirtualKeyCode.VK_I;


            case 'j':
            case 'J':
                return VirtualKeyCode.VK_J;


            case 'k':
            case 'K':
                return VirtualKeyCode.VK_K;


            case 'l':
            case 'L':
                return VirtualKeyCode.VK_L;


            case 'm':
            case 'M':
                return VirtualKeyCode.VK_M;


            case 'n':
            case 'N':
                return VirtualKeyCode.VK_N;


            case 'o':
            case 'O':
                return VirtualKeyCode.VK_O;


            case 'p':
            case 'P':
                return VirtualKeyCode.VK_P;


            case 'q':
            case 'Q':
                return VirtualKeyCode.VK_Q;


            case 'r':
            case 'R':
                return VirtualKeyCode.VK_R;


            case 's':
            case 'S':
                return VirtualKeyCode.VK_S;


            case 't':
            case 'T':
                return VirtualKeyCode.VK_T;


            case 'u':
            case 'U':
                return VirtualKeyCode.VK_U;


            case 'v':
            case 'V':
                return VirtualKeyCode.VK_V;


            case 'x':
            case 'X':
                return VirtualKeyCode.VK_X;


            case 'y':
            case 'Y':
                return VirtualKeyCode.VK_Y;


            case 'z':
            case 'Z':
                return VirtualKeyCode.VK_Z;


            case 'w':
            case 'W':
                return VirtualKeyCode.VK_W;

            case '0':
                return VirtualKeyCode.NUMPAD0;
            case '1':
                return VirtualKeyCode.NUMPAD1;
            case '2':
                return VirtualKeyCode.NUMPAD2;
            case '3':
                return VirtualKeyCode.NUMPAD3;
            case '4':
                return VirtualKeyCode.NUMPAD4;
            case '5':
                return VirtualKeyCode.NUMPAD5;
            case '6':
                return VirtualKeyCode.NUMPAD6;
            case '7':
                return VirtualKeyCode.NUMPAD7;
            case '8':
                return VirtualKeyCode.NUMPAD8;
            case '9':
                return VirtualKeyCode.NUMPAD9;
            case ' ':
                return VirtualKeyCode.SPACE;
            default:
                break;
        }

        return VirtualKeyCode.SPACE;

    }

    private static bool IsAlphaNumeric(string input)
    {
        return Regex.IsMatch(input, @"^[a-zA-Z0-9]+$");
    }
    //public static List<ActionVirtualLetter> GetActionVirtualLettersFrom(char character)
    //{
    //    List<ActionVirtualLetter> letters = new List<ActionVirtualLetter>();
    //    VirtualKeyCode keyCode = VirtualKeyCode.SPACE;
    //    //Debug.Log("C: " + character + " is "+ IsAlphaNumeric("" + character));

    //    if (IsAlphaNumeric("" + character))
    //    {
    //        bool isUpperCase = false;
    //        keyCode = GetInputSimKeyCodeFromAlphaNum(character, out isUpperCase);

    //        if (isUpperCase)
    //            letters.Add(new ActionVirtualLetter(VirtualKeyCode.LSHIFT, MechnicalActionType.Down));

    //        letters.Add(new ActionVirtualLetter(keyCode, MechnicalActionType.Down));
    //        letters.Add(new ActionVirtualLetter(keyCode, MechnicalActionType.Up));
    //        if (isUpperCase)
    //            letters.Add(new ActionVirtualLetter(VirtualKeyCode.LSHIFT, MechnicalActionType.Up));
    //    }

    //    //TODO all other character


    //    return letters;
    //}

    public static VirtualKeyCode GetInputSimKeyCodeFrom(KeyCode m_unityKeyCode)
    {

        VirtualKeyCode keyCode = VirtualKeyCode.NONCONVERT;
        switch (m_unityKeyCode)
        {
            case KeyCode.Backspace:
                keyCode = VirtualKeyCode.BACK;
                break;
            case KeyCode.Delete:
                keyCode = VirtualKeyCode.DELETE;
                break;
            case KeyCode.Tab:
                keyCode = VirtualKeyCode.TAB;
                break;
            case KeyCode.Clear:
                keyCode = VirtualKeyCode.CLEAR;
                break;
            case KeyCode.Return:
                keyCode = VirtualKeyCode.RETURN;
                break;
            case KeyCode.Pause:
                keyCode = VirtualKeyCode.PAUSE;
                break;
            case KeyCode.Escape:
                keyCode = VirtualKeyCode.ESCAPE;
                break;
            case KeyCode.Space:
                keyCode = VirtualKeyCode.SPACE;
                break;
            case KeyCode.Keypad0:
                keyCode = VirtualKeyCode.NUMPAD0;
                break;
            case KeyCode.Keypad1:
                keyCode = VirtualKeyCode.NUMPAD1;
                break;
            case KeyCode.Keypad2:
                keyCode = VirtualKeyCode.NUMPAD2;
                break;
            case KeyCode.Keypad3:
                keyCode = VirtualKeyCode.NUMPAD3;
                break;
            case KeyCode.Keypad4:
                keyCode = VirtualKeyCode.NUMPAD4;
                break;
            case KeyCode.Keypad5:
                keyCode = VirtualKeyCode.NUMPAD5;
                break;
            case KeyCode.Keypad6:
                keyCode = VirtualKeyCode.NUMPAD6;
                break;
            case KeyCode.Keypad7:
                keyCode = VirtualKeyCode.NUMPAD7;
                break;
            case KeyCode.Keypad8:
                keyCode = VirtualKeyCode.NUMPAD8;
                break;
            case KeyCode.Keypad9:
                keyCode = VirtualKeyCode.NUMPAD9;
                break;
            case KeyCode.KeypadPeriod:
                keyCode = VirtualKeyCode.OEM_PERIOD;
                break;
            case KeyCode.KeypadDivide:
                keyCode = VirtualKeyCode.DIVIDE;
                break;
            case KeyCode.KeypadMultiply:
                keyCode = VirtualKeyCode.MULTIPLY;
                break;
            case KeyCode.KeypadMinus:
                keyCode = VirtualKeyCode.OEM_MINUS;
                break;
            case KeyCode.KeypadPlus:
                keyCode = VirtualKeyCode.OEM_PLUS;
                break;
            case KeyCode.KeypadEnter:
                keyCode = VirtualKeyCode.RETURN;
                break;
            case KeyCode.UpArrow:
                keyCode = VirtualKeyCode.UP;
                break;
            case KeyCode.DownArrow:
                keyCode = VirtualKeyCode.DOWN;
                break;
            case KeyCode.RightArrow:
                keyCode = VirtualKeyCode.RIGHT;
                break;
            case KeyCode.LeftArrow:
                keyCode = VirtualKeyCode.LEFT;
                break;
            case KeyCode.Insert:
                keyCode = VirtualKeyCode.INSERT;
                break;
            case KeyCode.Home:
                keyCode = VirtualKeyCode.HOME;
                break;
            case KeyCode.End:
                keyCode = VirtualKeyCode.END;
                break;
            case KeyCode.F1:
                keyCode = VirtualKeyCode.F1;
                break;
            case KeyCode.F2:
                keyCode = VirtualKeyCode.F2;
                break;
            case KeyCode.F3:
                keyCode = VirtualKeyCode.F3;
                break;
            case KeyCode.F4:
                keyCode = VirtualKeyCode.F4;
                break;
            case KeyCode.F5:
                keyCode = VirtualKeyCode.F5;
                break;
            case KeyCode.F6:
                keyCode = VirtualKeyCode.F6;
                break;
            case KeyCode.F7:
                keyCode = VirtualKeyCode.F7;
                break;
            case KeyCode.F8:
                keyCode = VirtualKeyCode.F8;
                break;
            case KeyCode.F9:
                keyCode = VirtualKeyCode.F9;
                break;
            case KeyCode.F10:
                keyCode = VirtualKeyCode.F10;
                break;
            case KeyCode.F11:
                keyCode = VirtualKeyCode.F11;
                break;
            case KeyCode.F12:
                keyCode = VirtualKeyCode.F12;
                break;
            case KeyCode.F13:
                keyCode = VirtualKeyCode.F13;
                break;
            case KeyCode.F14:
                keyCode = VirtualKeyCode.F14;
                break;
            case KeyCode.F15:
                keyCode = VirtualKeyCode.F15;
                break;
            case KeyCode.Alpha0:
                keyCode = VirtualKeyCode.VK_0;
                break;
            case KeyCode.Alpha1:
                keyCode = VirtualKeyCode.VK_1;
                break;
            case KeyCode.Alpha2:
                keyCode = VirtualKeyCode.VK_2;
                break;
            case KeyCode.Alpha3:
                keyCode = VirtualKeyCode.VK_3;
                break;
            case KeyCode.Alpha4:
                keyCode = VirtualKeyCode.VK_4;
                break;
            case KeyCode.Alpha5:
                keyCode = VirtualKeyCode.VK_5;
                break;
            case KeyCode.Alpha6:
                keyCode = VirtualKeyCode.VK_6;
                break;
            case KeyCode.Alpha7:
                keyCode = VirtualKeyCode.VK_7;
                break;
            case KeyCode.Alpha8:
                keyCode = VirtualKeyCode.VK_8;
                break;
            case KeyCode.Alpha9:
                keyCode = VirtualKeyCode.VK_9;
                break;
            case KeyCode.Plus:
                keyCode = VirtualKeyCode.OEM_PLUS;
                break;
            case KeyCode.Comma:
                keyCode = VirtualKeyCode.OEM_COMMA;
                break;
            case KeyCode.Minus:
                keyCode = VirtualKeyCode.OEM_MINUS;
                break;
            case KeyCode.Period:
                keyCode = VirtualKeyCode.OEM_PERIOD;

                break;
            case KeyCode.Slash:
                keyCode = VirtualKeyCode.OEM_102;
                break;

            case KeyCode.A:
                keyCode = VirtualKeyCode.VK_A;
                break;
            case KeyCode.B:
                keyCode = VirtualKeyCode.VK_B;
                break;
            case KeyCode.C:
                keyCode = VirtualKeyCode.VK_C;
                break;
            case KeyCode.D:
                keyCode = VirtualKeyCode.VK_D;
                break;
            case KeyCode.E:
                keyCode = VirtualKeyCode.VK_E;
                break;
            case KeyCode.F:
                keyCode = VirtualKeyCode.VK_F;
                break;
            case KeyCode.G:
                keyCode = VirtualKeyCode.VK_G;
                break;
            case KeyCode.H:
                keyCode = VirtualKeyCode.VK_H;
                break;
            case KeyCode.I:
                keyCode = VirtualKeyCode.VK_I;
                break;
            case KeyCode.J:
                keyCode = VirtualKeyCode.VK_J;
                break;
            case KeyCode.K:
                keyCode = VirtualKeyCode.VK_K;
                break;
            case KeyCode.L:
                keyCode = VirtualKeyCode.VK_L;
                break;
            case KeyCode.M:
                keyCode = VirtualKeyCode.VK_M;
                break;
            case KeyCode.N:
                keyCode = VirtualKeyCode.VK_N;
                break;
            case KeyCode.O:
                keyCode = VirtualKeyCode.VK_O;
                break;
            case KeyCode.P:
                keyCode = VirtualKeyCode.VK_P;
                break;
            case KeyCode.Q:
                keyCode = VirtualKeyCode.VK_Q;
                break;
            case KeyCode.R:
                keyCode = VirtualKeyCode.VK_R;
                break;
            case KeyCode.S:
                keyCode = VirtualKeyCode.VK_S;
                break;
            case KeyCode.T:
                keyCode = VirtualKeyCode.VK_T;
                break;
            case KeyCode.U:
                keyCode = VirtualKeyCode.VK_U;
                break;
            case KeyCode.V:
                keyCode = VirtualKeyCode.VK_V;
                break;
            case KeyCode.W:
                keyCode = VirtualKeyCode.VK_W;
                break;
            case KeyCode.X:
                keyCode = VirtualKeyCode.VK_X;
                break;
            case KeyCode.Y:
                keyCode = VirtualKeyCode.VK_Y;
                break;
            case KeyCode.Z:
                keyCode = VirtualKeyCode.VK_Z;
                break;
            case KeyCode.Numlock:
                keyCode = VirtualKeyCode.NUMLOCK;
                break;
            case KeyCode.ScrollLock:
                keyCode = VirtualKeyCode.SCROLL;
                break;
            case KeyCode.RightShift:
                keyCode = VirtualKeyCode.RSHIFT;
                break;
            case KeyCode.LeftShift:
                keyCode = VirtualKeyCode.LSHIFT;
                break;
            case KeyCode.RightControl:
                keyCode = VirtualKeyCode.RCONTROL;
                break;
            case KeyCode.LeftControl:
                keyCode = VirtualKeyCode.LCONTROL;
                break;
            case KeyCode.RightAlt:

                keyCode = VirtualKeyCode.MENU;
                break;
            case KeyCode.LeftAlt:
                keyCode = VirtualKeyCode.MENU;
                break;
            case KeyCode.LeftWindows:
                keyCode = VirtualKeyCode.LWIN;
                break;

            case KeyCode.RightWindows:
                keyCode = VirtualKeyCode.RWIN;
                break;

            case KeyCode.Help:
                keyCode = VirtualKeyCode.HELP;

                break;
            case KeyCode.Print:
                keyCode = VirtualKeyCode.PRINT;

                break;
            case KeyCode.Break:
                keyCode = VirtualKeyCode.CANCEL;
                break;
            case KeyCode.Menu:
                keyCode = VirtualKeyCode.MENU;
                break;
            case KeyCode.Asterisk:
                keyCode = VirtualKeyCode.MULTIPLY;
                break;
            case KeyCode.CapsLock:
                keyCode = VirtualKeyCode.CAPITAL;
                break;



            case KeyCode.SysReq:
            case KeyCode.LeftCommand:
            case KeyCode.RightCommand:
            case KeyCode.AltGr:
            case KeyCode.Colon:
            case KeyCode.Semicolon:
            case KeyCode.Less:
            case KeyCode.Equals:
            case KeyCode.Greater:
            case KeyCode.Question:
            case KeyCode.At:
            case KeyCode.LeftBracket:
            case KeyCode.Backslash:
            case KeyCode.RightBracket:
            case KeyCode.Caret:
            case KeyCode.Underscore:
            case KeyCode.BackQuote:
            case KeyCode.Exclaim:
            case KeyCode.PageUp:
            case KeyCode.KeypadEquals:
            case KeyCode.PageDown:
            case KeyCode.DoubleQuote:
            case KeyCode.Hash:
            case KeyCode.Dollar:
            case KeyCode.Ampersand:
            case KeyCode.Quote:
            case KeyCode.LeftParen:
            case KeyCode.RightParen:
            default:

                Debug.LogWarning("Don't know what touch it is");
                keyCode = VirtualKeyCode.SPACE;

                break;
        }
        return keyCode;
    }
}
