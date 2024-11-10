using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

public class TextToBoolStateMachineParser {


   


   
 

    public static bool GetConditionOf(string line, out AbstractConditionBoolState conditionState ) {
        string conditionName = "";
        float time;
        List<BooleanValueChangeRef> lc;
        List<BooleanValueRef> lv;
        conditionState = null;
        if (!IsConditionStateParse(line, out conditionName, out time, out lv, out lc))
            return false;

        if (conditionName.ToLower().IndexOf("and")==0)
            conditionState = new AndBoolState(lv, lc, time);
        else if (conditionName.ToLower().IndexOf("or") == 0)
            conditionState = new OrBoolState(lv, lc, time);
        else if (conditionName.ToLower().IndexOf("xor") == 0)
            conditionState = new XorBoolState(lv, lc, time);
        return conditionState!=null;
    }



    public static string FindAndSubstring(string line ,char charToSeek, out bool didfound, out string textFound) {
        didfound = false; textFound = "";

        int index = line.IndexOf(charToSeek);
        if (index < 0) return line;
        didfound = true;
        textFound = line.Substring(0, index);
        return line.Substring(index + 1);
    }


    public static bool IsConditionStateParse(string line,out string conditionName, out float timeAsSecond,out List<BooleanValueRef> valueState, out List<BooleanValueChangeRef> valueChange)
    {
        conditionName = "";
        timeAsSecond = 0.3f;
        valueChange = new List<BooleanValueChangeRef>();
        valueState = new List<BooleanValueRef>();


        int index = line.IndexOf('|');
        if (index < 0) return false;
        conditionName = line.Substring(0, index);
        line = line.Substring(index+1);
        //Debug.Log("Word:"+conditionName +">>"+line + ":");


        index = line.IndexOf('|');
        if (index > 0)
        {
            float miliSecondValue ;
            string milisecondAsText = line.Substring(0, index);
            if (float.TryParse(milisecondAsText, out miliSecondValue))
            {
                timeAsSecond =miliSecondValue/ 1000;
            }
            else timeAsSecond = 0.3f;
            line = line.Substring(index + 1);
            //Debug.Log("Time:" + milisecondAsText + ">>" + line+":");
        }

        //Debug.Log("Line#" + line + "#");
        BooleanValueChangeRef.CreateFrom(line.Split(' '), out valueChange);
        //Debug.Log("Change:" + string.Join(" ", valueChange));
        BooleanValueRef.CreateFrom(line.Split(' '), out valueState);
        //Debug.Log("Value:" + string.Join(" ", valueState));
        return true;
    }

   
    public static bool IsClassicParse(string line, out ClassicBoolState classicState)
    {
        classicState = null;
        bool foundClassicToPrase = Regex.Matches(line, "[^!+a-zA-Z0-9▸\\s↑↓]").Count<=0;
        if (!foundClassicToPrase) 
            return false;
        //Debug.Log("Parse:" + line);
        line = line.Replace("+", " ");  
        List<BooleanValueChangeRef> valueChange ;
        List<BooleanValueRef> valueState ;
        BooleanValueChangeRef.CreateFrom(line.Split(' '), out valueChange);
        BooleanValueRef.CreateFrom(line.Split(' '), out valueState);
        classicState = new ClassicBoolState(valueState, valueChange, 0.5f);
        return true;
    }

