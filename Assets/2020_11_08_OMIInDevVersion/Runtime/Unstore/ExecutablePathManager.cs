using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Eloi;
using System.IO;

public class ExecutablePathManager : MonoBehaviour
{
    public ExecutalePathRegister m_register;
    public SoundFilePlayer m_soundFilePlayer;

    public void TryToLaunch(string textToLookFor, bool asHidden)
    {

        textToLookFor = textToLookFor.Trim();
        m_register.TryToGetExecutableFrom(textToLookFor, out bool found, out ExecutablePathAccessInfo toExecute);
        if (found)
            Launch(toExecute, asHidden);
        else if (File.Exists(textToLookFor))
        {
            Launch(new ExecutablePathAccessInfo(textToLookFor, ""), asHidden);
        }
    }

    public static string m_python = ".py";
    public static string m_exe = ".exe";
    public static string m_bat = ".bat";
    public static string m_ahk = ".ahk";
    public static string m_javaJar = ".jar";
    public static string m_mp3 = ".mp3";
    public static string m_wav = ".wav";
    public static string m_shortcut = ".lnk";

    public void Launch(ExecutablePathAccessInfo item, bool asHidden)
    {
        if (item == null)
            return;
        string[] args = null;
        if (item is ExecutablePathAccessInfoWithParams) {
            ExecutablePathAccessInfoWithParams tmp = (ExecutablePathAccessInfoWithParams)item;
            tmp.GetParams(out args);
        }

        // E_LaunchWindowBat.LaunchAsFile(in item.GetHashCode);
        string p = item.GetPath().ToLower();
        IMetaAbsolutePathFileGet file = new MetaAbsolutePathFile(p);
        IMetaAbsolutePathDirectoryGet directory;
        E_FileAndFolderUtility.GetDirectoryPathFromPath(in file, out directory);
        string cmd = "";
        // Python
        if (E_StringUtility.EndWith(in p, in m_python))
        {
            cmd = "python \"" + p + "\""; 
            if (args != null && args.Length > 0)
                cmd += " " + string.Join(" ", args);
        }
        // Java jar
        else if (E_StringUtility.EndWith(in p, in m_javaJar))
        {
            cmd = "java -jar  \"" + p + "\"";
            if (args != null && args.Length > 0)
                cmd += " " + string.Join(" ", args);
        }  // music jar
        else if (E_StringUtility.EndWith(in p, in m_mp3)
            || E_StringUtility.EndWith(in p, in m_wav))
        {

            m_soundFilePlayer.PlayOneShot(new ExecutableFile(p));
            return;
        }
        // Unkonwn
        else
        {
            cmd = "start \"\" \"" + p + "\"";
            if (args != null && args.Length > 0)
                cmd += " " + string.Join(" ", args);

        }

       // Debug.Log("Cmd Start: " + cmd);
        if (asHidden)
            E_LaunchWindowBat.ExecuteCommandHiddenWithReturnInThread(directory, cmd);
        else E_LaunchWindowBat.CreateAndExecuteBatFileInThread(
            new MetaAbsolutePathDirectory(Application.temporaryCachePath)
            , new MetaFileNameWithoutExtension("" + ((int)(UnityEngine.Random.value * 1000000)))
            , false, 
            new string[] { cmd });
        
        

       // Debug.Log("Cmd stop: " + cmd);
    }

    internal void TryToLaunch(string textToLookFor, bool asHidden, List<string> args)
    {
        if (args.Count == 0)
        {
            TryToLaunch(textToLookFor, asHidden);
        }
        else
        {


            textToLookFor = textToLookFor.Trim();
            m_register.TryToGetExecutableFrom(textToLookFor, out bool found, out ExecutablePathAccessInfo toExecute);
            if (!found || toExecute==null)
            {
                toExecute = new ExecutablePathAccessInfoWithParams(textToLookFor, "", args.ToArray());

            }

            if (! (toExecute is ExecutablePathAccessInfoWithParams))
            {
                toExecute = new ExecutablePathAccessInfoWithParams(toExecute.m_pathGiven, toExecute.m_aliasName, args.ToArray());
            }

            if (toExecute!=null)
            {
                Launch(toExecute, asHidden);
            }
        }
    }
}
