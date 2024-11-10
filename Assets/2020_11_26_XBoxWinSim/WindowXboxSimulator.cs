using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using vXboxInterfaceWrap;

public class WindowXboxSimulator : MonoBehaviour
{
  
    public string m_tutorialIfVbusDontExist= "https://github.com/EloiStree/OpenMacroInput/wiki/SCPvBusExeception";
    [Range(1,4)]
    public uint m_usedIndex=4;
    private VirtualXboxController m_controller;
    // Start is called before the first frame update
    public bool m_disconnectAllAtStart=true; 
    public bool m_disconnectAllAtEnd=true;



    public void Start()
    {
        if(m_disconnectAllAtStart)
            AboardAll();
    }


    private void OnDestroy()
    {
        if(m_disconnectAllAtEnd)
            AboardAll();
    }

    public void AboardAll()
    {
        for (uint i = 1; i <=4; i++)
        {
            try
            {
                VirtualXboxInterface.UnPlugForce(i);
            }
            catch (Exception e) {
                Debug.LogWarning(e.StackTrace);
            }

        }
        m_controller = null;
    }

    public void CreateConnection(int index1To4)
    {
        m_usedIndex =(uint) Mathf.Clamp(index1To4,1,4);
        CreateConnection();
    }

    public void  CreateConnection()
    {
     

        if (!VirtualXboxInterface.isVBusExists())
        { 
            Application.OpenURL(m_tutorialIfVbusDontExist);
        }
        else
        {
            if (VirtualXboxInterface.isControllerOwned(m_usedIndex)){
                Debug.LogWarning("The index of the X360 is use");
            }
            else {
                //VirtualXboxInterface
                if (m_controller == null) {
                    m_controller = new VirtualXboxController(m_usedIndex, true);
                }
            }
        }
    }
    IEnumerator TestAllInputToCheck(float timeBeforeStart)
    {
        yield return new WaitForSeconds(timeBeforeStart );
        if (m_controller != null)
        {
            while (true) {

                SetAxisLeftHorizonal(1f);
                yield return new WaitForSeconds(2);
                SetAxisLeftHorizonal(-1f);
                yield return new WaitForSeconds(2);
                SetAxisLeftHorizonal(0);
                yield return new WaitForSeconds(2);
                SetAxisLeftVertical(1f);
                yield return new WaitForSeconds(2);
                SetAxisLeftVertical(-1f);
                yield return new WaitForSeconds(2);
                SetAxisLeftVertical(0);
                yield return new WaitForSeconds(2);
                SetAxisRightHorizonal(1f);
                yield return new WaitForSeconds(2);
                SetAxisRightHorizonal(-1f);
                yield return new WaitForSeconds(2);
                SetAxisRightHorizonal(0);
                yield return new WaitForSeconds(2);
                SetAxisRightVertical(1f);
                yield return new WaitForSeconds(2);
                SetAxisRightVertical(-1f);
                yield return new WaitForSeconds(2);
                SetAxisRightVertical(0);
                yield return new WaitForSeconds(2);
                SetButtonDown_A(true);
                yield return new WaitForSeconds(2);
                SetButtonDown_A(false);
                yield return new WaitForSeconds(2);
                SetButtonLeft_X(true);
                yield return new WaitForSeconds(2);
                SetButtonLeft_X(false);

                yield return new WaitForSeconds(2);
                SetButtonRight_B(true);
                yield return new WaitForSeconds(2);
                SetButtonRight_B(false);

                yield return new WaitForSeconds(2);
                SetButtonUp_Y(true);
                yield return new WaitForSeconds(2);
                SetButtonUp_Y(false);


                yield return new WaitForSeconds(2);
                SetButtonJoystickLeft(true);
                yield return new WaitForSeconds(2);
                SetButtonJoystickLeft(false);
                yield return new WaitForSeconds(2);
                SetButtonJoystickRight(true);
                yield return new WaitForSeconds(2);
                SetButtonJoystickRight(false);


                yield return new WaitForSeconds(2);
                SetButtonSideLeft(true);
                yield return new WaitForSeconds(2);
                SetButtonSideLeft(false);

                yield return new WaitForSeconds(2);
                SetButtonSideRight(true);
                yield return new WaitForSeconds(2);
                SetButtonSideRight(false);



                yield return new WaitForSeconds(2);
                SetTriggerLeft(1f);
                yield return new WaitForSeconds(2);
                SetTriggerLeft(0);

                yield return new WaitForSeconds(2);
                SetTriggerRight(1f);
                yield return new WaitForSeconds(2);
                SetTriggerRight(0f);


                yield return new WaitForSeconds(2);
                SetButtonStart(true);
                yield return new WaitForSeconds(2);
                SetButtonStart(false); 
                
                yield return new WaitForSeconds(2);
                SetButtonBack(true);
                yield return new WaitForSeconds(2);
                SetButtonBack(false);

                yield return new WaitForSeconds(2);
                SetArrowDown(true);
                yield return new WaitForSeconds(2);
                SetArrowDown(false);
                yield return new WaitForSeconds(2);
                SetArrowLeft(true);
                yield return new WaitForSeconds(2);
                SetArrowLeft(false);

                yield return new WaitForSeconds(2);
                SetArrowRight(true);
                yield return new WaitForSeconds(2);
                SetArrowRight(false);

                yield return new WaitForSeconds(2);
                SetArrowUp(true);
                yield return new WaitForSeconds(2);
                SetArrowUp(false);





            }

        }
    }

