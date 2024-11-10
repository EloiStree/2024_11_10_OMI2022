using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;



public class User32ActionDefaultCatchToThreadExecution : User32ActionAbstractCatchToExecuteMono
{
    public bool m_useDebugHistory;
    public Eloi.StringClampHistory m_receivedHistory;
    public Eloi.StringClampHistory m_notManageHistory;
    public ThreadQueueDateTimeCallMono m_threadUsedForTime;

    public String[] m_useableClassInProject;

    private void Awake()
    {
        m_receivedHistory.Clear();
        m_notManageHistory.Clear();
    }
    private void OnValidate()
    {
        m_useableClassInProject = E_ReflectionCS.GetEnumerableOfInterfaceThroughAssemblies<IUser32Action>().Select(k=>k.ToString()).ToArray();
    }

    public AccessWindowZoneWithoutStressOnUser32 m_accessWindowInfo;

    public void GetWindowInfo(IntPtrWrapGet process, out DeductedInfoOfWindowSizeWithSource info) {

        if (m_accessWindowInfo != null)
            m_accessWindowInfo.GetWindowInfo(in process, out info);
        else FetchWindowInfoUtility.Get(process, out info);
    }

    public override void TryToExecute(int milliseconds, IUser32Action actionToExecute)
    {
        if (m_threadUsedForTime == null)
            m_threadUsedForTime = ThreadQueueDateTimeCall.Instance;
        m_threadUsedForTime.AddFromNowInMs(milliseconds, () => {
            TryToExecute(actionToExecute);
        });
    }
    public void TryToExecute(int milliseconds, Action actionToExecute)
    {
        if (m_threadUsedForTime == null)
            m_threadUsedForTime = ThreadQueueDateTimeCall.Instance;
        m_threadUsedForTime.AddFromNowInMs(milliseconds, () => {
            if (actionToExecute != null) actionToExecute.Invoke();
        });
    }

    public override void TryToExecute(DateTime specificTime, IUser32Action actionToExecute)
    {
        if (m_threadUsedForTime == null)
            m_threadUsedForTime = ThreadQueueDateTimeCall.Instance;
        m_threadUsedForTime.Add(new ThreadQueueDateTimeCallMono.DateTimeAction(specificTime, () => {
            TryToExecute(actionToExecute);
        }));
    }
    public void TryToExecute(DateTime specificTime, Action actionToExecute)
    {
        if (m_threadUsedForTime == null)
            m_threadUsedForTime = ThreadQueueDateTimeCall.Instance;
        m_threadUsedForTime.Add(new ThreadQueueDateTimeCallMono.DateTimeAction(specificTime, () => {
            if (actionToExecute != null) actionToExecute.Invoke();
        }));
    }

