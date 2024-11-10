using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Linq;
using UnityEngine;

public class XmlToAnalogDigitalCompressConfig : MonoBehaviour
{
    public static void LoadFromXML(string textXMl, 
        out List<NomenclatureLinkedToPortReference>  ports,
        out List<AnalogDigitalCompressorNomenclature> nomenclatures)
    {
        nomenclatures = new List<AnalogDigitalCompressorNomenclature>();
        ports = new List<NomenclatureLinkedToPortReference>();

        //Source: https://docs.microsoft.com/en-us/dotnet/api/system.xml.linq.xnode.readfrom?view=netcore-3.1
        foreach (var item in GetNodeInXml(textXMl, "PortConnection"))
        {
            NomenclatureLinkedToPortReference portRef = 
                new NomenclatureLinkedToPortReference(
                    uint.Parse( (string)item.Attribute("portId")),
                     (string)item.Attribute("patternName"));
            ports.Add(portRef);
        }
        
        foreach (var item in GetNodeInXml(textXMl, "AnalogDigitalCompressPattern"))
        {
            //Debug.Log(item);
            AnalogDigitalCompressorNomenclature nomenclature = new AnalogDigitalCompressorNomenclature();
            nomenclature.m_nomenclatureName = (string)item.Attribute("name");
            nomenclature.m_documentationInfoUrl = (string)item.Attribute("docUrl");
            foreach (var it in item.Elements())
            {
                if (it.Name == "analog")
                {
                    try {
                        AnalogToLabelWithRange a = new AnalogToLabelWithRange();
                        nomenclature.m_analogLabel.Add(a);
                        a.m_index = uint.Parse( (string) it.Attribute("index"));
                        a.m_labelId = (string) it.Attribute("label");
                        a.m_min = short.Parse((string)it.Attribute("from"));
                        a.m_max = short.Parse((string)it.Attribute("to"));
                    }
                    catch (Exception) { }
                }
                if (it.Name == "digit")
                {
                    try
                    {
                        DigitToLabel d = new DigitToLabel();
                        nomenclature.m_digitLabel.Add(d);
                        d.m_index = uint.Parse((string)it.Attribute("index"));
                        d.m_labelId = (string) it.Attribute("label");
                    }
                    catch (Exception) { }
                }
            }

            nomenclatures.Add(nomenclature);
        }
    }
    static IEnumerable<XElement> GetNodeInXml(string text, string nodeName)
    {
        using (XmlReader reader = XmlReader.Create(new StringReader(text))) 
        {
            reader.MoveToContent();

            // Parse the file and return each of the nodes.
            while (!reader.EOF)
            {
                if (reader.NodeType == XmlNodeType.Element && reader.Name == nodeName)
                {
                    XElement el = XElement.ReadFrom(reader) as XElement;
                    if (el != null)
                        yield return el;
                }
                else
                {
                    reader.Read();
                }
            }
        }
    }

}



