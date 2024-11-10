using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEngine;

public class ExecuteWindowCommands : MonoBehaviour
{
    public static void RunCommands(string cmd, string workingDirectory, bool useDebug = false)
    {
        RunCommands(new string[] { cmd }, workingDirectory, useDebug);
    }
    public static void RunCommands(string[] cmds, string workingDirectory, bool useDebug = false)
    {

        if (string.IsNullOrEmpty(workingDirectory)) {
            workingDirectory = Directory.GetCurrentDirectory();
        }

        char disk = 'C';
        if (workingDirectory[1] == ':')
            disk = workingDirectory[0];

        var process = new Process();
        var psi = new ProcessStartInfo();
        psi.FileName = "cmd.exe";
        psi.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
        psi.RedirectStandardInput = true;
        psi.RedirectStandardOutput = true;
        psi.RedirectStandardError = true;
        psi.UseShellExecute = false;
        psi.WorkingDirectory = workingDirectory;
        process.StartInfo = psi;
        process.Start();
        process.OutputDataReceived += (sender, e) => {
            if (useDebug)
                UnityEngine.Debug.Log(e.Data);
        };
        process.ErrorDataReceived += (sender, e) => {
            if (useDebug)
                UnityEngine.Debug.Log(e.Data);
        };
        process.BeginOutputReadLine();
        process.BeginErrorReadLine();



        using (StreamWriter sw = process.StandardInput)
        {
            sw.WriteLine(disk + ":");
            sw.WriteLine("cd " + workingDirectory);
            foreach (var cmd in cmds)
            {
                if (useDebug)
                    UnityEngine.Debug.Log("> " + cmd);
                sw.WriteLine(cmd);
            }
        }
        process.WaitForExit();
    }
}
