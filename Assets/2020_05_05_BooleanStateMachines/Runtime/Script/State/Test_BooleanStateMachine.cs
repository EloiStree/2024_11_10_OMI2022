using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using UnityEngine;

public class Test_BooleanStateMachine : MonoBehaviour
{
    [Header("State 1")]
    public string m_regex;
    public RegexBoolState m_state1;
    public bool m_stateResult1;
    [Header("State 2")]
    public string m_regex2;
    public BooleanStateRegisterMono m_register;
    public BooleanGroup m_group;
    public RegexableValueType m_regexTarget;
    public float m_minTime=0.1f;
    public RegexBoolState m_state2;
    public bool m_stateResult2;

    public bool[] m_stateResults;
    [Header("State 3")]
    public List<BooleanStateStep> m_steps= new List<BooleanStateStep>();
    public List<RegexBoolState> m_regexStateFound = new List<RegexBoolState>();
    public List<AndBoolState> m_andStateFound = new List<AndBoolState>();
    public List<OrBoolState> m_orStateFound = new List<OrBoolState>();
    public List<XorBoolState> m_xorStateFound = new List<XorBoolState>();
    public List<ClassicBoolState> m_classicStateFound = new List<ClassicBoolState>();
    public List<SetBooleanStateAction> m_setActionFound = new List<SetBooleanStateAction>();
    public List<EmitTextAction> m_textActionFound = new List<EmitTextAction>();
    public List<CallFunctionAction> m_callActionFound    = new List<CallFunctionAction>();

    public LinearBooleanStateMachine m_stateMachine;
    void Start()
    {
       //s m_state2 = new RegexBoolState(m_regex2, m_group, m_regexTarget, m_minTime);
     //   TryToCreateStateMachine(testText);
    }

    void Update()
    {
        BooleanStateRegister reg = m_register.GetRegister();
        m_stateResult2 = m_state2.IsConditionValide(reg);
        for (int i = 0; i < m_steps.Count; i++)
        {
            if(m_steps[i]!=null)
            m_stateResults[i]= m_steps[i].IsConditionValide(reg);
        }


    }
    [TextArea(0,10)]
    public string testText="";
    //private void OnValidate()
    //{
    //    if(Application.isPlaying)
    //    TryToCreateStateMachine(testText);
    //}

    public void TryToCreateStateMachine(string textToStateMachine)
    {
        m_steps.Clear();
        m_regexStateFound.Clear();
        m_andStateFound.Clear();
        m_orStateFound.Clear();
        m_xorStateFound.Clear();
        m_setActionFound.Clear();
        m_textActionFound.Clear();
        m_callActionFound.Clear();

        string[] lines = Regex.Split(textToStateMachine, "\r\n|\r|\n");
        string line = "";
        SetBooleanStateAction setAction;
        EmitTextAction  textAction;
        CallFunctionAction callAction;
        RegexBoolState regexFound;
        ClassicBoolState classicFound;
        BooleanGroup groupTmp = null;
        BooleanGroup groupSelected = null;

        BooleanGroup groupAll = null;
        BooleanStateRegister reg = m_register.GetRegister();
            reg.GetAll(out groupAll);
        List<BooleanStateStep> steps = new List<BooleanStateStep>();
       
        AndBoolState andState;
        for (int i = 0; i < lines.Length; i++)
        {
            line = lines[i];
            string groupName = "";
            AbstractConditionBoolState abstractCondition;
            if (TextToBoolStateMachineParser.IsClassicParse(line, out classicFound))
            {
                steps.Add(classicFound);
                this.m_classicStateFound.Add(classicFound);
            }
            //else if (TextToBoolStateMachineParser.IsGroupParse(line, out groupName, out groupTmp)) {
            //    groupSelected = groupTmp;
            //}
            //else if (TextToBoolStateMachineParser.IsRegexStateParse(line,
            //    groupSelected != null ? groupSelected : groupAll, out regexFound))
            //{
            //    steps.Add(regexFound);
            //    this.m_regexStateFound.Add(regexFound);
            //}
            else if (TextToBoolStateMachineParser.GetConditionOf(line, out abstractCondition))
            {
                steps.Add(abstractCondition);
                if (abstractCondition is AndBoolState)
                    this.m_andStateFound.Add((AndBoolState)abstractCondition);
                if (abstractCondition is OrBoolState)
                    this.m_orStateFound.Add((OrBoolState)abstractCondition);
                if (abstractCondition is XorBoolState)
                    this.m_xorStateFound.Add((XorBoolState)abstractCondition);
            }
            else Debug.Log("Fail");
        }
        m_steps = steps;
        m_stateResults = new bool[steps.Count];
      
    }


}



