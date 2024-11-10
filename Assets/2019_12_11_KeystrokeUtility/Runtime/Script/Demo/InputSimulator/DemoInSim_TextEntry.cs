using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WindowsInput;

public class DemoInSim_TextEntry : MonoBehaviour
{

    public float m_delay = 2;
    public string m_textToDisplay = "Bonjour Éloi ! Comment ça va ? ♥☻☺♣♠♦•◘○ []{} バカ  愚か";
 
    // Use this for initialization
    IEnumerator Start()
    {
            yield return new WaitForSeconds(m_delay);
            InputSimulator input = new InputSimulator();
            input.Keyboard.TextEntry(m_textToDisplay);
            Debug.Log("Hey mon ami");

        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