    public override void TryToExecute(IUser32Action actionToExecute)
    {
        if (m_useDebugHistory)
            m_receivedHistory.PushIn(actionToExecute.GetType().ToString());

        System.Type t = actionToExecute.GetType();
        
        if (t == typeof(Action_Real_SetCursorPosition_PX_LRTD))
        { Execute((Action_Real_SetCursorPosition_PX_LRTD)actionToExecute); }
       else  if (t == typeof(Action_Real_KeyInteraction))
        { Execute((Action_Real_KeyInteraction)actionToExecute); }

        else if (t == typeof(Action_Real_CursorInteraction))
        { Execute((Action_Real_CursorInteraction)actionToExecute); }

        else if (t == typeof(Action_Real_CursorClick))
        { Execute((Action_Real_CursorClick)actionToExecute); }

        else if (t == typeof(Action_Real_CursorHoldPression))
        { Execute((Action_Real_CursorHoldPression)actionToExecute); }
        else if (t == typeof(Action_Real_MoveCursor_PX_LRTD))
        { Execute((Action_Real_MoveCursor_PX_LRTD)actionToExecute); }
        else if (t == typeof(Action_Real_MoveCursor_PX_LRDT))
        { Execute((Action_Real_MoveCursor_PX_LRDT)actionToExecute); }

        else if (t == typeof(Action_Real_SetCursor_OverProcess_PX_LRTD))
        { Execute((Action_Real_SetCursor_OverProcess_PX_LRTD)actionToExecute); }
        else if (t == typeof(Action_Real_SetCursor_OverProcess_PCT_LRTD))
        { Execute((Action_Real_SetCursor_OverProcess_PCT_LRTD)actionToExecute); }




        else if (t == typeof(Action_SetClipboardText))
        { Execute((Action_SetClipboardText)actionToExecute); }
        else if (t == typeof(Action_Real_CutClipboard))
        { Execute((Action_Real_CutClipboard)actionToExecute); }
        else if (t == typeof(Action_Real_CopyClipboard))
        { Execute((Action_Real_CopyClipboard)actionToExecute); }
        else if (t == typeof(Action_Real_PastClipboard))
        { Execute((Action_Real_PastClipboard)actionToExecute); }
        else if (t == typeof(Action_SetClipboardText))
        { Execute((Action_SetClipboardText)actionToExecute); }

        else if (t == typeof(Action_User32_SetFocusWindow))
        { Execute((Action_User32_SetFocusWindow)actionToExecute); }



        else if (t == typeof(Action_Post_KeyInteraction))
        { Execute((Action_Post_KeyInteraction)actionToExecute); }

        else if (actionToExecute is Action_Post_CursorInteraction)
        { Execute((Action_Post_CursorInteraction)actionToExecute); }

        else if (actionToExecute is Action_Post_CursorClick)
        { Execute((Action_Post_CursorClick)actionToExecute); }

        else if (actionToExecute is Action_Post_CursorClickWithDelay)
        { Execute((Action_Post_CursorClickWithDelay)actionToExecute); }

        else if (actionToExecute is Action_Post_MoveCursor_PCT_LRTD)
        { Execute((Action_Post_MoveCursor_PCT_LRTD)actionToExecute); }

        else if (actionToExecute is Action_Post_MoveCursor_PX_LRTD)
        { Execute((Action_Post_MoveCursor_PX_LRTD)actionToExecute); }

        else if (actionToExecute is Action_Real_SetCursor_OverProcess_PX_LRTD)
        { Execute((Action_Real_SetCursor_OverProcess_PX_LRTD)actionToExecute); }

        else if (actionToExecute is Action_Real_SetCursor_OverProcess_PCT_LRTD)
        { Execute((Action_Real_SetCursor_OverProcess_PCT_LRTD)actionToExecute); }

        else if (actionToExecute is Action_ResizeCurrentWindowToSquareZone_PX_LRDT)
        { Execute((Action_ResizeCurrentWindowToSquareZone_PX_LRDT)actionToExecute); }
        else if (actionToExecute is Action_ResizeCurrentWindowToNative_PX_LRTD)
        { Execute((Action_ResizeCurrentWindowToNative_PX_LRTD)actionToExecute); }
        else if (actionToExecute is Action_ResizeProcessWindowToSquareZone_PX_LRDT)
        { Execute((Action_ResizeProcessWindowToSquareZone_PX_LRDT)actionToExecute); }
        else if (actionToExecute is Action_ResizeProcessWindowToNative_PX_LRTD)
        { Execute((Action_ResizeProcessWindowToNative_PX_LRTD)actionToExecute); }
        else if (actionToExecute is Action_ResizeProcessWindowAroundPoint_PX_LRTD)
        { Execute((Action_ResizeProcessWindowAroundPoint_PX_LRTD)actionToExecute); }
        else if (actionToExecute is Action_ResizeCurrentWindowAroundPoint_PX_LRTD)
        { Execute((Action_ResizeCurrentWindowAroundPoint_PX_LRTD)actionToExecute); }


        else if (actionToExecute is Action_Real_SetCursor_OverDisplayID_PCT_LRTD)
        { Execute((Action_Real_SetCursor_OverDisplayID_PCT_LRTD)actionToExecute); }
        else if (actionToExecute is Action_Real_SetCursor_OverDisplayName_PCT_LRTD)
        { Execute((Action_Real_SetCursor_OverDisplayName_PCT_LRTD)actionToExecute); }
        else if (actionToExecute is Action_Real_SetCursor_OverDisplayName_PX_LRTD)
        { Execute((Action_Real_SetCursor_OverDisplayName_PX_LRTD)actionToExecute); }
        else if (actionToExecute is Action_Real_SetCursor_OverDisplayID_PX_LRTD)
        { Execute((Action_Real_SetCursor_OverDisplayID_PX_LRTD)actionToExecute); }

        else if (actionToExecute is Action_ShowProcessId)
        { Execute((Action_ShowProcessId)actionToExecute); }

        else if (actionToExecute is Action_HideProcessId)
        { Execute((Action_HideProcessId)actionToExecute); }

        else { 
            if (m_useDebugHistory)
                m_notManageHistory.PushIn(actionToExecute.GetType().ToString());
        }
    }

