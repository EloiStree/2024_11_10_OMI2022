using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ReloadAllFiles : MonoBehaviour
{
    public float m_timeBeforeStart = 3;
    public UnityEvent m_loadOnceAtStart;
    public UnityEvent m_beforeReloading;
    public UnityEvent m_startReloading;
    public UnityEvent m_endReloading;
    public UnityEvent m_afterFirstImport;
    public bool m_isActive = false;

    private bool m_isFirstImport = true;
    public void Awake()
    {
        Invoke("StartBeActive", m_timeBeforeStart);
    }

    public void StartBeActive(){
        m_isActive = true;
        ReloadAll();
        m_loadOnceAtStart.Invoke();
    }

    private int m_countOfReimport;
    public void ReloadAll() {
        if (m_isActive) {
            Debug.Log("Reimport "+ (m_countOfReimport++));
            m_beforeReloading.Invoke();
            m_startReloading.Invoke();
            m_endReloading.Invoke();
            if (m_isFirstImport) {
                m_isFirstImport = false;
                m_afterFirstImport.Invoke();
            }
        }

    }
}
