using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LanguageChangeToGameobjects : MonoBehaviour
{

    public string m_previousLanguage = "";
    public string m_currentLanguage = "";

    public LanguageToUnityEvent [] m_languageToAction= new LanguageToUnityEvent[] {
        new LanguageToUnityEvent(){ m_languageAlias="EN"},
        new LanguageToUnityEvent(){ m_languageAlias="NL"},
        new LanguageToUnityEvent(){ m_languageAlias="FR"}
    };

    public void LanguageSelected(string languageAlias) {

        m_previousLanguage = m_currentLanguage;
        m_currentLanguage = languageAlias;

        SearchForResource(in m_previousLanguage, out bool found, out LanguageToUnityEvent actions);
        if (found)
        {
            Deactivate(actions);

        }
        SearchForResource(in m_currentLanguage, out  found, out  actions);
        if (found)
        {
            Activate(actions);

        }

    }

    private void Deactivate(LanguageToUnityEvent actions)
    {
        foreach (var item in actions.m_toDeactivate)
        {
            item.SetActive(false);
        }
        actions.m_languageDeselected.Invoke();
    }
    private void Activate(LanguageToUnityEvent actions)
    {
        foreach (var item in actions.m_toActive)
        {
            item.SetActive(true);
        }
        actions.m_languageSelecteEvent.Invoke();
    }

    private void SearchForResource(in string languageAlias, out bool found, out LanguageToUnityEvent actions)
    {
        for (int i = 0; i < m_languageToAction.Length; i++)
        {
            if (Eloi.E_StringUtility.AreEquals(m_languageToAction[i].m_languageAlias, languageAlias, true, true)) {
                actions = m_languageToAction[i];
                found = true;
                return;
            }
        }
        found = false;
        actions = null;
    }

    public void LanguageChangeDetected() {
        UserLanguagePreference.Instance.GetLanguageAliasToUse(out string alias);
        LanguageSelected(alias);
    }


    [System.Serializable]
    public class LanguageToUnityEvent {

        public string m_languageAlias;
        public GameObject [] m_toDeactivate;
        public UnityEvent m_languageDeselected;
        public GameObject [] m_toActive;
        public UnityEvent m_languageSelecteEvent;
    }
  
}
