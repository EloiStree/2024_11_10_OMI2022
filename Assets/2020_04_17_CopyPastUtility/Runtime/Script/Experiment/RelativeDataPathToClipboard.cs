using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class RelativeDataPathToClipboard : MonoBehaviour
{
    public string m_unityProjectRelativePath="../Packages/manifest.json";

    public void PushToClipboard()
    {
        ClipboardUtility.PushFileFromDataProjectRelativePath(m_unityProjectRelativePath);
    }


}
