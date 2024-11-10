using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using HWND = System.IntPtr;

public class MicrosoftWindowsManager : MonoBehaviour
{
    public WinWindowsUtility m_currentWindows;
    public WinWindowsRegister m_registered;
    private void Awake()
    {
        Invoke("RefreshCurrentWindow", 0.1f);
        //Invoke("Test", 5);
        //InvokeRepeating("Test2",0, 10);
        // h = Screen.currentResolution.height;
        //  w = Screen.currentResolution.width;
        m_registered.LoadSavedPlayerPrefs();
    }

    public void MoveWindow(string name, float pt1l2r,  float pt1d2t , float pt2l2r, float pt2d2t)    {
        NamedWindowPointer pointer;
        bool found;
        m_registered.Get(name, out found, out pointer);
        if (found)
        {
            pointer.m_pointer.SetPositionLD2TR( pt1l2r,  pt1d2t,  pt2l2r,  pt2d2t);
        }
    }

    public void SetFocusOn(string name)
    {
        NamedWindowPointer pointer;
        bool found;
        m_registered.Get(name,out found, out pointer);
        if (found)
        {
            pointer.m_pointer.SetFocusOn();
        }
    }

    public void GetCurrentWindowPosition(out float pt1lr, out float pt1dt, out float pt2lr, out float pt2ldt)
    {
        throw new NotImplementedException();
    }

    public void ChangeWindowTitle(string name, string title)
    {
        NamedWindowPointer pointer;
        bool found;
        m_registered.Get(name, out found, out pointer);
        if (found)
        {
            pointer.m_pointer.ChangeTitle(title);
        }
    }


    public void ChangeCurrentWindowTitle( string title)
    {
        WindowPtrRef pointer;
        WinWindowsUtility.GetCurrentFocusWithGivenName( out pointer);
        if (pointer!=null)
        {
            pointer.ChangeTitle(title);
        }
    }

    public void SaveCurrentFocusAs(string name, bool permanenceSave)
    {
        WindowPtrRefWithTitle pointer;
        WinWindowsUtility.SaveCurrentFocusWithGivenName(name, out pointer);
        m_registered.Add(new NamedWindowPointer(name, pointer.m_pointer), true);
        m_registered.SavePlayerPrefs();
    }

    //public RegexWindowTargetRef m_target;
    //public int h ,  w ;
    //public float l = 0.1f, r = 0.1f, t = 0.1f, d=0.1f;
    //public NamedWindowPtrRef winPtr;
    //public void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.E))
    //    {

    //        m_currentWindows = new WinWindowsUtility();
    //        m_currentWindows.Refresh();
    //    }
    //    if (Input.GetKeyDown(KeyCode.R))
    //    {
    //    }
    //    if (Input.GetKeyDown(KeyCode.T))
    //    {

    //    }

    //}
    //public void Test()
    //{
    //    m_target.Refresh();
    //    string named = "Test";
    //    WinWindowsUtility.SaveCurrentFocusWithGivenName(named, out winPtr);
    //}
    //public void Test2()
    //{

    //    if (winPtr != null && winPtr.m_pointer != null)
    //        winPtr.m_pointer.SetFocusOn();
    //    //winPtr.m_pointer.SetPositionLD2TR(0.11f, 0.02f, 0.95f, 0.99f);
    //   winPtr.m_pointer.SetPositionFromBorder(l, t, r, d);
    //}

    public void RefreshCurrentWindow() {
        m_currentWindows.Refresh();
    }

}

[System.Serializable]
public class WinWindowsUtility {

    public static WinWindowsUtility Instance;

    public List<NamedWindowPointer> m_currentWindow = new List<NamedWindowPointer>();

    public WinWindowsUtility() {
        Instance = this;
    }

