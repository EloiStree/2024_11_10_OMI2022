using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

public class TextAppendChangeObserverMono : MonoBehaviour
{
    [TextArea(0,10)]
    public string m_textObserved;

    public TextAppendChangeObserver m_observeText;

    public void SetNewText(string text)
    {
        m_textObserved = text;
        m_observeText.PushNewText(text, true);
    }
    public void SetNewText(string text, bool withNotification)
    {
        m_textObserved = text;
        m_observeText.PushNewText(text, withNotification);
    }
    private void OnValidate()
    {
        m_observeText.PushNewText(m_textObserved, true);
    }
}

[System.Serializable]
public class RelayTextAppendChangeObserver
{

    public Eloi.PrimitiveUnityEvent_String m_fullTextResetFound = new Eloi.PrimitiveUnityEvent_String();
    public Eloi.PrimitiveUnityEvent_String m_listenToAppendNewText = new Eloi.PrimitiveUnityEvent_String();
    public Eloi.PrimitiveUnityEvent_Char m_listenToAppendNewChars = new Eloi.PrimitiveUnityEvent_Char();
    public Eloi.PrimitiveUnityEvent_String m_listenToAppendNewLines = new Eloi.PrimitiveUnityEvent_String();
}


[System.Serializable]
public class TextAppendChangeObserver
{
    [Header("Listeners")]
    public Eloi.PrimitiveUnityEvent_String m_fullTextResetFound = new Eloi.PrimitiveUnityEvent_String();
    public Eloi.PrimitiveUnityEvent_String m_listenToAppendNewText = new Eloi.PrimitiveUnityEvent_String();
    public Eloi.PrimitiveUnityEvent_Char m_listenToAppendNewChars = new Eloi.PrimitiveUnityEvent_Char();
    public Eloi.PrimitiveUnityEvent_String m_listenToAppendNewLines = new Eloi.PrimitiveUnityEvent_String();
    [Space(10)]
    [Header("Text In")]
    [TextArea(0, 10)]
    public string m_textCurrent = "";
    public int m_textCurrentLength = 0;

    [Header("Text Previous")]
    [TextArea(0, 10)]
    public string m_textPrevious = "";
    public int m_textPreviousLength = 0;

    [Header("Info on append found")]
    public bool m_wasAppendText = false;
    public bool m_wasNewText = false;

    [TextArea(0, 10)]
    public string m_newTextFoundContent = "";


    public void PushNewText(in string text , in bool notifyChange)
    {
        CheckForChangeInText( text,   notifyChange);
    }
    
    void CheckForChangeInText( string newText,  bool notifyChange)
    {

        m_newTextFoundContent = "";
        m_wasNewText = false;
        m_wasAppendText = false;

        m_textPrevious = m_textCurrent;
        m_textPreviousLength = m_textCurrent.Length;

        m_textCurrent = ""+newText;
        m_textCurrentLength = m_textCurrent.Length;

        if (m_textPreviousLength == m_textCurrentLength)
        {
            //Debug.Log(m_textCurrent + " \n------------\n"+ m_textPrevious);
            if (Eloi.E_StringUtility.AreEquals( m_textCurrent,  m_textPrevious, false, false) )
            {
             //   Debug.Log("Are equal");
               
            }
            else
            {

               // Debug.Log("Are not equal");
                m_newTextFoundContent = m_textCurrent;
                PushFullNexText(notifyChange);
            }

        }
        else if (m_textPreviousLength > m_textCurrentLength)
        {
            m_newTextFoundContent = m_textCurrent;
            PushFullNexText(notifyChange);
        }
        else if (m_textPreviousLength < m_textCurrentLength)
        {
            if (Eloi.E_StringUtility.StartWith( m_textCurrent,  m_textPrevious, false, false))
            {
                m_newTextFoundContent = m_textCurrent.Substring(m_textPreviousLength,
                       m_textCurrentLength - m_textPreviousLength);
                    m_wasAppendText = true;
                if (notifyChange)
                    PushNewAppendText(in m_newTextFoundContent);
            }
            else
            {
                m_newTextFoundContent = m_textCurrent;
                PushFullNexText(notifyChange);
            }

        }


    }
    List<char> m_newChars = new List<char>();
    void PushNewAppendText(in string foundNewText)
    {
        m_listenToAppendNewText.Invoke(foundNewText);
        GoogleDocAndSheetUtility.ParseTextToChars(in foundNewText, ref m_newChars);
        foreach (char item in m_newChars)
        {
            m_listenToAppendNewChars.Invoke(item);
        }
        GoogleDocAndSheetUtility.ParseTextToLines(in foundNewText, out string[] lines);
        foreach (string item in lines)
        {
            m_listenToAppendNewLines.Invoke(item);
        }
    }

    void PushFullNexText(bool notifyChange)
    {
        m_wasNewText = true;
        if (notifyChange)
            m_fullTextResetFound.Invoke(m_textCurrent);
    }
}

