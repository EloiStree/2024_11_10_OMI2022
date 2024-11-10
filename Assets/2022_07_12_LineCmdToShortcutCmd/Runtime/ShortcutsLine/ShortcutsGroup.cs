using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[System.Serializable]
public class ShortcutsGroup 
{
    public ShortcutAsText[] m_shortcutList = new ShortcutAsText[0];


    public ShortcutsGroup() { m_shortcutList = new ShortcutAsText[0]; }
    public ShortcutsGroup(in string[] tokens, in bool checkContent)
    {
        if (checkContent)
            SetFromTokensWithCheck(tokens);
        else SetFromTokens(tokens);
    }
    public ShortcutsGroup(in string[] tokens)
    {
        SetFromTokens(tokens);
    }
    public ShortcutsGroup(in ShortcutAsText[] found)
    {
        m_shortcutList = found;
    }
    public void SetFromTokens(params string[] tokens)
    {
        if (tokens.Length == 0)
        {
            m_shortcutList = new ShortcutAsText[0];
        }
        else
        {
            m_shortcutList = tokens.Select(k => new ShortcutAsText(k)).ToArray();
        }
    }
    public void SetFromLine(string text, char spliter = ' ')
    {
        SetFromTokens( text.Split(spliter) );
    }
    public void SetFromTokensWithCheck(params string[] tokens)
    {
        if (tokens.Length == 0)
        {
            m_shortcutList = new ShortcutAsText[0];
        }
        else
        {
            m_shortcutList = tokens.Where(k => !string.IsNullOrWhiteSpace(k.Trim())).Select(k => new ShortcutAsText(k)).ToArray();
        }
    }
    public void SetFromLineWithCheck(in string text, in char spliter = ' ')
    {
        SetFromTokensWithCheck(text.Split(spliter).Where(k => !string.IsNullOrWhiteSpace(k.Trim())).ToArray());
    }
}
