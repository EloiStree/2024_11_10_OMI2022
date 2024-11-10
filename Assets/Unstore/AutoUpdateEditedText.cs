using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class AutoUpdateEditedText : MonoBehaviour
{

    public Text m_affected;
    public string m_format = "V: yyyy/MM/dd HH:mm";

    void Start()
    {

#if UNITY_EDITOR
        UpdateDate();
#endif
    }
    public void OnEnable()
    {

#if UNITY_EDITOR
        UpdateDate();
#endif

    }
    private void OnValidate()
    {

#if UNITY_EDITOR
        UpdateDate();
#endif
    }

    public void UpdateDate() {
        if(m_affected)
        m_affected.text = string.Format(DateTime.Now.ToString(m_format));   
    }
}
