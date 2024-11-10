using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecificWindowTargetPlayPrefIntPtrMono : SpecificWindowTargetIntPtrMono
{
    public string m_playerPerfIds;
    public bool m_usePlayerPrefs = true;

    private void Awake()
    {
        LoadFromPlayPerfs();
    }

    [ContextMenu("Load in memory")]
    private void LoadFromPlayPerfs()
    {
        if (m_usePlayerPrefs)
            SetAsInt(PlayerPrefs.GetInt(m_playerPerfIds));
    }

    private void OnDisable()
    {
        SetToPlayPrefs();
    }

    private void OnApplicationQuit()
    {
        SetToPlayPrefs();
    }
    [ContextMenu("Set in memory")]
    private void SetToPlayPrefs()
    {
        if (m_usePlayerPrefs)
            PlayerPrefs.SetInt(m_playerPerfIds, GetAsInt());
    }

    private void Reset()
    {
        ResetIdWithNowTime();

    }


    [ContextMenu("ResetIdWithNowTime")]
    private void ResetIdWithNowTime()
    {
        m_playerPerfIds = Guid.NewGuid().ToString();
    }
}

