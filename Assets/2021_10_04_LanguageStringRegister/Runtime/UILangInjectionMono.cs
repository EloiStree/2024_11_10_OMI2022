using Eloi;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UILangInjectionMono :  MonoBehaviour
{
    public bool m_useId;
    public ulong m_id;
    public bool m_useAlias;
    public string m_alias;
    public DefaultInspectorValue m_defaults;

    public void GetTextFor(in UserLanguagePreference preference, out bool found, out string text)
    {
        found = false;
        text = "";
        preference.GetLanguageAliasToUse(out string languageAlias);
        bool hasRegister = LanguageRegister.Access != null;
        if (!hasRegister)
        {
            m_defaults.GetTextFor(in languageAlias,out found, out  text);
        }
        else
        {
            if (m_useId)
                LanguageRegister.Access.SearchWithId(in m_id,in languageAlias, out  found, out text);
            if (!found && m_useAlias)
                LanguageRegister.Access.SearchWithAlias(in m_alias, in languageAlias, out  found, out text);
            if(!found)
                m_defaults.GetTextFor(in languageAlias, out found, out  text);

        }

    }

    public void Reset()
    {
         Eloi.E_GeneralUtility.GetTimeLongId(out m_id);
    }
}
[System.Serializable]
public class DefaultInspectorValue
{
    public string m_default;
    public LanguageToWordsCollection m_defaultValue;

    public void GetTextFor(in string languageAlias, out bool found, out string textByDefault)
    {
        bool ignoreCase=true;
        bool trim = true;
        string associatedWord="", wordlanguageAlias="";
        m_defaultValue.GetWords(out IEnumerable<LanguageToWord> words);
        foreach (var item in words)
        {
            item.GetInfo(out wordlanguageAlias, out associatedWord);
            if (E_StringUtility.AreEquals(in languageAlias, in wordlanguageAlias, in ignoreCase,in trim))
            {
                textByDefault = associatedWord;
                found = true;
                return;
            }
        }
        textByDefault = m_default;
        //found = E_StringUtility.IsFilled(textByDefault);
        found = true;
    }
}