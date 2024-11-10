using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using WindowsInput.Native;

public class KeyBindingTable : MonoBehaviour
{

    [System.Serializable]
    public class OnKeyEvent : UnityEvent<KeyboardTouch> { }


    /// ASCII INFO:
    /// https://theasciicode.com.ar/extended-ascii-code/registered-trademark-symbol-ascii-code-169.html

    public class InfoASCII
    {
        public string m_charDescription = "NULL";
        public int m_code = 0;
        public char m_character = ' ';
        public string m_name = "NULL";

        public InfoASCII(string description, int code, char character, string name = null)
        {
            m_charDescription = description;
            m_code = code;
            m_character = character;
            if (name != null)
                m_name = name;
        }
    }
    public static InfoASCII[] ASCII = new InfoASCII[]{
 new InfoASCII("NULL",00,' ', "Null" ),
 new InfoASCII("SOH",01, '☺', "Start of Heading"),
 new InfoASCII("STX",02, '☻', "Start of Text" ),
 new InfoASCII("ETX",03, '♥', "End of Text"),
 new InfoASCII("EOT",04, '♦',"End of Transmission"),
 new InfoASCII("ENQ",05, '♣',"Equiry" ),
 new InfoASCII("ACK",06, '♣',"Acknowledgement" ),
 new InfoASCII("BEL",07, '•',"Bell" ),
 new InfoASCII("BS",08 , '◘',"Backspace"),
 new InfoASCII("HT",09 , '○',"Horizontal Tab"),
 new InfoASCII("LF",10 , '◙',"Line Feed"),
 new InfoASCII("VT",11 , '♂',"Vertical Tab"),
 new InfoASCII("FF",12 , '♀',"Form Feed"),
 new InfoASCII("CR",13 , '♪',"Carriage Return"),
 new InfoASCII("SO",14 , '♫',"Shift Out"),
 new InfoASCII("SI",15 , '☼',"Shift In"),
 new InfoASCII("DLE",16, '►',"Data Link Escape" ),
 new InfoASCII("DC1",17, '◄',"Device Control 1" ),
 new InfoASCII("DC2",18, '↕',"Device Control 2" ),
 new InfoASCII("DC3",19, '‼',"Device Control 3" ),
 new InfoASCII("DC4",20, '¶',"Device Control 4" ),
 new InfoASCII("NAK",21, '§',"Negative Acknowledgement" ),
 new InfoASCII("SYN",22, '▬',"Synchronous Idle" ),
 new InfoASCII("ETB",23, '↨',"End of Transmission Block" ),
 new InfoASCII("CAN",24, '↑',"Cancel" ),
 new InfoASCII("EM" ,25, '↓',"End of Medium" ),
 new InfoASCII("SUB",26, '→',"Subtitute" ),
 new InfoASCII("ESC",27, '←',"Escape" ),
 new InfoASCII("FS" ,28, '∟',"File Separator" ),
 new InfoASCII("GS" ,29, '↔',"Group Separator" ),
 new InfoASCII("RS" ,30, '▲',"Record Separator" ),
 new InfoASCII("US" ,31, '▼',"Unity Separator" ),
 new InfoASCII("space", 32, ' ',"Space" ),
 new InfoASCII("!"  , 33, '!' ),
 new InfoASCII("\"" , 34, '\\' ),
 new InfoASCII("#"  , 35, '#' ),
 new InfoASCII("$"  , 36, '$' ),
 new InfoASCII("%"  , 37, '%' ),
 new InfoASCII("&"  , 38, '&' ),
 new InfoASCII("'"  , 39, '\'' ),
 new InfoASCII("("   ,40, '(' ),
 new InfoASCII(")"   ,41, ')' ),
 new InfoASCII("*"   ,42, '*' ),
 new InfoASCII("+"   ,43, '+' ),
 new InfoASCII(","   ,44, ',' ),
 new InfoASCII("-"   ,45, '-' ),
 new InfoASCII("."   ,46, '.' ),
 new InfoASCII("/"   ,47, '/' ),
 new InfoASCII("0"  , 48, '0' ),
 new InfoASCII("1"  , 49, '1' ),
 new InfoASCII("2"  , 50, '2' ),
 new InfoASCII("3"  , 51, '3' ),
 new InfoASCII("4"  , 52, '4' ),
 new InfoASCII("5"  , 53, '5' ),
 new InfoASCII("6"  , 54, '6' ),
 new InfoASCII("7"  , 55, '7' ),
 new InfoASCII("8"  , 56, '8' ),
 new InfoASCII("9"  , 57, '9' ),
 new InfoASCII(":"  , 58, ':' ),
 new InfoASCII(";"  , 59, ';' ),
 new InfoASCII("<"  , 60, '<' ),
 new InfoASCII("="  , 61, '=' ),
 new InfoASCII(">"  , 62, '>' ),
 new InfoASCII("?"  , 63, '?' ),
 new InfoASCII("@"  , 64, '@' ),
 new InfoASCII("A"  , 65, 'A' ),
 new InfoASCII("B"  , 66, 'B' ),
 new InfoASCII("C"  , 67, 'C' ),
 new InfoASCII("D"  , 68, 'D' ),
 new InfoASCII("E"  , 69, 'E' ),
 new InfoASCII("F"  , 70, 'F' ),
 new InfoASCII("G"  , 71, 'G' ),
 new InfoASCII("H"  , 72, 'H' ),
 new InfoASCII("I"  , 73, 'I' ),
 new InfoASCII("J"  , 74, 'J' ),
 new InfoASCII("K"  , 75, 'K' ),
 new InfoASCII("L"  , 76, 'L' ),
 new InfoASCII("M"  , 77, 'M' ),
 new InfoASCII("N"  , 78, 'N' ),
 new InfoASCII("O"  , 79, 'O' ),
 new InfoASCII("P"   ,80, 'P' ),
 new InfoASCII("Q"   ,81, 'Q' ),
 new InfoASCII("R"   ,82, 'R' ),
 new InfoASCII("S"   ,83, 'S' ),
 new InfoASCII("T"   ,84, 'T' ),
 new InfoASCII("U"   ,85, 'U' ),
 new InfoASCII("V"   ,86, 'V' ),
 new InfoASCII("W"   ,87, 'W' ),
 new InfoASCII("X"  , 88, 'X' ),
 new InfoASCII("Y"  , 89, 'Y' ),
 new InfoASCII("Z"  , 90, 'Z' ),
 new InfoASCII("["  , 91, '[' ),
 new InfoASCII("\\" ,  92, '\\' ),
 new InfoASCII("]"  ,  93, ']' ),
 new InfoASCII("^"  ,  94, '^' ),
 new InfoASCII("_"  ,  95, '_' ),
 new InfoASCII("`"  ,  96, '`' ),
 new InfoASCII("a"  ,  97, 'a'  ),
 new InfoASCII("b"  ,  98, 'b'  ),
 new InfoASCII("c"  ,  99, 'c'  ),
 new InfoASCII("d"  , 100, 'd'),
 new InfoASCII("e"  , 101, 'e'),
 new InfoASCII("f"  , 102, 'f'),
 new InfoASCII("g"  , 103, 'g'),
 new InfoASCII("h"  , 104, 'h'),
 new InfoASCII("i"  , 105, 'i'),
 new InfoASCII("j"  , 106, 'j'),
 new InfoASCII("k"  , 107, 'k'),
 new InfoASCII("l"  , 108, 'l'),
 new InfoASCII("m"  , 109, 'm'),
 new InfoASCII("n"  , 110, 'n'),
 new InfoASCII("o"  , 111, 'o'),
 new InfoASCII("p"  , 112, 'p'),
 new InfoASCII("q"  , 113, 'q'),
 new InfoASCII("r"  , 114, 'r'),
 new InfoASCII("s"  , 115, 's'),
 new InfoASCII("t"  , 116, 't'),
 new InfoASCII("u"  , 117, 'u'),
 new InfoASCII("v"  , 118, 'v'),
 new InfoASCII("w"  , 119, 'w'),
 new InfoASCII("x"  , 120, 'x'),
 new InfoASCII("y"  , 121, 'y'),
 new InfoASCII("z"  , 122, 'z'),
 new InfoASCII("{"  , 123, '{'),
 new InfoASCII("|"  , 124, '|'),
 new InfoASCII("}"  , 125, '}'),
 new InfoASCII("~"  , 126, '~'),
 new InfoASCII("DEL"  , 127, ' ', "Delete"),
 new InfoASCII("Ç"  , 128, 'Ç'),
 new InfoASCII("ü"  , 129, 'ü'),
 new InfoASCII("é"  , 130, 'é'),
 new InfoASCII("â"  , 131, 'â'),
 new InfoASCII("ä"  , 132, 'ä'),
 new InfoASCII("à"  , 133, 'à'),
 new InfoASCII("å"  , 134, 'å'),
 new InfoASCII("ç"  , 135, 'ç'),
 new InfoASCII("ê"  , 136, 'ê'),
 new InfoASCII("ë"  , 137, 'ë'),
 new InfoASCII("è"  , 138, 'è'),
 new InfoASCII("ï"  , 139, 'ï'),
 new InfoASCII("î"  , 140, 'î'),
 new InfoASCII("ì"  , 141, 'ì'),
 new InfoASCII("Ä"  , 142, 'Ä'),
 new InfoASCII("Å"  , 143, 'Å'),
 new InfoASCII("É"  , 144, 'É'),
 new InfoASCII("æ"  , 145, 'æ'),
 new InfoASCII("Æ"  , 146, 'Æ'),
 new InfoASCII("ô"  , 147, 'ô'),
 new InfoASCII("ö"  , 148, 'ö'),
 new InfoASCII("ò"  , 149, 'ò'),
 new InfoASCII("û"  , 150, 'û'),
 new InfoASCII("ù"  , 151, 'ù'),
 new InfoASCII("ÿ"  , 152, 'ÿ'),
 new InfoASCII("Ö"  , 153, 'Ö'),
 new InfoASCII("Ü"  , 154, 'Ü'),
 new InfoASCII("ø"  , 155, 'ø'),
 new InfoASCII("£"  , 156, '£'),
 new InfoASCII("Ø"  , 157, 'Ø'),
 new InfoASCII("×"  , 158, '×'),
 new InfoASCII("ƒ"  , 159, 'ƒ'),
 new InfoASCII("á"  , 160, 'á'),
 new InfoASCII("í"  , 161, 'í'),
 new InfoASCII("ó"  , 162, 'ó'),
 new InfoASCII("ú"  , 163, 'ú'),
 new InfoASCII("ñ"  , 164, 'ñ'),
 new InfoASCII("Ñ"  , 165, 'Ñ'),
 new InfoASCII("ª"  , 166, 'ª'),
 new InfoASCII("º"  , 167, 'º'),
 new InfoASCII("¿"  , 168, '¿'),
 new InfoASCII("®"  , 169, '®'),
 new InfoASCII("¬"  , 170, '¬'),
 new InfoASCII("½"  , 171, '½'),
 new InfoASCII("¼"  , 172, '¼'),
 new InfoASCII("¡"  , 173, '¡'),
 new InfoASCII("«"  , 174, '«'),
 new InfoASCII("»"  , 175, '»'),
 new InfoASCII("░"  , 176, '░'),
 new InfoASCII("▒"  , 177, '▒'),
 new InfoASCII("▓"  , 178, '▓'),
 new InfoASCII("│"  , 179, '│'),
 new InfoASCII("┤"  , 180, '┤'),
 new InfoASCII("Á"  , 181, 'Á'),
 new InfoASCII("Â"  , 182, 'Â'),
 new InfoASCII("À"  , 183, 'À'),
 new InfoASCII("©"  , 184, '©'),
 new InfoASCII("╣"  , 185, '╣'),
 new InfoASCII("║"  , 186, '║'),
 new InfoASCII("╗"  , 187, '╗'),
 new InfoASCII("╝"  , 188, '╝'),
 new InfoASCII("¢"  , 189, '¢'),
 new InfoASCII("¥"  , 190, '¥'),
 new InfoASCII("┐"  , 191, '┐'),
 new InfoASCII("└"  , 192, '└'),
 new InfoASCII("┴"  , 193, '┴'),
 new InfoASCII("┬"  , 194, '┬'),
 new InfoASCII("├"  , 195, '├'),
 new InfoASCII("─"  , 196, '─'),
 new InfoASCII("┼"  , 197, '┼'),
 new InfoASCII("ã"  , 198, 'ã'),
 new InfoASCII("Ã"  , 199, 'Ã'),
 new InfoASCII("╚"  , 200, '╚'),
 new InfoASCII("╔"  , 201, '╔'),
 new InfoASCII("╩"  , 202, '╩'),
 new InfoASCII("╦"  , 203, '╦'),
 new InfoASCII("╠"  , 204, '╠'),
 new InfoASCII("═"  , 205, '═'),
 new InfoASCII("╬"  , 206, '╬'),
 new InfoASCII("¤"  , 207, '¤'),
 new InfoASCII("ð"  , 208, 'ð'),
 new InfoASCII("Ð"  , 209, 'Ð'),
 new InfoASCII("Ê"  , 210, 'Ê'),
 new InfoASCII("Ë"  , 211, 'Ë'),
 new InfoASCII("È"  , 212, 'È'),
 new InfoASCII("ı"  , 213, 'ı'),
 new InfoASCII("Í"  , 214, 'Í'),
 new InfoASCII("Î"  , 215, 'Î'),
 new InfoASCII("Ï"  , 216, 'Ï'),
 new InfoASCII("┘"  , 217, '┘'),
 new InfoASCII("┌"  , 218, '┌'),
 new InfoASCII("█"  , 219, '█'),
 new InfoASCII("▄"  , 220, '▄'),
 new InfoASCII("¦"  , 221, '¦'),
 new InfoASCII("Ì"  , 222, 'Ì'),
 new InfoASCII("▀"  , 223, '▀'),
 new InfoASCII("Ó"  , 224, 'Ó'),
 new InfoASCII("ß"  , 225, 'ß'),
 new InfoASCII("Ô"  , 226, 'Ô'),
 new InfoASCII("Ò"  , 227, 'Ò'),
 new InfoASCII("õ"  , 228, 'õ'),
 new InfoASCII("Õ"  , 229, 'Õ'),
 new InfoASCII("µ"  , 230, 'µ'),
 new InfoASCII("þ"  , 231, 'þ'),
 new InfoASCII("Þ"  , 232, 'Þ'),
 new InfoASCII("Ú"  , 233, 'Ú'),
 new InfoASCII("Û"  , 234, 'Û'),
 new InfoASCII("Ù"  , 235, 'Ù'),
 new InfoASCII("ý"  , 236, 'ý'),
 new InfoASCII("Ý"  , 237, 'Ý'),
 new InfoASCII("¯"  , 238, '¯'),
 new InfoASCII("´"  , 239, '´'),
 new InfoASCII("≡"   ,240, '≡'),
 new InfoASCII("±"   ,241, '±'),
 new InfoASCII("‗"   ,242, '‗'),
 new InfoASCII("¾"   ,243, '¾'),
 new InfoASCII("¶"   ,244, '¶'),
 new InfoASCII("§"   ,245, '§'),
 new InfoASCII("÷"   ,246, '÷'),
 new InfoASCII("¸" ,  247, '¸'),
 new InfoASCII("°" ,  248, '°'),
 new InfoASCII("¨" ,  249, '¨'),
 new InfoASCII("·" ,  250, '·'),
 new InfoASCII("¹" ,  251, '¹'),
 new InfoASCII("³" ,  252, '³'),
 new InfoASCII("²" ,  253, '²'),
 new InfoASCII("■" ,  254, '■'),
 new InfoASCII("nbsp" ,  255, ' ')
};

