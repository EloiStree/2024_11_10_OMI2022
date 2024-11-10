using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AfkAdsSupportMono : MonoBehaviour
{
    public DefaultBooleanChangeListener m_isActive;
    public UnityEvent m_setActive;
    public UnityEvent m_setDisable;
    public void SetActive(bool isActive) {
        m_isActive.SetBoolean(isActive, out bool changed);
        if (changed)
        {
            if (isActive)
                m_setActive.Invoke();
            else m_setDisable.Invoke();
        }
    }

    public string m_adsUrl= "http://openmacroinput.com/ads/";
    public void OpenAdsPage() {
        Application.OpenURL(m_adsUrl);
    }
}
