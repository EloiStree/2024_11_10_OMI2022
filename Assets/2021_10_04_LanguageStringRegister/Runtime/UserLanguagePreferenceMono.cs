using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UserLanguagePreferenceMono : MonoBehaviour
{
    public string m_languagePreferenceByDefault = "EN";
    public string[] m_addSuggestion = new string[] { "EN", "FR", "NL" };

    [Header("Debug")]
    public string m_currentLanguage;
    public UserLanguagePreference m_currentPreference;

    private void Awake()
    {
        UserLanguagePreference.Instance.SetLanguagePreferenceByDefault(m_languagePreferenceByDefault);
    }


    public void SetLanguagePreferenceTo(string languageAlias) {
        UserLanguagePreference.Instance.SetLanguagePreferenceInGame(languageAlias);
        UserLanguagePreference.Instance.ForceLanguageChangeNotification();

    }
    public void ForceLanguageChangeNotification()
    {

        UserLanguagePreference.Instance.ForceLanguageChangeNotification();

    }
    public void OnValidate()
    {
        if (UserLanguagePreference.Instance != null && UserLanguagePreference.Instance.IsLanguagePreferenceDefine()) { 

            UserLanguagePreference.Instance.GetLanguageAliasToUse(out m_currentLanguage);
            m_currentPreference = UserLanguagePreference.Instance;
        }
    }
}
[System.Serializable]
public class UserLanguagePreference
{
    public static UserLanguagePreference Instance = new UserLanguagePreference();
    public static Action<UserLanguagePreference> m_onAnyLanguagePreferenceChange ;

    public bool m_languagePreferenceIsSet = false;
    public string m_languagePreference="";

    public bool m_languagePreferencePerDefaultIsSet = false;
    public string m_languagePreferencePerDefault = "";

    public List<string> m_languageSuggestion = new List<string>();
    public List<LanguageMetaInfo> m_languagesMetaInfo = new List<LanguageMetaInfo>();



    public void SetLanguagePreferenceByDefault(string languageAlias)
    {
        m_languagePreferencePerDefaultIsSet = true;
        m_languagePreferencePerDefault = languageAlias;
        if(m_onAnyLanguagePreferenceChange!=null)
            m_onAnyLanguagePreferenceChange.Invoke(this);
    }

    public void SetLanguagePreferenceInGame(string languageAlias)
    {
        m_languagePreferenceIsSet = true;
        m_languagePreference = languageAlias;
        if(m_onAnyLanguagePreferenceChange!=null)
            m_onAnyLanguagePreferenceChange.Invoke(this);
    }

    public void GetLanguageAliasToUse(out string language)
    {
        if (m_languagePreferenceIsSet)
            language = m_languagePreference;
        else if (m_languagePreferencePerDefaultIsSet)
            language = m_languagePreferencePerDefault;
        else throw new Exception("Languge preference not define. Please check if the language is define before trying to access it");
    }

    public bool IsLanguagePreferenceDefine()
    {
        return m_languagePreferencePerDefaultIsSet || m_languagePreferenceIsSet;
    }

    public static void AddChangeListener(Action<UserLanguagePreference> languageChange)
    {
        m_onAnyLanguagePreferenceChange += languageChange;
    }

    public static void RemoveChangeListener(Action<UserLanguagePreference> languageChange)
    {
        m_onAnyLanguagePreferenceChange -= languageChange;
    }

    public void ForceLanguageChangeNotification()
    {
        if (m_onAnyLanguagePreferenceChange != null)
            m_onAnyLanguagePreferenceChange.Invoke(this);
    }
}

[System.Serializable]
public class LanguagePreferenceChangeEvent: UnityEvent<UserLanguagePreference>
{ }
public delegate void LanguageChangeListener(string previousLanguageAlias, string newLanguageAlias);
[System.Serializable]
public class LanguageMetaInfo
{
    public string m_aliasId;
    public string m_universalCharId;
    public string m_englishName;
    public string m_nativeLanguageName;
    public string m_unicodeFlag;
}

[System.Serializable]
public class LanguageMetaInfoFlag
{
    public string m_aliasId ;
    public Texture2D m_flag;
}

[System.Serializable]
public class TextFromLanguageToInject : UnityEvent<string> { }