using Eloi;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsefulBatchUtility 
{
    public static void ZipTargetFolder(string folderPath, bool addDateTime=true)
    {
        ZipTargetFolder(folderPath, "", addDateTime);

    }
    public static void ZipTargetFolder(string folderPath, string zipName, bool addDateTime)
    {
        string parentOfFolderToZip = folderPath + "/../";
        E_FilePathUnityUtility.GetJustDirectoryName(in folderPath, out string folderName);

        if (Eloi.E_StringUtility.IsNullOrEmpty(zipName))
        {
            zipName = folderName;
        }
        if (addDateTime)
            zipName += DateTime.Now.ToString("_yyyy_MM_dd_HH_mm_ss");
        E_LaunchWindowBat.ExecuteCommandHiddenWithReturnInThread(
            new Eloi.MetaAbsolutePathDirectory(parentOfFolderToZip),
            $"tar.exe -a -cf \"{zipName}.zip\" \"{folderName}\" ");
    }


    public enum BrowserTypeToOpen{InternetExplorer,Edge, Chrome, Firefox, Microsoft, Other}
    public static void OpenSpecificBrowser(string url, BrowserTypeToOpen browser, bool privateMode ) {
        string line = "";

        if (!privateMode) {
            if (browser == BrowserTypeToOpen.InternetExplorer)
                line = "start iexplore.exe " + url;
            else if (browser == BrowserTypeToOpen.Chrome)
                line = "start chrome " + url;
            else if (browser == BrowserTypeToOpen.Firefox)
                line = "start firefox.exe " + url;
            else if (browser == BrowserTypeToOpen.Edge)
                line = "start microsoft-edge: " + url;
            else if (browser == BrowserTypeToOpen.Microsoft)
                line = "start iexplore.exe " + url;
            else
                line = "start \"\" " + url;
        }
        else {
            if (browser == BrowserTypeToOpen.InternetExplorer)
                line = "start iexplore.exe " + url;
            else if (browser == BrowserTypeToOpen.Chrome)
                line = "start / d \"c:\\Program files\\Google Chrome\" Chrome.exe --incognito " + url;
            else if (browser == BrowserTypeToOpen.Firefox)
                line = "\"C:\\Program Files\\Mozilla Firefox\\firefox.exe\" -private-window " + url;
            else if (browser == BrowserTypeToOpen.Edge)
                line = "start microsoft-edge: " + url;
            else if (browser == BrowserTypeToOpen.Microsoft)
                line = "start iexplore.exe " + url;
            else
                line = "start \"\" " + url;
        }
       





        MetaAbsolutePathDirectory file = new MetaAbsolutePathDirectory(  Application.dataPath );
        E_LaunchWindowBat.ExecuteCommandHiddenWithReturnInThread(file,  line);

    }
}
