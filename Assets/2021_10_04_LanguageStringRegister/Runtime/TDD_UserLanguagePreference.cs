using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDD_UserLanguagePreference : MonoBehaviour
{
    public bool m_useValidatorRefresh=false;
    public UserLanguagePreference m_currentValue;

    public void SetLanguagePreference(UserLanguagePreference preference) {
        m_currentValue = preference;
    }

    void OnValidate()
    {
        if (m_useValidatorRefresh) {
            if (UserLanguagePreference.Instance != null)
                m_currentValue = UserLanguagePreference.Instance ;
        }
        
    }
}
