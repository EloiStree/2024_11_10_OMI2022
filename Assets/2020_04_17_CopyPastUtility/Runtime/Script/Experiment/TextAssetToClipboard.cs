using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextAssetToClipboard : MonoBehaviour
{
    public TextAsset m_file;

    public void PushToClipboard() {
        ClipboardUtility.PushFileFromTextAsset(m_file);
    }



}
