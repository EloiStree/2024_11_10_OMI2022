using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDD_GroupOfDirectoryFilesObserverMono : MonoBehaviour
{
    public GroupOfDirectoryFilesObserverMono m_target;
    public List<string> m_directoryPathAbsolute = new List<string>();
    public bool m_listenToChildren = true;
    public float m_minTime = 10f;

    [ContextMenu("Inject")]
    public void Inject() {
        m_target.ClearAll();
        for (int i = 0; i < m_directoryPathAbsolute.Count; i++)
        {
            m_target.AddDirectoryToObserver(
                m_directoryPathAbsolute[i],
                m_listenToChildren,
                m_minTime
                );
        }
    }
}