    public static KeyboardTouch ConvertStringToTouch(string touchAsString)
    {
        touchAsString= touchAsString.ToLower();
        if (touchAsString.Length == 1) {
            char c = touchAsString[0];
            switch (c)
            {
                case '0': return KeyboardTouch.Alpha0;
                case '1': return KeyboardTouch.Alpha1;
                case '2': return KeyboardTouch.Alpha2;
                case '3': return KeyboardTouch.Alpha3;
                case '4': return KeyboardTouch.Alpha4;
                case '5': return KeyboardTouch.Alpha5;
                case '6': return KeyboardTouch.Alpha6;
                case '7': return KeyboardTouch.Alpha7;
                case '8': return KeyboardTouch.Alpha8;
                case '9': return KeyboardTouch.Alpha9;
                case 'A': return KeyboardTouch.A;
                case 'B': return KeyboardTouch.B;
                case 'C': return KeyboardTouch.C;
                case 'D': return KeyboardTouch.D;
                case 'E': return KeyboardTouch.E;
                case 'F': return KeyboardTouch.F;
                case 'G': return KeyboardTouch.G;
                case 'H': return KeyboardTouch.H;
                case 'I': return KeyboardTouch.I;
                case 'J': return KeyboardTouch.J;
                case 'K': return KeyboardTouch.K;
                case 'L': return KeyboardTouch.L;
                case 'M': return KeyboardTouch.M;
                case 'N': return KeyboardTouch.N;
                case 'O': return KeyboardTouch.O;
                case 'P': return KeyboardTouch.P;
                case 'Q': return KeyboardTouch.Q;
                case 'R': return KeyboardTouch.R;
                case 'S': return KeyboardTouch.S;
                case 'T': return KeyboardTouch.T;
                case 'U': return KeyboardTouch.U;
                case 'V': return KeyboardTouch.V;
                case 'W': return KeyboardTouch.W;
                case 'X': return KeyboardTouch.X;
                case 'Y': return KeyboardTouch.Y;
                case 'Z': return KeyboardTouch.Z;
            

            }
            
        }

        if (touchAsString == "ctrl") return KeyboardTouch.LeftControl;
        if (touchAsString == "escape") return KeyboardTouch.Escape;
        if (touchAsString == "esc") return KeyboardTouch.Escape;
        if (touchAsString == "shift") return KeyboardTouch.Shift;
        if (touchAsString == "alt") return KeyboardTouch.LeftAlt;
        if (touchAsString == "win") return KeyboardTouch.LeftWindow;

        foreach (var p in GetAllTouches())
        {
            if (touchAsString == p.ToString().ToLower())
                return p;
        }


        return KeyboardTouch.Unkown;
    }

    public static char GetCharUnicode(int codeId)
    {
        return (char)codeId;
    }

    public static char[] GetAllASCII()
    {
        return ASCII.Select(k => k.m_character).ToArray();
    }

    public static char GetCharASCII(int asciiCode)
    {
        if (asciiCode < 0 || asciiCode > 255) return ' ';
        return ASCII[asciiCode].m_character;
    }

    public static KeyboardTouch[] GetKeyPadSequenceOf(int number)
    {
        char[] numSplit = number.ToString().ToCharArray();
        KeyboardTouch[] result = new KeyboardTouch[numSplit.Length];
        for (int i = 0; i < numSplit.Length; i++)
        {

            if (numSplit[i] == '0') result[i] = KeyboardTouch.NumPad0;
            else if (numSplit[i] == '1') result[i] = KeyboardTouch.NumPad0;
            else if (numSplit[i] == '2') result[i] = KeyboardTouch.NumPad2;
            else if (numSplit[i] == '3') result[i] = KeyboardTouch.NumPad3;
            else if (numSplit[i] == '4') result[i] = KeyboardTouch.NumPad4;
            else if (numSplit[i] == '5') result[i] = KeyboardTouch.NumPad5;
            else if (numSplit[i] == '6') result[i] = KeyboardTouch.NumPad6;
            else if (numSplit[i] == '7') result[i] = KeyboardTouch.NumPad7;
            else if (numSplit[i] == '8') result[i] = KeyboardTouch.NumPad8;
            else if (numSplit[i] == '9') result[i] = KeyboardTouch.NumPad9;
        }
        return result;
    }
    public static KeyboardTouch[] GetAlphaNumSequenceOf(int number)
    {
        char[] numSplit = number.ToString().ToCharArray();
        KeyboardTouch[] result = new KeyboardTouch[numSplit.Length];
        for (int i = 0; i < numSplit.Length; i++)
        {

            if (numSplit[i] == '0') result[i] = KeyboardTouch.Alpha0;
            else if (numSplit[i] == '1') result[i] = KeyboardTouch.Alpha1;
            else if (numSplit[i] == '2') result[i] = KeyboardTouch.Alpha2;
            else if (numSplit[i] == '3') result[i] = KeyboardTouch.Alpha3;
            else if (numSplit[i] == '4') result[i] = KeyboardTouch.Alpha4;
            else if (numSplit[i] == '5') result[i] = KeyboardTouch.Alpha5;
            else if (numSplit[i] == '6') result[i] = KeyboardTouch.Alpha6;
            else if (numSplit[i] == '7') result[i] = KeyboardTouch.Alpha7;
            else if (numSplit[i] == '8') result[i] = KeyboardTouch.Alpha8;
            else if (numSplit[i] == '9') result[i] = KeyboardTouch.Alpha9;
       

            


            




        }
        return result;
    }

