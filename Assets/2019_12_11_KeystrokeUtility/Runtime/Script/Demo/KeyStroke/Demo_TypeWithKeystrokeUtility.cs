using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WindowsInput.Native;

public class Demo_TypeWithKeystrokeUtility : MonoBehaviour {

    public KeyboardWriteMono m_keyboard;
    [Header("Timing")]
    public float m_delayBeforeWriting=4;

    [Header("Touches")]
    public KeyboardTouch[] m_keyCodes = new KeyboardTouch[] { KeyboardTouch.O , KeyboardTouch.K };


    [Header("Charater")]
    public char[] m_characters=new char[] { '☢', '☣', '☠', '⚠', '®' , '¥' };




    [Header("Text")]
    [TextArea(2,10)]
    public string m_text = "(ಠ_ಠ)   (ノಠ益ಠ)ノ彡┻━┻       ┬──┬ ノ( ゜-゜ノ) ";



    [Header("Unicode")]
    public int m_fromCol = 550;
    [Header("Unicode")]
    public int m_toCol = 600;


    [Header("Info ASCII")]
    public string[] m_asciiInfo;

    public bool autoStart = true;

    void Start () {
       
        if (autoStart)
            Invoke("StartWritingAll", 0);
    }
    
    public void StartWritingAll()
    {
      
        StartCoroutine(  WritingAllCoroutine());
    }

    private IEnumerator WritingAllCoroutine()
    {
        Debug.Log("Start...");
        yield return new WaitForSeconds(m_delayBeforeWriting);
        m_keyboard.Stroke('\n');
        Debug.Log("Keyboard Touches... ");
        m_keyboard.Stroke('\n');
        foreach (KeyboardTouch touch in m_keyCodes)
        {
            m_keyboard.RealPressDown(touch);
            m_keyboard.RealPressUp(touch);
        }
        m_keyboard.Stroke('\n');
        Debug.Log("Characters... ");
        m_keyboard.Stroke('\n');
        foreach (char c in m_characters)
        {
            m_keyboard.Stroke(c);
        }
        m_keyboard.Stroke('\n');
        Debug.Log("Text... ");
        m_keyboard.Stroke('\n');
        m_keyboard.Stroke(m_text);


        m_keyboard.Stroke('\n');
        Debug.Log("Write ASCII... ");
        m_keyboard.Stroke('\n');
        for (int i = 0; i < 256; i++) {
            KeyBindingTable.InfoASCII info = KeyBindingTable.GetInfoASCII(i);
            char c = KeyBindingTable.GetCharASCII(i);
            m_keyboard.Stroke(i+": " +info.m_character);
            m_keyboard.Stroke('\n');
        }

        m_keyboard.Stroke('\n');
        Debug.Log("Write Unicode...");
        m_keyboard.Stroke('\n');
        int code;
        m_keyboard.Stroke("\n\r: ");
        for (int c = m_fromCol; c <m_toCol; c++)
        {
            m_keyboard.Stroke(" "+c);
        }
        for (int i = 0; i < 100; i++)
        {
            m_keyboard.Stroke("\n\r" + i + ": ");
            for (int c = m_fromCol; c < m_toCol; c++)
            {
                code = c * 100 + i;
                m_keyboard.Stroke(" ");

                if (code<33)
                    m_keyboard.Stroke("-");
                else
                   m_keyboard.Stroke(new UnicodeStrokeRequest(code));
            }

        }
        Debug.Log("End Writing");
    }

    public void OnValidate()
    {
        Refresh();
    }

    public void Refresh() {
        m_asciiInfo = new string[256];
        for (int i = 0; i < 256; i++)
        {
            KeyBindingTable.InfoASCII ascii = KeyBindingTable.GetInfoASCII(i);
            m_asciiInfo[i] = string.Format("{0}: {1} ({2} - {3})", i, ascii.m_character, ascii.m_charDescription, ascii.m_name);
          
        }

    }
}