    private void Execute(Action_Real_SetCursor_OverDisplayID_PCT_LRTD actionToExecute)
    {
        WindowMonitorsInformationUtility.SearchMonitorFromId(actionToExecute.m_displayID,
            out bool found,
            out WindowMonitorRef monitor);
        if (!found) return;
        monitor.HasDimension(out bool hasDimension);
        if (!hasDimension)
            return;
        monitor.GetNativeLeftRight(out int x);
        monitor.GetNativeTopDown(out int y);
        monitor.GetNativeHeight(out int h);
        monitor.GetNativeWidth(out int w);
        MouseOperations.SetCursorPosition(
            x + (int)(actionToExecute.m_screenRelative.m_xLeft2Right * (float)w),
            y + (int)(actionToExecute.m_screenRelative.m_yTop2Down * (float)h));
    }
    private void Execute(Action_Real_SetCursor_OverDisplayID_PX_LRTD actionToExecute)
    {
        WindowMonitorsInformationUtility.SearchMonitorFromId(actionToExecute.m_displayID,
            out bool found,
            out WindowMonitorRef monitor);
        if (!found) return;
        monitor.HasDimension(out bool hasDimension);
        if (!hasDimension)
            return;
        monitor.GetNativeLeftRight(out int x);
        monitor.GetNativeTopDown(out int y);
        MouseOperations.SetCursorPosition(
            x + (actionToExecute.m_screenRelative.m_xLeft2Right ),
            y + (actionToExecute.m_screenRelative.m_yTop2Down ));
    }
    private void Execute(Action_Real_SetCursor_OverDisplayName_PCT_LRTD actionToExecute)
    {
        WindowMonitorsInformationUtility.SearchMonitorFromIdName(actionToExecute.m_displayNameID,
            out bool found,
            out WindowMonitorRef monitor);
        if (!found) return;
        monitor.HasDimension(out bool hasDimension);
        if (!hasDimension)
            return;
        monitor.GetNativeLeftRight(out int x);
        monitor.GetNativeTopDown(out int y);
        monitor.GetNativeHeight(out int h);
        monitor.GetNativeWidth(out int w);
        MouseOperations.SetCursorPosition(
            x +(int)( actionToExecute.m_screenRelative.m_xLeft2Right*(double)w),
            y +(int)( actionToExecute.m_screenRelative.m_yTop2Down* (double)h));
    }

    private void Execute(Action_Real_SetCursor_OverDisplayName_PX_LRTD actionToExecute)
    {
        WindowMonitorsInformationUtility.SearchMonitorFromIdName(actionToExecute.m_displayNameID,
            out bool found,
            out WindowMonitorRef monitor);
        if (!found) return;
        monitor.HasDimension(out bool hasDimension);
        if (!hasDimension)
            return;
        monitor.GetNativeLeftRight(out int x);
        monitor.GetNativeTopDown(out int y);
        monitor.GetNativeHeight(out int h);
        monitor.GetNativeWidth(out int w);
        MouseOperations.SetCursorPosition(
            x + actionToExecute.m_screenRelative.m_xLeft2Right,
            y + actionToExecute.m_screenRelative.m_yTop2Down);
    }





    private void Execute(Action_ResizeCurrentWindowAroundPoint_PX_LRTD actionToExecute)
    {
        WindowIntPtrUtility.GetCurrentProcessId(out IntPtrWrapGet id); 
        WindowIntPtrUtility.MoveWindowAtCenter(
            id,
          actionToExecute.m_centerPoint,
                      actionToExecute.m_width,
                      actionToExecute.m_height);
    }

