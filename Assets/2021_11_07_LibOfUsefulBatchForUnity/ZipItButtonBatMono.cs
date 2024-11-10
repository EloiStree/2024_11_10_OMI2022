using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ZipItButtonBatMono : MonoBehaviour
{

    public void ZipCurrentDirectory() {
#if !UNITY_EDITOR
        string dir = Directory.GetCurrentDirectory();
        UsefulBatchUtility.ZipTargetFolder(dir, true);
#endif
    }
    public void OpenZipFolder()
    {
        string dir = Directory.GetCurrentDirectory();
        Application.OpenURL(dir + "/..");
    }

    public void ZipAndOpenFolder() {
        ZipCurrentDirectory();
        OpenZipFolder();
    }
}
