using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRegisterJomiManager : MonoBehaviour
{
    public UI_ServerDropdownJavaOMI m_serverTarget;
    public MouseRegister m_mouseRegister;

    public void SaveMouseCursor(string name) {
        m_mouseRegister.RefreshPosition(name);
    }
    public void RecoverMousePosition(TimeSpan  whenToExecute, string name) {
        bool hasPosition;
        ScreenPositionInPourcentBean bean;
        m_mouseRegister.GetPosition(name, out hasPosition, out bean);
        
        foreach (var item in m_serverTarget.GetJavaOMISelected())
        {
            item.SendRawCommand(whenToExecute, string.Format("mm:{0}%:{1}%", bean.GetLeftToRightValue(), 1f-bean.GetBotToTopValue()));
        }
    }

}