    private void Execute(Action_ResizeProcessWindowAroundPoint_PX_LRTD actionToExecute)
    {
        WindowIntPtrUtility.MoveWindowAtCenter(
            actionToExecute.m_processId,
          actionToExecute.m_centerPoint,
                      actionToExecute.m_width, 
                      actionToExecute.m_height);
    }

  
    private void Execute(Action_ResizeCurrentWindowToSquareZone_PX_LRDT actionToExecute)
    {
        WindowIntPtrUtility.GetCurrentProcessId(out IntPtrWrapGet id);
        WindowIntPtrUtility.MoveWindow(id,
          actionToExecute.m_downLeftCorner,
                      actionToExecute.m_topRightCorner);
    }
    private void Execute(Action_ResizeProcessWindowToSquareZone_PX_LRDT actionToExecute)
    {
        WindowIntPtrUtility.MoveWindow(actionToExecute.m_processId,
           actionToExecute.m_downLeftCorner,
                       actionToExecute.m_topRightCorner);
    }

    private void Execute(Action_ResizeCurrentWindowToNative_PX_LRTD actionToExecute)
    {
        WindowIntPtrUtility.GetCurrentProcessId(out IntPtrWrapGet id);
        WindowIntPtrUtility.MoveWindow(id,
           actionToExecute.m_startPoint.m_pixelLeft2Right,
                       actionToExecute.m_startPoint.m_pixelTop2Bot,
                                   actionToExecute.m_width,
                                               actionToExecute.m_height, true);
    }

   

    private void Execute(Action_ResizeProcessWindowToNative_PX_LRTD actionToExecute)
    {
        WindowIntPtrUtility.MoveWindow(actionToExecute.m_processId,
            actionToExecute.m_startPoint.m_pixelLeft2Right,
                        actionToExecute.m_startPoint.m_pixelTop2Bot,
                                    actionToExecute.m_width,
                                                actionToExecute.m_height,true);
    }

    private void Execute(Action_ShowProcessId actionToExecute)
    {
        WindowIntPtrUtility.ShowWindow(actionToExecute.m_processId,
            WindowIntPtrUtility.WindowDisplayType.Show);
    }

    private void Execute(Action_HideProcessId actionToExecute)
    {
        WindowIntPtrUtility.ShowWindow(actionToExecute.m_processId,
            WindowIntPtrUtility.WindowDisplayType.Hide);
    }

    private void Execute(Action_Real_KeyInteraction actionToExecute)
    {
        User32KeyStrokeManager.SendKeyStroke(in actionToExecute.m_targetKey, in actionToExecute.m_pressionType);
    }

    private void Execute(Action_Real_SetCursorPosition_PX_LRTD actionToExecute)
    {
        WindowIntPtrUtility.SetCursorPos(actionToExecute.m_whereToMove);
    }
    private void Execute(Action_User32_SetFocusWindow actionToExecute)
    {
        WindowIntPtrUtility.SetForegroundWindow(actionToExecute.m_processId);
    }

