using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDD_KoFiWidget : MonoBehaviour
{
    public string [] m_loadUserInformation = new string[] { "monarobot", "pollybarks", "eloistree" };
    public List<ExtractedInfoFromPublicProfile> m_userInfo= new List<ExtractedInfoFromPublicProfile>();
    public void Start()
    {
        for (int i = 0; i < m_loadUserInformation.Length; i++)
        {
            StartCoroutine( KoFiExtractor.ExtractInfoFrom(m_loadUserInformation[i], FoundProfile));
        }
    }

    private void FoundProfile(ExtractedInfoFromPublicProfile profile)
    {
        m_userInfo.Add(profile);
    }
}
