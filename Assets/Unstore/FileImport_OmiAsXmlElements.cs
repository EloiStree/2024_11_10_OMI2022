using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using UnityEngine;
using System.Xml.Serialization;
using OMIAbstraction;

public class FileImport_OmiAsXmlElements : MonoBehaviour
{

    public List<string> m_filesPathFound = new List<string>();
    public List<Omixml> m_xmlFoundAndValide= new List<Omixml>();
    public List<MidiConnectionIn> m_midiIn;
    public List<MidiConnectionOut> m_midiOut;
    public List<JomiUdpTarget> m_jomiUdpTargets;
    public List<XomiUdpTarget> m_xomiUdpTargets;
    public List<Mouse2booleans> m_mouse2booleans;
    public void Clear() {
        m_filesPathFound.Clear();
        m_filesPathFound.Clear();
        m_xmlFoundAndValide.Clear();
        m_midiIn.Clear();
        m_midiOut.Clear();
        m_jomiUdpTargets.Clear();
        m_xomiUdpTargets.Clear();
        m_mouse2booleans.Clear();

    }

    public void TryToImportAllFiles(string[] filesPath)
    {
        for (int i = 0; i < filesPath.Length; i++)
        {
            string p = filesPath[i];
            TryToImportAllFile(p);

        }

    }
    public void TryToImportAllFile(string filePath)
    {
        if (!File.Exists(filePath))
            return;

        m_filesPathFound.Add(filePath);
        string text  = File.ReadAllText(filePath);
      
         XmlSerializer serializer = new XmlSerializer(typeof(Omixml));
         using (StringReader reader = new StringReader(text))
         {
            Omixml omiXml = (Omixml)serializer.Deserialize(reader);
            m_xmlFoundAndValide.Add(omiXml);
            foreach (var item in omiXml.Midiconnectionin)
            {
                m_midiIn.Add(new MidiConnectionIn(item.MidiName));
            }
            foreach (var item in omiXml.Midiconnectionout)
            {
                m_midiOut.Add(new MidiConnectionOut(item.MidiName));
            }
            foreach (var item in omiXml.JomiUdpTarget)
            {
                m_jomiUdpTargets.Add(new JomiUdpTarget(item.JomiIdName, item.IpAddress, item.portName));
            }
            foreach (var item in omiXml.XomiUdpTarget)
            {
                m_xomiUdpTargets.Add(new XomiUdpTarget(item.XomiIdName, item.IpAddress, item.portName));
            }
            foreach (var item in omiXml.Mouse2Booleans)
            {

                Mouse2booleans m2b = new Mouse2booleans();
                 m2b.m_north        = item.m_north     ;
                 m2b.m_south        = item.m_south     ;
                 m2b.m_east         = item.m_east      ;
                 m2b.m_west         = item.m_west      ;
                 m2b.m_southEast    = item.m_southEast;
                m2b.m_southWest = item.m_southWest;
                m2b.m_northEast = item.m_northEast;
                m2b.m_northWest = item.m_northWest;
                m2b.m_mouseMove = item.m_mouseMove;
                 m2b.m_wheelLeft    = item.m_wheelLeft  ;
                 m2b.m_wheelUp      = item.m_wheelUp    ;
                 m2b.m_wheelDown    = item.m_wheelDown  ;
                 m2b.m_wheelRight   = item.m_wheelRight;
                m2b.m_mouseMoveEndDelayInSeconds = item.m_mouseMoveEndDelayInSeconds;

                 m_mouse2booleans.Add(m2b);
            }
        }

    }
}
