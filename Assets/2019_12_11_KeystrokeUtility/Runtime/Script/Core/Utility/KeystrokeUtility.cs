using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using WindowsInput.Native;
public class KeystrokeUtility  {

    public static bool IsASCII(char character) {
        return KeyBindingTable.IsCharASCII(character);
    }
    public static List<KeyCode> GetUnityKeyCodes()
    { return GetEnumList<KeyCode>(); }
    public static List<VirtualKeyCode> GetVirtualKeyCode()
    { return GetEnumList<VirtualKeyCode>(); }
    public static char[] GetAllASCIIAsChar()
    { return KeyBindingTable.GetAllASCII(); }
    public static List<KeyboardTouch> GetKeyboardTouches()
    { return GetEnumList<KeyboardTouch>(); }
  
    public static List<T> GetEnumList<T>() where T : struct, IConvertible
    {
        return Enum.GetValues(typeof(T)).OfType<T>().ToList();
    }
    public static List<string> GetEnumNamesList<T>() where T : struct, IConvertible
    {
        return Enum.GetNames(typeof(T)).ToList();
    }

    public static void BrutalSearchKeyboardInScene(out IKeyboardWrite m_keyboard)
    {
        m_keyboard = null;
        GameObject[] allObjs = GameObject.FindObjectsOfType<GameObject>();
        for (int i = 0; i < allObjs.Length; i++)
        {
            m_keyboard = allObjs[i].GetComponent<IKeyboardWrite>();
            if (m_keyboard != null)
                return;
        }
    }
    public static void BrutalSearchKeyboardInScene(out IKeyboardRead m_keyboard)
    {
        m_keyboard = null;
        GameObject[] allObjs = GameObject.FindObjectsOfType<GameObject>();
        for (int i = 0; i < allObjs.Length; i++)
        {
            m_keyboard = allObjs[i].GetComponent<IKeyboardRead>();
            if (m_keyboard != null)
                return;
        }
    }
}


