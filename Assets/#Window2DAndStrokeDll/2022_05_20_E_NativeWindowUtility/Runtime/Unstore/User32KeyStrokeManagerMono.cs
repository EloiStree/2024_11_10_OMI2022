using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User32KeyStrokeManagerMono : MonoBehaviour
{

    
}

public class User32KeyStrokeManager 
{
    public static void SendKeyPostMessage(
        in IntPtrWrapGet pointer,
        in User32PostMessageKeyEnum key,
        in  User32PressionType pressionType, 
        in bool usePost=true)
    {
        if (User32PressionType.Press == pressionType)
            SendKeyMessageToWindowsDLL.SendKeyDown(in key, pointer, in usePost);
        else
            SendKeyMessageToWindowsDLL.SendKeyUp(in key, pointer, in usePost);
    }
    public static void SendKeyStroke(
        in IntPtrWrapGet pointer,
        in User32KeyboardStrokeCodeEnum key,
        in User32PressionType pressionType)
    {
        WindowIntPtrUtility.SetForegroundWindow(pointer.GetAsIntPtr());
        if (User32PressionType.Press == pressionType)
            SendKeyStrokeToComputerDLL.KeyPress(key);
        else
            SendKeyStrokeToComputerDLL.KeyRelease(key);
    }
    public static void SendKeyStroke(
       in User32KeyboardStrokeCodeEnum key,
       in User32PressionType pressionType)
    {
        if (User32PressionType.Press == pressionType)
            SendKeyStrokeToComputerDLL.KeyPress(key);
        else
            SendKeyStrokeToComputerDLL.KeyRelease(key);
    }

    public static User32KeyboardStrokeCodeEnum LeftControl = User32KeyboardStrokeCodeEnum.LCONTROL;
    public static User32KeyboardStrokeCodeEnum V = User32KeyboardStrokeCodeEnum.KEY_V;
    public static User32KeyboardStrokeCodeEnum C = User32KeyboardStrokeCodeEnum.KEY_C;
    public static User32KeyboardStrokeCodeEnum X = User32KeyboardStrokeCodeEnum.KEY_X;
    public static User32PressionType PRESS = User32PressionType.Press;
    public static User32PressionType RELEASE= User32PressionType.Release;

    public static void Past()
    {
        User32KeyStrokeManager.SendKeyStroke(in LeftControl, in PRESS);
        User32KeyStrokeManager.SendKeyStroke(in C, in PRESS);
        User32KeyStrokeManager.SendKeyStroke(in C, in RELEASE);
        User32KeyStrokeManager.SendKeyStroke(in LeftControl, in RELEASE);
    }
    public static void Copy()
    {
        User32KeyStrokeManager.SendKeyStroke(in LeftControl, in PRESS);
        User32KeyStrokeManager.SendKeyStroke(in V, in PRESS);
        User32KeyStrokeManager.SendKeyStroke(in V, in RELEASE);
        User32KeyStrokeManager.SendKeyStroke(in LeftControl, in RELEASE);
    }
    public static void Cut()
    {
        User32KeyStrokeManager.SendKeyStroke(in LeftControl, in PRESS);
        User32KeyStrokeManager.SendKeyStroke(in X, in PRESS);
        User32KeyStrokeManager.SendKeyStroke(in X, in RELEASE);
        User32KeyStrokeManager.SendKeyStroke(in LeftControl, in RELEASE);
    }
    public static void CopyPast()
    {
        Copy();
        Past();
    }
}





