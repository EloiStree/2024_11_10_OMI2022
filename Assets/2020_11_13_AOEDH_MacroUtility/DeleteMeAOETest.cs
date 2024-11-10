using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DeleteMeAOETest : MonoBehaviour
{

    public UI_ServerDropdownJavaOMI m_target;
    public BooleanStateRegisterMono m_register;
    public OMI_AOE_ProposedMacroAndShortcut m_aoeMacro;

    public List<Trigger> m_triggers = new List<Trigger>();
    public List<TriggerToAction> m_triggersCustom = new List<TriggerToAction>();


    [System.Serializable]
    public class Trigger
    {
        public string m_conditionIn;
        public ClassicBoolState m_conditionInState;
        public string m_idIn;
        public string m_idOut;
        public BooleanSwitchListener m_swithcer= new BooleanSwitchListener();

    }
    [System.Serializable]
    public class TriggerToAction: Trigger
    {
        public UnityEvent m_whatToDoIn;
        public UnityEvent m_whatToDoOut;
    }
    
    private void Start()
    {
        m_triggers.Add(new Trigger()
        {
            m_conditionIn = "j",
            m_idIn = "CreateGroupAntiArcher"
        });
        m_triggers.Add(new Trigger()
        {
            m_conditionIn = "o",
            m_idIn = "CreateGroupAntiHorse"
        });
        m_triggers.Add(new Trigger()
        {
            m_conditionIn = "i",
            m_idIn = "CreateGroupFoodHorse"
        });
        m_triggers.Add(new Trigger()
        {
            m_conditionIn = "k",
            m_idIn = "CreateGroupCastle"
        });
        m_triggers.Add(new Trigger()
        {
            m_conditionIn = "l",
            m_idIn = "SetRallyStable"
        }); m_triggers.Add(new Trigger()
        {
            m_conditionIn = "c",
            m_idIn = "SetRallyTown"
        });
        m_triggers.Add(new Trigger()
        {
            m_conditionIn = "m",
            m_idIn = "AllCheatCode"
        });

        m_triggers.Add(new Trigger()
        {
            m_conditionIn = "t",
            m_idIn = "GoToMiningCamp"
        });

        for (int i = 0; i < m_triggers.Count; i++)
        {
            TextToBoolStateMachineParser.IsClassicParse(m_triggers[i].m_conditionIn, out m_triggers[i].m_conditionInState);
        }
    }

   

    public void Update()
    {
        BooleanStateRegister reg=null;
        m_register.GetRegister(ref reg);
        for (int i = 0; i < m_triggers.Count; i++)
        {
            bool hasChange;
            bool state = m_triggers[i].m_conditionInState.IsConditionValide(reg);
            m_triggers[i].m_swithcer.SetValue(state, out hasChange);
            if (hasChange)
            {
                if (state)
                    TryToCall(m_triggers[i].m_idIn);
                else
                    TryToCall(m_triggers[i].m_idOut);
            }
        }
        for (int i = 0; i < m_triggersCustom.Count; i++)
        {
            bool hasChange;
            bool state = m_triggersCustom[i].m_conditionInState.IsConditionValide(reg);
            m_triggersCustom[i].m_swithcer.SetValue(state, out hasChange);
            if (hasChange)
            {
                if (state)
                    m_triggersCustom[i].m_whatToDoIn.Invoke();
                else
                    m_triggersCustom[i].m_whatToDoOut.Invoke();
            }
        }

    }

    public void TryToCall(string idToCall)
    {
        if (string.IsNullOrEmpty(idToCall)) return;

        List<string> cmds = new List<string>();
        m_aoeMacro.AppendCommands(idToCall, ref cmds);
        uint delay = 100;
        for (uint i = 0; i < cmds.Count; i++)
        {
            foreach (var item in m_target.GetJavaOMISelected())
            {
                item.SendRawCommand(i * delay, cmds[(int)i]);
            }
        }
    }
}
