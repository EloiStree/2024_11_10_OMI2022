using Eloi;
using JavaOpenMacroInput;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryScriptOmiXml2Stuffs : MonoBehaviour
{
    public FileImport_OmiAsXmlElements m_import;
    public ManageMidiInListenersMono m_midiInListeners;

    public UI_ServerDropdownJavaOMI m_dropdown;
    public System.Threading.ThreadPriority m_threadPriority = System.Threading.ThreadPriority.Highest;


    public MouseInfoToBooleansDirectionMono m_mouse;
    public MouseWheelEventToBoolean m_mouseWheel;

    public UDPThreadSender m_xomiSender;



    public void Clear() {

        JavaOMI.Clear();
        m_midiInListeners.Clear();
    }

    public void ApplyImport() {

        foreach (var item in m_import.m_midiIn)
        {
            item.GetMidiConnectionInIdName(out string midiName);
            m_midiInListeners.StartListeningTo(midiName);
        }

        foreach (var item in m_import.m_jomiUdpTargets)
        {
            item.GetTargetIdName(out string udpTargetName);
            Eloi.E_CodeTag.DirtyCode.Info("Here I should manage the override but I am not. Like killing or checking that I am not killing the same target that before.");
            Eloi.E_CodeTag.SleepyCode.Info("I was a bit sleepy when I was coding this part. Be warn. Problem is that it is an important part.");
            if (E_StringUtility.IsFilled(in item.m_ipAddress)
                && E_StringUtility.IsFilled(in item.m_idName)
                && int.TryParse(item.m_port, out int port))
            {
                JavaOMI previous = JavaOMI.GetRegistered(item.m_idName);
                if (previous != null)
                    previous.StopThread();

                JavaOMI jomi = new JavaOMI(
                        new JavaOpenMacroCommunicationProcess(
                            item.m_ipAddress,
                            port, m_threadPriority
                            ));

                JavaOMI.RegisterShortcut(udpTargetName, jomi, true);

            }
        }
        foreach (var item in m_import.m_xomiUdpTargets)
        {
            m_xomiSender.Clear();
           item.GetTargetIdName(out string udpTargetName);
           if (E_StringUtility.IsFilled(in item.m_ipAddress)
                && E_StringUtility.IsFilled(in item.m_idName)
                && int.TryParse(item.m_port, out int port))
            {
                m_xomiSender.AddAlias(new IpSingleAlias(item.m_idName, item.m_ipAddress , port ));
            }

        }
        m_xomiSender.ResetTheTargetFromAlias();

        foreach (var item in m_import.m_mouse2booleans)
        {
            m_mouse.m_mouseOrientation.m_N = item.m_north;
            m_mouse.m_mouseOrientation.m_S = item.m_south;
            m_mouse.m_mouseOrientation.m_E = item.m_east;
            m_mouse.m_mouseOrientation.m_W = item.m_west;
            m_mouse.m_mouseOrientation.m_SE = item.m_southEast;
            m_mouse.m_mouseOrientation.m_SW = item.m_southWest;
            m_mouse.m_mouseOrientation.m_NE = item.m_northEast;
            m_mouse.m_mouseOrientation.m_NW = item.m_northWest;
            m_mouse.m_mouseMovingNamed = item.m_mouseMove;
            m_mouse.m_mouseMovingEndDelay = item.m_mouseMoveEndDelayInSeconds;

            m_mouseWheel.m_mouseWheelLeft = item.m_wheelLeft;
            m_mouseWheel.m_mouseWheelRight = item.m_wheelRight;
            m_mouseWheel.m_mouseWheelUp = item.m_wheelUp;
            m_mouseWheel.m_mouseWheelDown = item.m_wheelDown;
        }

    }
}