    public static void ConvertUnityKeyToTouch(KeyCode key, out KeyboardTouch bindKey, out bool isConvertable)
    {

        bindKey = KeyboardTouch.Unkown;
        isConvertable = true;

        for (int i = 0; i < KEYTOUNITYKEY.Length; i++)
        {
            if (KEYTOUNITYKEY[i].m_unityKey == key)
            {
                bindKey = KEYTOUNITYKEY[i].m_touch;
                isConvertable = true;
                return;
            }
        }
        isConvertable = false;
    }


    public static void ConvertTouchToUnityKey(KeyboardTouch touch, out KeyCode bindKey, out bool isConvertable)
    {

        bindKey =KeyCode.None;
        isConvertable = true;

        for (int i = 0; i < KEYTOUNITYKEY.Length; i++)
        {
            if (KEYTOUNITYKEY[i].m_touch == touch)
            {
                bindKey = KEYTOUNITYKEY[i].m_unityKey;
                isConvertable = true;
                return;
            }
        }
        isConvertable = false;
    }

    public class TouchToChar
    {
        public KeyboardTouch m_touch; public char m_char;

        public TouchToChar(KeyboardTouch touch, char c)
        {
            this.m_touch = touch;
            this.m_char = c;
        }
    }
    public class TouchToUnityKeyCode
    {
        public KeyboardTouch m_touch;
        public UnityEngine.KeyCode m_unityKey;

        public TouchToUnityKeyCode(KeyboardTouch touch, KeyCode unityKey)
        {
            m_touch = touch;
            m_unityKey = unityKey;
        }
    }
    
    public class TouchToVirtualKeyCode
    {
        public KeyboardTouch m_touch;
        public VirtualKeyCode m_virtualKey;

        public TouchToVirtualKeyCode(KeyboardTouch touch, VirtualKeyCode unityKey)
        {
            m_touch = touch;
            m_virtualKey = unityKey;
        }
    }

    public static TouchToUnityKeyCode[] KEYTOUNITYKEY = new TouchToUnityKeyCode[] {
 new TouchToUnityKeyCode(KeyboardTouch. A   , KeyCode.  A),
 new TouchToUnityKeyCode(KeyboardTouch. B   , KeyCode.  B),
 new TouchToUnityKeyCode(KeyboardTouch. C   , KeyCode.  C),
 new TouchToUnityKeyCode(KeyboardTouch. D   , KeyCode.  D),
 new TouchToUnityKeyCode(KeyboardTouch. E   , KeyCode.  E),
 new TouchToUnityKeyCode(KeyboardTouch. F   , KeyCode.  F),
 new TouchToUnityKeyCode(KeyboardTouch. G   , KeyCode.  G),
 new TouchToUnityKeyCode(KeyboardTouch. H   , KeyCode.  H),
 new TouchToUnityKeyCode(KeyboardTouch. I   , KeyCode.  I),
 new TouchToUnityKeyCode(KeyboardTouch. J   , KeyCode.  J),
 new TouchToUnityKeyCode(KeyboardTouch. K   , KeyCode.  K),
 new TouchToUnityKeyCode(KeyboardTouch. L   , KeyCode.  L),
 new TouchToUnityKeyCode(KeyboardTouch. M   , KeyCode.  M),
 new TouchToUnityKeyCode(KeyboardTouch. N   , KeyCode.  N),
 new TouchToUnityKeyCode(KeyboardTouch. O   , KeyCode.  O),
 new TouchToUnityKeyCode(KeyboardTouch. P   , KeyCode.  P),
 new TouchToUnityKeyCode(KeyboardTouch. Q   , KeyCode.  Q),
 new TouchToUnityKeyCode(KeyboardTouch. R   , KeyCode.  R),
 new TouchToUnityKeyCode(KeyboardTouch. S   , KeyCode.  S),
 new TouchToUnityKeyCode(KeyboardTouch. T   , KeyCode.  T),
 new TouchToUnityKeyCode(KeyboardTouch. U   , KeyCode.  U),
 new TouchToUnityKeyCode(KeyboardTouch. V   , KeyCode.  V),
 new TouchToUnityKeyCode(KeyboardTouch. W   , KeyCode.  W),
 new TouchToUnityKeyCode(KeyboardTouch. X   , KeyCode.  X),
 new TouchToUnityKeyCode(KeyboardTouch. Y   , KeyCode.  Y),
 new TouchToUnityKeyCode(KeyboardTouch. Z   , KeyCode.  Z),
 new TouchToUnityKeyCode(KeyboardTouch. NumPad1    , KeyCode.  Keypad1),
 new TouchToUnityKeyCode(KeyboardTouch. NumPad2    , KeyCode.  Keypad2),
 new TouchToUnityKeyCode(KeyboardTouch. NumPad3    , KeyCode.  Keypad3),
 new TouchToUnityKeyCode(KeyboardTouch. NumPad4    , KeyCode.  Keypad4),
 new TouchToUnityKeyCode(KeyboardTouch. NumPad5    , KeyCode.  Keypad5),
 new TouchToUnityKeyCode(KeyboardTouch. NumPad6    , KeyCode.  Keypad6),
 new TouchToUnityKeyCode(KeyboardTouch. NumPad7    , KeyCode.  Keypad7),
 new TouchToUnityKeyCode(KeyboardTouch. NumPad8    , KeyCode.  Keypad8),
 new TouchToUnityKeyCode(KeyboardTouch. NumPad9    , KeyCode.  Keypad9),
 new TouchToUnityKeyCode(KeyboardTouch. NumPad0    , KeyCode.  Keypad0),
 new TouchToUnityKeyCode(KeyboardTouch. Alpha1  , KeyCode.  Alpha1),
 new TouchToUnityKeyCode(KeyboardTouch. Alpha2  , KeyCode.  Alpha2),
 new TouchToUnityKeyCode(KeyboardTouch. Alpha3  , KeyCode.  Alpha3),
 new TouchToUnityKeyCode(KeyboardTouch. Alpha4  , KeyCode.  Alpha4),
 new TouchToUnityKeyCode(KeyboardTouch. Alpha5  , KeyCode.  Alpha5),
 new TouchToUnityKeyCode(KeyboardTouch. Alpha6  , KeyCode.  Alpha6),
 new TouchToUnityKeyCode(KeyboardTouch. Alpha7  , KeyCode.  Alpha7),
 new TouchToUnityKeyCode(KeyboardTouch. Alpha8  , KeyCode.  Alpha8),
 new TouchToUnityKeyCode(KeyboardTouch. Alpha9  , KeyCode.  Alpha9),
 new TouchToUnityKeyCode(KeyboardTouch. Alpha0  , KeyCode.  Alpha0),
 new TouchToUnityKeyCode(KeyboardTouch. Up  , KeyCode.  UpArrow),
 new TouchToUnityKeyCode(KeyboardTouch. Down    , KeyCode.  DownArrow),
 new TouchToUnityKeyCode(KeyboardTouch. Right   , KeyCode.  RightArrow),
 new TouchToUnityKeyCode(KeyboardTouch. Left    , KeyCode.  LeftArrow),
 new TouchToUnityKeyCode(KeyboardTouch. F1  , KeyCode.  F1),
 new TouchToUnityKeyCode(KeyboardTouch. F2  , KeyCode.  F2),
 new TouchToUnityKeyCode(KeyboardTouch. F3  , KeyCode.  F3),
 new TouchToUnityKeyCode(KeyboardTouch. F4  , KeyCode.  F4),
 new TouchToUnityKeyCode(KeyboardTouch. F5  , KeyCode.  F5),
 new TouchToUnityKeyCode(KeyboardTouch. F6  , KeyCode.  F6),
 new TouchToUnityKeyCode(KeyboardTouch. F7  , KeyCode.  F7),
 new TouchToUnityKeyCode(KeyboardTouch. F8  , KeyCode.  F8),
 new TouchToUnityKeyCode(KeyboardTouch. F9  , KeyCode.  F9),
 new TouchToUnityKeyCode(KeyboardTouch. F10 , KeyCode.  F10),
 new TouchToUnityKeyCode(KeyboardTouch. F11 , KeyCode.  F11),
 new TouchToUnityKeyCode(KeyboardTouch. F12 , KeyCode.  F12),
 new TouchToUnityKeyCode(KeyboardTouch. F13 , KeyCode.  F13),
 new TouchToUnityKeyCode(KeyboardTouch. F14 , KeyCode.  F14),
 new TouchToUnityKeyCode(KeyboardTouch. F15 , KeyCode.  F15),
 new TouchToUnityKeyCode(KeyboardTouch. Space   , KeyCode.  Space),
 new TouchToUnityKeyCode(KeyboardTouch. Enter   , KeyCode.  Return),
 new TouchToUnityKeyCode(KeyboardTouch. PageUp  , KeyCode.  PageUp),
 new TouchToUnityKeyCode(KeyboardTouch. PageDown    , KeyCode.  PageDown),
 new TouchToUnityKeyCode(KeyboardTouch. Home    , KeyCode.  Home),
 new TouchToUnityKeyCode(KeyboardTouch. End , KeyCode.  End),
 new TouchToUnityKeyCode(KeyboardTouch. Insert  , KeyCode.  Insert),
 new TouchToUnityKeyCode(KeyboardTouch. Delete  , KeyCode.  Delete),
 new TouchToUnityKeyCode(KeyboardTouch. Menu    , KeyCode.  Menu),
 new TouchToUnityKeyCode(KeyboardTouch. LeftShift   , KeyCode.  LeftShift),
 new TouchToUnityKeyCode(KeyboardTouch. RightShift  , KeyCode.  RightShift),
 new TouchToUnityKeyCode(KeyboardTouch. LeftControl , KeyCode.  LeftControl),
 new TouchToUnityKeyCode(KeyboardTouch. RightControl    , KeyCode.  RightControl),
 new TouchToUnityKeyCode(KeyboardTouch. LeftAlt , KeyCode.  LeftAlt),
 new TouchToUnityKeyCode(KeyboardTouch. RightAlt    , KeyCode.  RightAlt),
 new TouchToUnityKeyCode(KeyboardTouch. Alt , KeyCode.  LeftAlt),
 new TouchToUnityKeyCode(KeyboardTouch. AltGr   , KeyCode.  AltGr),
 new TouchToUnityKeyCode(KeyboardTouch. Break   , KeyCode.  Break),
 new TouchToUnityKeyCode(KeyboardTouch. ScrollLock  , KeyCode.  ScrollLock),
 new TouchToUnityKeyCode(KeyboardTouch. Print   , KeyCode.  Print),
 new TouchToUnityKeyCode(KeyboardTouch. Help    , KeyCode.  Help),
 new TouchToUnityKeyCode(KeyboardTouch. SystemRequest   , KeyCode.  SysReq),
 new TouchToUnityKeyCode(KeyboardTouch. NumLock , KeyCode.  Numlock),
 new TouchToUnityKeyCode(KeyboardTouch. CapsLock    , KeyCode.  CapsLock),
 new TouchToUnityKeyCode(KeyboardTouch. MouseLeft   , KeyCode.  Mouse0),
 new TouchToUnityKeyCode(KeyboardTouch. MouseRight  , KeyCode.  Mouse1),
 new TouchToUnityKeyCode(KeyboardTouch. MouseMiddle , KeyCode.  Mouse2),
 new TouchToUnityKeyCode(KeyboardTouch. Shift   , KeyCode.  LeftShift),
 new TouchToUnityKeyCode(KeyboardTouch. Control , KeyCode.  LeftControl),
 new TouchToUnityKeyCode(KeyboardTouch. Unkown  , KeyCode.  None    ),
 new TouchToUnityKeyCode(KeyboardTouch. None    , KeyCode.  None),
 new TouchToUnityKeyCode(KeyboardTouch. Backspace   , KeyCode.  Backspace),
 new TouchToUnityKeyCode(KeyboardTouch. Tab , KeyCode.  Tab),
 new TouchToUnityKeyCode(KeyboardTouch. Clear   , KeyCode.  Clear),
 new TouchToUnityKeyCode(KeyboardTouch. Escape  , KeyCode.  Escape),
 new TouchToUnityKeyCode(KeyboardTouch. Return  , KeyCode.  Return),
 new TouchToUnityKeyCode(KeyboardTouch. Play    , KeyCode.  None    ),
 new TouchToUnityKeyCode(KeyboardTouch. Pause   , KeyCode.  Pause),
 new TouchToUnityKeyCode(KeyboardTouch. LeftWindow  , KeyCode.  LeftWindows),
 new TouchToUnityKeyCode(KeyboardTouch. RightWindow , KeyCode.  RightWindows),
 new TouchToUnityKeyCode(KeyboardTouch. LeftCommand , KeyCode.  LeftCommand),
 new TouchToUnityKeyCode(KeyboardTouch. RightCommand  , KeyCode.  RightApple),

    };