    public void Refresh() {
        ReloadInformationOnWindowsOpenCurrently();
    }
    public void ReloadInformationOnWindowsOpenCurrently()
    {

        m_currentWindow.Clear();
        foreach (KeyValuePair<IntPtr, string> window in OpenWindowGetter.GetOpenWindows())
        {
            IntPtr handle = window.Key;
            string title = window.Value;
            m_currentWindow.Add(new NamedWindowPointer(title, handle));

        }
    }
    public static void GetCurrentFocusWithGivenName(out WindowPtrRef windowReference)
    {
        windowReference = new WindowPtrRef( GetForegroundWindow());
    }
    public static void SaveCurrentFocusWithGivenName(string windowName, out WindowPtrRefWithTitle windowReference)
    {
       windowReference = new WindowPtrRefWithTitle(windowName, GetForegroundWindow());
    }
    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    static extern IntPtr GetForegroundWindow();

}

[System.Serializable]
public class WinWindowsRegister
{
    [SerializeField] private List<WindowPtrRefWithTitle> m_currentlyOpenWindow = new List<WindowPtrRefWithTitle>();
    [SerializeField] private List<NamedWindowPointer> m_registeredDebug = new List<NamedWindowPointer>();
    public Dictionary<string, NamedWindowPointer> m_registered = new Dictionary<string, NamedWindowPointer>();

    public void RefreshOpenWindows(List<WindowPtrRefWithTitle> list) {
        m_currentlyOpenWindow.Clear();
        m_currentlyOpenWindow.AddRange(list);
    }

    public void Clear()
    {
        m_currentlyOpenWindow.Clear();
        m_registeredDebug.Clear();
        m_registered.Clear();
    }

    public void Add(NamedWindowPointer pointer, bool allowOverride)
    {
        string name = pointer.m_currentName.ToLower();
        if (!m_registered.ContainsKey(name))
        {
            m_registered.Add(name, pointer);
            m_registeredDebug.Add(pointer);
        }

        if (allowOverride) {
            m_registered[name] = pointer;
        }
        
    }


    public void Get(string name, out bool found, out NamedWindowPointer pointer)
    {
        name = name.ToLower();
        if (m_registered.ContainsKey(name))
        {
            pointer = m_registered[name];
            found = true;
        }
        else { 
            pointer = null;
            found = false;
        }
    }

    public void LoadSavedPlayerPrefs()
    {
        //throw new NotImplementedException();
    }

    public void SavePlayerPrefs()
    {
        //throw new NotImplementedException();
    }
}


[System.Serializable]
public class NamedWindowPointer
{
    public NamedWindowPointer(string name, IntPtr pointer) : this(name, new WindowPtrRef(pointer))
    { }
    public NamedWindowPointer(string name, WindowPtrRef pointer)
    {
        m_currentName = name;
        m_pointer = pointer;
    }
    public string m_currentName;
    public WindowPtrRef m_pointer;


}

public class WindowPtrRef {
    private IntPtr m_pointer;

    public WindowPtrRef(IntPtr pointer)
    {
        this.m_pointer = pointer;
    }

    public void SetFocusOn() {
        SetForegroundWindow(m_pointer);
    }

    //    winPtr.m_pointer.SetPosition(0.1f, 0.2f, 0.8f, 0.95f);
    public void SetPositionFromBorder(float left, float top, float right, float down)
    {

        SetPositionLD2TR(left, down, 1f-right, 1f - top);

    }

    public void SetPositionLD2TR(float pt1l2r, float pt1d2t, float pt2l2r, float pt2d2t) {
        int h = Screen.currentResolution.height,
             w = Screen.currentResolution.width; 

        SetWindowPos((int)m_pointer, 0,
            (int)(w * pt1l2r),
            (int)(h * (1f-pt2d2t)), 
            (int)(w * (pt2l2r - pt1l2r)), 
            (int)(h * (pt2d2t- pt1d2t)), 0);

    }

    [DllImport("user32.dll")]
    static extern bool SetWindowText(IntPtr hWnd, string windowName);
    [DllImport("user32.dll")]
    static extern bool SetForegroundWindow(IntPtr hWnd);
    [DllImport("user32.dll")]
     static extern bool SetWindowPos(int hWnd, int hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);


    public void ChangeTitle(string title)
    {
        SetWindowText(m_pointer, title);
    }

