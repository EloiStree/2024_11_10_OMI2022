using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;

public class WindowMonitorsInformation 
{
    public static List<MonitorInformation> m_devices = new List<MonitorInformation>();
    public static PointInter m_mousePosition;

    public static string info;
    public static void Refresh() {
        GetCursorPosition(out m_mousePosition);

        info = "";
        DISPLAY_DEVICE ddAdapter = new DISPLAY_DEVICE();
        ddAdapter.cb = Marshal.SizeOf(typeof(DISPLAY_DEVICE));
        m_devices.Clear();
        for (uint nAdapter = 0; EnumDisplayDevices(null, nAdapter, ref ddAdapter, 0); nAdapter++)
        {

            info += string.Format("\nAdapter n°{0}", nAdapter + 1);
            info +=string.Format ("\n\tDevice name : {0}", ddAdapter.DeviceName);
            info +=string.Format ("\n\tDevice string : {0}", ddAdapter.DeviceString);
            info +=string.Format ("\n\tDevice ID : {0}", ddAdapter.DeviceID);
            info +=string.Format ("\n\tActive : {0}", ((ddAdapter.StateFlags & DISPLAY_DEVICE_ACTIVE) == DISPLAY_DEVICE_ACTIVE) ? "TRUE" : "FALSE");
            info +=string.Format ("\n\tPrimary : {0}", ((ddAdapter.StateFlags & DISPLAY_DEVICE_PRIMARY_DEVICE) == DISPLAY_DEVICE_PRIMARY_DEVICE) ? "TRUE" : "FALSE");
            info += string.Format("\n\tAttached : {0}", ((ddAdapter.StateFlags & DISPLAY_DEVICE_ATTACHED_TO_DESKTOP) == DISPLAY_DEVICE_ATTACHED_TO_DESKTOP) ? "TRUE" : "FALSE");

            IntPtr pDeviceName = Marshal.StringToHGlobalUni(ddAdapter.DeviceName);
            DEVMODE devmode = new DEVMODE();
            devmode.dmSize = (short)Marshal.SizeOf(typeof(DEVMODE));
            bool bRet = EnumDisplaySettingsEx(pDeviceName, ENUM_CURRENT_SETTINGS, ref devmode, 0);
            if (!bRet)
            {
                devmode.dmSize = (short)Marshal.SizeOf(typeof(DEVMODE));
                bRet = EnumDisplaySettingsEx(pDeviceName, ENUM_REGISTRY_SETTINGS, ref devmode, 0);
            }
            info +=string.Format("\n\tBits Per Pel : {0}", devmode.dmBitsPerPel);
            info +=string.Format("\n\tDisplay Frequency : {0} Htz", devmode.dmDisplayFrequency);
            info +=string.Format("\n\tLog Pixels : {0}", devmode.dmLogPixels);
            info +=string.Format("\n\tPels Width : {0}", devmode.dmPelsWidth);
            info +=string.Format("\n\tPels Height : {0}", devmode.dmPelsHeight);
            info +=string.Format("\n\tPosition X : {0}", devmode.dmPositionX);
            info += string.Format("\n\tPosition Y : {0}", devmode.dmPositionY);
            Marshal.FreeHGlobal(pDeviceName);

            DISPLAY_DEVICE ddMonitor = new DISPLAY_DEVICE();
            ddMonitor.cb = Marshal.SizeOf(typeof(DISPLAY_DEVICE));
            for (uint nMonitor = 0; EnumDisplayDevices(ddAdapter.DeviceName, nMonitor, ref ddMonitor, EDD_GET_DEVICE_INTERFACE_NAME); nMonitor++)
            {
                info +=string.Format("\n\t\tMonitor n°{0}", nMonitor + 1);
                info +=string.Format("\n\t\tMonitor name : {0}", ddMonitor.DeviceName);
                info +=string.Format("\n\t\tMonitor string : {0}", ddMonitor.DeviceString);
                info += string.Format("\n\t\tMonitor ID : {0}", ddMonitor.DeviceID);
            }
            info += ("\n");
            m_devices.Add(new MonitorInformation(ddAdapter, devmode));
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

    

    [DllImport("user32.dll")]
    public static extern bool GetCursorPos(out PointInter lpPoint);

    public static void GetCursorPosition(out PointInter infoOfMouse)
    {
        GetCursorPos(out infoOfMouse);

    }
    public static void GetCursorPosition(out int left2RightX, out int top2BotY)
    {
        GetCursorPos(out PointInter mouse);
        left2RightX = mouse.X;
        top2BotY = mouse.Y;

    }






    [System.Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct PointInter
    {
        public int X;
        public int Y;
    }
    [System.Serializable]
    public class MonitorInformation
    {
        public DISPLAY_DEVICE m_display;
        public DEVMODE m_devMode;
        public List<DISPLAY_DEVICE> m_inner;

        public MonitorInformation(DISPLAY_DEVICE display, DEVMODE devMode, params DISPLAY_DEVICE[] inner)
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

}