    public static TouchToVirtualKeyCode[] TOUCHTOWINDOWKEY = new TouchToVirtualKeyCode[]{
                new TouchToVirtualKeyCode(KeyboardTouch.A, VirtualKeyCode.VK_A),
new TouchToVirtualKeyCode(KeyboardTouch.A, VirtualKeyCode.VK_A),
new TouchToVirtualKeyCode(KeyboardTouch.B, VirtualKeyCode.VK_B),
new TouchToVirtualKeyCode(KeyboardTouch.C, VirtualKeyCode.VK_C),
new TouchToVirtualKeyCode(KeyboardTouch.D, VirtualKeyCode.VK_D),
new TouchToVirtualKeyCode(KeyboardTouch.E, VirtualKeyCode.VK_E),
new TouchToVirtualKeyCode(KeyboardTouch.F, VirtualKeyCode.VK_F),
new TouchToVirtualKeyCode(KeyboardTouch.G, VirtualKeyCode.VK_G),
new TouchToVirtualKeyCode(KeyboardTouch.H, VirtualKeyCode.VK_H),
new TouchToVirtualKeyCode(KeyboardTouch.I, VirtualKeyCode.VK_I),
new TouchToVirtualKeyCode(KeyboardTouch.J, VirtualKeyCode.VK_J),
new TouchToVirtualKeyCode(KeyboardTouch.A, VirtualKeyCode.VK_A),
new TouchToVirtualKeyCode(KeyboardTouch.K, VirtualKeyCode.VK_K),
new TouchToVirtualKeyCode(KeyboardTouch.L, VirtualKeyCode.VK_L),
new TouchToVirtualKeyCode(KeyboardTouch.M, VirtualKeyCode.VK_M),
new TouchToVirtualKeyCode(KeyboardTouch.N, VirtualKeyCode.VK_N),
new TouchToVirtualKeyCode(KeyboardTouch.O, VirtualKeyCode.VK_O),
new TouchToVirtualKeyCode(KeyboardTouch.P, VirtualKeyCode.VK_P),
new TouchToVirtualKeyCode(KeyboardTouch.Q, VirtualKeyCode.VK_Q),
new TouchToVirtualKeyCode(KeyboardTouch.R, VirtualKeyCode.VK_R),
new TouchToVirtualKeyCode(KeyboardTouch.S, VirtualKeyCode.VK_S),
new TouchToVirtualKeyCode(KeyboardTouch.T, VirtualKeyCode.VK_T),
new TouchToVirtualKeyCode(KeyboardTouch.U, VirtualKeyCode.VK_U),
new TouchToVirtualKeyCode(KeyboardTouch.V, VirtualKeyCode.VK_V),
new TouchToVirtualKeyCode(KeyboardTouch.W, VirtualKeyCode.VK_W),
new TouchToVirtualKeyCode(KeyboardTouch.X, VirtualKeyCode.VK_X),
new TouchToVirtualKeyCode(KeyboardTouch.Y, VirtualKeyCode.VK_Y),
new TouchToVirtualKeyCode(KeyboardTouch.Z, VirtualKeyCode.VK_Z),
new TouchToVirtualKeyCode(KeyboardTouch.NumPad1    , VirtualKeyCode.   NUMPAD1),
new TouchToVirtualKeyCode(KeyboardTouch.NumPad2    , VirtualKeyCode.   NUMPAD2),
new TouchToVirtualKeyCode(KeyboardTouch.NumPad3    , VirtualKeyCode.   NUMPAD3),
new TouchToVirtualKeyCode(KeyboardTouch.NumPad4    , VirtualKeyCode.   NUMPAD4),
new TouchToVirtualKeyCode(KeyboardTouch.NumPad5    , VirtualKeyCode.  NUMPAD5),
new TouchToVirtualKeyCode(KeyboardTouch.NumPad6    , VirtualKeyCode.   NUMPAD6),
new TouchToVirtualKeyCode(KeyboardTouch.NumPad7    , VirtualKeyCode.  NUMPAD7),
new TouchToVirtualKeyCode(KeyboardTouch.NumPad8    , VirtualKeyCode.   NUMPAD8),
new TouchToVirtualKeyCode(KeyboardTouch.NumPad9    , VirtualKeyCode.  NUMPAD9),
new TouchToVirtualKeyCode(KeyboardTouch.NumPad0    , VirtualKeyCode.  NUMPAD0),
new TouchToVirtualKeyCode(KeyboardTouch.NumPadDecimal   , VirtualKeyCode. DECIMAL),
new TouchToVirtualKeyCode(KeyboardTouch.Up   , VirtualKeyCode.      UP),
new TouchToVirtualKeyCode(KeyboardTouch.Down     , VirtualKeyCode.      DOWN),
new TouchToVirtualKeyCode(KeyboardTouch.Right    , VirtualKeyCode.      RIGHT),
new TouchToVirtualKeyCode(KeyboardTouch.Left     , VirtualKeyCode.      LEFT),
new TouchToVirtualKeyCode(KeyboardTouch.    F1 , VirtualKeyCode.   F1       ),
new TouchToVirtualKeyCode(KeyboardTouch.    F2 , VirtualKeyCode.   F2       ),
new TouchToVirtualKeyCode(KeyboardTouch.    F3 , VirtualKeyCode.   F3       ),
new TouchToVirtualKeyCode(KeyboardTouch.    F4 , VirtualKeyCode.   F4       ),
new TouchToVirtualKeyCode(KeyboardTouch.    F5 , VirtualKeyCode.   F5       ),
new TouchToVirtualKeyCode(KeyboardTouch.    F6 , VirtualKeyCode.   F6       ),
new TouchToVirtualKeyCode(KeyboardTouch.    F7 , VirtualKeyCode.   F7       ),
new TouchToVirtualKeyCode(KeyboardTouch.    F8 , VirtualKeyCode.   F8       ),
new TouchToVirtualKeyCode(KeyboardTouch.    F9 , VirtualKeyCode.   F9       ),
new TouchToVirtualKeyCode(KeyboardTouch.    F10 , VirtualKeyCode.   F10     ),
new TouchToVirtualKeyCode(KeyboardTouch.    F11 , VirtualKeyCode.   F11     ),
new TouchToVirtualKeyCode(KeyboardTouch.    F12 , VirtualKeyCode.   F12     ),
new TouchToVirtualKeyCode(KeyboardTouch.    F13 , VirtualKeyCode.   F13     ),
new TouchToVirtualKeyCode(KeyboardTouch.    F14 , VirtualKeyCode.   F14     ),
new TouchToVirtualKeyCode(KeyboardTouch.    F15 , VirtualKeyCode.   F15     ),
new TouchToVirtualKeyCode(KeyboardTouch.    F16 , VirtualKeyCode.   F16     ),
new TouchToVirtualKeyCode(KeyboardTouch.    F17 , VirtualKeyCode.   F17     ),
new TouchToVirtualKeyCode(KeyboardTouch.    F18 , VirtualKeyCode.   F18     ),
new TouchToVirtualKeyCode(KeyboardTouch.    F19 , VirtualKeyCode.   F19     ),
new TouchToVirtualKeyCode(KeyboardTouch.    F20 , VirtualKeyCode.   F20     ),
new TouchToVirtualKeyCode(KeyboardTouch.    F21 , VirtualKeyCode.   F21     ),
new TouchToVirtualKeyCode(KeyboardTouch.    F22 , VirtualKeyCode.   F22     ),
new TouchToVirtualKeyCode(KeyboardTouch.    F23 , VirtualKeyCode.   F23     ),
new TouchToVirtualKeyCode(KeyboardTouch.    F24 , VirtualKeyCode.   F24     ),
new TouchToVirtualKeyCode(KeyboardTouch. Alpha1   , VirtualKeyCode. VK_1  ),
new TouchToVirtualKeyCode(KeyboardTouch. Alpha2   , VirtualKeyCode. VK_2  ),
new TouchToVirtualKeyCode(KeyboardTouch. Alpha3   , VirtualKeyCode. VK_3  ),
new TouchToVirtualKeyCode(KeyboardTouch. Alpha4   , VirtualKeyCode. VK_4  ),
new TouchToVirtualKeyCode(KeyboardTouch. Alpha5   , VirtualKeyCode. VK_5  ),
new TouchToVirtualKeyCode(KeyboardTouch. Alpha6   , VirtualKeyCode. VK_6  ),
new TouchToVirtualKeyCode(KeyboardTouch. Alpha7   , VirtualKeyCode. VK_7  ),
new TouchToVirtualKeyCode(KeyboardTouch. Alpha8   , VirtualKeyCode. VK_8  ),
new TouchToVirtualKeyCode(KeyboardTouch. Alpha9   , VirtualKeyCode. VK_9  ),
new TouchToVirtualKeyCode(KeyboardTouch. Alpha0   , VirtualKeyCode. VK_0  ),
new TouchToVirtualKeyCode(KeyboardTouch.    Space  , VirtualKeyCode.   SPACE           ),
new TouchToVirtualKeyCode(KeyboardTouch.    Enter  , VirtualKeyCode.   RETURN          ),
new TouchToVirtualKeyCode(KeyboardTouch.    PageUp , VirtualKeyCode.    PRIOR          ),
new TouchToVirtualKeyCode(KeyboardTouch.    PageDown  , VirtualKeyCode.   NEXT         ),
new TouchToVirtualKeyCode(KeyboardTouch.    Home , VirtualKeyCode.    HOME             ),
new TouchToVirtualKeyCode(KeyboardTouch.    End , VirtualKeyCode.    END               ),
new TouchToVirtualKeyCode(KeyboardTouch.    Insert  , VirtualKeyCode.   INSERT         ),
new TouchToVirtualKeyCode(KeyboardTouch.    Delete  , VirtualKeyCode.   DELETE         ),
new TouchToVirtualKeyCode(KeyboardTouch.    Menu  , VirtualKeyCode.   MENU             ),
new TouchToVirtualKeyCode(KeyboardTouch.    LeftShift  , VirtualKeyCode.   LSHIFT       ),
new TouchToVirtualKeyCode(KeyboardTouch.    RightShift , VirtualKeyCode.    RSHIFT      ),
new TouchToVirtualKeyCode(KeyboardTouch.    LeftControl  , VirtualKeyCode.   LCONTROL   ),
new TouchToVirtualKeyCode(KeyboardTouch.    RightControl , VirtualKeyCode.    RCONTROL ),
new TouchToVirtualKeyCode(KeyboardTouch.    LeftAlt  , VirtualKeyCode.   LMENU         ),
new TouchToVirtualKeyCode(KeyboardTouch.    RightAlt , VirtualKeyCode.    RMENU         ),
new TouchToVirtualKeyCode(KeyboardTouch.    Alt , VirtualKeyCode.    LMENU              ),
new TouchToVirtualKeyCode(KeyboardTouch.    AltGr , VirtualKeyCode.    RMENU           ),
new TouchToVirtualKeyCode(KeyboardTouch.    ScrollLock  , VirtualKeyCode.   SCROLL      ),
new TouchToVirtualKeyCode(KeyboardTouch.    Print  , VirtualKeyCode.   PRINT           ),
new TouchToVirtualKeyCode(KeyboardTouch.    PrintScreen , VirtualKeyCode.    SNAPSHOT   ),
new TouchToVirtualKeyCode(KeyboardTouch.    Help  , VirtualKeyCode.   HELP             ),
new TouchToVirtualKeyCode(KeyboardTouch.    NumLock , VirtualKeyCode.    NUMLOCK         ),
new TouchToVirtualKeyCode(KeyboardTouch.    CapsLock , VirtualKeyCode.    CAPITAL        ),
new TouchToVirtualKeyCode(KeyboardTouch.    MouseLeft , VirtualKeyCode.    LBUTTON       ),
new TouchToVirtualKeyCode(KeyboardTouch.    MouseRight  , VirtualKeyCode.   RBUTTON      ),
new TouchToVirtualKeyCode(KeyboardTouch.    MouseMiddle , VirtualKeyCode.    MBUTTON     ),
new TouchToVirtualKeyCode(KeyboardTouch.    Shift  , VirtualKeyCode.   SHIFT             ),
new TouchToVirtualKeyCode(KeyboardTouch.    Control  , VirtualKeyCode.   CONTROL         ),
new TouchToVirtualKeyCode(KeyboardTouch.    LaunchApplicationOne , VirtualKeyCode.LAUNCH_APP1        ),
new TouchToVirtualKeyCode(KeyboardTouch.    LaunchApplicationTwo , VirtualKeyCode.LAUNCH_APP2        ),
new TouchToVirtualKeyCode(KeyboardTouch.    Sleep  , VirtualKeyCode.   SLEEP             ),
new TouchToVirtualKeyCode(KeyboardTouch.    Select  , VirtualKeyCode.   SELECT           ),
new TouchToVirtualKeyCode(KeyboardTouch.    Execute  , VirtualKeyCode.   EXECUTE         ),
new TouchToVirtualKeyCode(KeyboardTouch.    Backspace  , VirtualKeyCode.   BACK          ),
new TouchToVirtualKeyCode(KeyboardTouch.    Tab  , VirtualKeyCode.   TAB                 ),
new TouchToVirtualKeyCode(KeyboardTouch.    Clear  , VirtualKeyCode.   CLEAR             ),
new TouchToVirtualKeyCode(KeyboardTouch.    Escape  , VirtualKeyCode.   ESCAPE           ),
new TouchToVirtualKeyCode(KeyboardTouch.    Return  , VirtualKeyCode.   RETURN           ),
new TouchToVirtualKeyCode(KeyboardTouch.    NumPadDivide      , VirtualKeyCode.     DIVIDE        ),
new TouchToVirtualKeyCode(KeyboardTouch.    NumPadMultiply , VirtualKeyCode.    MULTIPLY          ),
new TouchToVirtualKeyCode(KeyboardTouch.    NumPadSubstract   , VirtualKeyCode.   SUBTRACT        ),
new TouchToVirtualKeyCode(KeyboardTouch.    NumPadAdd    , VirtualKeyCode.      ADD               ),
new TouchToVirtualKeyCode(KeyboardTouch.    NumPadEnter  , VirtualKeyCode.   RETURN               ),
new TouchToVirtualKeyCode(KeyboardTouch.    NumPadPeriod  , VirtualKeyCode.   OEM_PERIOD          ),
new TouchToVirtualKeyCode(KeyboardTouch.    NextTrack , VirtualKeyCode.    MEDIA_NEXT_TRACK      ),
new TouchToVirtualKeyCode(KeyboardTouch.    PreviousTrack  , VirtualKeyCode.   MEDIA_PREV_TRACK  ),
new TouchToVirtualKeyCode(KeyboardTouch.    StopTrack  , VirtualKeyCode.   MEDIA_STOP             ),
new TouchToVirtualKeyCode(KeyboardTouch.    Play , VirtualKeyCode.    PLAY                        ),
new TouchToVirtualKeyCode(KeyboardTouch.    Pause , VirtualKeyCode.    PAUSE                      ),
new TouchToVirtualKeyCode(KeyboardTouch.    VolumeMute  , VirtualKeyCode.   VOLUME_MUTE                  ),
new TouchToVirtualKeyCode(KeyboardTouch.    VolumeDown , VirtualKeyCode.    VOLUME_DOWN            ),
new TouchToVirtualKeyCode(KeyboardTouch.    VolumeUp  , VirtualKeyCode.   VOLUME_UP                ),
new TouchToVirtualKeyCode(KeyboardTouch.    Zoom  , VirtualKeyCode.   ZOOM                         ),
new TouchToVirtualKeyCode(KeyboardTouch.    LeftWindow  , VirtualKeyCode.   LWIN                   ),
new TouchToVirtualKeyCode(KeyboardTouch.    RightWindow  , VirtualKeyCode.   RWIN                  ),

new TouchToVirtualKeyCode(KeyboardTouch.    WinUS_OEM_102_BackSlash  , VirtualKeyCode.   OEM_102                   ),
new TouchToVirtualKeyCode(KeyboardTouch.    WinUS_OEM_1_SemiColon  , VirtualKeyCode.   OEM_1                   ),
new TouchToVirtualKeyCode(KeyboardTouch.    WinUS_OEM_2_Slash  , VirtualKeyCode.   OEM_2                   ),
new TouchToVirtualKeyCode(KeyboardTouch.    WinUS_OEM_3_GraveAccent  , VirtualKeyCode.   OEM_3                   ),
new TouchToVirtualKeyCode(KeyboardTouch.    WinUS_OEM_4_LeftBracket  , VirtualKeyCode.   OEM_4                   ),
new TouchToVirtualKeyCode(KeyboardTouch.    WinUS_OEM_5_BackSlash  , VirtualKeyCode.   OEM_5                   ),
new TouchToVirtualKeyCode(KeyboardTouch.    WinUS_OEM_6_RightBlacket  , VirtualKeyCode.   OEM_6                   ),
new TouchToVirtualKeyCode(KeyboardTouch.    WinUS_OEM_7_Quote  , VirtualKeyCode.   OEM_7                   ),
new TouchToVirtualKeyCode(KeyboardTouch.    WinUS_OEM_Comma  , VirtualKeyCode.   OEM_COMMA                   ),
new TouchToVirtualKeyCode(KeyboardTouch.    WinUS_OEM_Minus  , VirtualKeyCode.   OEM_MINUS                   ),
new TouchToVirtualKeyCode(KeyboardTouch.    WinUS_OEM_Period  , VirtualKeyCode.   OEM_PERIOD                   ),
new TouchToVirtualKeyCode(KeyboardTouch.    MouseLeft  , VirtualKeyCode.   LBUTTON                   ),
new TouchToVirtualKeyCode(KeyboardTouch.    MouseRight  , VirtualKeyCode.   RBUTTON                   ),
new TouchToVirtualKeyCode(KeyboardTouch.    MouseMiddle  , VirtualKeyCode.   MBUTTON                   ),
new TouchToVirtualKeyCode(KeyboardTouch.    MouseSupp1  , VirtualKeyCode.   XBUTTON1                   ),
new TouchToVirtualKeyCode(KeyboardTouch.    MouseSupp2  , VirtualKeyCode.   XBUTTON2                   )

};



