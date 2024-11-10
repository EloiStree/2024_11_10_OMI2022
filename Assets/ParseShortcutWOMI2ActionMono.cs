using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParseShortcutWOMI2ActionMono : MonoBehaviour
{
    public string m_receivedString;



    public List<TimedAction> m_actionsFound;
    public List<AlternativeString> m_alternativeText;

    
    public string[] m_shouldHaveSpaceAround = new string[] {
    "(",")"
    };
    public string[] m_shouldHaveSpaceInFront = new string[] {
    "⌛","⏰","🖱","🐁", "🖱","💾","✂","📋","🗁","🗀"
    };

    [System.Serializable]
    public class AlternativeString
    {
        public string m_wantedText;
        public string m_alternativeText;
    }

    [System.Serializable]
    public class TimedAction {
        public long milliseconds;
        public DateTime m_when;
        public string m_actionDescription;
        public IUser32Action m_action;
    }

    public void ParseShortcutToActions(string shortcut) {
        shortcut = " " + shortcut + " ";
        shortcut = shortcut.Replace("(", " ( ").Replace(")", " ) ");
        shortcut = shortcut.Replace("⌛", " ⌛");
        shortcut = shortcut.Replace("⏰", " ⏰");
        shortcut = shortcut.Replace("🖱", " ⏰");

    }
}

public class MyUnicodeChar
{
    // GIST IF IT IS MISSING: https://gist.github.com/EloiStree/37b8e4d02b144284bf729c0491e614c8
    //↕↓↑
    public static  char press = '↓';
    public static  char stroke = '↕';
    public static  char release = '↑';
    public static  char releaseThenPress = '⇅';
    public static  char pressThenRelease = '⇵';
    public static  string youRock="🤘";
	public static  char watchTime = '⏰';
    public static  char split = '裂';
    public static  char sandTime = '⌛';
    public static string mouse ="🖱";
	public static string mouseType2 ="🖯";
	public static string mouseType3 ="🖰";
	public static string mouseType4 ="🐁";
	public static string textDocument ="🖹";
	public static  char arrowRight = '→';
    public static  char arrowLeft = '←';
    public static  char arrowUp = '↑';
    public static  char arrowDown = '↓';
    public static string floppydisk ="💾";

	public static String arrows()
    {
        return "" + press + stroke + release;
    }

    public static String arrowslrtd()
    {
        return "" + arrowLeft + arrowRight + arrowUp + arrowDown;
    }
}

