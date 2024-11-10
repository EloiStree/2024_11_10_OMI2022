using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Test_TextToLineAndCondition : MonoBehaviour
{
    [TextArea(0,20)]
    public string m_textToConvert= "Ctlr + LeftArrow↑ ⮞ Ctlr + RightArrow↑ ⮞\"Outil de sélection\"";
    public TextAsLinesSplitInParts m_text;
     char[] m_lineSpliters = { '\n' ,'\r'};
     char[] m_linePartSpliters = { '➤', '⮞' };

    public void SetText(string text) {
        m_text = new TextAsLinesSplitInParts(text, m_lineSpliters, m_linePartSpliters);
    }
    private void OnValidate()
    {
        SetText(m_textToConvert);
    }
}

[System.Serializable]
public class TextAsLinesSplitInParts
{
    public List<LineAsParts> m_lines = new List<LineAsParts>();
    public TextAsLinesSplitInParts(string text, char[] splitsChar, char[] partSplitsChar)
    {
        string[] tokens = text.Split(splitsChar);
        for (int i = 0; i < tokens.Length; i++)
        {
            if (tokens[i] != null && tokens[i].Length > 0)
            {
                m_lines.Add(new LineAsParts(tokens[i], partSplitsChar));
            }
            //else m_lines.Add(new LinePart(" ", partSplitsChar));
        }
    }
    public List<string> GetParts()
    {
        List<string> l = new List<string>();
        for (int i = 0; i < m_lines.Count; i++)
        {
            l.AddRange(m_lines[i].GetParts());
        }
        return l;
    }
    public List<string> GetLinesAsText()
    {
        List<string> l = new List<string>();
        for (int i = 0; i < m_lines.Count; i++)
        {
            l.Add(m_lines[i].GetRawLine());
        }
        return l;
    }
    public List<LineAsParts> GetLines()
    {
        return m_lines;
    }
    [System.Serializable]
    public class LineAsParts
    {
        public string m_line = "";
        public List<string> m_parts = new List<string>();
        public LineAsParts(string text, char[] splitsChar)
        {
            string[] tokens = text.Split(splitsChar);
            m_line = text;
            for (int i = 0; i < tokens.Length; i++)
            {
                if (tokens[i] != null && tokens[i].Length > 0)
                {
                    m_parts.Add(tokens[i]);
                }


            }
        }

        public string GetRawLine()
        {
            return m_line;
        }

        public IEnumerable<string> GetParts()
        {
            return m_parts;
        }
    }

}
