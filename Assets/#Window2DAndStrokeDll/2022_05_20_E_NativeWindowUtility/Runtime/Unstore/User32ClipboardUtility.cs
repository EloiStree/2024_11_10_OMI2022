using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;

public class User32ClipboardUtility 
{
    //SOURCE/ https://exchangetuts.com/clipbrd-e-cant-open-error-when-setting-the-clipboard-from-net-1639570866408892

    [DllImport("user32.dll", SetLastError = true)]
    static extern bool OpenClipboard(IntPtr hWndNewOwner);

    [DllImport("user32.dll", SetLastError = true)]
    static extern bool CloseClipboard();

    [DllImport("user32.dll")]
    private static extern bool SetClipboardData(uint uFormat, IntPtr data);
    [DllImport("kernel32.dll")]
    static extern IntPtr GlobalLock(IntPtr hMem);
    [DllImport("kernel32.dll")]
    static extern IntPtr GlobalUnlock(IntPtr hMem);
    [DllImport("user32.dll")]
    static extern bool EmptyClipboard();
    private const uint CF_UNICODETEXT = 13;

    public static bool CopyTextToClipboard(string text, bool emptyAfter=true)
    {
        if (!OpenClipboard(IntPtr.Zero))
        {
            return false;
        }

        var global = Marshal.StringToHGlobalUni(text);
        try
        {
            //GlobalLock(global);
            SetClipboardData(CF_UNICODETEXT, global);
           // GlobalUnlock(global);
        }
        catch (Exception e)
        {
            //throw e;
        }
        finally
        {
            //Marshal.FreeHGlobal(global);
         if(emptyAfter)
            EmptyClipboard();
            CloseClipboard();
        }

//-------------------------------------------
// Not sure, but it looks like we do not need 
// to free HGLOBAL because Clipboard is now 
// responsible for the copied data. (?)
//
// Otherwise the second call will crash
// the app with a Win32 exception 
// inside OpenClipboard() function
//-------------------------------------------
// Marshal.FreeHGlobal(global);

return true;
    }
}




class User32ClipboardUtilityType2
{
    //https://blog.katastros.com/a?ID=00650-090a191b-2b7d-459e-9379-07caaa151f66
    [DllImport("kernel32.dll")]
    static extern IntPtr GlobalLock(IntPtr hMem);
    [DllImport("kernel32.dll")]
    static extern IntPtr GlobalUnlock(IntPtr hMem);
    [DllImport("user32.dll", SetLastError = true)]
    static extern bool OpenClipboard(IntPtr hWndNewOwner);
    [DllImport("user32.dll")]
    static extern bool EmptyClipboard();
    [DllImport("user32.dll")]
    static extern IntPtr SetClipboardData(uint uFormat, IntPtr hData);
    [DllImport("user32.dll", SetLastError = true)]
    static extern bool CloseClipboard();

    [StructLayout(LayoutKind.Sequential)]
    struct DROPFILES
    {
        public uint pFiles;
        public int x;
        public int y;
        public int fNC;
        public int fWide;
    };

    public static byte[] StructureToByte<T>(T structure)
    {
        int size = Marshal.SizeOf(typeof(T));
        byte[] buffer = new byte[size];
        IntPtr bufferIntPtr = Marshal.AllocHGlobal(size);
        try
        {
            Marshal.StructureToPtr(structure, bufferIntPtr, true);
            Marshal.Copy(bufferIntPtr, buffer, 0, size);
        }
        finally
        {
            Marshal.FreeHGlobal(bufferIntPtr);
        }
        return buffer;
    }

    public static void SetClipboardData(List<string> pathList)
    {
        StringBuilder builder = new StringBuilder();
        for (int i = 0; i < pathList.Count; i++)
        {
            builder.Append(pathList[i]);
            builder.Append('\0');
        }
        builder.Append('\0');
        string path = builder.ToString();
        OpenClipboard(IntPtr.Zero);
        int length = Marshal.SizeOf(typeof(DROPFILES));
        IntPtr bufferPtr = Marshal.AllocHGlobal(length + path.Length * sizeof(char) + 8);
        try
        {
            GlobalLock(bufferPtr);
            DROPFILES config = new DROPFILES();
            config.pFiles = (uint)length;
            config.fNC = 1;
            int seek = 0;
            byte[] configData = StructureToByte(config);
            for (int i = 0; i < configData.Length; i++)
            {
                Marshal.WriteByte(bufferPtr, seek, configData[i]);
                seek++;
            }
            for (int i = 0; i < path.Length; i++)
            {
                Marshal.WriteInt16(bufferPtr, seek, path[i]);
                seek++;
            }
            GlobalUnlock(bufferPtr);
            EmptyClipboard();
            SetClipboardData(15, bufferPtr);
        }
        catch (Exception e)
        {
            throw e;
        }
        finally
        {
            Marshal.FreeHGlobal(bufferPtr);
            CloseClipboard();
        }
    }

    static void Demo()
    {
        SetClipboardData(new List<string> { "D:\\666.txt", "D:\\333.txt" });
        Console.Read();
    }
}