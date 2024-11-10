using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ToScreenPosition : MonoBehaviour
{
    public string m_zoneName="";
    public Canvas m_mainScreenZone;
    public RectTransform m_screenZone;
    [Header("Debug")]
    public bool m_useDebug;
    public ScreenZoneFullRecord m_spaceInCanvas = new ScreenZoneFullRecord();

  


    private void Update()
    {
        if(m_useDebug)
        Refresh();
    }
    private void OnValidate()
    {
        if (m_useDebug)
            Refresh();
    }
    private void Refresh()
    {
       ScreenPositionsUtility.GetZone( m_mainScreenZone, m_screenZone, out m_spaceInCanvas , m_zoneName);
    }
        
    public ScreenZoneFullRecord GetZoneInformationFromCanvas() {
        Refresh();
        return m_spaceInCanvas;
    }

    private void Reset()
    {
        m_mainScreenZone = GetComponentInParent<Canvas>();
        m_screenZone = GetComponent<RectTransform>();
    }
}

[System.Serializable]
public class NamedScreenPourcentZone : Namable {
    public ScreenZoneInPourcentBean m_zone=new ScreenZoneInPourcentBean();
    public NamedScreenPourcentZone() { }
    public NamedScreenPourcentZone(string name, float downTopMinPourcent, float downTopMaxPourcent, float leftRightMinPourcent, float leftRightMaxPourcent)
    {
        SetName(name);
        m_zone.SetWith(leftRightMinPourcent, downTopMinPourcent, downTopMaxPourcent, leftRightMaxPourcent);
    }
}

[System.Serializable]
public class ScreenZoneInPourcentBean
{
    public ScreenZoneInPourcentBean() : this(
       new ScreenPositionInPourcentBean(0, 0),
       new ScreenPositionInPourcentBean(1, 1))
    {

    }
    public ScreenZoneInPourcentBean(float botLeft2LeftPourcent, float botLeft2BotPourcent, float botLeft2TopPourcent, float botLeft2RightPourcent):this (
        new ScreenPositionInPourcentBean(botLeft2LeftPourcent, botLeft2BotPourcent),
        new ScreenPositionInPourcentBean(botLeft2RightPourcent, botLeft2TopPourcent))
    {

    }
    public ScreenZoneInPourcentBean(ScreenPositionInPourcentBean botLeft, ScreenPositionInPourcentBean topRight)
    {
        m_botLeft = botLeft;
        m_topRight = topRight;
    }

    public ScreenPositionInPourcentBean m_botLeft= new ScreenPositionInPourcentBean(0f,0f);
    public ScreenPositionInPourcentBean m_topRight = new ScreenPositionInPourcentBean(1f,1f);



    public ScreenPositionInPourcentBean GetPositionFromXY(float l2rPoucentAsX, float b2tPourcentAsY)
    {
        return GetPositionFromBotLeft((l2rPoucentAsX + 1f) / 2f, (b2tPourcentAsY + 1f) / 2f); 
    }
    public ScreenPositionInPourcentBean GetPositionFromBotLeft(float l2rPoucent, float b2tPourcent) {
        return new ScreenPositionInPourcentBean(
         Mathf.Lerp(m_botLeft.GetLeftToRightValue(), m_topRight.GetLeftToRightValue(), l2rPoucent)
            ,
         Mathf.Lerp(m_botLeft.GetBotToTopValue(), m_topRight.GetBotToTopValue(), b2tPourcent)
            );
    }
    public ScreenPositionInPourcentBean GetBotLeft() { return m_botLeft; }
    public ScreenPositionInPourcentBean GetBotRight()
    {
        return GetPositionFromBotLeft(1f, 0f);
    }
    public ScreenPositionInPourcentBean GetTopRight() { return m_topRight; }
    public ScreenPositionInPourcentBean GetTopLeft()
    {
        return GetPositionFromBotLeft(0.0f, 1f);
    }
    public ScreenPositionInPourcentBean GetCenter()
    {
        return GetPositionFromBotLeft(0.5f, 0.5f);
    }
    public ScreenPositionInPourcentBean GetLeft()
    {
        return GetPositionFromBotLeft(0f, 0.5f);
    }
    public ScreenPositionInPourcentBean GetRight()
    {
        return GetPositionFromBotLeft(1f, 0.5f);
    }
    public ScreenPositionInPourcentBean GetBot()
    {
        return GetPositionFromBotLeft(0.5f, 1f);
    }
    public ScreenPositionInPourcentBean GetTop()
    {
        return GetPositionFromBotLeft(0.5f, 0f);
    }

    public ScreenPositionInPourcentBean GetSquareRandom()
    {
        return GetPositionFromBotLeft(UnityEngine.Random.value, UnityEngine.Random.value);
    }
    public ScreenPositionInPourcentBean GetEliptiqueRandom()
    {
        return GetEliptique(UnityEngine.Random.Range(-1f,1f), UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(0f, 1f));
    }
    public ScreenPositionInPourcentBean GetEliptique(float horizontal, float vertical, float pourcentDistance=1f)
    {
        Vector2 test = new Vector2(horizontal, vertical).normalized *pourcentDistance;
        return GetPositionFromBotLeft((test .x+ 1f)/2f, (test.y + 1f) / 2f);
    }

    public IEnumerator<ScreenPositionInPourcentBean> GetBorderSubdivided(int subdivice)
    {
        throw new NotImplementedException();
    }
    public IEnumerator<ScreenPositionInPourcentBean> GetHorizontalCrossSubdivied(int subdivice)
    {
        throw new NotImplementedException();
    }
    public IEnumerator<ScreenPositionInPourcentBean> GetDiagonalCrossSubdivied(int subdivice)
    {
        throw new NotImplementedException();
    }
    public IEnumerable<ScreenPositionInPourcentBean> GetGrid(int columns, int lines, bool onborder=true)
    {
        List<ScreenPositionInPourcentBean> result = new List<ScreenPositionInPourcentBean>();
        if (onborder) {
            for (int l2r = 0; l2r < columns; l2r++)
            {
                for (int b2t = 0; b2t < lines; b2t++)
                {
                    result.Add( GetPositionFromBotLeft((float)l2r / ((float)(columns-1f)), (float)b2t / ((float)(lines-1f)) ));
                }

            }
        
        }
        return result;
    }

    public void SetWith(float bl_left, float bl_bot, float bl_top, float bl_right)
    {
        m_botLeft.SetBotToTopValue(bl_bot);
        m_botLeft.SetLeftToRightValue(bl_left);
        m_topRight.SetBotToTopValue(bl_top);
        m_topRight.SetLeftToRightValue(bl_right);
    }

    public bool IsInZone(float leftRightAsPoucent, float botTopAsPourcent)
    {
        return leftRightAsPoucent >= m_botLeft.GetLeftToRightValue() && leftRightAsPoucent <= m_topRight.GetLeftToRightValue()
            && botTopAsPourcent >= m_botLeft.GetBotToTopValue() && botTopAsPourcent <= m_topRight.GetBotToTopValue();
    }
}