    public static bool IsCharASCII(char character)
    {
        int code = (int)character;
        return code > 255;
    }

    public static void ConvertCharToASCII(char character, out int asciiCode, out bool isConvertable)
    {
        asciiCode = (int)character;
        isConvertable = asciiCode <= 255;
        //TOVERIFY

    }
    public static void ConvertASCIIToChar(int asciiCode, out char character, out bool isConvertable)
    {
        character = (char)asciiCode;
        isConvertable = asciiCode <= 255;
        //TOVERIFY
    }
    public static void ConvertCharToUnicode(char character, out int unicode, out bool isConvertable)
    {
        throw new NotImplementedException();
    }
    public static void ConvertUnicodeToChar(int unicode, out char character, out bool isConvertable)
    {
        
        character = Convert.ToChar(unicode);
       isConvertable = unicode > -1;
    }

    public static InfoASCII GetInfoASCII(int asciiCode)
    {
        if (asciiCode < 0 || asciiCode > 255) return null;
        return ASCII[asciiCode];

    }

    public static void ConvertWindowVirtualKeyCodesToTouch(VirtualKeyCode[] virtualKeyCodeout, out KeyboardTouch key, out bool isConvertable)
    {
        throw new NotImplementedException();
    }
    public class VirtualToTouch
    {
        public VirtualKeyCode m_windowCode;
        public KeyboardTouch m_touch;
        public VirtualToTouch(VirtualKeyCode code, KeyboardTouch touch)
        {
            m_windowCode = code; m_touch = touch;
        }
    }