    public void SetArrowUp(bool v)
    {

        SetButton(v, Button.DpadUp);
    }

    public void SetArrowRight(bool v)
    {
        SetButton(v, Button.DpadRight);
    }

    public void SetArrowLeft(bool v)
    {
        SetButton(v, Button.DpadLeft);
    }

    public void SetArrowDown(bool v)
    {
        SetButton(v, Button.DpadDown);
    }

    public void SetButtonBack(bool v)
    {
        SetButton(v, Button.Back);
    }

    public void SetButtonStart(bool v)
    {
        SetButton(v, Button.Start);
    }

    public void SetTriggerRight(float v)
    {

        SetButton(v > 0f, Button.RightTrigger);
    }

    public void SetTriggerLeft(float v)
    {
        SetButton(v > 0f, Button.LeftTrigger);
    }

    public void SetButtonSideRight(bool v)
    {
        SetButton(v, Button.RightBumper);
    }

    public void SetButtonSideLeft(bool v)
    {
        SetButton(v, Button.LeftBumper);
    }

    public void SetButtonJoystickRight(bool v)
    {
        SetButton(v, Button.RightStick);
    }

    public void SetButtonJoystickLeft(bool v)
    {
        SetButton(v, Button.LeftStick);
    }

    public void SetButtonUp_Y(bool v)
    {
        SetButton(v, Button.Y);
    }

    public void SetButtonRight_B(bool v)
    {
        SetButton(v, Button.B);
    }

    public void SetButtonLeft_X(bool v)
    {
        SetButton(v, Button.X);
    }

    public void SetButtonDown_A(bool v)
    {
        SetButton(v, Button.A);
    }

    public void SetButton(bool setOn, Button button)
    {
        if (m_controller == null)
            return;
        if (setOn)
            m_controller.Press(button);
        else
            m_controller.Release(button);
    }

    public void SetAxisLeftHorizonal(float pct)
    {
        if (m_controller == null)
            return;
        m_controller.SetAxisX((int)(pct * VirtualXboxController.AXIS_MAX));
    }
    public void SetAxisRightHorizonal(float pct)
    {
        if (m_controller == null)
            return;
        m_controller.SetAxisRx((int)(pct * VirtualXboxController.AXIS_MAX));
    }
    public void SetAxisLeftVertical(float pct)
    {
        if (m_controller == null)
            return;
        m_controller.SetAxisY((int)(pct * VirtualXboxController.AXIS_MAX));
    }
    public void SetAxisRightVertical(float pct)
    {
        if (m_controller == null)
            return;
        m_controller.SetAxisRy((int)(pct * VirtualXboxController.AXIS_MAX));
    }
}
