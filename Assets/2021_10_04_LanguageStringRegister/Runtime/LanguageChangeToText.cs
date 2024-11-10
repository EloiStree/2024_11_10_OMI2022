using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanguageChangeToText : AbstractLanguageChangeToText
{

    public Text m_target;

    protected override void OnTextChangeRequested(in string text)
    {
        if (m_target!=null)
            m_target.text = text;
    }

    protected override void TryToAutoFill() {
        base.TryToAutoFill();
        if (m_target == null)
            m_target = gameObject.GetComponent<Text>();
        if (m_target == null)
            m_target = gameObject.GetComponentInChildren<Text>();
    }
}
[RequireComponent(typeof(UILangInjectionMono))]
public abstract class AbstractLanguageChangeToText : MonoBehaviour
    {
        public UILangInjectionMono m_source;
        public bool m_autoListenToChange=true;
        public void Awake()
        {
        if (UserLanguagePreference.Instance != null
            && UserLanguagePreference.Instance.IsLanguagePreferenceDefine()) { 
            LanguageChange(UserLanguagePreference.Instance);
        }
            if (m_autoListenToChange)
                UserLanguagePreference.AddChangeListener(LanguageChange);
        }
        public void OnDestroy()
        {
            if (m_autoListenToChange)
                UserLanguagePreference.RemoveChangeListener(LanguageChange);
        }

        public void LanguageChange(UserLanguagePreference preference)
    {
        if (m_source != null) { 
                m_source.GetTextFor(in preference, out bool found, out string text);
                if(found)
                    OnTextChangeRequested(in text);
            }
        }
        protected abstract void OnTextChangeRequested(in string text);

        private void Reset()
        {
            TryToAutoFill();

        }
        [ContextMenu("Try to auto fill")]
        protected virtual void TryToAutoFill()
        {
            if (m_source == null)
                m_source = gameObject.GetComponent<UILangInjectionMono>();
        }
    }
