using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileObservedEventFilter : MonoBehaviour
{
    public ExtensionRelay[] m_relays = new ExtensionRelay[] { new ExtensionRelay() { m_extension = ".txt" } };
    public string m_lastFile = "";
    public string m_lastFileExtension = "";

    public void ChangeFileObserved(FileObserved file)
    {
        m_lastFile = file.GetName(false);
        m_lastFileExtension = file.GetExtension();
        for (int i = 0; i < m_relays.Length; i++)
        {
            if (m_relays[i].m_extension == m_lastFileExtension)
                m_relays[i].m_action.Invoke(file);

        }
    }
}
[System.Serializable]
public class ExtensionRelay {

    public string m_extension;
    public FileObservedChangeEvent m_action= new FileObservedChangeEvent();
}
