using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Demo_ListenToMorseEmittor : MonoBehaviour {

    public Text m_displayCurrentlyTyped;
    public Text m_displayCurrentlyTypedMorse;
    public InputField m_displayKeyFound;
    public MorseDetectorDefault[] m_morseEmittors;

    public MorseValue m_lastMorseReceived;

    void Awake () {
        for (int i = 0; i < m_morseEmittors.Length; i++)
        {
            m_morseEmittors[i].m_onMorseDetected.AddListener(DisplayMorseDetected);
            m_morseEmittors[i].m_onMorseKeyDetected.AddListener(DisplayMorseInChange);

        }
		
	}
    


    private void DisplayMorseInChange(MorseKey key, MorseValueWithOrigine morse)
    {
        DisplayMorseFound(morse);

    }


    private void DisplayMorseFound(MorseValueWithOrigine morse)
    {
        string morseText = TryToGestMorseValue(morse);
        m_displayCurrentlyTyped.text = morseText;
        m_displayCurrentlyTypedMorse.text = morse.GetMorseValue().ToString();

        m_lastMorseReceived = morse.GetMorseValue();
    }

    public void DisplayMorseDetected(MorseValueWithOrigine morse)
    {

        string morseText = TryToGestMorseValue(morse);
        DisplayMorseFound(morse);
        if (morseText != "?")
            m_displayKeyFound.text += morseText;
    }

    private string TryToGestMorseValue(MorseValueWithOrigine morse)
    {
        return MorseUtility.GuessValueInClassicMorse(morse.GetMorseValue());
    }
    
}
