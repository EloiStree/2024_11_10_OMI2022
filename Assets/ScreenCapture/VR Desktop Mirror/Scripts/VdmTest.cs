using System;
using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using static VdmDesktopManager;

public class VdmTest : MonoBehaviour
{
    public RawImage[] m_debug;


    public int m_displayNumber;
    public Texture2D[] m_screenTexture;

    [Tooltip("Monitor Color Space")]
    public bool LinearColorSpace = false;


    IEnumerator Start()
    {

        yield return new WaitForSeconds(1);
        //while (true)
        { 
            m_displayNumber = DesktopCapturePlugin_GetNDesks();

            DesktopCapturePlugin_Initialize();
            m_screenTexture = new Texture2D[m_displayNumber];
            for (int i = 0; i < m_displayNumber; i++)
            {
                int width = DesktopCapturePlugin_GetWidth(i);
                int height = DesktopCapturePlugin_GetHeight(i);
                var tex = new Texture2D(width, height, TextureFormat.BGRA32, false, LinearColorSpace);
                //m_screenTexture[i] = new Texture2D(2, 2);
                m_screenTexture[i] = tex;
                if (i < m_debug.Length)
                    m_debug[i].texture = tex;

                DesktopCapturePlugin_SetTexturePtr(i, m_screenTexture[i].GetNativeTexturePtr());
            }
            yield return new WaitForSeconds(1);
        }
       StartCoroutine(OnRender());
    }
    IEnumerator OnRender()
    {
        for (;;)
        {
            yield return new WaitForEndOfFrame();

            GL.IssuePluginEvent(DesktopCapturePlugin_GetRenderEventFunc(), 0);
        }
    }



    //public void HackStart()
    //{
    //    HackStop();

    //    string exePath = "Assets\\VR Desktop Mirror\\Hack\\VrDesktopMirrorWorkaround.exe";
    //    if (System.IO.File.Exists(exePath))
    //    {
    //        m_process = new System.Diagnostics.Process();
    //        m_process.StartInfo.FileName = exePath;
    //        m_process.StartInfo.CreateNoWindow = true;
    //        m_process.StartInfo.UseShellExecute = true;
    //        m_process.StartInfo.Arguments = GetActiveWindow().ToString();
    //        m_process.Start();
    //    }
    //    else
    //    {
    //        Debug.Log("VR Desktop Mirror Hack exe not found: " + exePath);
    //    }
    //}

    //public void HackStop()
    //{
    //    if (m_process != null)
    //    {
    //        if (m_process.HasExited == false)
    //        {
    //            m_process.Kill();
    //        }
    //    }
    //    m_process = null;
    //}


    [DllImport("user32.dll")]
    private static extern System.IntPtr GetActiveWindow();
    [DllImport("DesktopCapture")]
    private static extern void DesktopCapturePlugin_Initialize();
    [DllImport("DesktopCapture")]
    private static extern int DesktopCapturePlugin_GetNDesks();
    [DllImport("DesktopCapture")]
    private static extern int DesktopCapturePlugin_GetWidth(int iDesk);
    [DllImport("DesktopCapture")]
    private static extern int DesktopCapturePlugin_GetHeight(int iDesk);
    [DllImport("DesktopCapture")]
    private static extern int DesktopCapturePlugin_GetNeedReInit();
    [DllImport("DesktopCapture")]
    private static extern bool DesktopCapturePlugin_IsPointerVisible(int iDesk);
    [DllImport("DesktopCapture")]
    private static extern int DesktopCapturePlugin_GetPointerX(int iDesk);
    [DllImport("DesktopCapture")]
    private static extern int DesktopCapturePlugin_GetPointerY(int iDesk);
    [DllImport("DesktopCapture")]
    private static extern int DesktopCapturePlugin_SetTexturePtr(int iDesk, IntPtr ptr);
    [DllImport("DesktopCapture")]
    private static extern IntPtr DesktopCapturePlugin_GetRenderEventFunc();


}