    /*
    public static bool IsGroupParse(string line, out string name, out BooleanGroup group) {

        name = "";
        group = null;
        if (!line.StartsWith("Group|"))
            return false;
        line = line.Substring(6);
        int i = line.IndexOf('|');
        if (i < 0)
            name = "";
        else name = line.Substring(0,i);
        line = line.Substring(i+1);
        //Debug.Log(name + "-->" + line);
        group = new BooleanGroup(line.Split(' ').Where(k=>k.Trim().Length>0).ToArray() );
        return true;
    }
    public static bool IsSetBooleanActionParse(string line, out SetBooleanStateAction setBooleanEmit)
    {
        setBooleanEmit = null;
        bool valideCheck;
        string prefix = "";
        string variableName = "";
        line = FindAndSubstring(line, '|', out valideCheck, out prefix);
        if (!valideCheck)
            return false;
        if (!(prefix.Trim().ToLower() == "set" ))
            return false;

        line = FindAndSubstring(line, '|', out valideCheck, out variableName);
        if (!valideCheck)
            return false;

        setBooleanEmit = new SetBooleanStateAction("", variableName, line.Trim()=="true");
        return true;
    }
    public static bool IsTextActionParse(string line, out EmitTextAction textEmit)
    {
        textEmit = null;
        if (line == null)
            return false;
        line = line.Trim();
        if (line.Length > 2 && line[0] == '"' && line[line.Length - 1] == '"') {
            textEmit = new EmitTextAction(line.Substring(1, line.Length - 2));
            return true;
        }
        return false;
    }
    public static bool IsFunctionActionParse(string line, out CallFunctionAction fctEmit)
    {
        fctEmit = null;
        bool valideCheck;
        string prefix = "";
        string fctName = "";
        line = FindAndSubstring(line, '|', out valideCheck, out prefix);
        if (!valideCheck )
             return false;
        if(!(prefix.Trim().ToLower() == "fct" || prefix.Trim().ToLower() == "function"))
            return false;

        line = FindAndSubstring(line, '|', out valideCheck, out fctName);
        if (!valideCheck) 
            return false;

        fctEmit = new CallFunctionAction(fctName, line.Split(' ').Where(k=>k.Trim().Length>0).ToArray());
        return true;


    }
    public static bool IsRegexStateParse(string line, BooleanGroup group, out RegexBoolState regexState)
    {
        string regex;
        string action;
        RegexableValueType target;
        float delayToCheck = 0.1f;
        bool found = IsRegexStateParse(line, out regex, out action, out target, out delayToCheck);
        regexState = new RegexBoolState(regex, group, target, delayToCheck);
        return found;
    }
    public static bool IsRegexStateParse(string line, out string regex, out string action, out RegexableValueType target, out float delayToCheck)
    {
        //"Regex|NL|300|(A↓.*Z↓)|(Z↓.*A↓)>\"Step 1\""  //↑
        regex = "";
        action = "";
        target = RegexableValueType.NewToLast;
        delayToCheck = 0;

        ////////////////
        if (!line.ToLower().StartsWith("regex|"))
            return false;
        line = line.Substring(6);

        ////////////////
        string actionPattern = "(>\\s*\".*\"\\s*$)|(>\\s*$)";
        Match m = Regex.Match(line, actionPattern);
        if (m.Length > 0)
        {
            string actionToken = m.Value;
            int start = actionToken.IndexOf("\"");
            int end = actionToken.LastIndexOf("\"");
            if (start < 0)
                start = 0;
            if (end < 0)
                end = actionToken.Length - 1;
            if (actionToken.Length > 1)
                action = actionToken.Substring(start + 1, end - start - 1);
            else action = "";
            line = line.Substring(0, line.Length - m.Value.Length);
        }

        ////////////////
        if (line.ToLower().StartsWith("nl|"))
        {
            target = RegexableValueType.NewToLast;
            line = line.Substring(3);
        }
        else if (line.ToLower().StartsWith("ln|"))
        {
            target = RegexableValueType.LastToNew;
            line = line.Substring(3);
        }
        else if (line.ToLower().StartsWith("on|"))
        {
            target = RegexableValueType.On;
            line = line.Substring(3);
        }
        else if (line.ToLower().StartsWith("off|"))
        {
            target = RegexableValueType.Off;
            line = line.Substring(4);
        }

        int timeIndex = line.IndexOf("|");
        if (timeIndex < 0) return false;

        string t = line.Substring(0, timeIndex);
        float v = 0.3f;
        if (!float.TryParse(t, out v))
        {
            return false;
        }
        else delayToCheck = v / 1000f;
        regex = line.Substring(timeIndex + 1);
        return true;
    }
     
     
     */
}
