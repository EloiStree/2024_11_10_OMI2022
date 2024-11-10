using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class UI_BooleanStateRegisterDebug : MonoBehaviour
{
    public BooleanStateRegisterMono m_linked;
    public Text m_textDebug;
    public int m_lenght=50;
    private List<BooleanState> stateRef;
    private BooleanStateRegister registerRef;
    StringBuilder m_sb = new StringBuilder();
    void Update()
    {
        m_sb.Clear();
        if (registerRef == null)
          m_linked.GetRegister(ref registerRef);
        if (stateRef == null)
            registerRef.GetAllState(ref stateRef);

        for (int i = 0; i < stateRef.Count; i++)
        {
            string d = BoolHistoryDescription.GetDescriptionNowToPast(stateRef[i].GetHistory());
            m_sb.Append( string.Format("{0}: {1}\n", stateRef[i].GetName(), Clamp(d, m_lenght)));

        }
        
        m_textDebug.text = m_sb.ToString();
    }

    private object Clamp(string text, int count)
    {
        if (text.Length < count)
            return text;
        return text.Substring(0, count);
    }
}
