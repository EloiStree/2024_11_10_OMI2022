using UnityEngine;

[System.Serializable]
public class TextLinkedToId
{
    public TextLinkedToId() { }
    public TextLinkedToId(string id, string text) {
        m_nameId = id;
        m_text = text;
    }
    public string m_nameId="";
    [TextArea(1, 10)]
    public string m_text="";

}