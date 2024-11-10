using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AppStaticConfChangeListenerMono : MonoBehaviour
{
    public UnityEvent m_onMajorChanged;
    public void Awake()
    {
        StringKeyPropertyStatic.m_onScriptNotifyStaticMajorChange +=
            SomeChangeHappened;
    }
    public void OnDestory()
    {
        StringKeyPropertyStatic.m_onScriptNotifyStaticMajorChange -=
            SomeChangeHappened;
    }

    private void SomeChangeHappened()
    {
        m_onMajorChanged.Invoke();
    }
}