    private void Execute(Action_Real_PastClipboard actionToExecute)
    {
        TryToExecute(0,()=>
        User32KeyStrokeManager.SendKeyStroke(in User32KeyStrokeManager.LeftControl, in User32KeyStrokeManager.PRESS));
        TryToExecute(actionToExecute.m_millisecondsBetweenKey, () =>
         User32KeyStrokeManager.SendKeyStroke(in User32KeyStrokeManager.V, in User32KeyStrokeManager.PRESS));
        TryToExecute(actionToExecute.m_millisecondsBetweenKey * 2, () =>
         User32KeyStrokeManager.SendKeyStroke(in User32KeyStrokeManager.V, in User32KeyStrokeManager.RELEASE));
        TryToExecute(actionToExecute.m_millisecondsBetweenKey * 3, () =>
         User32KeyStrokeManager.SendKeyStroke(in User32KeyStrokeManager.LeftControl, in User32KeyStrokeManager.RELEASE));
    }
    private void Execute(Action_Real_CutClipboard actionToExecute)
    {
        TryToExecute(0, () =>
           User32KeyStrokeManager.SendKeyStroke(in User32KeyStrokeManager.LeftControl, in User32KeyStrokeManager.PRESS));
        TryToExecute(actionToExecute.m_millisecondsBetweenKey, () =>
         User32KeyStrokeManager.SendKeyStroke(in User32KeyStrokeManager.X, in User32KeyStrokeManager.PRESS));
        TryToExecute(actionToExecute.m_millisecondsBetweenKey * 2, () =>
           User32KeyStrokeManager.SendKeyStroke(in User32KeyStrokeManager.X, in User32KeyStrokeManager.RELEASE));
        TryToExecute(actionToExecute.m_millisecondsBetweenKey * 3, () =>
           User32KeyStrokeManager.SendKeyStroke(in User32KeyStrokeManager.LeftControl, in User32KeyStrokeManager.RELEASE));
    }
    private void Execute(Action_Real_CopyClipboard actionToExecute)
    {
        TryToExecute(0, () =>
           User32KeyStrokeManager.SendKeyStroke(in User32KeyStrokeManager.LeftControl, in User32KeyStrokeManager.PRESS));
        TryToExecute(actionToExecute.m_millisecondsBetweenKey, () =>
         User32KeyStrokeManager.SendKeyStroke(in User32KeyStrokeManager.C, in User32KeyStrokeManager.PRESS));
        TryToExecute(actionToExecute.m_millisecondsBetweenKey * 2, () =>
           User32KeyStrokeManager.SendKeyStroke(in User32KeyStrokeManager.C, in User32KeyStrokeManager.RELEASE));
        TryToExecute(actionToExecute.m_millisecondsBetweenKey * 3, () =>
           User32KeyStrokeManager.SendKeyStroke(in User32KeyStrokeManager.LeftControl, in User32KeyStrokeManager.RELEASE));
    }

    private void Execute(Action_SetClipboardText actionToExecute)
    {
        User32ClipboardUtility.CopyTextToClipboard(actionToExecute.m_textToPast, false);
    }
    private void Execute(Action_Real_MoveCursor_PX_LRTD actionToExecute)
    {
        MouseOperations.MoveCursorPosition(actionToExecute.m_xPixelLeft2Right, actionToExecute.m_yPixelTop2Down);
    }
    private void Execute(Action_Real_MoveCursor_PX_LRDT actionToExecute)
    {
        MouseOperations.MoveCursorPosition(actionToExecute.m_xPixelLeft2Right, -actionToExecute.m_yPixelDown2Top);
    }


    private void Execute(Action_Real_CursorHoldPression actionToExecute)
    {
        MouseOperations.MouseEventFlags es = MouseOperationsUtility.GetTypeEventFrom(actionToExecute.m_targetButton, User32PressionType.Press);
        MouseOperations.MouseEventFlags ee = MouseOperationsUtility.GetTypeEventFrom(actionToExecute.m_targetButton, User32PressionType.Release);
        MouseOperations.MouseEventWithCurrentPosition(es);
        TryToExecute((int)(actionToExecute.m_secondToMaintain*1000f), () =>{ MouseOperations.MouseEventWithCurrentPosition(ee);} );
        
    }

    private void Execute(Action_Real_CursorClick actionToExecute)
    {
        MouseOperations.MouseEventFlags es = MouseOperationsUtility.GetTypeEventFrom(actionToExecute.m_targetButton, User32PressionType.Press);
        MouseOperations.MouseEventFlags ee = MouseOperationsUtility.GetTypeEventFrom(actionToExecute.m_targetButton, User32PressionType.Release);
        MouseOperations.MouseEventWithCurrentPosition(es);
        MouseOperations.MouseEventWithCurrentPosition(ee);
    }

