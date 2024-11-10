using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using UnityEngine;

public class ExecutableLauncher 
{

    public static void LaunchExecutable(string absolutPath, string arg)
    {

        String path = @absolutPath;
        Process foo = new Process();
        foo.StartInfo.FileName = path;
        foo.StartInfo.Arguments = arg;
        foo.Start();
            
    }

    public static void GetAutoHotKeyFiles(string absolutepath, bool checkChildfolders, out string[] filesAbsolutePath)
    {
        GetFilesByExtension(absolutepath, checkChildfolders, out filesAbsolutePath, ".ahk");
    }
    public static void GetBatchFiles(string absolutepath, bool checkChildfolders, out string[] filesAbsolutePath)
    {
        GetFilesByExtension(absolutepath, checkChildfolders, out filesAbsolutePath, ".bat");
    }

    public static void GetJarFiles(string absolutepath, bool checkChildfolders, out string[] filesAbsolutePath)
    {
        GetFilesByExtension(absolutepath, checkChildfolders, out filesAbsolutePath, ".jar");
    }

    public static void GetExeFiles(string absolutepath, bool checkChildfolders, out string[] filesAbsolutePath)
    {

        GetFilesByExtension(absolutepath, checkChildfolders, out filesAbsolutePath, ".exe");

    }
    public static void GetFilesByExtension(string absolutepath, bool checkChildfolders, out string[] filesAbsolutePath, string extension) {
        string[] files = Directory.GetFiles(absolutepath, "*", checkChildfolders ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
        filesAbsolutePath = files.Where(k => k.ToLower().EndsWith(extension)).ToArray();
    }

    public class ExecutableFile {

        public ExecutableFile(string absolutePath) {
            m_absolutePath = absolutePath;
        }
         string m_absolutePath;
        public string GetAbsolutePath() { return m_absolutePath; }
        public string GetRootFolderPath() { return Path.GetPathRoot(m_absolutePath); }
        public string GetFileName() { return Path.GetFileNameWithoutExtension(m_absolutePath); }
        public ExecutableFileType  GetFileType() {
            string ext = Path.GetExtension(m_absolutePath).ToLower();
            switch (ext)
            {
                case "exe": return ExecutableFileType.Executable;
                case "bat": return ExecutableFileType.Batch; 
                case "ahk": return ExecutableFileType.AutoHotKey; 
                default:
                    break;
            }
            return ExecutableFileType.Unknown; }
        
    }
    public enum ExecutableFileType {
        AutoHotKey, Batch, Executable, Unknown
    }
}
