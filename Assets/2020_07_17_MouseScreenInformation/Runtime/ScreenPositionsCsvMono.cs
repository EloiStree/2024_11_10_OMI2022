using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

public class ScreenPositionsCsvMono : MonoBehaviour
{
    public bool m_importAtStart=true;
    public TextAsset [] m_scvZones;
    public NamedScreenPourcentZoneEvent m_zoneFound;
    public TextAsset [] m_scvPositions;
    public NamedScreenPourcentPositionEvent m_positionFound;
    public UnityEvent m_endLoading;
    void Start()
    {
        if (m_importAtStart)
            LoadCSV();
        
    }

    private void LoadCSV()
    {
        List<NamedScreenPourcentZone> zone= new List<NamedScreenPourcentZone>();
        List<NamedScreenPourcentPosition> positions= new List<NamedScreenPourcentPosition>();
        string txt = "";
        for (int i = 0; i < m_scvZones.Length; i++)
        {
            txt = m_scvZones[i].text;
            ExtractFromText(out zone, out positions, txt);

        }
        for (int i = 0; i < m_scvPositions.Length; i++)
        {
            txt = m_scvPositions[i].text;
            ExtractFromText(out zone, out positions, txt);
        }
        m_endLoading.Invoke();
    }

    private void ExtractFromText(out List<NamedScreenPourcentZone> zone, out List<NamedScreenPourcentPosition> positions, string txt)
    {
        ScreenInfo.ImportFromCSV(txt, out zone, out positions);
        for (int j = 0; j < zone.Count; j++)
        {
            m_zoneFound.Invoke(zone[j]);
        }
        for (int j = 0; j < positions.Count; j++)
        {
            m_positionFound.Invoke(positions[j]);
        }
    }
}
public class ScreenInfo
{
    public static void ImportFromCSV(string textCsv, out List<NamedScreenPourcentZone> pourcents, out List<NamedScreenPourcentPosition> positions)
    {
        ImportFromCSV(textCsv, out pourcents);
        ImportFromCSV(textCsv, out positions);
    }
    public static void ImportFromCSV(string textCsv,  out List<NamedScreenPourcentPosition> positions)
    {
        positions= new List<NamedScreenPourcentPosition>();
        foreach (string line in textCsv.Split('\n'))
        {
            string[] tokens = line.Split(',');
            if (tokens.Length == 3) {
                float leftRight, botTop;
                if (float.TryParse(tokens[1], out leftRight) 
                    && float.TryParse(tokens[2], out botTop)) {
                    positions.Add(
                        new NamedScreenPourcentPosition()
                        {
                            m_name = tokens[0],
                            m_position = new ScreenPositionInPourcentBean(leftRight, botTop)
                        });
                
                }
            
            }

        }

    }
    public static void ImportFromCSV(string textCsv, out List<NamedScreenPourcentZone> pourcents)
    {
       

         pourcents= new List<NamedScreenPourcentZone>();
        foreach (string line in textCsv.Split('\n'))
        {
            string[] tokens = line.Split(',');
            if (tokens.Length == 5)
            {
                float left1, bot1, left2,bot2;
                if (float.TryParse(tokens[1], out left1)
                    && float.TryParse(tokens[2], out bot1)
                    && float.TryParse(tokens[3], out left2)
                    && float.TryParse(tokens[4], out bot2))
                {
                    pourcents.Add(
                        new NamedScreenPourcentZone()
                        {
                            m_name = tokens[0],
                            m_zone = new ScreenZoneInPourcentBean(
                                left1,
                                bot1, 
                                bot2,
                                left2
                                )
                        });

                }

            }

        }
    }

    public static void ExportToCSV(out string textCsv, List<NamedScreenPourcentPosition> positions)
    {
        StringBuilder sb = new StringBuilder();
        textCsv = "";
        for (int i = 0; i < positions.Count; i++)
        {
            sb.Append(string.Format("{0},{1},{2}\n",
                positions[i].GetName(),
                positions[i].m_position.GetLeftToRightValue(),
                positions[i].m_position.GetBotToTopValue()));
        }
        textCsv = sb.ToString();
    }
    public static void ExportToCSV(out string textCsv, List<NamedScreenPourcentZone> zones)
    {
        StringBuilder sb = new StringBuilder();
        textCsv = "";
        for (int i = 0; i < zones.Count; i++)
        {
            sb.Append(  string.Format("{0},{1},{2},{3},{4}\n",
                zones[i].GetName(),
                zones[i].m_zone.GetBotLeft().GetLeftToRightValue(),
                zones[i].m_zone.GetBotLeft().GetBotToTopValue(),
                zones[i].m_zone.GetTopRight().GetBotToTopValue(),
                zones[i].m_zone.GetTopRight().GetLeftToRightValue()));
        }
        textCsv = sb.ToString();
    }
}
