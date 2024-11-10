using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShortcutAsText 
{
    public string m_text;

    public ShortcutAsText(string text)
    {
        if (text == null || string.IsNullOrWhiteSpace(text))
            m_text = "";
        else
            m_text = text.Trim();
    }
    public ShortcutAsText()
    {
            m_text = "";
    }
}