    public static  VirtualToTouch[] VIRTUALTOTOUCH = new VirtualToTouch[] {
        new VirtualToTouch(VirtualKeyCode.LBUTTON, KeyboardTouch.MouseLeft ),
        new VirtualToTouch(VirtualKeyCode.RBUTTON, KeyboardTouch. MouseRight),
        new VirtualToTouch(VirtualKeyCode.CANCEL, KeyboardTouch. Unkown),
        new VirtualToTouch(VirtualKeyCode.MBUTTON, KeyboardTouch.MouseMiddle ),
        new VirtualToTouch(VirtualKeyCode.XBUTTON1, KeyboardTouch.Unkown ),
        new VirtualToTouch(VirtualKeyCode.XBUTTON2, KeyboardTouch.Unkown ),
        new VirtualToTouch(VirtualKeyCode.BACK, KeyboardTouch.Backspace ),
        new VirtualToTouch(VirtualKeyCode.TAB, KeyboardTouch.Tab ),
        new VirtualToTouch(VirtualKeyCode.CLEAR, KeyboardTouch.Clear ),
        new VirtualToTouch(VirtualKeyCode.RETURN, KeyboardTouch.Return ),
        new VirtualToTouch(VirtualKeyCode.SHIFT, KeyboardTouch.Shift ),
        new VirtualToTouch(VirtualKeyCode.CONTROL, KeyboardTouch.Control ),
        new VirtualToTouch(VirtualKeyCode.MENU, KeyboardTouch.Menu ),
        new VirtualToTouch(VirtualKeyCode.PAUSE, KeyboardTouch.Pause ),
        new VirtualToTouch(VirtualKeyCode.CAPITAL, KeyboardTouch.CapsLock ),
        new VirtualToTouch(VirtualKeyCode.KANA, KeyboardTouch.KanaMode ),
        new VirtualToTouch(VirtualKeyCode.FINAL, KeyboardTouch.Unkown),
        new VirtualToTouch(VirtualKeyCode.ESCAPE, KeyboardTouch.Escape ),
        new VirtualToTouch(VirtualKeyCode.CONVERT, KeyboardTouch.Convert ),
        new VirtualToTouch(VirtualKeyCode.NONCONVERT, KeyboardTouch.NonConvert ),
        new VirtualToTouch(VirtualKeyCode.ACCEPT, KeyboardTouch.Unkown ),
        new VirtualToTouch(VirtualKeyCode.MODECHANGE, KeyboardTouch.Unkown ),
        new VirtualToTouch(VirtualKeyCode.SPACE, KeyboardTouch.Space ),
        new VirtualToTouch(VirtualKeyCode.PRIOR, KeyboardTouch.PageUp ),
        new VirtualToTouch(VirtualKeyCode.NEXT, KeyboardTouch.PageDown ),
        new VirtualToTouch(VirtualKeyCode.END, KeyboardTouch.End ),
        new VirtualToTouch(VirtualKeyCode.HOME, KeyboardTouch.Home ),
        new VirtualToTouch(VirtualKeyCode.LEFT, KeyboardTouch.Left ),
        new VirtualToTouch(VirtualKeyCode.UP, KeyboardTouch.Up ),
        new VirtualToTouch(VirtualKeyCode.RIGHT, KeyboardTouch. Right),
        new VirtualToTouch(VirtualKeyCode.DOWN, KeyboardTouch. Down),
        new VirtualToTouch(VirtualKeyCode.SELECT, KeyboardTouch.Select ),
        new VirtualToTouch(VirtualKeyCode.PRINT, KeyboardTouch.Print ),
        new VirtualToTouch(VirtualKeyCode.EXECUTE, KeyboardTouch.Execute ),
        new VirtualToTouch(VirtualKeyCode.SNAPSHOT, KeyboardTouch.Unkown ),
        new VirtualToTouch(VirtualKeyCode.INSERT, KeyboardTouch.Insert ),
        new VirtualToTouch(VirtualKeyCode.DELETE, KeyboardTouch.Delete ),
        new VirtualToTouch(VirtualKeyCode.HELP, KeyboardTouch.Help ),
        new VirtualToTouch(VirtualKeyCode.VK_0, KeyboardTouch.Alpha0 ),
        new VirtualToTouch(VirtualKeyCode.VK_1, KeyboardTouch.Alpha1 ),
        new VirtualToTouch(VirtualKeyCode.VK_2, KeyboardTouch.Alpha2 ),
        new VirtualToTouch(VirtualKeyCode.VK_3, KeyboardTouch.Alpha3 ),
        new VirtualToTouch(VirtualKeyCode.VK_4, KeyboardTouch.Alpha4 ),
        new VirtualToTouch(VirtualKeyCode.VK_5, KeyboardTouch.Alpha5 ),
        new VirtualToTouch(VirtualKeyCode.VK_6, KeyboardTouch.Alpha6 ),
        new VirtualToTouch(VirtualKeyCode.VK_7, KeyboardTouch.Alpha7 ),
        new VirtualToTouch(VirtualKeyCode.VK_8, KeyboardTouch.Alpha8 ),
        new VirtualToTouch(VirtualKeyCode.VK_9, KeyboardTouch.Alpha9 ),
        new VirtualToTouch(VirtualKeyCode.VK_A, KeyboardTouch.A ),
        new VirtualToTouch(VirtualKeyCode.VK_B, KeyboardTouch.B ),
        new VirtualToTouch(VirtualKeyCode.VK_C, KeyboardTouch.C ),
        new VirtualToTouch(VirtualKeyCode.VK_D, KeyboardTouch.D ),
        new VirtualToTouch(VirtualKeyCode.VK_E, KeyboardTouch.E ),
        new VirtualToTouch(VirtualKeyCode.VK_F, KeyboardTouch.F ),
        new VirtualToTouch(VirtualKeyCode.VK_G, KeyboardTouch.G ),
        new VirtualToTouch(VirtualKeyCode.VK_H, KeyboardTouch.H ),
        new VirtualToTouch(VirtualKeyCode.VK_I, KeyboardTouch.I ),
        new VirtualToTouch(VirtualKeyCode.VK_J, KeyboardTouch.J ),
        new VirtualToTouch(VirtualKeyCode.VK_K, KeyboardTouch.K ),
        new VirtualToTouch(VirtualKeyCode.VK_L, KeyboardTouch.L ),
        new VirtualToTouch(VirtualKeyCode.VK_M, KeyboardTouch.M ),
        new VirtualToTouch(VirtualKeyCode.VK_N, KeyboardTouch.N ),
        new VirtualToTouch(VirtualKeyCode.VK_O, KeyboardTouch.O ),
        new VirtualToTouch(VirtualKeyCode.VK_P, KeyboardTouch.P ),
        new VirtualToTouch(VirtualKeyCode.VK_Q, KeyboardTouch.Q ),
        new VirtualToTouch(VirtualKeyCode.VK_R, KeyboardTouch.R ),
        new VirtualToTouch(VirtualKeyCode.VK_S, KeyboardTouch.S ),
        new VirtualToTouch(VirtualKeyCode.VK_T, KeyboardTouch.T ),
        new VirtualToTouch(VirtualKeyCode.VK_U, KeyboardTouch.U ),
        new VirtualToTouch(VirtualKeyCode.VK_V, KeyboardTouch.V ),
        new VirtualToTouch(VirtualKeyCode.VK_W, KeyboardTouch.W ),
        new VirtualToTouch(VirtualKeyCode.VK_X, KeyboardTouch.X ),
        new VirtualToTouch(VirtualKeyCode.VK_Y, KeyboardTouch.Y ),
        new VirtualToTouch(VirtualKeyCode.VK_Z, KeyboardTouch.Z ),
        new VirtualToTouch(VirtualKeyCode.LWIN, KeyboardTouch.LeftWindow ),
        new VirtualToTouch(VirtualKeyCode.RWIN, KeyboardTouch.RightWindow ),
        new VirtualToTouch(VirtualKeyCode.APPS, KeyboardTouch.Application ),
        new VirtualToTouch(VirtualKeyCode.SLEEP, KeyboardTouch.Sleep ),
        new VirtualToTouch(VirtualKeyCode.NUMPAD0, KeyboardTouch.NumPad0 ),
        new VirtualToTouch(VirtualKeyCode.NUMPAD1, KeyboardTouch.NumPad1 ),
        new VirtualToTouch(VirtualKeyCode.NUMPAD2, KeyboardTouch.NumPad2 ),
        new VirtualToTouch(VirtualKeyCode.NUMPAD3, KeyboardTouch.NumPad3 ),
        new VirtualToTouch(VirtualKeyCode.NUMPAD4, KeyboardTouch.NumPad4 ),
        new VirtualToTouch(VirtualKeyCode.NUMPAD5, KeyboardTouch.NumPad5 ),
        new VirtualToTouch(VirtualKeyCode.NUMPAD6, KeyboardTouch.NumPad6 ),
        new VirtualToTouch(VirtualKeyCode.NUMPAD7, KeyboardTouch.NumPad7 ),
        new VirtualToTouch(VirtualKeyCode.NUMPAD8, KeyboardTouch.NumPad8 ),
        new VirtualToTouch(VirtualKeyCode.NUMPAD9, KeyboardTouch.NumPad9 ),
        new VirtualToTouch(VirtualKeyCode.MULTIPLY, KeyboardTouch.NumPadMultiply ),
        new VirtualToTouch(VirtualKeyCode.ADD, KeyboardTouch.NumPadAdd ),
        new VirtualToTouch(VirtualKeyCode.SEPARATOR, KeyboardTouch.Unkown ),
        new VirtualToTouch(VirtualKeyCode.SUBTRACT, KeyboardTouch.NumPadSubstract ),
        new VirtualToTouch(VirtualKeyCode.DECIMAL, KeyboardTouch.NumPadDecimal ),
        new VirtualToTouch(VirtualKeyCode.DIVIDE, KeyboardTouch.NumPadDivide ),
        new VirtualToTouch(VirtualKeyCode.F1, KeyboardTouch.F1 ),
        new VirtualToTouch(VirtualKeyCode.F2, KeyboardTouch.F2 ),
        new VirtualToTouch(VirtualKeyCode.F3, KeyboardTouch.F3),
        new VirtualToTouch(VirtualKeyCode.F4, KeyboardTouch.F4 ),
        new VirtualToTouch(VirtualKeyCode.F5, KeyboardTouch.F5 ),
        new VirtualToTouch(VirtualKeyCode.F6, KeyboardTouch.F6 ),
        new VirtualToTouch(VirtualKeyCode.F7, KeyboardTouch.F7 ),
        new VirtualToTouch(VirtualKeyCode.F8, KeyboardTouch.F8 ),
        new VirtualToTouch(VirtualKeyCode.F9, KeyboardTouch.F9 ),
        new VirtualToTouch(VirtualKeyCode.F10, KeyboardTouch.F10 ),
        new VirtualToTouch(VirtualKeyCode.F11, KeyboardTouch.F11 ),
        new VirtualToTouch(VirtualKeyCode.F12, KeyboardTouch.F12 ),
        new VirtualToTouch(VirtualKeyCode.F13, KeyboardTouch.F13 ),
        new VirtualToTouch(VirtualKeyCode.F14, KeyboardTouch.F14 ),
        new VirtualToTouch(VirtualKeyCode.F15, KeyboardTouch.F15 ),
        new VirtualToTouch(VirtualKeyCode.F16, KeyboardTouch.F16 ),
        new VirtualToTouch(VirtualKeyCode.F17, KeyboardTouch.F17 ),
        new VirtualToTouch(VirtualKeyCode.F18, KeyboardTouch.F18 ),
        new VirtualToTouch(VirtualKeyCode.F19, KeyboardTouch.F19 ),
        new VirtualToTouch(VirtualKeyCode.F20, KeyboardTouch.F20 ),
        new VirtualToTouch(VirtualKeyCode.F21, KeyboardTouch.F21 ),
        new VirtualToTouch(VirtualKeyCode.F22, KeyboardTouch.F22 ),
        new VirtualToTouch(VirtualKeyCode.F23, KeyboardTouch.F23 ),
        new VirtualToTouch(VirtualKeyCode.F24, KeyboardTouch.F24 ),
        new VirtualToTouch(VirtualKeyCode.NUMLOCK, KeyboardTouch.NumLock ),
        new VirtualToTouch(VirtualKeyCode.SCROLL, KeyboardTouch.ScrollLock ),
        new VirtualToTouch(VirtualKeyCode.LSHIFT, KeyboardTouch.LeftShift ),
        new VirtualToTouch(VirtualKeyCode.RSHIFT, KeyboardTouch.RightShift ),
        new VirtualToTouch(VirtualKeyCode.LCONTROL, KeyboardTouch.LeftControl ),
        new VirtualToTouch(VirtualKeyCode.RCONTROL, KeyboardTouch.RightControl ),
        new VirtualToTouch(VirtualKeyCode.LMENU, KeyboardTouch.LeftAlt ),
        new VirtualToTouch(VirtualKeyCode.RMENU, KeyboardTouch.RightAlt ),
        new VirtualToTouch(VirtualKeyCode.BROWSER_BACK, KeyboardTouch.BrowserBack ),
        new VirtualToTouch(VirtualKeyCode.BROWSER_FORWARD, KeyboardTouch.BrowserForward ),
        new VirtualToTouch(VirtualKeyCode.BROWSER_REFRESH, KeyboardTouch.BrowserRefresh ),
        new VirtualToTouch(VirtualKeyCode.BROWSER_STOP, KeyboardTouch.BrowserStop ),
        new VirtualToTouch(VirtualKeyCode.BROWSER_SEARCH, KeyboardTouch.BrowserSearch ),
        new VirtualToTouch(VirtualKeyCode.BROWSER_FAVORITES, KeyboardTouch.BrowserFavorites ),
        new VirtualToTouch(VirtualKeyCode.BROWSER_HOME, KeyboardTouch.BrowserHome ),
        new VirtualToTouch(VirtualKeyCode.VOLUME_MUTE, KeyboardTouch.VolumeMute ),
        new VirtualToTouch(VirtualKeyCode.VOLUME_DOWN, KeyboardTouch.VolumeDown ),
        new VirtualToTouch(VirtualKeyCode.VOLUME_UP, KeyboardTouch.VolumeUp ),
        new VirtualToTouch(VirtualKeyCode.MEDIA_NEXT_TRACK, KeyboardTouch.NextTrack ),
        new VirtualToTouch(VirtualKeyCode.MEDIA_PREV_TRACK, KeyboardTouch.PreviousTrack),
        new VirtualToTouch(VirtualKeyCode.MEDIA_STOP, KeyboardTouch.StopTrack ),
        new VirtualToTouch(VirtualKeyCode.MEDIA_PLAY_PAUSE, KeyboardTouch.Play ),
        new VirtualToTouch(VirtualKeyCode.LAUNCH_MAIL, KeyboardTouch.LaunchMail ),
        new VirtualToTouch(VirtualKeyCode.LAUNCH_MEDIA_SELECT, KeyboardTouch.Select ),
        new VirtualToTouch(VirtualKeyCode.LAUNCH_APP1, KeyboardTouch.LaunchApplicationOne ),
        new VirtualToTouch(VirtualKeyCode.LAUNCH_APP2, KeyboardTouch.LaunchApplicationTwo ),
        new VirtualToTouch(VirtualKeyCode.OEM_1, KeyboardTouch.WinUS_OEM_1_SemiColon ),
        new VirtualToTouch(VirtualKeyCode.OEM_PLUS, KeyboardTouch.WinUS_OEM_PLUS ),
        new VirtualToTouch(VirtualKeyCode.OEM_COMMA, KeyboardTouch.WinUS_OEM_Comma ),
        new VirtualToTouch(VirtualKeyCode.OEM_MINUS, KeyboardTouch.WinUS_OEM_Minus ),
        new VirtualToTouch(VirtualKeyCode.OEM_PERIOD, KeyboardTouch.WinUS_OEM_Period ),
        new VirtualToTouch(VirtualKeyCode.OEM_2, KeyboardTouch.WinUS_OEM_2_Slash ),
        new VirtualToTouch(VirtualKeyCode.OEM_3, KeyboardTouch.WinUS_OEM_3_GraveAccent ),
        new VirtualToTouch(VirtualKeyCode.OEM_4, KeyboardTouch.WinUS_OEM_4_LeftBracket ),
        new VirtualToTouch(VirtualKeyCode.OEM_5, KeyboardTouch.WinUS_OEM_5_BackSlash ),
        new VirtualToTouch(VirtualKeyCode.OEM_6, KeyboardTouch.WinUS_OEM_6_RightBlacket ),
        new VirtualToTouch(VirtualKeyCode.OEM_7, KeyboardTouch.WinUS_OEM_7_Quote ),
        new VirtualToTouch(VirtualKeyCode.OEM_8, KeyboardTouch.Unkown ),
        new VirtualToTouch(VirtualKeyCode.OEM_102, KeyboardTouch.WinUS_OEM_102_BackSlash ),
        new VirtualToTouch(VirtualKeyCode.PROCESSKEY, KeyboardTouch.Unkown ),
        new VirtualToTouch(VirtualKeyCode.PACKET, KeyboardTouch.Unkown ),
        new VirtualToTouch(VirtualKeyCode.ATTN, KeyboardTouch.Unkown ),
        new VirtualToTouch(VirtualKeyCode.CRSEL, KeyboardTouch.Unkown ),
        new VirtualToTouch(VirtualKeyCode.EXSEL, KeyboardTouch.Unkown ),
        new VirtualToTouch(VirtualKeyCode.EREOF, KeyboardTouch.Unkown ),
        new VirtualToTouch(VirtualKeyCode.PLAY, KeyboardTouch.Play ),
        new VirtualToTouch(VirtualKeyCode.ZOOM, KeyboardTouch.Zoom ),
        new VirtualToTouch(VirtualKeyCode.NONAME, KeyboardTouch.Unkown ),
        new VirtualToTouch(VirtualKeyCode.PA1, KeyboardTouch.Unkown ),
        new VirtualToTouch(VirtualKeyCode.OEM_CLEAR, KeyboardTouch.Clear )
    };

