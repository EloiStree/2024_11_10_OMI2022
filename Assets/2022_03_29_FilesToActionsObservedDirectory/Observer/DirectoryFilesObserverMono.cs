using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectoryFilesObserverMono : MonoBehaviour
{
    public bool m_searchInChildrensDirectory=true;
    public Eloi.AbstractMetaAbsolutePathDirectoryMono m_directoryTargeted;
    public DefaultDirectoryFilesObserver m_observedDirectory;

    public void Awake()
    {
        RefreshTargetDirectory();
    }

    private void RefreshTargetDirectory()
    {
        m_directoryTargeted.GetPath(out string path);
        m_observedDirectory.SetDirectoryTarget(path, m_searchInChildrensDirectory);
        m_observedDirectory.RefreshFilesState(false);
    }

    public void Refresh()
    {
        m_observedDirectory.RefreshFilesState(true);
    }
}
