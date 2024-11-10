using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDD_PushActionsToExecution : MonoBehaviour
{
    public IntPtrProcessId m_focusProcess;
    public IntPtrTemp m_focusProcessParent;
    public IntPtrTemp m_focusProcessActiveChild;
    public float m_timeToMoveToTestZone = 3;


    [ContextMenu("ResizeWindow")]
    public void ResizeWindow()
    {
        StartCoroutine(ResizeWindowCoroutine());
    }


    private IEnumerator ResizeWindowCoroutine()
    {
        yield return new WaitForSeconds(m_timeToMoveToTestZone);

        WindowIntPtrUtility.MoveWindow(m_focusProcess.GetAsIntPtr(),200,500,200,500,true);


    }
        [ContextMenu("Write Hello World in chat")]
    void PrintHelloWorldInTarget() {

        StartCoroutine(TestPrintHelloWorldCoroutine());
    
    }

    private IEnumerator TestPrintHelloWorldCoroutine() {

        yield return new WaitForSeconds(m_timeToMoveToTestZone);

        User32ActionAbstractCatchToExecute.PushActionIn(0, new Action_Real_KeyInteraction()
        {
            m_pressionType = User32PressionType.Press,
            m_targetKey = User32KeyboardStrokeCodeEnum.ENTER
        }); 
        User32ActionAbstractCatchToExecute.PushActionIn(10, new Action_Real_KeyInteraction()
        {
            m_pressionType = User32PressionType.Release,
            m_targetKey = User32KeyboardStrokeCodeEnum.ENTER
        });
        User32ActionAbstractCatchToExecute.PushActionIn(25, new Action_SetClipboardText()
        {
            m_textToPast = "Hello World !!!"
        });
        User32ActionAbstractCatchToExecute.PushActionIn(150, new Action_Real_PastClipboard()); 

        User32ActionAbstractCatchToExecute.PushActionIn(200, new Action_Real_KeyInteraction()
        {
            m_pressionType = User32PressionType.Release,
            m_targetKey = User32KeyboardStrokeCodeEnum.ENTER
        }); 
        User32ActionAbstractCatchToExecute.PushActionIn(210, new Action_Real_KeyInteraction()
        {
            m_pressionType = User32PressionType.Release,
            m_targetKey = User32KeyboardStrokeCodeEnum.ENTER
        });

    }

    [ContextMenu("Cut and N past")]
    void CutAndMultiPast()
    {

        StartCoroutine(CutAndMultiPastCoroutine());

    }

    private IEnumerator CutAndMultiPastCoroutine()
    {

        yield return new WaitForSeconds(m_timeToMoveToTestZone);
        User32ActionAbstractCatchToExecute.PushActionIn(0, new Action_Real_CutClipboard());
        User32ActionAbstractCatchToExecute.PushActionIn(100, new Action_Real_PastClipboard());
        User32ActionAbstractCatchToExecute.PushActionIn(100+10, new Action_Real_PastClipboard());
        User32ActionAbstractCatchToExecute.PushActionIn(100 + 20, new Action_Real_PastClipboard());
        User32ActionAbstractCatchToExecute.PushActionIn(100 + 30, new Action_Real_PastClipboard());
        User32ActionAbstractCatchToExecute.PushActionIn(100 + 40, new Action_Real_PastClipboard());
        User32ActionAbstractCatchToExecute.PushActionIn(100 + 50, new Action_Real_PastClipboard());
       
   
    }

    public void Update()
    {
        WindowIntPtrUtility.GetCurrentProcessId(out IntPtrWrapGet p);
        WindowIntPtrUtility.GetParentAndChildOf(p,
            out IntPtrWrapGet parent,
            out IntPtrWrapGet activeChild);
        m_focusProcess = new IntPtrProcessId(p);
        m_focusProcessParent = new IntPtrTemp(parent.GetAsInt(), true);
        m_focusProcessActiveChild = new IntPtrTemp(activeChild.GetAsInt(), false);
    }

    #region TDD Element
    public Action_Post_KeyInteraction m_postKey;
    [ContextMenu("Push_PostKey")]
    public void Push_PostKey()
    {
        User32ActionAbstractCatchToExecute.PushAction(m_postKey);
    }
    #endregion


    #region TDD Element
    public Action_Real_MoveCursor_PX_LRDT m_moveMouseDT;
    [ContextMenu("Move Real Mouse DT")]
    public void MoveMouseDT()
    {
        User32ActionAbstractCatchToExecute.PushAction(m_moveMouseDT);
    }
    #endregion

    #region TDD Element
    public Action_Real_MoveCursor_PX_LRTD m_moveMouseTD;
    [ContextMenu("Move Real Mouse TD")]
    public void MoveMouseTD()
    {
        User32ActionAbstractCatchToExecute.PushAction(m_moveMouseTD);
    }
    #endregion
    #region TDD Element
    public Action_Real_SetCursorPosition_PX_LRTD m_setCursorTD;
    [ContextMenu("Set Real Mouse TD")]
    public void SetMouseTD()
    {
        User32ActionAbstractCatchToExecute.PushAction(m_setCursorTD);
    }
    #endregion

    #region TDD Element
    public Action_Real_KeyInteraction m_realKey;
    [ContextMenu("Push_RealKey")]
    public void Push_RealKey()
    {
        User32ActionAbstractCatchToExecute.PushAction(m_realKey);
    }
    #endregion

    #region TDD Element
    public Action_User32_SetFocusWindow m_setFocusOnWindow;
    [ContextMenu("Action_User32_SetFocusWindow")]
    public void Action_User32_SetFocusWindow()
    {
        User32ActionAbstractCatchToExecute.PushAction(m_setFocusOnWindow);
    }
    #endregion

    #region TDD Element
    public Action_SetClipboardText m_setClipboard;
    [ContextMenu("Action_SetClipboardText")]
    public void Action_SetClipboardText()
    {
        User32ActionAbstractCatchToExecute.PushAction(m_setClipboard);
    }
    #endregion

   
   
        #region TDD Element
    public Action_Post_CursorClickWithDelay m_cursorWithDelay;
    [ContextMenu("Action_Real_CursorClickWithDelay")]
    public void Push_Action_Real_CursorClickWithDelay()
    {
        User32ActionAbstractCatchToExecute.PushAction(m_cursorWithDelay);
    }
    #endregion

        #region TDD Element
    public Action_Real_CursorClick m_cursorClick;
    [ContextMenu("Action_Real_CursorClick")]
    public void Push_Action_Real_CursorClick()
    {
        User32ActionAbstractCatchToExecute.PushAction(m_cursorClick);
    }
    #endregion
    
        #region TDD Element
    public Action_Real_CursorInteraction m_cursorInteraction;
    [ContextMenu("Action_Real_CursorInteraction")]
    public void Push_Action_Real_CursorInteractiony()
    {
        User32ActionAbstractCatchToExecute.PushAction(m_cursorInteraction);
    }
    #endregion


    //Action_Real_MoveCursor_InProcess_PX_LRTD

    #region TDD Element
    public Action_Real_SetCursor_OverProcess_PX_LRTD m_moveOverPx;
    [ContextMenu("Action_Real_SetCursor_OverProcess_PX_LRTD")]
    public void Push_Action_Real_SetCursor_OverProcess_PX_LRTD()
    {
        User32ActionAbstractCatchToExecute.PushAction(m_moveOverPx);
    }
    #endregion

    //#region TDD Element
    //public Action_Real_SetCursor_OverProcess_PCT_LRTD m_realKey;
    //[ContextMenu("Set cursor Over")]
    //public void Push_RealKey()
    //{
    //    User32ActionAbstractCatchToExecute.PushAction(m_realKey);
    //}
    //#endregion

    /*
    
    Action_Post_MoveCursor_PX_LRTD
        #region TDD Element
    public Action_Real_KeyInteraction m_realKey;
    [ContextMenu("Push_RealKey")]
    public void Push_RealKey()
    {
        User32ActionAbstractCatchToExecute.PushAction(m_realKey);
    }
    #endregion

    Action_Post_MoveCursor_PCT_LRTD
        #region TDD Element
    public Action_Real_KeyInteraction m_realKey;
    [ContextMenu("Push_RealKey")]
    public void Push_RealKey()
    {
        User32ActionAbstractCatchToExecute.PushAction(m_realKey);
    }
    #endregion

    Action_Post_KeyInteraction
        #region TDD Element
    public Action_Real_KeyInteraction m_realKey;
    [ContextMenu("Push_RealKey")]
    public void Push_RealKey()
    {
        User32ActionAbstractCatchToExecute.PushAction(m_realKey);
    }
    #endregion

    Action_Post_CursorInteraction
        #region TDD Element
    public Action_Real_KeyInteraction m_realKey;
    [ContextMenu("Push_RealKey")]
    public void Push_RealKey()
    {
        User32ActionAbstractCatchToExecute.PushAction(m_realKey);
    }
    #endregion

    Action_Post_CursorClick
        #region TDD Element
    public Action_Real_KeyInteraction m_realKey;
    [ContextMenu("Push_RealKey")]
    public void Push_RealKey()
    {
        User32ActionAbstractCatchToExecute.PushAction(m_realKey);
    }
    #endregion


    Action_Post_CursorClickWithDelay
        #region TDD Element
    public Action_Real_KeyInteraction m_realKey;
    [ContextMenu("Push_RealKey")]
    public void Push_RealKey()
    {
        User32ActionAbstractCatchToExecute.PushAction(m_realKey);
    }
    #endregion

    */
}
