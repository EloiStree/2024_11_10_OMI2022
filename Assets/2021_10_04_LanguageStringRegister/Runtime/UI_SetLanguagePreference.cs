using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_SetLanguagePreference : MonoBehaviour
{
    public string m_languageAlias = "EN";
    public void SetLanguageUserPreference(string languageAlias)
    {
        UserLanguagePreference.Instance.SetLanguagePreferenceInGame(languageAlias);
    }
    public void SetLanguageUserPreferenceWithParams()
    {
        SetLanguageUserPreference(m_languageAlias);
    }

}
