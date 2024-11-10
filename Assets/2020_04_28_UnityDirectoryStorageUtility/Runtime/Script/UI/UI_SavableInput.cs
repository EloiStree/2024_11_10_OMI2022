using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_SavableInput : MonoBehaviour, I_TextSavable
{
    public InputField m_linkedInputField;
    public string m_defaultText="";
    public bool m_withNotification=true;
    public string GetSavableDefaultText()
    {
        return m_defaultText;
    }

    public string GetSavableText()
    {
        return m_linkedInputField.text;
    }
    public void SetTextFromLoad(string text)
    {
        SetTextFromLoad(text, m_withNotification);
    }
    public void SetTextFromLoad(string text, bool withNotificaiton= true)
    {
        if(withNotificaiton)
            m_linkedInputField.text = text;
        else
            m_linkedInputField.SetTextWithoutNotify(text);
    }

    private void Reset()
    {
        m_linkedInputField = GetComponent<InputField>();
        DirectoryStorageLoadAndSave saver = GetComponent<DirectoryStorageLoadAndSave>();
        if (saver == null)
            gameObject.AddComponent<DirectoryStorageLoadAndSave>();
    }
}
