using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;
using UnityEngine;
using UnityEngine.EventSystems;




//Based on SOURCE: https://youtu.be/RqgsGaMPZTw
public class TransparentWindow : MonoBehaviour
{

    private struct MARGINS
    {
        public int cxLeftWidth;
        public int cxRightWidth;
        public int cyTopHeight;
        public int cyBottomHeight;
    }

    [DllImport("user32.dll")]
    private static extern IntPtr GetActiveWindow();

    [DllImport("user32.dll")]
    private static extern int SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);

    [DllImport("user32.dll")]
    static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);

    [DllImport("user32.dll", EntryPoint = "SetLayeredWindowAttributes")]
    static extern int SetLayeredWindowAttributes(IntPtr hwnd, int crKey, byte bAlpha, uint dwFlags);

    [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
    private static extern int SetWindowPos(IntPtr hwnd, IntPtr hwndInsertAfter, int x, int y, int cx, int cy, int uFlags);

    [DllImport("Dwmapi.dll")]
    private static extern uint DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS margins);

    const int GWL_EXSTYLE = -20;
    const uint WS_EX_LAYERED = 0x00080000;
    const uint WS_EX_TRANSPARENT = 0x00080020;
    const uint LWA_COLORKEY = 0x00000001;
    static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);

    IntPtr hwnd;
    MARGINS margins;

    public bool pixelBased;
    public int wantedFrameRate=15;



    void Start()
    {
        Application.targetFrameRate = wantedFrameRate;


#if UNITY_EDITOR

        margins = new MARGINS() { cxLeftWidth = -1 };
        hwnd = GetActiveWindow();

        // make the window clickthrough
        if (pixelBased)
        {
            SetWindowLong(hwnd, GWL_EXSTYLE, WS_EX_LAYERED);
        }
        else
            SetWindowLong(hwnd, GWL_EXSTYLE, WS_EX_LAYERED | WS_EX_TRANSPARENT);

        // Everything black is click throught or not.
        //SetLayeredWindowAttributes(hwnd, 0, 0, LWA_COLORKEY);

        //avoid to leave the window when click through
        if (pixelBased) { 
            SetWindowPos(hwnd, HWND_TOPMOST, 0, 0,0,0,0);
        }
        // Make background trasnaprent if blacktransparent
        DwmExtendFrameIntoClientArea(hwnd, ref margins);
 
#endif
        Application.runInBackground = true;
    }

    public void SetClickThrough(bool clickThrough) {

        if (clickThrough)
            SetWindowLong(hwnd, GWL_EXSTYLE, WS_EX_LAYERED | WS_EX_TRANSPARENT);
        else
            SetWindowLong(hwnd, GWL_EXSTYLE, WS_EX_LAYERED);
    }

    //public void  SetClickThrough

    public static bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
    // also Application.targetFrameRate = wantedFrameRate;

}




[RequireComponent(typeof(Camera))]
public class TransparentWindow_V1 : MonoBehaviour
{
    [SerializeField]
    private Material m_Material;

    [SerializeField]
    private Camera mainCamera;

    private bool clickThrough = true;
    private bool prevClickThrough = true;

    private struct MARGINS
    {
        public int cxLeftWidth;
        public int cxRightWidth;
        public int cyTopHeight;
        public int cyBottomHeight;
    }

    [DllImport("user32.dll")]
    private static extern IntPtr GetActiveWindow();

    [DllImport("user32.dll")]
    private static extern int SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);

    [DllImport("user32.dll")]
    static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);

    [DllImport("user32.dll", EntryPoint = "SetLayeredWindowAttributes")]
    static extern int SetLayeredWindowAttributes(IntPtr hwnd, int crKey, byte bAlpha, int dwFlags);

    [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
    private static extern int SetWindowPos(IntPtr hwnd, int hwndInsertAfter, int x, int y, int cx, int cy, int uFlags);

    [DllImport("Dwmapi.dll")]
    private static extern uint DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS margins);

    const int GWL_STYLE = -16;
    const uint WS_POPUP = 0x80000000;
    const uint WS_VISIBLE = 0x10000000;
    const int HWND_TOPMOST = -1;

    int fWidth;
    int fHeight;
    IntPtr hwnd;
    MARGINS margins;

    void Start()
    {
        mainCamera = GetComponent<Camera>();

#if !UNITY_EDITOR 
 
        fWidth = Screen.width;
        fHeight = Screen.height;
        margins = new MARGINS() { cxLeftWidth = -1 };
        hwnd = GetActiveWindow();
 
        SetWindowLong(hwnd, GWL_STYLE, WS_POPUP | WS_VISIBLE);
        SetWindowPos(hwnd, HWND_TOPMOST, 0, 0, fWidth, fHeight, 32 | 64); //SWP_FRAMECHANGED = 0x0020 (32); //SWP_SHOWWINDOW = 0x0040 (64)
        DwmExtendFrameIntoClientArea(hwnd, ref margins);
 
        Application.runInBackground = true;
#endif
    }

    void Update()
    {
        // If our mouse is overlapping an object
        RaycastHit hit = new RaycastHit();
        clickThrough = !Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition).origin,
                mainCamera.ScreenPointToRay(Input.mousePosition).direction, out hit, 100,
                Physics.DefaultRaycastLayers);

        if (clickThrough != prevClickThrough)
        {
            if (clickThrough)
            {
#if !UNITY_EDITOR
                SetWindowLong(hwnd, GWL_STYLE, WS_POPUP | WS_VISIBLE);
                SetWindowLong (hwnd, -20, (uint)524288 | (uint)32);//GWL_EXSTYLE=-20; WS_EX_LAYERED=524288=&h80000, WS_EX_TRANSPARENT=32=0x00000020L
                SetLayeredWindowAttributes (hwnd, 0, 255, 2);// Transparency=51=20%, LWA_ALPHA=2
                SetWindowPos(hwnd, HWND_TOPMOST, 0, 0, fWidth, fHeight, 32 | 64); //SWP_FRAMECHANGED = 0x0020 (32); //SWP_SHOWWINDOW = 0x0040 (64)
#endif
            }
            else
            {
#if !UNITY_EDITOR
                SetWindowLong (hwnd, -20, ~(((uint)524288) | ((uint)32)));//GWL_EXSTYLE=-20; WS_EX_LAYERED=524288=&h80000, WS_EX_TRANSPARENT=32=0x00000020L
                SetWindowPos(hwnd, HWND_TOPMOST, 0, 0, fWidth, fHeight, 32 | 64); //SWP_FRAMECHANGED = 0x0020 (32); //SWP_SHOWWINDOW = 0x0040 (64)
#endif
            }
            prevClickThrough = clickThrough;
        }
    }

    void OnRenderImage(RenderTexture from, RenderTexture to)
    {
        Graphics.Blit(from, to, m_Material);
    }
}