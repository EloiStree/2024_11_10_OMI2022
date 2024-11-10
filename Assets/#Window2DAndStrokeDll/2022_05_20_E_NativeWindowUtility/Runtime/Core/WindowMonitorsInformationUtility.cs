using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;
using static WindowMonitorsInformationUtility;

public class WindowMonitorsInformationUtility
{
    public static List<NativeMonitorInformation> m_devices = new List<NativeMonitorInformation>();


    public static string m_globalInfoOnMonitorLog;
    public static void RefreshMonitorInformation()
    {
        m_globalInfoOnMonitorLog = "";
        DISPLAY_DEVICE ddAdapter = new DISPLAY_DEVICE();
        ddAdapter.cb = Marshal.SizeOf(typeof(DISPLAY_DEVICE));
        m_devices.Clear();
        for (uint nAdapter = 0; EnumDisplayDevices(null, nAdapter, ref ddAdapter, 0); nAdapter++)
        {

            m_globalInfoOnMonitorLog += string.Format("\nAdapter n°{0}", nAdapter + 1);
            m_globalInfoOnMonitorLog += string.Format("\n\tDevice name : {0}", ddAdapter.DeviceName);
            m_globalInfoOnMonitorLog += string.Format("\n\tDevice string : {0}", ddAdapter.DeviceString);
            m_globalInfoOnMonitorLog += string.Format("\n\tDevice ID : {0}", ddAdapter.DeviceID);
            m_globalInfoOnMonitorLog += string.Format("\n\tActive : {0}", ((ddAdapter.StateFlags & DISPLAY_DEVICE_ACTIVE) == DISPLAY_DEVICE_ACTIVE) ? "TRUE" : "FALSE");
            m_globalInfoOnMonitorLog += string.Format("\n\tPrimary : {0}", ((ddAdapter.StateFlags & DISPLAY_DEVICE_PRIMARY_DEVICE) == DISPLAY_DEVICE_PRIMARY_DEVICE) ? "TRUE" : "FALSE");
            m_globalInfoOnMonitorLog += string.Format("\n\tAttached : {0}", ((ddAdapter.StateFlags & DISPLAY_DEVICE_ATTACHED_TO_DESKTOP) == DISPLAY_DEVICE_ATTACHED_TO_DESKTOP) ? "TRUE" : "FALSE");

            IntPtr pDeviceName = Marshal.StringToHGlobalUni(ddAdapter.DeviceName);
            DEVMODE devmode = new DEVMODE();
            devmode.dmSize = (short)Marshal.SizeOf(typeof(DEVMODE));
            bool bRet = EnumDisplaySettingsEx(pDeviceName, ENUM_CURRENT_SETTINGS, ref devmode, 0);
            if (!bRet)
            {
                devmode.dmSize = (short)Marshal.SizeOf(typeof(DEVMODE));
                bRet = EnumDisplaySettingsEx(pDeviceName, ENUM_REGISTRY_SETTINGS, ref devmode, 0);
            }
            m_globalInfoOnMonitorLog += string.Format("\n\tBits Per Pel : {0}", devmode.dmBitsPerPel);
            m_globalInfoOnMonitorLog += string.Format("\n\tDisplay Frequency : {0} Htz", devmode.dmDisplayFrequency);
            m_globalInfoOnMonitorLog += string.Format("\n\tLog Pixels : {0}", devmode.dmLogPixels);
            m_globalInfoOnMonitorLog += string.Format("\n\tPels Width : {0}", devmode.dmPelsWidth);
            m_globalInfoOnMonitorLog += string.Format("\n\tPels Height : {0}", devmode.dmPelsHeight);
            m_globalInfoOnMonitorLog += string.Format("\n\tPosition X : {0}", devmode.dmPositionX);
            m_globalInfoOnMonitorLog += string.Format("\n\tPosition Y : {0}", devmode.dmPositionY);
            Marshal.FreeHGlobal(pDeviceName);

            DISPLAY_DEVICE ddMonitor = new DISPLAY_DEVICE();
            ddMonitor.cb = Marshal.SizeOf(typeof(DISPLAY_DEVICE));
            for (uint nMonitor = 0; EnumDisplayDevices(ddAdapter.DeviceName, nMonitor, ref ddMonitor, EDD_GET_DEVICE_INTERFACE_NAME); nMonitor++)
            {
                m_globalInfoOnMonitorLog += string.Format("\n\t\tMonitor n°{0}", nMonitor + 1);
                m_globalInfoOnMonitorLog += string.Format("\n\t\tMonitor name : {0}", ddMonitor.DeviceName);
                m_globalInfoOnMonitorLog += string.Format("\n\t\tMonitor string : {0}", ddMonitor.DeviceString);
                m_globalInfoOnMonitorLog += string.Format("\n\t\tMonitor ID : {0}", ddMonitor.DeviceID);
            }
            m_globalInfoOnMonitorLog += ("\n");
            m_devices.Add(new NativeMonitorInformation(ddAdapter, devmode));
        }
    }


    [DllImport("User32.dll", SetLastError = true)]
    public static extern bool EnumDisplayDevices(string lpDevice, uint iDevNum, ref DISPLAY_DEVICE lpDisplayDevice, uint dwFlags);

    public const int EDD_GET_DEVICE_INTERFACE_NAME = 0x00000001;

