using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User32BoardcastUtilityMono : MonoBehaviour
{
}

public class User32BoardcastUtilityToThread {



    public static void SendKey(IntPtrWrapGet activeProcessId,
      User32PostMessageKeyEnum keyToSend)
    {
        Send(activeProcessId, keyToSend, User32PressionType.Press);
        Send(activeProcessId, keyToSend, User32PressionType.Release);
    }



    public static void Send(IntPtrWrapGet activeProcessId,
        User32PostMessageKeyEnum keyToSend,
        User32PressionType pressType)
    {
        ThreadQueueDateTimeCall.Instance.AddFromNowInMs(0, () =>
        {
            TargetChildrenProcessIntPtr p = new TargetChildrenProcessIntPtr();
            p.SetAsInt(activeProcessId.GetAsInt());
            User32KeyStrokeManager.SendKeyPostMessage(p,
               keyToSend, pressType);
        });
    }
    public static void Send(int activeProcessId,
       User32KeyboardStrokeCodeEnum keyToSend,
       User32PressionType pressType) => Send(activeProcessId, keyToSend, pressType, 1);
    public static void Send(int activeProcessId,
        User32KeyboardStrokeCodeEnum keyToSend,
        User32PressionType pressType,
        int timeToFocusInMs=150)
    {
        ThreadQueueDateTimeCall.Instance.AddFromNowInMs(0, () =>
        {
            WindowIntPtrUtility.SetForegroundWindow(activeProcessId);
        }); 
        ThreadQueueDateTimeCall.Instance.AddFromNowInMs(timeToFocusInMs, () =>
        {
            User32KeyStrokeManager.SendKeyStroke(keyToSend, pressType);
        });
    }

    public static void HeavyTryParseAndSendToProcesses( string processNameId, User32PostMessageKeyEnum whatToCast)
    {
       
            ProcessesAccessInScene.Instance.FetchListOfProcessesBasedOnName(processNameId,
                out GroupOfProcessesParentToChildrens info, false);

            for (int i = 0; i < info.m_processesAndChildrens.Count; i++)
            {
                WindowIntPtrUtility.FetchFirstChildrenThatHasDimension(
                    info.m_processesAndChildrens[i].m_parent,
                    out bool foundchild, out IntPtrWrapGet target);
                TryParseAndSendToProcess(target, whatToCast);
            }
        
    }
    public static void HeavyTryParseAndSendToProcesses(string processNameId, string whatToCast)
    {
        StringToUser32PostMessageKeyEnum.Get(whatToCast, out bool found,
               out User32PostMessageKeyEnum tocast);
        if (found) {
            HeavyTryParseAndSendToProcesses(processNameId, tocast);
        }
    }


    public static void TryParseAndSendToProcess(IntPtrWrapGet activeProcessId, string whatToCast)
    {
        StringToUser32PostMessageKeyEnum.Get(whatToCast, out bool found,
               out User32PostMessageKeyEnum tocast);
        if (found)
        {
            TryParseAndSendToProcess(activeProcessId, tocast);
        }
    }
    public static void TryParseAndSendToProcess(IntPtrWrapGet activeProcessId, User32PostMessageKeyEnum whatToCast)
    {
        
            User32BoardcastUtilityToThread.Send(activeProcessId, whatToCast, User32PressionType.Press);
            User32BoardcastUtilityToThread.Send(activeProcessId, whatToCast, User32PressionType.Release);
        
    }

    internal static void CopyPastChatText(IntPtrWrapGet processId, string text)
    {
        IntPtrTemp t = new IntPtrTemp(processId);

        ThreadQueueDateTimeCall.Instance.AddFromNowInMs(0, () =>
        {
            WindowIntPtrUtility.SetForegroundWindow(t);
        });
        ThreadQueueDateTimeCall.Instance.AddFromNowInMs(80, () =>
        {
            User32KeyStrokeManager.SendKeyPostMessage(t,
               User32PostMessageKeyEnum.VK_RETURN,
               User32PressionType.Press);
            User32KeyStrokeManager.SendKeyPostMessage(t,
                User32PostMessageKeyEnum.VK_RETURN,
                User32PressionType.Release
                );

        });
        ThreadQueueDateTimeCall.Instance.AddFromNowInMs(150, () =>
        {
            User32ClipboardUtility.CopyTextToClipboard(text,false);
        });
        ThreadQueueDateTimeCall.Instance.AddFromNowInMs(250, () =>
        {

            User32KeyStrokeManager.SendKeyPostMessage(t,
                  User32PostMessageKeyEnum.VK_LCONTROL,
                  User32PressionType.Press);
            User32KeyStrokeManager.SendKeyPostMessage(t,
               User32PostMessageKeyEnum.VK_V,
               User32PressionType.Press);
            User32KeyStrokeManager.SendKeyPostMessage(t,
                User32PostMessageKeyEnum.VK_V,
                User32PressionType.Release
                );
            User32KeyStrokeManager.SendKeyPostMessage(t,
                User32PostMessageKeyEnum.VK_LCONTROL,
                User32PressionType.Release
                );
        }); 
        ThreadQueueDateTimeCall.Instance.AddFromNowInMs(300, () =>
        {
            User32KeyStrokeManager.SendKeyPostMessage(t,
                   User32PostMessageKeyEnum.VK_BACK,
                   User32PressionType.Press); 
            User32KeyStrokeManager.SendKeyPostMessage(t,
                 User32PostMessageKeyEnum.VK_BACK,
                 User32PressionType.Press);
            User32KeyStrokeManager.SendKeyPostMessage(t,
                User32PostMessageKeyEnum.VK_RETURN,
                User32PressionType.Press); 
            User32KeyStrokeManager.SendKeyPostMessage(t,
                User32PostMessageKeyEnum.VK_RETURN,
                User32PressionType.Release
                );
        });

    }
}