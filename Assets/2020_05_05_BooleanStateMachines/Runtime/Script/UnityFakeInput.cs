using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityFakeInput : MonoBehaviour
{
    public BooleanStateRegisterMono m_linked;

  
    void Update()   
    {
        BooleanStateRegister reg=   m_linked.GetRegister();

        reg.Set("A", Input.GetKey(KeyCode.A));
        reg.Set("Z", Input.GetKey(KeyCode.Z));
        reg.Set("E", Input.GetKey(KeyCode.E));
        reg.Set("R", Input.GetKey(KeyCode.R));
        reg.Set("T", Input.GetKey(KeyCode.T));
        reg.Set("Y", Input.GetKey(KeyCode.Y));
        reg.Set("D", Input.GetKey(KeyCode.D));
        reg.Set("F", Input.GetKey(KeyCode.F));
        reg.Set("G", Input.GetKey(KeyCode.G));
        reg.Set("B", Input.GetKey(KeyCode.B));
        reg.Set("S", Input.GetKey(KeyCode.F));
        reg.Set("Q", Input.GetKey(KeyCode.G));
        reg.Set("NP0", Input.GetKey(KeyCode.Keypad0));
        reg.Set("NP1", Input.GetKey(KeyCode.Keypad1));
        reg.Set("NP2", Input.GetKey(KeyCode.Keypad2));
        reg.Set("NP3", Input.GetKey(KeyCode.Keypad3));
        reg.Set("NP4", Input.GetKey(KeyCode.Keypad4));
        reg.Set("NP5", Input.GetKey(KeyCode.Keypad5));
        reg.Set("NP6", Input.GetKey(KeyCode.Keypad6));
        reg.Set("NP7", Input.GetKey(KeyCode.Keypad7));
        reg.Set("NP8", Input.GetKey(KeyCode.Keypad8));
        reg.Set("NP9", Input.GetKey(KeyCode.Keypad9));

        reg.Set("NPPlus", Input.GetKey(KeyCode.KeypadPlus));
        reg.Set("NPMinus", Input.GetKey(KeyCode.KeypadMinus));
        reg.Set("NPMultiply", Input.GetKey(KeyCode.KeypadMultiply));
        reg.Set("NPDivide", Input.GetKey(KeyCode.KeypadDivide));
        reg.Set("NPPeriod", Input.GetKey(KeyCode.KeypadPeriod));
        reg.Set("NPEnter", Input.GetKey(KeyCode.KeypadEnter));

        reg.Set("ArrowUp", Input.GetKey(KeyCode.UpArrow));
        reg.Set("ArrowRight", Input.GetKey(KeyCode.RightArrow));
        reg.Set("ArrowDown", Input.GetKey(KeyCode.DownArrow));
        reg.Set("ArrowLeft", Input.GetKey(KeyCode.LeftArrow));

        reg.Set("Space", Input.GetKey(KeyCode.Space));



    }
}
