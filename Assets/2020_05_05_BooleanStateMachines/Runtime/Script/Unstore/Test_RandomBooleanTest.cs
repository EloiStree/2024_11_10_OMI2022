using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class Test_RandomBooleanTest : MonoBehaviour
{
    public BooleanStateRegisterMono m_linked;
    public BooleanGroup m_observeGroup = new BooleanGroup("ArrowUp", "ArrowLeft", "ArrowRight", "ArrowDown");
    public BooleanGroup m_listened = new BooleanGroup("ArrowUp", "ArrowDown");

    [TextArea(0, 2)]
    public string m_areUp;
    [TextArea(0, 2)]
    public string m_areDown;


    public bool m_groupOR;
    public bool m_groupAND;
    public bool m_groupXOR;
    public bool m_groupNOR;
    public bool m_groupNAND;

    public bool m_listenedAreDown;
    public bool m_listenedAreDownStrict;
    public bool m_listenedAreUp;
    public bool m_listenedAreUpStrict;

    [Range(0, 3)]
    public float m_timeToCheck = 1;
    public string m_recentToLatestChange = "";
    public string m_latestToRecentChange = "";
    public string m_test = "";

    public string m_regex;
    public InputField m_inputRecent;
    public InputField m_inputLatest;
    public Image m_imgRecent;
    public Image m_imgLatest;
    public InputField m_inputOn;
    public InputField m_inputOff;


    void Update()
    {

        BooleanStateRegister reg = m_linked.GetRegister();
        List<string> areDown = new List<string>();
        List<string> areUp = new List<string>();
        foreach (var item in m_listened.GetNames())
        {
            if (reg.GetStateOf(item).GetValue())
                areDown.Add(item);
            else areUp.Add(item);
        }
        m_areDown = string.Join("+", areDown);
        m_areUp = string.Join("+", areUp);


        m_groupOR = BooleanStateUtility.OR(reg, m_listened);
        m_groupAND = BooleanStateUtility.AND(reg, m_listened);
        m_groupXOR = BooleanStateUtility.XOR(reg, m_listened);
        m_groupNOR = BooleanStateUtility.NOR(reg, m_listened);
        m_groupNAND = BooleanStateUtility.NAND(reg, m_listened);

        m_listenedAreDown = BooleanStateUtility.AreDown(reg, m_listened);
        m_listenedAreDownStrict = BooleanStateUtility.AreDownStrict(reg, m_observeGroup, m_listened);
        m_listenedAreUp = BooleanStateUtility.AreUp(reg, m_listened);
        m_listenedAreUpStrict = BooleanStateUtility.AreUpStrict(reg, m_observeGroup, m_listened);

        List<NamedBooleanChangeTimed> recentToLatest = BooleanStateUtility.GetBoolChangedBetweenTimeRangeFromNow(reg, m_observeGroup, m_timeToCheck, BooleanStateUtility.SortType.RecentToLatest);
        NamedBooleanChangeTimed[] latestToRecent = recentToLatest.ToArray();
        Array.Reverse(latestToRecent);

        m_latestToRecentChange = BooleanStateUtility.Description.Join("", recentToLatest);
        m_recentToLatestChange = BooleanStateUtility.Description.Join("", latestToRecent);
        CheckRegexFound();

        BoolHistory rbh = reg.GetStateOf("R").GetHistory();
        BoolStatePeriode[] change;
        rbh.GetFromPastToNow(out change, true);
        m_test = BooleanStateUtility.Description.Join("", change);

        string[] on = BooleanStateUtility.GetAllOnAsString(reg);
        string[] off = BooleanStateUtility.GetAllOffAsString(reg);

        if (m_inputOn)
            m_inputOn.text = string.Join(" ", on);
        if (m_inputOff)
            m_inputOff.text = string.Join(" ", off);

    }

    public void SetRegex(string regex) { m_regex = regex; }
    public void SetGroupSplitBySpace(string groupAsString) {
        string[] tokens = groupAsString.Split();
       m_observeGroup = new BooleanGroup(tokens.Where(k=>k.Length>0).ToArray());
    }

    private void CheckRegexFound()
    {
        Color green = new Color(0.4f, 0.8f, 0.4f);
        Color red = new Color(0.8f, 0.4f, 0.4f);

        if (m_inputRecent)
        {
            m_inputRecent.text = m_recentToLatestChange;
            if ( m_regex.Length > 0)
            {

                try
                {
                    Match m = Regex.Match(m_recentToLatestChange, m_regex);
                    if (m.Success)
                        m_imgRecent.color = green;
                    else
                        m_imgRecent.color = red;
                }
                catch (Exception e)
                {
                    m_imgRecent.color = red;
                }
            }
        }
        if (m_inputLatest)
        {
            m_inputLatest.text = m_latestToRecentChange;
            if ( m_regex.Length > 0)
            {
                try
                {
                    Match m = Regex.Match(m_latestToRecentChange, m_regex);
                    if (m.Success)
                        m_imgLatest.color = green;
                    else
                        m_imgLatest.color = red;
                }
                catch (Exception e)
                {
                    m_imgLatest.color = red;
                }
            }
        }
    }

}