    private void Execute(Action_Real_CursorInteraction actionToExecute)
    {
        MouseOperations.MouseEventFlags e= MouseOperationsUtility.GetTypeEventFrom(actionToExecute.m_targetButton, actionToExecute.m_interaction);
        MouseOperations.MouseEventWithCurrentPosition(e);
    }
    private void Execute(Action_Real_SetCursor_OverProcess_PCT_LRTD actionToExecute)
    {
        this.GetWindowInfo(actionToExecute.m_processId, out DeductedInfoOfWindowSizeWithSource info);
        info.m_frameSize.GetLeftToRightToAbsolute((float)actionToExecute.m_whereToMove.m_xLeft2Right, out int x);
        info.m_frameSize.GetTopToBottomToAbsolute((float)actionToExecute.m_whereToMove.m_yTop2Down, out int y);
        MouseOperations.SetCursorPosition(x, y);
    }

    private void Execute(Action_Real_SetCursor_OverProcess_PX_LRTD actionToExecute)
    {
        this.GetWindowInfo(actionToExecute.m_processId, out DeductedInfoOfWindowSizeWithSource info);
        info.m_frameSize.GetAbsoluteFromRelativePixelLeft2Right(actionToExecute.m_whereToMove.m_xLeft2Right, out int x);
        info.m_frameSize.GetAbsoluteFromRelativePixelTop2Bot(actionToExecute.m_whereToMove.m_yTop2Down, out int y);
        MouseOperations.SetCursorPosition(x, y);
    }
    private void Execute(Action_Post_MoveCursor_PX_LRTD actionToExecute)
    {
        this.GetWindowInfo(actionToExecute.m_processId, out DeductedInfoOfWindowSizeWithSource info);
        info.m_frameSize.GetAbsoluteFromRelativePixelLeft2Right(actionToExecute.m_whereToMove.m_xLeft2Right, out int x);
        info.m_frameSize.GetAbsoluteFromRelativePixelTop2Bot(actionToExecute.m_whereToMove.m_yTop2Down, out int y);
        PostMouseUtility.MoveTo(actionToExecute.m_processId, x, y, false, true);
          
    }

    private void Execute(Action_Post_MoveCursor_PCT_LRTD actionToExecute)
    {
        this.GetWindowInfo(actionToExecute.m_processId, out DeductedInfoOfWindowSizeWithSource info);
        info.m_frameSize.GetLeftToRightToAbsolute(actionToExecute.m_whereToMove.m_xLeft2Right
            , out int x);
        info.m_frameSize.GetTopToBottomToAbsolute(actionToExecute.m_whereToMove.m_yTop2Down
            , out int y);
        PostMouseUtility.MoveTo(actionToExecute.m_processId, x, y, false, true);

    }


    private void Execute(Action_Post_CursorClickWithDelay actionToExecute)
    {
        MouseOperations.MouseEventFlags e =
                 MouseOperationsUtility.GetTypeEventFrom(
                     actionToExecute.m_targetButton, User32PressionType.Press);
        MouseOperations.MouseEventFlags ee =
               MouseOperationsUtility.GetTypeEventFrom(
                   actionToExecute.m_targetButton, User32PressionType.Release);
        MouseOperations.MouseEvent(e);
        TryToExecute((int)(actionToExecute.m_secondToMaintain*1000f), () => MouseOperations.MouseEvent(ee) );
    }

    private void Execute(Action_Post_CursorClick actionToExecute)
    {
        MouseOperations.MouseEventFlags e =
               MouseOperationsUtility.GetTypeEventFrom(
                   actionToExecute.m_targetButton, User32PressionType.Press);
        MouseOperations.MouseEventFlags ee =
               MouseOperationsUtility.GetTypeEventFrom(
                   actionToExecute.m_targetButton, User32PressionType.Release);
        MouseOperations.MouseEvent(e);
        MouseOperations.MouseEvent(ee);
    }

    private void Execute(Action_Post_CursorInteraction actionToExecute)
    {
        MouseOperations.MouseEventFlags e = 
            MouseOperationsUtility.GetTypeEventFrom(
                actionToExecute.m_targetButton,
                actionToExecute.m_interactionType);
        MouseOperations.MouseEvent(e);
    }

    private void Execute(Action_Post_KeyInteraction actionToExecute)
    {
        User32KeyStrokeManager.SendKeyPostMessage( actionToExecute.m_processId,
            actionToExecute.m_targetKey, actionToExecute.m_pressionType);
    }
}
