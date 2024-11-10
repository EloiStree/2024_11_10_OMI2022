using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SetPathAtStart : MonoBehaviour
{

    public InputField m_field;
    public LoadDisplayFromJava m_display;

    void Start()
    {
        string path = Directory.GetCurrentDirectory();
        string stored =  PlayerPrefs.GetString("DisplayPathPreviouslyUsed");
        if (!string.IsNullOrEmpty(stored))
            path = stored;
        m_field.text = path;
        m_display.SetWithPath(path);

    }


    private void OnDestroy()
    {
        PlayerPrefs.SetString("DisplayPathPreviouslyUsed", m_field.text);
    }
}
