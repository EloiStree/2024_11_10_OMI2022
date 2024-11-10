using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_UnityInputToTextListener : MonoBehaviour
{
    public TextListenerMono m_textListener;
    public KeyCode m_keyToListen = KeyCode.KeypadEnter;
    public KeyCode m_keyToPush = KeyCode.RightControl;

    void Update()
    {
        if (Input.GetKeyDown(m_keyToListen))
        {
            m_textListener.SwitchTheListenerState();
        }
        if (Input.GetKeyDown(m_keyToPush))
        {
            m_textListener.PushAndClear();
        }
        string t = Input.inputString;
        if (t.Length > 0)
            m_textListener.Append(t);



    }
}
