using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickTestAroundScript : MonoBehaviour
{

    public string path= "C:\\Users\\OpenMacroInput\\Desktop\\OMI Come On\\JOMI\\2020_04_10_JavaOpenMacroInputRuntime\\HelloWorld.py";
    public string toFind = ".py";
    public string toLaunch = "HelloWorld";
    public bool m_asHidden;

    public string m_startupPath=        Environment.GetFolderPath(Environment.SpecialFolder.Startup);
    public ExecutablePathManager m_executePath;

    [ContextMenu("Test")]
    public void Test()
    {
        m_executePath.TryToLaunch(toLaunch, m_asHidden);
    }

}
