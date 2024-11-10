using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Test_CreateAndListenToLinearState : MonoBehaviour
{
    public BooleanStateRegisterMono m_register;
    public Test_TextToLineAndCondition m_linked;
    public List<LinearBooleanStateMachine> m_bsm;
    public bool m_useValideFailDebug;
    public int m_bsmFound;

    [System.Serializable]
    public class IteratorAndResult
    {

        [TextArea(1, 5)]
        public string m_description = "";
        public LinearBSMIterator m_iterators;
        public int m_index = 0;
        public bool m_stateValide = false;
        public bool m_hasFinish = false;
        public float m_cooldownState = -1f;

        public float m_decideMaxtimeBSM;
        public float m_decideMaxtimeState;
        public float m_maxStateTimeBSMCount=2f;
        public float m_maxStateTimeStateCount=0.7f;
        public IteratorAndResult(LinearBSMIterator iterator)
        {
            m_iterators = iterator;
        }

        public void SetCooldown(float time)
        {
            m_cooldownState = time;
        }

        public bool HasCooldown()
        {
            return m_cooldownState > 0f;
        }

        public void RemoveCoolddowntime(float deltaTime)
        {
            m_cooldownState -= deltaTime;
        }

        public void SetAsNotFinished()
        {
            m_hasFinish = true;
        }

        public void Reset()
        {
            m_iterators.ResetToReuse();



        }

        public bool HasReachMaxTimeStateMachine()
        {
            return m_maxStateTimeBSMCount > m_decideMaxtimeBSM;
        }
        public bool HasReachMaxTimeState()
        {
            return m_maxStateTimeStateCount > m_decideMaxtimeState;
        }

        public void AddStateTime(float deltaTime)
        {
            m_maxStateTimeBSMCount += deltaTime;
            m_maxStateTimeStateCount += deltaTime;
    }
        public void ResetMaxStateTime()
        {
            m_maxStateTimeBSMCount = 0;
        }
    }

    public IteratorAndResult[] m_iteratorsAndInfo;

    public TextEmittedEvent m_textEmitted;
    public CallFunctionEvent m_functionEmitted;
    public SetBooleanEvent m_setBooleanEmitted;

    void Start()
    {

        ReloadFromText();
    }
    private void OnDestroy()
    {

    }

    public void ReloadFromText()
    {


        m_bsm = BooleanStateMachineBuilder.GetStateMachineOf(m_register.GetRegister(), m_linked.m_text, m_useValideFailDebug);
        m_bsmFound = m_bsm.Count;
        m_iteratorsAndInfo = new IteratorAndResult[m_bsm.Count];
        for (int i = 0; i < m_bsm.Count; i++)
        {
            m_iteratorsAndInfo[i] = new IteratorAndResult(new LinearBSMIterator(m_bsm[i]));
            m_iteratorsAndInfo[i].m_description = m_bsm[i].GetPerLineDescription();
            Debug.Log(">>: " + m_iteratorsAndInfo[i].m_description);
        }
    }



    private void Update()
    {
        if (Time.timeSinceLevelLoad < 3) return;
        if (m_iteratorsAndInfo.Length <= 0) return;

        for (int i = 0; i < m_iteratorsAndInfo.Length; i++)
        {

           
            if (m_iteratorsAndInfo[i].HasCooldown())
            {
                m_iteratorsAndInfo[i].RemoveCoolddowntime(Time.deltaTime);
            }
            else
            {
                if (m_iteratorsAndInfo[i].m_index > 0)
                {
                    m_iteratorsAndInfo[i].AddStateTime(Time.deltaTime);
                    if (m_iteratorsAndInfo[i].HasReachMaxTimeStateMachine())
                        m_iteratorsAndInfo[i].Reset();
                    if (m_iteratorsAndInfo[i].HasReachMaxTimeState())
                        m_iteratorsAndInfo[i].Reset();
                }


                //Debug.Log("..." + m_bsm[i].GetStepsCount());
                m_iteratorsAndInfo[i].m_index = m_iteratorsAndInfo[i].m_iterators.GetIndex();
                if (m_iteratorsAndInfo[i].m_iterators.IsStateConditionValide())
                {

                    m_iteratorsAndInfo[i].ResetMaxStateTime();
                    BooleanStateAction[] actions = m_iteratorsAndInfo[i].m_iterators.GetStep().GetActions();
                    for (int j = 0; j < actions.Length; j++)
                    {
                        //Debug.Log("Do: " + actions[j]);
                        if (actions[j] is SetBooleanStateAction)
                            m_setBooleanEmitted.Invoke((SetBooleanStateAction)actions[j]);
                        else if (actions[j] is CallFunctionAction)
                            m_functionEmitted.Invoke((CallFunctionAction)actions[j]);
                        else if (actions[j] is EmitTextAction)
                            m_textEmitted.Invoke((EmitTextAction)actions[j]);
                    }
                    // if (m_iteratorsAndInfo[i].m_iterators.HasNextState())

                    m_iteratorsAndInfo[i].m_iterators.GoNextState();

                    if (m_iteratorsAndInfo[i].m_iterators.IsFinish())
                        m_iteratorsAndInfo[i].SetCooldown(0.1f);
                    //     Debug.Log("Dddd ");
                }
                else {
                    if (m_iteratorsAndInfo[i].m_iterators.IsFinish())
                    {
                        m_iteratorsAndInfo[i].Reset();
                    }
                    if (m_iteratorsAndInfo[i].HasReachMaxTimeStateMachine())
                    {

                        m_iteratorsAndInfo[i].Reset();
                    }
                }
                m_iteratorsAndInfo[i].m_stateValide = m_iteratorsAndInfo[i].m_iterators.IsStateConditionValide();
                m_iteratorsAndInfo[i].m_hasFinish = m_iteratorsAndInfo[i].m_iterators.IsFinish();
            }
        }
    }

}
[System.Serializable]
public class TextEmittedEvent : UnityEvent<EmitTextAction> { }
[System.Serializable]
public class CallFunctionEvent : UnityEvent<CallFunctionAction> { }
[System.Serializable]
public class SetBooleanEvent : UnityEvent<SetBooleanStateAction> { }
