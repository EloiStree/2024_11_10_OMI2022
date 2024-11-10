using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.UI;

public class CaptureScreenWithDll : MonoBehaviour
{
    public Texture2D m_mainWindow;
    public int m_widht;
    public int m_height;

    void Start()
    {
        IntPtr target= ScreenCaptureWindow.GetDesktopWindow();
        int w, h;
        ScreenCaptureWindow.GetWidthAndHeightOfPtr(target, out w, out h);
        BitmapToTexture.ConvertV1(target, w,h, out m_mainWindow);
        m_widht = w;
        m_height = h;
    }

}
public class ScreenCaptureWindow
{
    [DllImport("user32.dll")]
    private static extern IntPtr GetForegroundWindow();

    [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
    public static extern IntPtr GetDesktopWindow();

    [StructLayout(LayoutKind.Sequential)]
    private struct Rect
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }

    [DllImport("user32.dll")]
    private static extern IntPtr GetWindowRect(IntPtr hWnd, ref Rect rect);
    public static void GetWidthAndHeightOfPtr(IntPtr source, out int width, out int height) {
        var rect = new Rect();
        GetWindowRect(source, ref rect);
        width = rect.Right - rect.Left;
        height = rect.Bottom - rect.Top;
    }

    //public static Bitmap CaptureDesktop()
    //{
    //    return  CaptureWindow(GetDesktopWindow());
    //}

    //public static Bitmap CaptureActiveWindow()
    //{
    //    return CaptureWindow(GetForegroundWindow());
    //}

    //public static Bitmap CaptureWindow(IntPtr handle)
    //{
    //    var rect = new Rect();
    //    GetWindowRect(handle, ref rect);
    //    var bounds = new Rectangle(rect.Left, rect.Top, rect.Right - rect.Left, rect.Bottom - rect.Top);
    //    var result = new Bitmap(bounds.Width, bounds.Height);

    //    using (var graphics = System.Drawing.Graphics.FromImage(result))
    //    {
    //        graphics.CopyFromScreen(new Point(bounds.Left, bounds.Top), Point.Empty, bounds.Size);
    //    }

    //    return result;
    //}


}

public class BitmapToTexture
{

    public static void ConvertV1(IntPtr source,int width, int height,  out Texture2D result) {
      
        result = new Texture2D(width, height,TextureFormat.RGBA32,true);
        result.LoadRawTextureData(source, width * height * 4);
        result.Apply();
    }
    
    //public static void ConvertV1(Bitmap source, out Texture2D result)
    //{
    //    MemoryStream ms = new MemoryStream();
    //    source.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
    //    var buffer = new byte[ms.Length];
    //    ms.Position = 0;
    //    ms.Read(buffer, 0, buffer.Length);
    //    result = new Texture2D(1, 1);
    //    result.LoadImage(buffer);
    //}

   

}