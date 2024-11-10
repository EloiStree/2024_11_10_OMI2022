using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Eloi;
public class LanguageRegisterMono : MonoBehaviour
{

    public string[] m_languages;
    public LanguageCollection[] m_textPerLanguage;

    private void Awake()
    {
        LanguageRegister.AddRegisterChangeListener(Refresh);
    }
    public void Refresh()
    {
        LanguageRegister.Access.GetLanguageAlias(out m_languages);
        //DIRTY CODE but not time now to make a cleaner access.
        LanguageRegister.m_default.GetCollection(out m_textPerLanguage);
    }
   

    public void OnDestroy()
    {
        LanguageRegister.RemoveRegisterChangeListener(Refresh);
    }
}

public class ImportExcelToLanguageRegister
{

    public static void Import(in string textAsCSV,
        ref ILanguageRegisterSet whereToAddData,
        out bool converted, 
        out string errorMessage) {

        string[] firstLineTokens= new string [0];
        converted = false;
        errorMessage = "";
        if (whereToAddData == null)
        {
            errorMessage = "Register Set should not be null";
            return;
        }
        if (string.IsNullOrWhiteSpace(textAsCSV ))
        {
            errorMessage = "CSV file should not be empty or null";
            return;
        }
        string [ ] lines = textAsCSV.Split('\n');
        for (int i = 0; i < lines.Length; i++)
        {
            string[] tokens = lines[i].Split(';');

            if (i == 0)
            {
                if (tokens.Length < 4)
                {
                    errorMessage = "The excel should start by a first line with at least id, alias,context; [0..n language;]";
                    return;
                }
                firstLineTokens = tokens.ToArray();
            }
            else {
                for (int iToken = 3; iToken < tokens.Length; iToken++)
                {
                    ulong id;
                    if (ulong.TryParse(tokens[0].Trim(), out id))
                    {
                        whereToAddData.Append(id, tokens[1].Trim(), firstLineTokens[iToken].Trim(), tokens[iToken].Trim());
                    }
                    else {
                        whereToAddData.Append(tokens[1].Trim(), firstLineTokens[iToken].Trim(), tokens[iToken].Trim());
                    }
                }
            }

        }

        converted=true;
        // ID;Alias;Context; EN;FR;IT;NL;...


    }


}


public class LanguageRegister {

    static LanguageRegister(){
        m_default = new LanguageRegisterByDefault();
        Access = m_default;
        Set = m_default;
    }
    public static LanguageRegisterByDefault m_default;
    public static ILanguageRegisterAccess Access;
    public static ILanguageRegisterSet Set;
    public static Action m_registerChangeListener;
    public static void NotifyRegisterChange() {
        if(m_registerChangeListener!=null)
            m_registerChangeListener.Invoke();
    }
    public static void AddRegisterChangeListener(Action toDo)
    {
        m_registerChangeListener += toDo.Invoke;

    }
    public static void RemoveRegisterChangeListener(Action toDo)
    {
        m_registerChangeListener -= toDo.Invoke;

    }

}

    public class LanguageRegisterByDefault : ILanguageRegisterAccess, ILanguageRegisterSet
{
    public Dictionary<string, LanguageCollection> m_langues = new Dictionary<string,LanguageCollection>();

    public void Append(ulong id, string alias, string language, string text)
    {
        if (!m_langues.ContainsKey(language))
            m_langues.Add(language, new LanguageCollection(language));

        LanguageCollection collection = m_langues[language];
        collection.Add(new IdentifiableText(id, alias, text));
    }
    public void Append(string language,  IdentifiableText value)
    {
        if (!m_langues.ContainsKey(language))
            m_langues.Add(language, new LanguageCollection(language));

        LanguageCollection collection = m_langues[language];
        collection.Add(value);
    }
    public void Append(string alias, string language, string word)
    {
        Append(language, new IdentifiableText(alias, word));
    }

    public void Append(ulong id, string language, string word)
    {
        Append(language, new IdentifiableText(id, word));
    }
    public void ClearAll()
    {
        m_langues.Clear();
    }

    public void SearchWithAlias(in string alias, in string languageAlias, out bool found, out string text)
    {
        IsLanguageExist(in languageAlias, out bool exist, out LanguageCollection collection);
        if (exist)
        {
            collection.LookForWithAlias(in alias, out found, out text);
        }
        else
        {
            found = false;
            text = "";
        }
    }

