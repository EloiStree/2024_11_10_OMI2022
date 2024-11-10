//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class ConditionToJomiMacro : MonoBehaviour
//{

//    public BooleanStateRegisterMono m_booleanRegister;
//    public List<AndBooleanChangeToAction<string>> m_macroCallers = new List<AndBooleanChangeToAction<string>>();
//    public JomiMacroRawRegister m_macroRegister;


//    public bool[] testBoolean;
//    void Update()
//    {
//        BooleanStateRegister register = m_booleanRegister.m_register;
       

//        testBoolean = new bool[m_macroCallers.Count];
//        for (int i = 0; i < m_macroCallers.Count; i++)
//        {
//            AndBooleanChangeToAction<string> b2j = m_macroCallers[i];
//            bool isvalide = b2j.m_andBooleanState.IsConditionValide(register);
//            bool hasChange;
//            testBoolean[i] = isvalide;
//            b2j.m_eventListener.SetValue(isvalide, out hasChange);
//            if (hasChange)
//            {
//                if (isvalide && b2j.m_listenToTrueChange)
//                {
//                    m_macroRegister.Call(b2j.m_informationToTrigger);
//                }

//                if (!isvalide && b2j.m_listenToFalseChange)
//                {
//                    m_macroRegister.Call(b2j.m_informationToTrigger);
//                }


//            }
//            if (b2j.m_looperTrue != null && b2j.m_useLoopTrue)
//                b2j.m_looperTrue.SetAsActive(isvalide);
//            if (b2j.m_looperFalse != null && b2j.m_useLoopFalse)
//                b2j.m_looperFalse.SetAsActive(!isvalide);
//            SendShortCutWhenLoopIsReady(b2j.m_looperFalse);
//            SendShortCutWhenLoopIsReady(b2j.m_looperTrue);
//        }
//    }


//    private void SendShortCutWhenLoopIsReady(ActionLooper<SendJomiShortcut> loop)
//    {
//        if (loop == null) return;
//        if (!loop.IsActive()) return;
//        loop.RemoveTimeToCountdown(Time.deltaTime);
//        if (loop.IsReadyToExecute())
//        {
//            loop.DoDefaultAction();
//            loop.ResetTCountDown();
//        }
//    }


//    public void Clear()
//    {
//        m_macroCallers.Clear();
//    }
//    public void Add(AndBooleanChangeToAction<string> macroCaller)
//    {
//        m_macroCallers.Add(macroCaller);
//    }
//}

