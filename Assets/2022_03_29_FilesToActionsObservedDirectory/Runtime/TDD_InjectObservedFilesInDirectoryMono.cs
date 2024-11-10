using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDD_InjectObservedFilesInDirectoryMono : MonoBehaviour
{
    public ObservedFilesInDirectoryMono m_observer;
    public Eloi.AbstractMetaAbsolutePathDirectoryMono[] m_source;

    public void Start()
    {
        for (int i = 0; i < m_source.Length; i++)
        {
            m_observer.AddDirectoryToObserve(m_source[i]);

        }
    }
}
