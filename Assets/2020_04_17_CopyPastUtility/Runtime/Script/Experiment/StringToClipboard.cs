using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringToClipboard : MonoBehaviour
{
    [TextArea(0, 6)]
    public string m_text;


    public void Push()
    {
        ClipboardUtility.Set(m_text);
    }
    public void Push(string text)
    {
        ClipboardUtility.Set(text);

    }
}
