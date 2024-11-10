using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TextToCommandLinesWithCondition : MonoBehaviour
{
    public BooleanStateRegisterMono m_booleanRegister;
    public CommandLineEvent m_commandLineEmitted;
    public List<TextToActionsWithCondition> m_stringRegister = new List<TextToActionsWithCondition>();

    public void TryToPush(string text) {

        text = text.ToLower().Trim();
        if (m_booleanRegister == null)
            return;
        BooleanStateRegister reg=null;
        m_booleanRegister.GetRegister(ref reg);
        if (reg != null) { 
            for (int i = 0; i < m_stringRegister.Count; i++)
            {
                if (m_stringRegister[i] != null &&
                    m_stringRegister[i].m_textToCall == text) {

                    if (!m_stringRegister[i].HasCondition() ||
                        (m_stringRegister[i].HasCondition()
                    && m_stringRegister[i].IsConditionValide(reg))) { 

                        foreach (var item in m_stringRegister[i].m_commands)
                        {
                            m_commandLineEmitted.Invoke(item);
                        }
                    }
                }
            }
        }
    
    }

    public void Clear() {
        m_stringRegister.Clear();
    }
    public void Add(TextToActionsWithCondition textToActions) {
        m_stringRegister.Add(textToActions);
    }
}

[System.Serializable]
public class TextToActionsWithCondition {

    public TextToActionsWithCondition(string textToCall, ClassicBoolState condition, params ICommandLine[] commands) {
        m_textToCall = textToCall.ToLower().Trim();
        m_condition = condition;
        m_commands = commands.ToList();
    }

    public string m_textToCall;
    public ClassicBoolState m_condition;
    public List<ICommandLine> m_commands = new List<ICommandLine>();


    public bool HasCondition() { return m_condition != null; }
    public bool IsConditionValide(BooleanStateRegister register) {
      return  m_condition.IsBooleansRegistered(register) && m_condition.IsConditionValide(register);
    }
}
