using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DocButtonFileName : MonoBehaviour
{
    public string m_fileName= ".fileExtension";
    public string m_docLink= "https://github.com/EloiStree/OpenMacroInput/wiki/";
    public Button m_linkedButton;
    public Text m_linkedButtonText;

    public void Awake()
    {

        if (m_linkedButton != null)
            m_linkedButton.onClick.AddListener(GoToDoc);
    }

    private void GoToDoc()
    {
        Application.OpenURL(m_docLink + m_fileName);
    }

    private void OnValidate()
    {
        Refresh();
        if (m_linkedButtonText != null)
            m_linkedButtonText.text = m_fileName;
    }
    private void Reset()
    {
        Refresh();
    }
    
    private void Refresh()
    {
        if (m_linkedButton == null)
            m_linkedButton = GetComponent<Button>();
        if (m_linkedButtonText == null)
            m_linkedButtonText = GetComponentInChildren<Text>();
    }
}
