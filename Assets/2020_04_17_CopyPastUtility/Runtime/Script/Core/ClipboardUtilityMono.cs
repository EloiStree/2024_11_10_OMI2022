using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ClipboardUtilityMono : MonoBehaviour
{

    [SerializeField] bool m_listenToCopyPastKeyCodes=true;
    [SerializeField] string m_textToCopyWhenRequested= "Hello world !";
    [SerializeField] string m_textThatHadBeenPast="";
    [SerializeField] CopyPastEvent m_onCopy = new CopyPastEvent();
    [SerializeField] CopyPastEvent m_onPast = new CopyPastEvent();
    [System.Serializable]
    public class CopyPastEvent : UnityEvent<string> { }

    public Text m_debug;
    public void Copy() {
        UnityClipboard.Set(m_textToCopyWhenRequested);
        m_onCopy.Invoke(m_textToCopyWhenRequested);

    }

    public void Past() {
        m_textThatHadBeenPast = GetTextInClipboard();
        m_onPast.Invoke(m_textThatHadBeenPast);
    }
  
    public void SetValueToCopyIfRequested(string text) {
        m_textToCopyWhenRequested = text;
    }
    public string GetTextInClipboard() {

        return UnityClipboard.Get(); ;
    }

    private void Update()
    {
        if (m_listenToCopyPastKeyCodes) {
            if (Input.GetKeyDown(KeyCode.C) && Input.GetKey(KeyCode.LeftControl)) {
                Copy();
            }
            if (Input.GetKeyDown(KeyCode.V) && Input.GetKey(KeyCode.LeftControl))
            {
                Past();
            }
        }

    }
}

public class UnityClipboard
{

        //https://flystone.tistory.com/138
         public static string Get()
        {
            TextEditor _textEditor = new TextEditor();
             _textEditor.Paste();
            return _textEditor.text;
        }
        public static void Set(string value)
        {
            TextEditor _textEditor = new TextEditor
            { text = value };
            _textEditor.OnFocus();
            _textEditor.Copy();
        }
}

public class  WinClipboardApp {
    //Doc: https://www.c3scripts.com/tutorials/msdos/clip.html
    //Source: https://stackoverflow.com/a/58875512/13305320
    public static void Set(string value)
    {
        if (value == null)
            throw new ArgumentNullException("Attempt to set clipboard with null");

        Process clipboardExecutable = new Process();
        clipboardExecutable.StartInfo = new ProcessStartInfo
        {
            RedirectStandardInput = true,
            FileName = @"clip",
            UseShellExecute = false
        };
        clipboardExecutable.Start();
        clipboardExecutable.StandardInput.Write(value);
        clipboardExecutable.StandardInput.Close();

        return;
    }

}

public class GUIClipboard
{
    //https://answers.unity.com/questions/266244/how-can-i-add-copypaste-clipboard-support-to-my-ga.html

    public static string Get()
    {
            return GUIUtility.systemCopyBuffer;
    }
    public static void Set(string value)
    {
            GUIUtility.systemCopyBuffer = value;
        
    }
}