    public static void ConvertWindowVirtualKeyCodesToTouch(VirtualKeyCode virtualKeyCodeout, out KeyboardTouch key, out bool isConvertable)
    {
        key = KeyboardTouch.Unkown;
        isConvertable = false;
        KeyboardTouch[] touchs =  VIRTUALTOTOUCH.Where(k => virtualKeyCodeout == k.m_windowCode && k.m_touch!=KeyboardTouch.Unkown).Select(k=>k.m_touch).ToArray();
        if (touchs.Length <= 0)
            return;
        isConvertable = true;
        key = touchs[0];
    }
    public static void ConvertWindowVirtualKeyCodesToTouch(VirtualKeyCode virtualKeyCodeout, out KeyboardTouch [] keys, out bool isConvertable)
    {
       
        isConvertable = false;
        keys = VIRTUALTOTOUCH.Where(k => virtualKeyCodeout == k.m_windowCode).Select(k => k.m_touch).ToArray();
        if (keys.Length <= 0)
            return;
        isConvertable = true;
    }
    public static WindowKeyboardLanguage keyboardLanguage = WindowKeyboardLanguage.FancaisBelgique;
    public static void ConvertTouchToWindowVirtualKeyCodes(KeyboardTouch key, out VirtualKeyCode virtualKeyCode, out bool isConvertable)
    {
        virtualKeyCode = VirtualKeyCode.NONCONVERT;
        isConvertable = false;
        VirtualKeyCode[] keys = TOUCHTOWINDOWKEY.Where(k => key == k.m_touch).Select(k => k.m_virtualKey).ToArray();
        if (keys.Length <= 0)
            return;
        isConvertable = true;
        virtualKeyCode = keys[0];
    }
    







