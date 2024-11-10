using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Experiment_WowFishing : MonoBehaviour
{
    public bool m_fishingActive=true;
    public BooleanStateRegisterMono m_register;

    internal void Clear()
    {
        m_fishingActive = false;
        m_loopFishingActiveCondition = "";
        m_fishingCastCommand = "";
        m_triggerCastCondition = "";
    }

    public string m_loopFishingActiveCondition="isfishing";
    public string m_fishingCastCommand="macro:wow:collectandcast";
    public string m_triggerCastCondition = "hasfishsound";
    public float m_pauseDetectionTimeInSec=5;
    public float m_recallCastAfterTime = 20;
    public CommandLineEvent m_actionWhenTrue;
    

    [Header("Debug")]
    public ClassicBoolState m_triggerCastConditionState;
    public ClassicBoolState m_loopFishingActiveConditionState;
    public BooleanSwitchListener m_isGameMakingFishingSound;
    BooleanStateRegister reg = null;
    public float m_pauseTheDetectionCooldown;
    public float m_recallCastAfterTimeCooldown;

    
    public void Awake()
    {
        m_register.GetRegister(ref reg);
        SetActiveCondition(m_loopFishingActiveCondition);
        SetCastCondition(m_triggerCastCondition);


    }

    public void SetCastCondition(string condition)
    {
        m_triggerCastCondition = condition;
        TextToBoolStateMachineParser.IsClassicParse(m_triggerCastCondition, out m_triggerCastConditionState);

    }
    public void SetActiveCondition(string condition)
    {
        m_loopFishingActiveCondition = condition;
        TextToBoolStateMachineParser.IsClassicParse(m_loopFishingActiveCondition, out m_loopFishingActiveConditionState);

    }

    public void Update()
    {


        if (m_loopFishingActiveConditionState.IsBooleansRegistered(reg))
        {
            m_fishingActive = m_loopFishingActiveConditionState.IsConditionValide(reg);
        }

        if (!m_fishingActive)
            return;
       
        if (m_fishingActive && m_triggerCastConditionState.IsBooleansRegistered(reg) ) {


            bool haschanged;
            bool hasfishsound = m_triggerCastConditionState.IsConditionValide(reg);
            m_isGameMakingFishingSound.SetValue( hasfishsound, out haschanged);

            if (haschanged && hasfishsound)
            {
                if (m_pauseTheDetectionCooldown < 0f) {
                    CatchAndFishAgain();
                }
            }

            if (m_pauseTheDetectionCooldown > 0)
                m_pauseTheDetectionCooldown -= Time.deltaTime;

            if (m_recallCastAfterTimeCooldown < m_recallCastAfterTime)
                m_recallCastAfterTimeCooldown += Time.deltaTime;
            if (m_recallCastAfterTimeCooldown >= m_recallCastAfterTime)
            {
                CatchAndFishAgain();
            }

        }
    }

    public void CatchAndFishAgain()
    {
        m_pauseTheDetectionCooldown = m_pauseDetectionTimeInSec;
        m_recallCastAfterTimeCooldown = 0;
        m_actionWhenTrue.Invoke(new CommandLine(m_fishingCastCommand));
    }
}
