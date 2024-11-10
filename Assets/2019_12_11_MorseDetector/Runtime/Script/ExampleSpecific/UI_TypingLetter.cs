using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_TypingLetter : MonoBehaviour {

    public Text m_letterDisplay;
    public Text m_morseDisplay;
    private MorseValue m_linkedMorseValue;
    public bool m_isSetWithData;
    public void SetDisplay(MorseValue value) {
        m_letterDisplay.text = MorseUtility.GuessValueInClassicMorse(value);
        m_morseDisplay.text = value.ToString();
        m_linkedMorseValue = value;
        m_isSetWithData = true;
    }

    public void Awake()
    {
        Reset();
    }

    public void Reset()
    {
        m_morseDisplay.text = "";                                
        m_letterDisplay.text = "";
        m_linkedMorseValue = new MorseValue();
        m_isSetWithData = false;
    }

    public bool IsDefine()
    {
        return m_isSetWithData;
    }

    public MorseValue GetLinkedMorseValue() {
        return m_linkedMorseValue;
    }
}