    #region GROUP OF KEYS
    public static KeyboardTouch[] GetAllTouches()
    {
        return KeystrokeUtility.GetEnumList<KeyboardTouch>().ToArray();
    }
    public static KeyboardTouch[] GetKeyPadNumbers()
    {
        KeyboardTouch[] keypadNumbers = new KeyboardTouch[] {
        KeyboardTouch.NumPad0,
            KeyboardTouch.NumPad1,
            KeyboardTouch.NumPad2,
            KeyboardTouch.NumPad3,
            KeyboardTouch.NumPad4,
            KeyboardTouch.NumPad5,
            KeyboardTouch.NumPad6,
            KeyboardTouch.NumPad7,
            KeyboardTouch.NumPad8,
            KeyboardTouch.NumPad9
        };
        return keypadNumbers;
    }
    public static KeyboardTouch[] GetAlphaNumbers()
    {
        KeyboardTouch[] keypadNumbers = new KeyboardTouch[] {
        KeyboardTouch.Alpha0,
            KeyboardTouch.Alpha1,
            KeyboardTouch.Alpha2,
            KeyboardTouch.Alpha3,
            KeyboardTouch.Alpha4,
            KeyboardTouch.Alpha5,
            KeyboardTouch.Alpha6,
            KeyboardTouch.Alpha7,
            KeyboardTouch.Alpha8,
            KeyboardTouch.Alpha9};
        return keypadNumbers;
    }
    public static KeyboardTouch[] GetAlphaCharacter()
    {
        List<KeyboardTouch> keys = new List<KeyboardTouch>();

        keys.Add(KeyboardTouch.A);
        keys.Add(KeyboardTouch.B);
        keys.Add(KeyboardTouch.C);
        keys.Add(KeyboardTouch.D);
        keys.Add(KeyboardTouch.E);
        keys.Add(KeyboardTouch.F);
        keys.Add(KeyboardTouch.G);
        keys.Add(KeyboardTouch.H);
        keys.Add(KeyboardTouch.I);
        keys.Add(KeyboardTouch.J);
        keys.Add(KeyboardTouch.K);
        keys.Add(KeyboardTouch.L);
        keys.Add(KeyboardTouch.M);
        keys.Add(KeyboardTouch.N);
        keys.Add(KeyboardTouch.O);
        keys.Add(KeyboardTouch.P);
        keys.Add(KeyboardTouch.Q);
        keys.Add(KeyboardTouch.R);
        keys.Add(KeyboardTouch.S);
        keys.Add(KeyboardTouch.T);
        keys.Add(KeyboardTouch.U);
        keys.Add(KeyboardTouch.V);
        keys.Add(KeyboardTouch.W);
        keys.Add(KeyboardTouch.X);
        keys.Add(KeyboardTouch.Y);
        keys.Add(KeyboardTouch.Z);

        return keys.ToArray();
    }
    #endregion


}
#region Enumerations
public enum KeyboardTouch : int
{


    //Unknow
    None,
    Unkown,

    //Alpha
    A,
    B,
    C,
    D,
    E,
    F,
    G,
    H,
    I,
    J,
    K,
    L,
    M,
    N,
    O,
    P,
    Q,
    R,
    S,
    T,
    U,
    V,
    W,
    X,
    Y,
    Z,

    //Numpad
    NumLock,
    NumPad0,
    NumPad1,
    NumPad2,
    NumPad3,
    NumPad4,
    NumPad5,
    NumPad6,
    NumPad7,
    NumPad8,
    NumPad9,
    NumPadDecimal,
    NumPadDivide,
    NumPadMultiply,
    NumPadSubstract,
    NumPadAdd,
    NumPadEnter,
    NumPadPeriod,
    NumPadEquals,

    // Numpad Plus
    NumPadClear, AllClear, AC,
    NumPadClearEntry, ClearEntry, CE,
    NumpadMemoryAdd, MA,
    NumpadMemoryClear, MC,
    NumpadMemoryRecall, MR,
    NumpadMemoryStore, MS,
    NumpadMemorySubtract, MSub,

    //AlphaNumeric
    Alpha0,
    Alpha1,
    Alpha2,
    Alpha3,
    Alpha4,
    Alpha5,
    Alpha6,
    Alpha7,
    Alpha8,
    Alpha9,

    //Arrow
    Up,
    Down,
    Right,
    Left,

    //Control Pad
    PageUp,
    PageDown,
    Home,
    End,
    Insert,
    Delete, DEL,

    //Function
    F1,
    F2,
    F3,
    F4,
    F5,
    F6,
    F7,
    F8,
    F9,
    F10,
    F11,
    F12,
    F13,
    F14,
    F15,
    F16,
    F17,
    F18,
    F19,
    F20,
    F21,
    F22,
    F23,
    F24,


    //Special Character
    WinUS_OEM_Comma,
    WinUS_OEM_Period,
    WinUS_OEM_Minus,
    WinUS_OEM_PLUS,
    WinUS_OEM_102_BackSlash,
    WinUS_OEM_1_SemiColon,
    WinUS_OEM_2_Slash,
    WinUS_OEM_3_GraveAccent,
    WinUS_OEM_4_LeftBracket,
    WinUS_OEM_5_BackSlash,
    WinUS_OEM_6_RightBlacket,
    WinUS_OEM_7_Quote,

    // Border Control
    Space, SP,
    CapsLock,
    Backspace, BS,
    Tab, HT,

    Shift,
    LeftShift,
    RightShift,

    Control,
    LeftControl,
    RightControl,

    Menu,
    Alt,
    AltGr,
    LeftAlt,
    RightAlt,
    ContextMenu,

    Application,

    //Control Pad Function
    Break,
    ScrollLock,
    Print,
    PrintScreen,
    Help,
    SystemRequest,



    //Mouse Button

    MouseLeft,
    MouseRight,
    MouseMiddle,
    MouseSupp1,
    MouseSupp2,
    MouseSupp3,



    //Legacy Button

    EndOfText, ETX,
    Escape, ESC,
    Null, NUL,
    InformationSeparatorOne, US,
    InformationSeparatorTwo, RS,
    InformationSeparatorThree, GS,
    InformationSeparatorFour, FS,
    LineFeed, LF,
    Return, Enter, CarriageReturn, CR,




    //Media Function
    Eject,
    LaunchApplicationOne,
    LaunchApplicationTwo,
    LaunchMail,
    Sleep, Power, WakeUp,


    Select,
    Execute,
    Clear,
    NextTrack,
    PreviousTrack,
    StopTrack,
    Play,
    Pause,
    VolumeMute,
    VolumeDown,
    VolumeUp,
    Zoom,

    BrowserBack,
    BrowserForward,
    BrowserFavorites,
    BrowserHome,
    BrowserRefresh,
    BrowserSearch,
    BrowserStop,


    // OS DEPENDANT
    Meta,
    MetaLeft,
    MetaRight,
    LeftWindow,
    RightWindow,
    LeftCommand,
    RightCommand,


    //Language
    Convert,
    KanaMode,
    Lang1,
    Lang2,
    Lang3,
    Lang4,
    Lang5,
    NonConvert
}
public enum KeyboardMapping { Base, Shit, Control, AltGR, ShitControl, ShitAltGr }
public enum WindowWrapper
{
    Shift,
    Control,
    Window,
    Alt,
    AltGr,
    LeftWindow,
    RightWindow,
    LeftShift,
    RightShift,
    LeftControl,
    RightControl
}
public enum WindowKeyboardLanguage
{
    FancaisBelgique,
    EnglishUnitedStates
}
#endregion

