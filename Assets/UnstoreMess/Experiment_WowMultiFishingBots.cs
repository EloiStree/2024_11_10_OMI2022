using Eloi;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Experiment_WowMultiFishingBots : MonoBehaviour
{



    public bool m_useModuleFishing = true;
    public Eloi.AbstractMetaAbsolutePathFileMono m_jsonToLoad;
    public BooleanStateRegisterMono m_register;
    public CommandLineEvent m_emittedCommand;



    public FishingBots m_defaultValue = new FishingBots() {
        m_fishingBotsInfo = new FishingBot[] {
            new FishingBot(){
                m_recovertFishAndRecastCommand=
                "macro:wow:recoverfishbot0",
                m_isBotShouldBeFishingCondition=
                "isfishing",
                m_isFishingPresentCondition=
                "soundbot0",
            },
            new FishingBot(){
                m_recovertFishAndRecastCommand=
                "macro:wow:recoverfishbot1",
                m_isBotShouldBeFishingCondition=
                "isfishing",
                m_isFishingPresentCondition=
                "soundbot1",
            },
            new FishingBot(){
                m_recovertFishAndRecastCommand=
                "macro:wow:recoverfishbot2",
                m_isBotShouldBeFishingCondition=
                "isfishing",
                m_isFishingPresentCondition=
                "soundbot2",
            }
        }
    };
    public FishingBots m_importedValue;


    private void GetDefaultText(out string textToUse)
    {
        textToUse = JsonUtility.ToJson(m_defaultValue,true);
    }
    [ContextMenu("Reload File")]
    public void ReloadFile()
    {
        try
        {
            Eloi.E_FileAndFolderUtility.ImportOrCreateThenImport(
                out string json, m_jsonToLoad, GetDefaultText);
            m_importedValue = JsonUtility.FromJson<FishingBots>(json);
            m_useModuleFishing = m_importedValue.m_useFishingBotTool;
        }
        catch (Exception)
        {
            m_useModuleFishing = false;
        }

    }
}
//public 
//    public FishingKeepTrackOfBot[] m_bots;






//    public void Update()
//    {
//        if (m_importedValue.m_useFishingBotTool) { 
//            for (int i = 0; i < m_bots.Length; i++)
//            {
//                m_bots[i].UpdateFromUnity();
//            }
//        }
//    }
//}


[System.Serializable]
public class FishingBots
{
    public bool m_useFishingBotTool = true;
    public float m_pauseToAvoidRecastLoopInSeconds = 5;
    public float m_nothingHappenRecastInSeconds = 20;
    public FishingBot[] m_fishingBotsInfo= new FishingBot[0];

}
[System.Serializable]
public class FishingBot
{
    public string m_isBotShouldBeFishingCondition = "isfishing";
    public string m_isFishingPresentCondition = "hasfishsound";
    public string m_recovertFishAndRecastCommand = "macro:wow:collectandcast";
}

//[System.Serializable]
//public class FishingKeepTrackOfBot
//{
//    public FishingBot m_target= new FishingBot();
//    public DateTime m_fishDetected = DateTime.Now;
//    public string m_fishDetectedDebug="";
//    public DateTime m_lastCast = DateTime.Now;
//    public string m_lastCastDebug="";

//    public BooleanStateRegister m_register;

//    public FishingKeepTrackOfBot(FishingBot target,
//        ref BooleanStateRegister register )
//    {
//        m_target = target;
//        m_register = register;
//        TextToBoolStateMachineParser.IsClassicParse(
//            target.m_isFishingPresentCondition,
//            out m_triggerCastConditionState); 
//        TextToBoolStateMachineParser.IsClassicParse(
//             target.m_isBotShouldBeFishingCondition,
//             out m_loopFishingActiveConditionState);
//        SetActiveCondition(target.m_isBotShouldBeFishingCondition);
//        SetCastCondition(target.m_isFishingPresentCondition);


//    }

//    public bool m_isFishAppActive = true;
//    public float m_pauseDetectionTimeInSec = 5;
//    public float m_recallCastAfterTime = 20;
//    public CommandLineEvent m_actionWhenTrue= new CommandLineEvent();


//    [Header("Debug")]
//    public ClassicBoolState m_triggerCastConditionState;
//    public ClassicBoolState m_loopFishingActiveConditionState;
//    public BooleanSwitchListener m_isGameMakingFishingSound;
//    BooleanStateRegister reg = null;
//    public float m_pauseTheDetectionCooldown;
//    public float m_recallCastAfterTimeCooldown;


  
//    public void SetCastCondition(string condition)
//    {
//        TextToBoolStateMachineParser.IsClassicParse(condition
//            , out m_triggerCastConditionState);

//    }
//    public void SetActiveCondition(string condition)
//    {
//        TextToBoolStateMachineParser.IsClassicParse(condition
//            , out m_loopFishingActiveConditionState);

//    }

//    public void UpdateFromUnity()
//    {

//        if (m_loopFishingActiveConditionState.IsBooleansRegistered(reg))
//        {
//            m_isFishAppActive = m_loopFishingActiveConditionState.IsConditionValide(reg);
//        }

//        if (!m_isFishAppActive)
//            return;

//        if (m_isFishAppActive && m_triggerCastConditionState.IsBooleansRegistered(reg))
//        {


//            bool haschanged;
//            bool hasfishsound = m_triggerCastConditionState.IsConditionValide(reg);
//            m_isGameMakingFishingSound.SetValue(hasfishsound, out haschanged);

//            if (haschanged && hasfishsound)
//            {
//                if (m_pauseTheDetectionCooldown < 0f)
//                {
//                    CatchAndFishAgain();
//                }
//            }

//            if (m_pauseTheDetectionCooldown > 0)
//                m_pauseTheDetectionCooldown -= Time.deltaTime;

//            if (m_recallCastAfterTimeCooldown < m_recallCastAfterTime)
//                m_recallCastAfterTimeCooldown += Time.deltaTime;
//            if (m_recallCastAfterTimeCooldown >= m_recallCastAfterTime)
//            {
//                CatchAndFishAgain();
//            }

//        }
//    }

//    public void CatchAndFishAgain()
//    {
//        m_pauseTheDetectionCooldown = m_pauseDetectionTimeInSec;
//        m_recallCastAfterTimeCooldown = 0;
//        m_actionWhenTrue.Invoke(new CommandLine(m_target.m_recovertFishAndRecastCommand));
//    }
//}