using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;

public class DeleteMeMonkTest : MonoBehaviour
{

    public string m_conditionToTrigger = "aoe + !pause + wololo↓";
    public string m_selectTeam = "sc:9↓ 9↑";
    public string m_doAfter = "sc:Q↓ Q↑";
    public string m_doAfterUnselect = "sc:9↓ 9↑";
    public MouseRegisterJomiManager m_mouseRegister;
    public AgeOfEmpireDefinitiveEditionUnitySelect m_aoeSelector = new AgeOfEmpireDefinitiveEditionUnitySelect(15,4);
    public UI_ServerDropdownJavaOMI m_serverTarget;
    public float m_secondsTimeBeforeSave = 0.1f;
    public uint m_millisecondsBetweenMoveAndClick=20;
    public float m_secondsTimeBetweenSaveAndMove = 0.1f;
    public float m_minTimeBetween = 0.6f;
    //public float m_timeBetweenSelection = 1f;
    public BooleanStateRegisterMono m_register;
    public int m_currentIndex;
    public int m_maxIndex = 10;
    public bool m_selectTeamAfter=false;
    IEnumerator Start()
    {
        yield return new WaitForSeconds(4);
        while (true) {

            BooleanStateRegister reg = m_register.m_register;
            if (reg != null) { 
                ClassicBoolState condition;
                TextToBoolStateMachineParser.IsClassicParse(m_conditionToTrigger, out condition);
                if (condition.IsConditionValide(reg))
                {
                        int currentIndex = m_currentIndex;
                        SendCommand(0, m_selectTeam);
                        yield return SelectUnit(m_currentIndex);
                        m_currentIndex++;
                        if (m_currentIndex >= m_maxIndex)
                            m_currentIndex = 0;
                        SendCommand(100, m_doAfter);
                        SendCommand(150, "ms:r");
                      //  SendCommand(200, "sc:Escape↓ Escape↑");

                    if (m_doAfterUnselect.Length > 0)
                        SendCommand(250, m_selectTeam);

                    if (m_selectTeamAfter) {
                        yield return new WaitForSeconds(0.400f);
                        SendCommand(0, "sc: Control↓ ");
                        yield return SelectUnit(currentIndex);
                        SendCommand(60, "sc:  Control↑");
                    }
                    
                    yield return new WaitForSeconds(m_minTimeBetween);
                }
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void SendCommand(uint milli, string command)
    {
        foreach (var item in m_serverTarget.GetJavaOMISelected())
        {
            item.SendRawCommand(milli, command);
        }

    }
    public IEnumerator SelectUnit(int index)
    {

        m_mouseRegister.SaveMouseCursor("Wololo");
        yield return new WaitForSeconds(m_secondsTimeBetweenSaveAndMove);
        DateTime now = DateTime.Now;
        MoveMouseTo(now.AddMilliseconds(10),index);
        m_mouseRegister.RecoverMousePosition(now.AddMilliseconds(80).TimeOfDay, "Wololo");
    }
    public void MoveMouseTo(DateTime when,  int index)
    {
        bool hasPosition;
        ScreenPositionInPourcentBean bean;
        float lr, bt;
        m_aoeSelector.GetPourcent(index, out lr, out bt);
        foreach (var item in m_serverTarget.GetJavaOMISelected())
        {
            item.SendRawCommand(when.TimeOfDay, string.Format("mm:{0}%:{1}%", lr, bt));
            item.SendRawCommand(when.AddMilliseconds(m_millisecondsBetweenMoveAndClick).TimeOfDay, string.Format("ms:l"));
        }
      
    }

}


public class AgeOfEmpireDefinitiveEditionUnitySelect {

    public int m_vertical = 4;
    public int m_horizontal = 15;
    float m_pctHorizontalRange;
    float m_pctVerticalRange;
    public ScreenPositionInPourcentBean m_botLeft= new ScreenPositionInPourcentBean(0.157f,0.02f);
    public ScreenPositionInPourcentBean m_topRight = new ScreenPositionInPourcentBean(0.427f,0.145f);

    public AgeOfEmpireDefinitiveEditionUnitySelect(int horizontal, int vertical ) {
        m_vertical = vertical;
        m_horizontal = horizontal;
         m_pctHorizontalRange = 1f / (float)m_horizontal;
         m_pctVerticalRange = 1f / (float)m_vertical;
    }

    public void GetPourcent(int index, out float leftRightPct, out float botTopPct) {
        int x = index % m_horizontal, y = (int)(index / m_horizontal);

        float pctHorizontal = m_pctHorizontalRange/2f+x* m_pctHorizontalRange;
        float pctVertical = m_pctVerticalRange / 2f + y * m_pctVerticalRange;
        Debug.Log(string.Format("{0}-{1}  {2}x{3} ", pctHorizontal, pctVertical, x, y));
        leftRightPct = 0.5f;
        botTopPct = 0.5f;
         leftRightPct = m_botLeft.GetLeftToRightValue() + (m_topRight.GetLeftToRightValue()- m_botLeft.GetLeftToRightValue())*pctHorizontal ;
         botTopPct = (1f-m_topRight.GetBotToTopValue()) + ((1f - m_botLeft.GetBotToTopValue())-(1f-m_topRight.GetBotToTopValue()))*pctVertical;
    }

}