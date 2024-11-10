using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WindowsInput;
using WindowsInput.Native;

public class DemoInSim_TextEntryUnicode : MonoBehaviour
{


    public float m_delay = 2;
    // public string m_textToDisplay = "Bonjour Éloi ! Comment ça va ? ♥☻☺♣♠♦•◘○ []{} バカ  愚か";

    public InputSimulator input = new InputSimulator();
    // Use this for initialization
    IEnumerator Start()
    {
        input = new InputSimulator();
        yield return new WaitForSeconds(m_delay);
         int i = 0;
       
        while(true)
        {
            i++;
            yield return new WaitForEndOfFrame();
            input.Keyboard.TextEntry( (char)i+ "\t\t"+i).KeyPress(VirtualKeyCode.RETURN);
        }



    }
}