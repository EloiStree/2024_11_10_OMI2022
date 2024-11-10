using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using UnityEngine;

public class FileImporter_BooleanSpliter : MonoBehaviour
{
    public BooleanSpliterRegister m_spliterRegister;
    public void ClearRegister()
    {
        m_spliterRegister.Clear();
    }

    public void Load(params string[] filePath)
    {
        for (int i = 0; i < filePath.Length; i++)
        {
            if (File.Exists(filePath[i]))
            {
                LoadFromXML(File.ReadAllText(filePath[i]));
         
            }
        }
    }

    public  void LoadFromXML(string textXMl  )
    {
        textXMl =textXMl.ToLower();
        //Source: https://docs.microsoft.com/en-us/dotnet/api/system.xml.linq.xnode.readfrom?view=netcore-3.1
        foreach (var item in GetNodeInXml(textXMl, "duospliter"))
        {
            DuoSpliter value = new DuoSpliter((string)item.Attribute("first"), (string)item.Attribute("second"));
            value.SetName((string)item.Attribute("name"));
            foreach (var it in item.Elements())
            {
                if (it.Name == "none") value.m_none = it.Value.Trim();
                if (it.Name == "onlyfirst") value.m_onlyfirst = it.Value.Trim();
                if (it.Name == "onlysecond") value.m_onlysecond = it.Value.Trim();
                if (it.Name == "both" || it.Name == "all") value.m_all = it.Value.Trim();
                if (it.Name == "firstrenamed") value.m_firstRenamed = it.Value.Trim();
                if (it.Name == "secondrenamed") value.m_secondRenamed = it.Value.Trim();

            }
            m_spliterRegister.Add(value);
        }

        foreach (var item in GetNodeInXml(textXMl, "joystick2dspliter"))
        {
            Joystick2DSpliter value = new Joystick2DSpliter((string)item.Attribute("forward"), (string)item.Attribute("backward"), (string)item.Attribute("left"), (string)item.Attribute("right"));
            value.SetName((string)item.Attribute("name"));
            foreach (var it in item.Elements())
            {
                if (it.Name == "none") value.m_none = it.Value.Trim();
                if (it.Name == "neutral") value.m_none = it.Value.Trim();
                if (it.Name == "se") value.m_se = it.Value.Trim();
                if (it.Name == "sw" || it.Name == "so") value.m_sw = it.Value.Trim();
                if (it.Name == "ne") value.m_ne = it.Value.Trim();
                if (it.Name == "nw" || it.Name == "no") value.m_nw = it.Value.Trim();

                if (it.Name == "s") value.m_s = it.Value.Trim();
                if (it.Name == "n") value.m_n = it.Value.Trim();
                if (it.Name == "e") value.m_e = it.Value.Trim();
                if (it.Name == "o" || it.Name == "w") value.m_w = it.Value.Trim();

            }
            m_spliterRegister.Add(value);
        }

        foreach (var item in GetNodeInXml(textXMl, "joystick3dspliter"))
        {
            Joystick3DSpliter value = new Joystick3DSpliter((string)item.Attribute("forward"), (string)item.Attribute("backward"), (string)item.Attribute("left"), (string)item.Attribute("right"), (string)item.Attribute("top"), (string)item.Attribute("down"));
            value.SetName((string)item.Attribute("name"));
            foreach (var it in item.Elements())
            {
                string v = it.Value.Trim();
                if (it.Name == "neutral")
                    value.m_none = v;
                if (it.Name == "ds")
                    value.Set(Joystick3DSpliter.Height.Down, Joystick3DSpliter.Direction.S, v);
                if (it.Name == "dn")
                    value.Set(Joystick3DSpliter.Height.Down, Joystick3DSpliter.Direction.N, v);
                if (it.Name == "de")
                    value.Set(Joystick3DSpliter.Height.Down, Joystick3DSpliter.Direction.E, v);
                if (it.Name == "do" || it.Name == "dw")
                    value.Set(Joystick3DSpliter.Height.Down, Joystick3DSpliter.Direction.W, v);
                if (it.Name == "dse")
                    value.Set(Joystick3DSpliter.Height.Down, Joystick3DSpliter.Direction.SE, v);
                if (it.Name == "dso" || it.Name == "dsw")
                    value.Set(Joystick3DSpliter.Height.Down, Joystick3DSpliter.Direction.SW, v);
                if (it.Name == "dne")
                    value.Set(Joystick3DSpliter.Height.Down, Joystick3DSpliter.Direction.NE, v);
                if (it.Name == "dno" || it.Name == "dnw")
                    value.Set(Joystick3DSpliter.Height.Down, Joystick3DSpliter.Direction.NW, v);
                if (it.Name == "dc")
                    value.Set(Joystick3DSpliter.Height.Down, Joystick3DSpliter.Direction.C, v);

                if (it.Name == "ms")
                    value.Set(Joystick3DSpliter.Height.Middle, Joystick3DSpliter.Direction.S, v);
                if (it.Name == "mn")
                    value.Set(Joystick3DSpliter.Height.Middle, Joystick3DSpliter.Direction.N, v);
                if (it.Name == "me")
                    value.Set(Joystick3DSpliter.Height.Middle, Joystick3DSpliter.Direction.E, v);
                if (it.Name == "mo" || it.Name == "mw")
                    value.Set(Joystick3DSpliter.Height.Middle, Joystick3DSpliter.Direction.W, v);
                if (it.Name == "mse")
                    value.Set(Joystick3DSpliter.Height.Middle, Joystick3DSpliter.Direction.SE, v);
                if (it.Name == "mso" || it.Name == "msw")
                    value.Set(Joystick3DSpliter.Height.Middle, Joystick3DSpliter.Direction.SW, v);
                if (it.Name == "mne")
                    value.Set(Joystick3DSpliter.Height.Middle, Joystick3DSpliter.Direction.NE, v);
                if (it.Name == "mno" || it.Name == "mnw")
                    value.Set(Joystick3DSpliter.Height.Middle, Joystick3DSpliter.Direction.NW, v);
                if (it.Name == "mc")
                    value.Set(Joystick3DSpliter.Height.Middle, Joystick3DSpliter.Direction.C, v);

                if (it.Name == "ts")
                    value.Set(Joystick3DSpliter.Height.Up , Joystick3DSpliter.Direction.S, v);
                if (it.Name == "tn")
                    value.Set(Joystick3DSpliter.Height.Up , Joystick3DSpliter.Direction.N, v);
                if (it.Name == "te")
                    value.Set(Joystick3DSpliter.Height.Up , Joystick3DSpliter.Direction.E, v);
                if (it.Name == "to" || it.Name == "tw")
                    value.Set(Joystick3DSpliter.Height.Up , Joystick3DSpliter.Direction.W, v);
                if (it.Name == "tse")
                    value.Set(Joystick3DSpliter.Height.Up , Joystick3DSpliter.Direction.SE, v);
                if (it.Name == "tso" || it.Name == "tsw")
                    value.Set(Joystick3DSpliter.Height.Up , Joystick3DSpliter.Direction.SW, v);
                if (it.Name == "tne")
                    value.Set(Joystick3DSpliter.Height.Up , Joystick3DSpliter.Direction.NE, v);
                if (it.Name == "tno" || it.Name == "tnw")
                    value.Set(Joystick3DSpliter.Height.Up , Joystick3DSpliter.Direction.NW, v);
                if (it.Name == "tc")
                    value.Set(Joystick3DSpliter.Height.Up, Joystick3DSpliter.Direction.C, v);

            }
            m_spliterRegister.Add(value);
        }
        foreach (var item in GetNodeInXml(textXMl, "triolinespliter"))
        {
            TrioLineSpliter value = new TrioLineSpliter((string)item.Attribute("first"), (string)item.Attribute("second"), (string)item.Attribute("third"));
            value.SetName((string)item.Attribute("name"));
            foreach (var it in item.Elements())
            {
                if (it.Name == "ooo") value.m_ooo = it.Value.Trim();
                if (it.Name == "ooi") value.m_ooi = it.Value.Trim();
                if (it.Name == "oio") value.m_oio = it.Value.Trim();
                if (it.Name == "oii") value.m_oii = it.Value.Trim();
                if (it.Name == "ioo") value.m_ioo = it.Value.Trim();
                if (it.Name == "ioi") value.m_ioi = it.Value.Trim();
                if (it.Name == "iio") value.m_iio = it.Value.Trim();
                if (it.Name == "iii") value.m_iii = it.Value.Trim();

            }
            m_spliterRegister.Add(value);
        }
        foreach (var item in GetNodeInXml(textXMl, "quatrolinespliter"))
        {
            QuatroLineSpliter value = new QuatroLineSpliter((string)item.Attribute("first"), (string)item.Attribute("second"), (string)item.Attribute("third"), (string)item.Attribute("fourth"));
            value.SetName((string)item.Attribute("name"));
            foreach (var it in item.Elements())
            {
                if (it.Name == "oooo") value.m_oooo = it.Value.Trim();
                if (it.Name == "oooi") value.m_oooi = it.Value.Trim();
                if (it.Name == "ooio") value.m_ooio = it.Value.Trim();
                if (it.Name == "ooii") value.m_ooii = it.Value.Trim();
                if (it.Name == "oioo") value.m_oioo = it.Value.Trim();
                if (it.Name == "oioi") value.m_oioi = it.Value.Trim();
                if (it.Name == "oiio") value.m_oiio = it.Value.Trim();
                if (it.Name == "oiii") value.m_oiii = it.Value.Trim();
                if (it.Name == "iooo") value.m_iooo = it.Value.Trim();
                if (it.Name == "iooi") value.m_iooi = it.Value.Trim();
                if (it.Name == "ioio") value.m_ioio = it.Value.Trim();
                if (it.Name == "ioii") value.m_ioii = it.Value.Trim();
                if (it.Name == "iioo") value.m_iioo = it.Value.Trim();
                if (it.Name == "iioi") value.m_iioi = it.Value.Trim();
                if (it.Name == "iiio") value.m_iiio = it.Value.Trim();
                if (it.Name == "iiii") value.m_iiii = it.Value.Trim();

            }
            m_spliterRegister.Add(value);
        }

        foreach (var item in GetNodeInXml(textXMl, "fingerspliter"))
        {
            FingerSpliter value = new FingerSpliter((string)item.Attribute("pinky"), (string)item.Attribute("ring"), (string)item.Attribute("middle"), (string)item.Attribute("index"), (string)item.Attribute("thumb"));
            value.SetName((string)item.Attribute("name"));
            foreach (var it in item.Elements())
            {
                if (it.Name == "ooooo") value.m_ooooo = it.Value.Trim();
                if (it.Name == "ooooi") value.m_ooooi = it.Value.Trim();
                if (it.Name == "oooio") value.m_oooio = it.Value.Trim();
                if (it.Name == "oooii") value.m_oooii = it.Value.Trim();
                if (it.Name == "ooioo") value.m_ooioo = it.Value.Trim();
                if (it.Name == "ooioi") value.m_ooioi = it.Value.Trim();
                if (it.Name == "ooiio") value.m_ooiio = it.Value.Trim();
                if (it.Name == "ooiii") value.m_ooiii = it.Value.Trim();
                if (it.Name == "oiooo") value.m_oiooo = it.Value.Trim();
                if (it.Name == "oiooi") value.m_oiooi = it.Value.Trim();
                if (it.Name == "oioio") value.m_oioio = it.Value.Trim();
                if (it.Name == "oioii") value.m_oioii = it.Value.Trim();
                if (it.Name == "oiioo") value.m_oiioo = it.Value.Trim();
                if (it.Name == "oiioi") value.m_oiioi = it.Value.Trim();
                if (it.Name == "oiiio") value.m_oiiio = it.Value.Trim();
                if (it.Name == "oiiii") value.m_oiiii = it.Value.Trim();
                if (it.Name == "ioooo") value.m_ioooo = it.Value.Trim();
                if (it.Name == "ioooi") value.m_ioooi = it.Value.Trim();
                if (it.Name == "iooio") value.m_iooio = it.Value.Trim();
                if (it.Name == "iooii") value.m_iooii = it.Value.Trim();
                if (it.Name == "ioioo") value.m_ioioo = it.Value.Trim();
                if (it.Name == "ioioi") value.m_ioioi = it.Value.Trim();
                if (it.Name == "ioiio") value.m_ioiio = it.Value.Trim();
                if (it.Name == "ioiii") value.m_ioiii = it.Value.Trim();
                if (it.Name == "iiooo") value.m_iiooo = it.Value.Trim();
                if (it.Name == "iiooi") value.m_iiooi = it.Value.Trim();
                if (it.Name == "iioio") value.m_iioio = it.Value.Trim();
                if (it.Name == "iioii") value.m_iioii = it.Value.Trim();
                if (it.Name == "iiioo") value.m_iiioo = it.Value.Trim();
                if (it.Name == "iiioi") value.m_iiioi = it.Value.Trim();
                if (it.Name == "iiiio") value.m_iiiio = it.Value.Trim();
                if (it.Name == "iiiii") value.m_iiiii = it.Value.Trim();

            }
            m_spliterRegister.Add(value);
        }

        foreach (var item in GetNodeInXml(textXMl, "diamondspliter"))
        {
            DiamondSpliter value = new DiamondSpliter((string)item.Attribute("top"), (string)item.Attribute("down"), (string)item.Attribute("left"), (string)item.Attribute("right"));
            value.SetName((string)item.Attribute("name"));
            foreach (var it in item.Elements())
            {
                if (it.Name == "none") value.m_none = it.Value.Trim();
                if (it.Name == "ne") value.m_ne = it.Value.Trim();
                if (it.Name == "no" || it.Name == "nw") value.m_nw = it.Value.Trim();
                if (it.Name == "se") value.m_se = it.Value.Trim();
                if (it.Name == "so" || it.Name == "sw") value.m_sw = it.Value.Trim();
                if (it.Name == "vertical") value.m_vertical = it.Value.Trim();
                if (it.Name == "horizontal") value.m_horizontal = it.Value.Trim();
                if (it.Name == "fulldown") value.m_fulldown = it.Value.Trim();
                if (it.Name == "fullup") value.m_fullup = it.Value.Trim();
                if (it.Name == "fullleft") value.m_fullleft = it.Value.Trim();
                if (it.Name == "fullright") value.m_fullright = it.Value.Trim();
                if (it.Name == "all") value.m_all = it.Value.Trim();

            }
            m_spliterRegister.Add(value);
        }
        foreach (var item in GetNodeInXml(textXMl, "squarespliter"))
        {
            SquareSpliter value = new SquareSpliter((string)item.Attribute("topleft"), (string)item.Attribute("topright"), (string)item.Attribute("downleft"), (string)item.Attribute("downright"));
            value.SetName((string)item.Attribute("name"));
            foreach (var it in item.Elements())
            {
                if (it.Name == "none") value.m_none = it.Value.Trim();
                if (it.Name == "all") value.m_all = it.Value.Trim();

                if (it.Name == "onlytopleft") value.m_onlytopleft = it.Value.Trim();
                if (it.Name == "onlytopright") value.m_onlytopright = it.Value.Trim();
                if (it.Name == "onlydownleft") value.m_onlydownleft = it.Value.Trim();
                if (it.Name == "onlydownright") value.m_onlydownright = it.Value.Trim();

                if (it.Name == "allexcepttopleft") value.m_allexcepttopleft = it.Value.Trim();
                if (it.Name == "allexcepttopright") value.m_allexcepttopright= it.Value.Trim();
                if (it.Name == "allexceptdownleft") value.m_allexceptdownleft = it.Value.Trim();
                if (it.Name == "allexceptdownright") value.m_allexceptdownright = it.Value.Trim();

                if (it.Name == "fulldown") value.m_fulldown = it.Value.Trim();
                if (it.Name == "fullup") value.m_fullup = it.Value.Trim();
                if (it.Name == "fullleft") value.m_fullleft = it.Value.Trim();
                if (it.Name == "fullright") value.m_fullright = it.Value;
                if (it.Name == "slashdiagonal") value.m_slashdiagonal = it.Value.Trim();
                if (it.Name == "backslashdiagonal") value.m_backslashdiagonal = it.Value.Trim();

            }
            m_spliterRegister.Add(value);
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
                    try
                    {
                        reader.Read();
                    }
                    catch (Exception e) {
                        Debug.Log("XML error at import." + e.StackTrace);
                    }
                }
            }
        }
    }

}
