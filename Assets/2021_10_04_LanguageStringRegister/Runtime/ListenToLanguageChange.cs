using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListenToLanguageChange : MonoBehaviour
{
    public LanguagePreferenceChangeEvent m_onAnyLanguagePreferenceChange;
    private void Awake()
    {

        UserLanguagePreference.m_onAnyLanguagePreferenceChange+=NotifyLanguageChange;
        NotifyLanguageChange(UserLanguagePreference.Instance);
        
    }

    private void NotifyLanguageChange(UserLanguagePreference obj)
    {
        m_onAnyLanguagePreferenceChange.Invoke(obj);
    }
}
