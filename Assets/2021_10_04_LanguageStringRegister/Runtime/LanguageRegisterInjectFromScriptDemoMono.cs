using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageRegisterInjectFromScriptDemoMono : MonoBehaviour
{

    public int m_idTest = 1;
    public bool m_idTestFound;
    public string m_idTestText;

    public string m_aliasTest = "Start";
    public bool m_aliasTestFound;
    public string m_aliasTestText;
    public bool m_isEnglishExist;
    public LanguageCollection m_languageCollection;

    public string[] m_languages;
    public LanguageCollection[] m_textPerLanguage;

    void Start()
    {
        InjectDataFromScript();
        RefreshDebugInfo();
    }

    private static void InjectDataFromScript()
    {
        LanguageRegister.Set.Append(0, "Hello", "FR", "Bonjour");
        LanguageRegister.Set.Append(0, "Hello", "EN", "Hello");
        LanguageRegister.Set.Append(0, "Hello", "IT", "Ciao");
        LanguageRegister.Set.Append(1, "Start", "FR", "Débuter");
        LanguageRegister.Set.Append(1, "Start", "EN", "Start");
        LanguageRegister.Set.Append(1, "Start", "IT", "Let's Go");
    }

    private void RefreshDebugInfo()
    {
        LanguageRegister.Access.GetLanguageAlias(out m_languages);
        LanguageRegister.m_default.GetCollection(out m_textPerLanguage);
        string english = "EN";
        LanguageRegister.m_default.IsLanguageExist(in english, out m_isEnglishExist, out m_languageCollection);
        LanguageRegister.Access.SearchWithId(0, "EN", out m_idTestFound, out m_idTestText);
        LanguageRegister.Access.SearchWithAlias(m_aliasTest, "EN", out m_aliasTestFound, out m_aliasTestText);
    }
}