    public const int DISPLAY_DEVICE_ATTACHED_TO_DESKTOP = 0x00000001;
    public const int DISPLAY_DEVICE_MULTI_DRIVER = 0x00000002;
    public const int DISPLAY_DEVICE_PRIMARY_DEVICE = 0x00000004;
    public const int DISPLAY_DEVICE_MIRRORING_DRIVER = 0x00000008;
    public const int DISPLAY_DEVICE_VGA_COMPATIBLE = 0x00000010;
    public const int DISPLAY_DEVICE_REMOVABLE = 0x00000020;
    public const int DISPLAY_DEVICE_ACC_DRIVER = 0x00000040;
    public const int DISPLAY_DEVICE_MODESPRUNED = 0x08000000;
    public const int DISPLAY_DEVICE_RDPUDD = 0x01000000;
    public const int DISPLAY_DEVICE_REMOTE = 0x04000000;
    public const int DISPLAY_DEVICE_DISCONNECT = 0x02000000;
    public const int DISPLAY_DEVICE_TS_COMPATIBLE = 0x00200000;
    public const int DISPLAY_DEVICE_UNSAFE_MODES_ON = 0x00080000;
    public const int DISPLAY_DEVICE_ACTIVE = 0x00000001;
    public const int DISPLAY_DEVICE_ATTACHED = 0x00000002;


    [DllImport("User32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
    public static extern bool EnumDisplaySettingsEx(IntPtr lpszDeviceName, int iModeNum, ref DEVMODE lpDevMode, int dwFlags);

    public const int ENUM_CURRENT_SETTINGS = -1;
    public const int ENUM_REGISTRY_SETTINGS = -2;



    [System.Serializable]
    public class NativeMonitorInformation
    {
        public DISPLAY_DEVICE m_display;
        public DEVMODE m_devMode;
        public List<DISPLAY_DEVICE> m_inner;

        public NativeMonitorInformation(DISPLAY_DEVICE display, DEVMODE devMode, params DISPLAY_DEVICE[] inner)
        {
            m_display = display;
            m_devMode = devMode;
            m_inner = inner.ToList();
        }
    }
    [System.Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct DISPLAY_DEVICE
    {
        public int cb;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string DeviceName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string DeviceString;
        public int StateFlags;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string DeviceID;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string DeviceKey;
    }

    [System.Serializable]
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct DEVMODE
    {
        private const int CCHDEVICENAME = 32;
        private const int CCHFORMNAME = 32;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = CCHDEVICENAME)]
        public string dmDeviceName;
        public short dmSpecVersion;
        public short dmDriverVersion;
        public short dmSize;
        public short dmDriverExtra;
        public int dmFields;
        public int dmPositionX;
        public int dmPositionY;
        public ScreenOrientation dmDisplayOrientation;
        public int dmDisplayFixedOutput;
        public short dmColor;
        public short dmDuplex;
        public short dmYResolution;
        public short dmTTOption;
        public short dmCollate;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = CCHFORMNAME)]
        public string dmFormName;
        public short dmLogPixels;
        public int dmBitsPerPel;
        public int dmPelsWidth;
        public int dmPelsHeight;
        public int dmDisplayFlags;
        public int dmDisplayFrequency;
        public int dmICMMethod;
        public int dmICMIntent;
        public int dmMediaType;
        public int dmDitherType;
        public int dmReserved1;
        public int dmReserved2;
        public int dmPanningWidth;
        public int dmPanningHeight;
    }

    public static bool Contains(string search, string text) { 
        return text.ToLower()
                .IndexOf(search.ToLower().ToString()) > -1;
    }
    public static void SearchMonitorFromIdName(string displayNameID, out bool found, out WindowMonitorRef monitor)
    {
        displayNameID= displayNameID.ToLower();
        for (int i = 0; i < m_devices.Count; i++)
        {
            if (
                Contains(m_devices[i].m_display.DeviceName, displayNameID) ||
                Contains(m_devices[i].m_display.DeviceString, displayNameID) ||
                Contains(m_devices[i].m_display.DeviceID , displayNameID) ||
                Contains(m_devices[i].m_display.DeviceKey, displayNameID) 
                )
            {
                found = true;
                monitor = new WindowMonitorRef(m_devices[i]);
                return;
            }
        }
        found = false;
        monitor = null;
    }

    public static void SearchMonitorFromId(int displayID, out bool found, out WindowMonitorRef monitor)
    {
        for (int i = 0; i < m_devices.Count; i++)
        {
            if (m_devices[i].m_display.DeviceName.IndexOf(displayID.ToString()) > -1) {
                found = true;
                monitor = new WindowMonitorRef(m_devices[i]);
                return;
            }
        }
        found = false;
        monitor = null;
    }
}

[System.Serializable]
public class WindowMonitorRef {
    public NativeMonitorInformation m_monitor;
    public WindowMonitorRef(NativeMonitorInformation monitor) {
        m_monitor = monitor;
    }

    public void GetNativeLeftRight(out int x)
    {
       x= m_monitor.m_devMode.dmPositionX;
    }

    public void GetNativeTopDown(out int y)
    {
        y = m_monitor.m_devMode.dmPositionY;
    }

    public void GetNativeHeight(out int height)
    {

        height = m_monitor.m_devMode.dmPelsHeight;
    }

    public void GetNativeWidth(out int width)
    {
        width = m_monitor.m_devMode.dmPelsWidth;
    }

    public void HasDimension(out bool hasDimension)
    {
        hasDimension = m_monitor.m_devMode.dmPositionX != 0 ||
        m_monitor.m_devMode.dmPositionY != 0 ||
        m_monitor.m_devMode.dmPelsHeight != 0 ||
         m_monitor.m_devMode.dmPelsWidth != 0;
    }
    public bool HasDimension()
    {
        HasDimension(out bool hasDimension);
        return hasDimension;
    }
}