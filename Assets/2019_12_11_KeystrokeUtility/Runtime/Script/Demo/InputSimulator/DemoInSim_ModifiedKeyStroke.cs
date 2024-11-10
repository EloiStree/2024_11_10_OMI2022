using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WindowsInput;
using WindowsInput.Native;

public class DemoInSim_ModifiedKeyStroke : MonoBehaviour {

    public float m_delay = 2;
    public int m_asciiCode=50;
    public VirtualKeyCode[] modifiers = new VirtualKeyCode[] { VirtualKeyCode.LSHIFT, VirtualKeyCode.LCONTROL };
    public VirtualKeyCode[] keys = new VirtualKeyCode[] {  VirtualKeyCode.VK_N
};
	// Use this for initialization
	IEnumerator Start () {
        while (true) {
        yield return new WaitForSeconds(m_delay);
        KeyBindingTable.GetKeyPadSequenceOf(m_asciiCode);
        InputSimulator input = new InputSimulator();
        input.Keyboard.ModifiedKeyStroke(modifiers, keys);

        }
	}
}
