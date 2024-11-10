
public class StringToUser32PostMessageKeyEnum
{
    public static void Get(in string text, out bool found, out User32PostMessageKeyEnum message)
    {
        if (text.Length == 0)
        {
            found = false;
            message = User32PostMessageKeyEnum.VK_0;
            return;
        }
        if (text.Length == 1)
        {
            Get(text.ToLower()[0], out found, out message);
            if (found)
                return;
        }
        found = true;
        string t = text.ToUpper();
        switch (t)
        {
            case "NP0": message = User32PostMessageKeyEnum.VK_NUMPAD0; return;
            case "NP1": message = User32PostMessageKeyEnum.VK_NUMPAD1; return;
            case "NP2": message = User32PostMessageKeyEnum.VK_NUMPAD2; return;
            case "NP3": message = User32PostMessageKeyEnum.VK_NUMPAD3; return;
            case "NP4": message = User32PostMessageKeyEnum.VK_NUMPAD4; return;
            case "NP5": message = User32PostMessageKeyEnum.VK_NUMPAD5; return;
            case "NP6": message = User32PostMessageKeyEnum.VK_NUMPAD6; return;
            case "NP7": message = User32PostMessageKeyEnum.VK_NUMPAD7; return;
            case "NP8": message = User32PostMessageKeyEnum.VK_NUMPAD8; return;
            case "NP9": message = User32PostMessageKeyEnum.VK_NUMPAD9; return;
            case "F1": message = User32PostMessageKeyEnum.VK_F1; return;
            case "F2": message = User32PostMessageKeyEnum.VK_F2; return;
            case "F3": message = User32PostMessageKeyEnum.VK_F3; return;
            case "F4": message = User32PostMessageKeyEnum.VK_F4; return;
            case "F5": message = User32PostMessageKeyEnum.VK_F5; return;
            case "F6": message = User32PostMessageKeyEnum.VK_F6; return;
            case "F7": message = User32PostMessageKeyEnum.VK_F7; return;
            case "F8": message = User32PostMessageKeyEnum.VK_F8; return;
            case "F9": message = User32PostMessageKeyEnum.VK_F9; return;
            case "F10": message = User32PostMessageKeyEnum.VK_F10; return;
            case "F11": message = User32PostMessageKeyEnum.VK_F11; return;
            case "F12": message = User32PostMessageKeyEnum.VK_F12; return;

            //https://docs.microsoft.com/en-us/windows/win32/inputdev/virtual-key-codes
            case "SPACE": message = User32PostMessageKeyEnum.VK_SPACE; return;
            case "ESCAPE": message = User32PostMessageKeyEnum.VK_ESCAPE; return;
            case "LEFT": message = User32PostMessageKeyEnum.VK_LEFT; return;
            case "RIGHT": message = User32PostMessageKeyEnum.VK_RIGHT; return;
            case "UP": message = User32PostMessageKeyEnum.VK_UP; return;
            case "DOWN": message = User32PostMessageKeyEnum.VK_DOWN; return;
            case "CTRL": message = User32PostMessageKeyEnum.VK_LCONTROL; return;
            case "CONTROL": message = User32PostMessageKeyEnum.VK_LCONTROL; return;
            case "ALT": message = User32PostMessageKeyEnum.VK_LMENU; return;
            case "SHIFT": message = User32PostMessageKeyEnum.VK_SHIFT; return;
            case "TAB": message = User32PostMessageKeyEnum.VK_TAB; return;
            case "ENTER": message = User32PostMessageKeyEnum.VK_RETURN; return;
            case "BACKSPACE": message = User32PostMessageKeyEnum.VK_BACK; return;
            case "MENU": message = User32PostMessageKeyEnum.VK_MENU; return;
            case "CAPSLOCK": message = User32PostMessageKeyEnum.VK_CAPITAL; return;
            case "HOME": message = User32PostMessageKeyEnum.VK_HOME; return;
            case "END": message = User32PostMessageKeyEnum.VK_END; return;
            case "PAGEUP": message = User32PostMessageKeyEnum.VK_PRIOR; return;
            case "PAGEDOWN": message = User32PostMessageKeyEnum.VK_NEXT; return;
            case "INSERT": message = User32PostMessageKeyEnum.VK_INSERT; return;
            case "DELETE": message = User32PostMessageKeyEnum.VK_DELETE; return;
            case "NUMLOCK": message = User32PostMessageKeyEnum.VK_NUMLOCK; return;
            case "LeftClick": message = User32PostMessageKeyEnum.VK_LBUTTON; return;
            case "MidClick": message = User32PostMessageKeyEnum.VK_MBUTTON; return;
            case "MiddleClick": message = User32PostMessageKeyEnum.VK_MBUTTON; return;
            case "RightClick": message = User32PostMessageKeyEnum.VK_RBUTTON; return;


            case "F13": message = User32PostMessageKeyEnum.VK_F13; return;
            case "F14": message = User32PostMessageKeyEnum.VK_F14; return;
            case "F15": message = User32PostMessageKeyEnum.VK_F15; return;
            case "F16": message = User32PostMessageKeyEnum.VK_F16; return;
            case "F17": message = User32PostMessageKeyEnum.VK_F17; return;
            case "F18": message = User32PostMessageKeyEnum.VK_F18; return;
            case "F19": message = User32PostMessageKeyEnum.VK_F19; return;
            case "F20": message = User32PostMessageKeyEnum.VK_F20; return;
            case "F21": message = User32PostMessageKeyEnum.VK_F21; return;
            case "F22": message = User32PostMessageKeyEnum.VK_F22; return;
            case "F23": message = User32PostMessageKeyEnum.VK_F23; return;


            default:
                break;
        }
        message = User32PostMessageKeyEnum.VK_0;
        found = false;


    }
    public static void Get(char text, out bool found, out User32PostMessageKeyEnum message)
    {

        found = true;
        switch (text)
        {
            case '0': message = User32PostMessageKeyEnum.VK_0; return;
            case '1': message = User32PostMessageKeyEnum.VK_1; return;
            case '2': message = User32PostMessageKeyEnum.VK_2; return;
            case '3': message = User32PostMessageKeyEnum.VK_3; return;
            case '4': message = User32PostMessageKeyEnum.VK_4; return;
            case '5': message = User32PostMessageKeyEnum.VK_5; return;
            case '6': message = User32PostMessageKeyEnum.VK_6; return;
            case '7': message = User32PostMessageKeyEnum.VK_7; return;
            case '8': message = User32PostMessageKeyEnum.VK_8; return;
            case '9': message = User32PostMessageKeyEnum.VK_9; return;


            case 'a': message = User32PostMessageKeyEnum.VK_A; return;
            case 'b': message = User32PostMessageKeyEnum.VK_B; return;
            case 'c': message = User32PostMessageKeyEnum.VK_C; return;
            case 'd': message = User32PostMessageKeyEnum.VK_D; return;
            case 'e': message = User32PostMessageKeyEnum.VK_E; return;
            case 'f': message = User32PostMessageKeyEnum.VK_F; return;
            case 'g': message = User32PostMessageKeyEnum.VK_G; return;
            case 'h': message = User32PostMessageKeyEnum.VK_H; return;
            case 'i': message = User32PostMessageKeyEnum.VK_I; return;
            case 'j': message = User32PostMessageKeyEnum.VK_J; return;
            case 'k': message = User32PostMessageKeyEnum.VK_K; return;
            case 'l': message = User32PostMessageKeyEnum.VK_L; return;
            case 'm': message = User32PostMessageKeyEnum.VK_M; return;
            case 'n': message = User32PostMessageKeyEnum.VK_N; return;
            case 'o': message = User32PostMessageKeyEnum.VK_O; return;
            case 'p': message = User32PostMessageKeyEnum.VK_P; return;
            case 'q': message = User32PostMessageKeyEnum.VK_Q; return;
            case 'r': message = User32PostMessageKeyEnum.VK_R; return;
            case 's': message = User32PostMessageKeyEnum.VK_S; return;
            case 't': message = User32PostMessageKeyEnum.VK_T; return;
            case 'u': message = User32PostMessageKeyEnum.VK_U; return;
            case 'v': message = User32PostMessageKeyEnum.VK_V; return;
            case 'w': message = User32PostMessageKeyEnum.VK_W; return;
            case 'x': message = User32PostMessageKeyEnum.VK_X; return;
            case 'y': message = User32PostMessageKeyEnum.VK_Y; return;
            case 'z': message = User32PostMessageKeyEnum.VK_Z; return;

            case '*': message = User32PostMessageKeyEnum.VK_MULTIPLY; return;
            case '-': message = User32PostMessageKeyEnum.VK_SUBTRACT; return;
            case '/': message = User32PostMessageKeyEnum.VK_DIVIDE; return;
            case '+': message = User32PostMessageKeyEnum.VK_ADD; return;
            case '↑': message = User32PostMessageKeyEnum.VK_UP; return;
            case '↓': message = User32PostMessageKeyEnum.VK_DOWN; return;
            case '→': message = User32PostMessageKeyEnum.VK_RIGHT; return;
            case '←': message = User32PostMessageKeyEnum.VK_LEFT; return;

            default:
                break;
        }
        message = User32PostMessageKeyEnum.VK_0;
        found = false;
    }

}