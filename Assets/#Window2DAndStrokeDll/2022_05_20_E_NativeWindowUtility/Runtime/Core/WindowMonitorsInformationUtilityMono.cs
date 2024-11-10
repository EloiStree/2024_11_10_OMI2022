using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static WindowMonitorsInformationUtility;

public class WindowMonitorsInformationUtilityMono : MonoBehaviour
{

    public  List<NativeMonitorInformation> m_devices = new List<NativeMonitorInformation>();

    [ContextMenu("Refresh all monitors")]
    public void RefreshAllMonitorsInfo() {

        WindowMonitorsInformationUtility.RefreshMonitorInformation();
        m_devices = WindowMonitorsInformationUtility.m_devices;
    }
}