    public void SearchWithId(in ulong id, in string languageAlias, out bool found, out string text)
    {
        IsLanguageExist(in languageAlias, out bool exist, out LanguageCollection collection);
        if (exist)
        {
            collection.LookFor(in id, out found, out text);
        }
        else {
            found = false;
            text = "";
        }
    }

    public void IsLanguageExist(in string languageAlias, out bool exist, out LanguageCollection collection)
    {
        exist = m_langues.ContainsKey(languageAlias);
        if (exist)
        {
            collection = m_langues[languageAlias];
        }
        else {
            collection = null;
        }
    }

    public void GetCollection(out LanguageCollection[] collections)
    {
        collections = m_langues.Values.ToArray();
    }
    public void GetLanguageAlias(out string[] langues)
    {
        langues = m_langues.Keys.ToArray();
    }

 
}




[System.Serializable]
public class LanguageCollection {
    public string languageAlias;
    public List<IdentifiableText> m_words= new List<IdentifiableText>();

    public LanguageCollection(string languageAlias)
    {
        this.languageAlias = languageAlias;
    }

    public void Add(IdentifiableText identifiableText)
    {
        m_words.Add(identifiableText);
    }
    public void CheckForMultipleEntityBasedOnId() {
        // to verify not sure it if s distinct
        m_words = m_words.Distinct().ToList();
    }

    public void LookFor(in ulong id, out bool found, out string text)
    {
        for (int i = 0; i < m_words.Count; i++)
        {
            if (m_words[i].m_hasId
                && m_words[i].m_id == id
                )
            {
                found = true;
                text = m_words[i].m_text;
                return;
            }
        }
        found = false;
        text = "";
    }

    private bool trim=true, ignorecase=true;
    public void LookForWithAlias(in string alias, out bool found, out string text)
    {
        for (int i = 0; i < m_words.Count; i++)
        {
            if (m_words[i].m_hasAlias && E_StringUtility.AreEquals( m_words[i].m_alias, in alias, in ignorecase,  in trim)  )
            {
                found = true;
                text = m_words[i].m_text;
                return;
            }
        }
        found = false;
        text ="";
    }
}

[System.Serializable]
public struct IdentifiableText {
    public string m_text;
    public string m_alias;
    public bool m_hasAlias;
    public ulong m_id;
    public bool m_hasId;

    public IdentifiableText(ulong id, string alias,string text)
    {
        m_text = text;
        this.m_hasId = true;
        this.m_id = id;
        if (!string.IsNullOrEmpty(alias))
        {
            this.m_hasAlias = true;
            this.m_alias = alias;
        }
        else
        {
            this.m_hasAlias = false;
            this.m_alias = "";

        }
    }
    public IdentifiableText( string alias, string text)
    {
        m_text = text;
        this.m_hasId = false;
        this.m_id = 0;
        if (!string.IsNullOrEmpty(alias))
        {
            this.m_hasAlias = true;
            this.m_alias = alias;
        }
        else
        {
            this.m_hasAlias = false;
            this.m_alias = "";

        }
    }
    public IdentifiableText(ulong id, string text)
    {
        m_text = text;
        this.m_hasId = true;
        this.m_id = id;
        
            this.m_hasAlias = false;
            this.m_alias = "";
        
    }

}


public interface ILanguageRegisterAccess
{

    void GetLanguageAlias(out string[] languageAlias);
    //void GetTextWithAlias(out string[] textAlias);
    //void GetTextWithId(out string[] textAlias);
    void SearchWithId(in ulong id, in string languageAlias, out bool found, out string word);
    void SearchWithAlias(in string alias, in string languageAlias, out bool found, out string word);
}
public interface ILanguageRegisterRetrieveComplexeInfo {

    void GetAllTextId(out ulong[] textId);
    void GetAllTextAlias(out ulong[] textId);

}

public interface ILanguageRegisterSet
{

    void ClearAll();
    void Append(ulong id, string alias, string language, string word);
    void Append(string alias, string language, string word);
    void Append(ulong id, string language, string word);
}


[System.Serializable]
public class LanguageToWordsCollection {
    [SerializeField] List<LanguageToWord> m_words = new List<LanguageToWord>();

    public void GetWords(out IEnumerable<LanguageToWord> words) => words = m_words;
}

[System.Serializable]
public struct LanguageToWord {
    [SerializeField] string m_associatedWord;
    [SerializeField] string m_languageAlias;
    public void GetInfo(out string languageAlias, out string associatedWord)
    {
        languageAlias = m_languageAlias;
        associatedWord = m_associatedWord;
    }
}