using Eloi;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GroupOfDirectoryFilesObserverMono : MonoBehaviour
{
    public RelayDirectoryFilesObserver m_listenToObserved = new RelayDirectoryFilesObserver();
    public GroupOfLoopingTick m_tickGroup = new GroupOfLoopingTick();
    public List<DefaultDirectoryFilesObserver> m_directories = new List<DefaultDirectoryFilesObserver>();

    public void Update()
    {
        m_tickGroup.RemoveTimeToAll(Time.deltaTime);
    }

    public void AddDirectoryToObserver(string directoryPath, bool observeChildren, float timeBetweenRefreshInSeconds)
    {
        DefaultDirectoryFilesObserver observer = new DefaultDirectoryFilesObserver(directoryPath, observeChildren);
        AddLoopingRefresh(timeBetweenRefreshInSeconds, observer);
    }

    public void AddDirectoryToObserver(Eloi.IMetaAbsolutePathDirectoryGet directoryPath, bool observeChildren, float timeBetweenRefreshInSeconds)
    {
        DefaultDirectoryFilesObserver observer = new DefaultDirectoryFilesObserver(directoryPath, observeChildren);
        AddLoopingRefresh(timeBetweenRefreshInSeconds, observer);
    }

    private void AddLoopingRefresh(float timeBetweenRefreshInSeconds, DefaultDirectoryFilesObserver observer)
    {
        m_directories.Add(observer);

        observer.m_onRefreshed.AddListener(m_listenToObserved.m_onRefreshed.Invoke);
        observer.m_onNewFiles.AddListener(m_listenToObserved.m_onNewFiles.Invoke);
        observer.m_onFilesChanged.AddListener(m_listenToObserved.m_onFilesChanged.Invoke);
        observer.m_onDeletedFile.AddListener(m_listenToObserved.m_onDeletedFile.Invoke);

        m_tickGroup.AddLoopingTick(new LoopingTickBean(timeBetweenRefreshInSeconds,
            () =>
            {
                observer.RefreshFilesState(true);
            }));
    }

    public void ClearAll() {
        m_tickGroup.m_loopingTick.Clear();
        m_directories.Clear();
    }

}