    public enum HWND : int
    {
        BOTTOM = 1,
        NOTOPMOST = -2,
        TOPMOST = -1,
        TOP = 0
    }
    public enum SWP : uint
    {
        ASYNCWINDOWPOS = 0x4000,
        DEFERERASE = 0x2000,
        FRAMECHANGED = 0x0020,
        HIDEWINDOW = 0x0080,
        NOACTIVATE = 0x0010,
        NOCOPYBITS = 0x0100,
        NOMOVE = 0x0002,
        NOOWNERZORDER = 0x0200,
        NOREDRAW = 0x0008,
        NOSENDCHANGING = 0x0400,
        NOSIZE = 0x0001,
        NOZORDER = 0x0004,
        SHOWWINDOW = 0x0040
    }

}

[System.Serializable]
public class WindowPtrRefWithTitle {
    public WindowPtrRef m_pointer;
    public string m_givenName;
    

    public WindowPtrRefWithTitle(string windowName, IntPtr intPtr)
    {
        m_givenName = windowName;
        m_pointer = new WindowPtrRef(intPtr);
    }
}
/// <summary>
/// Regex to refresh the pointer toward an existing window.
/// </summary>
[System.Serializable]
public class RegexWindowTarget
{
    public string m_regexOfWindowWanted;


    public RegexWindowTarget(string regexOfWindowWanted)
    {
        m_regexOfWindowWanted = regexOfWindowWanted;
    }

    public void GetWindow( out bool found, out NamedWindowPointer result)
    {
        GetWindow(WinWindowsUtility.Instance, out found, out result);
    }
    public void GetWindow(WinWindowsUtility currentWindow, out bool found, out NamedWindowPointer result)
    {
        found = false;
        result = null;
        for (int i = 0; i < currentWindow.m_currentWindow.Count; i++)
        {
            if (Regex.IsMatch(currentWindow.m_currentWindow[i].m_currentName, m_regexOfWindowWanted)) {
                found = true;
                result = currentWindow.m_currentWindow[i];
                return;
            }
            
        }
    }


}
/// <summary>
/// Store the result of a regex search on open windows
/// </summary>
[System.Serializable]
public class RegexWindowTargetRef : RegexWindowTarget
{
    public NamedWindowPointer m_pointerFound;


    public RegexWindowTargetRef(string regexOfWindowWanted): base(regexOfWindowWanted)
    {
        Refresh();
    }

    public void Refresh()
    {
        bool isFound;
        GetWindow(out isFound, out m_pointerFound);
    }

    public bool HasPointer() {
        return m_pointerFound != null;
    }

    public NamedWindowPointer GetPointer() { return m_pointerFound; }
}



//NOT MY CODE
/// <summary>Contains functionality to get all the open windows.</summary>
public static class OpenWindowGetter
{
   public static  IntPtr shellWindow = GetShellWindow();
    public static    Dictionary<IntPtr, string> windows = new Dictionary<IntPtr, string>();
    /// <summary>Returns a dictionary that contains the handle and title of all the open windows.</summary>
    /// <returns>A dictionary that contains the handle and title of all the open windows.</returns>
    public static IDictionary<IntPtr, string> GetOpenWindows()
    {
        windows.Clear();
        shellWindow = GetShellWindow();
        EnumWindows(Test, 0);

        return windows;
    }

    public static  bool Test(IntPtr hWnd, int lParam) {

        if (hWnd == shellWindow) return true;
        if (!IsWindowVisible(hWnd)) return true;

        int length = GetWindowTextLength(hWnd);
        if (length == 0) return true;

        StringBuilder builder = new StringBuilder(length);
        GetWindowText(hWnd, builder, length + 1);

        windows[hWnd] = builder.ToString();
        return true;
    }

    private delegate bool EnumWindowsProc(IntPtr hWnd, int lParam);

    [DllImport("USER32.DLL")]
    private static extern bool EnumWindows(EnumWindowsProc enumFunc, int lParam);

    [DllImport("USER32.DLL")]
    private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

    [DllImport("USER32.DLL")]
    private static extern int GetWindowTextLength(IntPtr hWnd);

    [DllImport("USER32.DLL")]
    private static extern bool IsWindowVisible(IntPtr hWnd);

    [DllImport("USER32.DLL")]
    private static extern IntPtr GetShellWindow();
}

