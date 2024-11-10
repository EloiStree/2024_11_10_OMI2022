using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FilesReload : MonoBehaviour
{
    public FileToObserveRegister m_observedFile;
    public UnityEvent m_beforeReload;
    public FilesPathEvent m_filesFound;
    public UnityEvent m_afterReload;

    public void ReloadExtension(string extension)
    {

        m_beforeReload.Invoke();

        string[] files = m_observedFile.GetFilesPathWithExtension(extension);
        m_filesFound.Invoke(files);
        m_afterReload.Invoke();
    }
    public void ReloadFileEndingBy(string extension)
    {

        m_beforeReload.Invoke();

        string[] files = m_observedFile.GetFilesEndingBy(extension);
        m_filesFound.Invoke(files);
        m_afterReload.Invoke();
    }
}
