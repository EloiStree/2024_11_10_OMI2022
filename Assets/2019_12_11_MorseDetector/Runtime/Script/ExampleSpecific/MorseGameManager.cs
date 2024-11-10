using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MorseGameManager : MonoBehaviour {


    [Header("Params")]
    public UI_TypingLetter [] m_letterDisplayer;
    public float m_popTiming=3f;

    [Header("Event")]
    public UnityEvent m_gameLost;

    [Header("Dirty code")]
    public Text m_scoreDisplay;

    [SerializeField, Header("Info")]
    int m_playerScore;

	void Start () {
        ResetGameToZero();
        InvokeRepeating("PopNewMorse",0, m_popTiming);
        MorseDetectorUtilitary.OnMorseDetected.AddListener(CheckForGoodAnswer);
	}

    private void CheckForGoodAnswer(MorseValueWithOrigine value)
    {

        for (int i = 0; i < m_letterDisplayer.Length; i++)
        {
            if (value.GetMorseValue() == m_letterDisplayer[i].GetLinkedMorseValue())
            {
                m_letterDisplayer[i].Reset();
                m_scoreDisplay.text = (++m_playerScore).ToString();
            }

        }
    }
    public void CheckForGoodAnswerIgnoringNameId(MorseStack value)
    {

        for (int i = 0; i < m_letterDisplayer.Length; i++)
        {
            if (value.GetMorseValue() == m_letterDisplayer[i].GetLinkedMorseValue())
            {
                m_letterDisplayer[i].Reset();
                m_scoreDisplay.text = (++m_playerScore).ToString();
            }

        }
    }

    private void ResetGameToZero()
    {
        for (int i = 0; i < m_letterDisplayer.Length; i++)
        {
            m_letterDisplayer[i].Reset();
        }
    }
    

    void PopNewMorse() {

        for (int i = 0; i < m_letterDisplayer.Length; i++)
        {
            if ( ! m_letterDisplayer[i].IsDefine())
            {
                SetRandomMorseForDisplay(m_letterDisplayer[i]);
                break;
            }
        }
    }

    private void SetRandomMorseForDisplay(UI_TypingLetter uiLetter)
    {
        uiLetter.SetDisplay(GetRandomClassicMorse());
    }

    private MorseValue GetRandomClassicMorse()
    {
      MorseValue result = MorseUtility.GetRandomClassicMorseCode();
        return result;
    }
}
