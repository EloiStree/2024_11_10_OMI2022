using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BooleanStateMachineBuilder 
{

    public static List<LinearBooleanStateMachine> GetStateMachineOf(BooleanStateRegister reg, TextAsLinesSplitInParts textToTranlsate, bool useDebug)
    {
        List<LinearBooleanStateMachine> result = new List<LinearBooleanStateMachine>();
        LinearBSMBuilder builder = LinearBSMBuilder.Create();
        builder.Set(reg);

        ClassicBoolState classicState;
        RegexBoolState regexState;
        AbstractConditionBoolState conditionState;

        EmitTextAction textEmit;
        CallFunctionAction fctEmit;
        SetBooleanStateAction setBooleanEmit;

        string groupName;
        BooleanGroup lastGroupFound;
        TextAsLinesSplitInParts.LineAsParts previousLine = null;
        foreach (TextAsLinesSplitInParts.LineAsParts line in textToTranlsate.GetLines())
        {

            builder.FlushLastState();
            if (previousLine != null && builder.IsValide())
            {
                if (useDebug)
                    Debug.Log("Valide Line:" + previousLine.GetRawLine() + "\n" + builder.BuildHistory());
                result.Add(builder.Finalize());
            }
            else if (previousLine != null)
            {
                if (useDebug)
                    Debug.Log("FAIL: " + previousLine.GetRawLine() + "\n" + builder.BuildHistory());
            }
            builder.Reset();
            builder.Set(reg);

            foreach (string item in line.GetParts())
            {

                if (TextToBoolStateMachineParser.IsClassicParse(item, out classicState))
                {
                    builder.Add(classicState);
                }
                //else if (TextToBoolStateMachineParser.IsTextActionParse(item, out textEmit))
                //{
                //    builder.Add(textEmit);
                //}
                //else if (TextToBoolStateMachineParser.IsFunctionActionParse(item, out fctEmit))
                //{
                //    builder.Add(fctEmit);
                //}
                //else if (TextToBoolStateMachineParser.IsSetBooleanActionParse(item, out setBooleanEmit))
                //{
                //    builder.Add(setBooleanEmit);
                //}
                else if (TextToBoolStateMachineParser.GetConditionOf(item, out conditionState))
                {
                    builder.Add(conditionState);
                }
                //else if (TextToBoolStateMachineParser.IsGroupParse(item, out groupName, out lastGroupFound))
                //{
                //    builder.Set(lastGroupFound);
                //}
                //else if (TextToBoolStateMachineParser.IsRegexStateParse(item, lastGroupFound, out regexState))
                //{
                //    builder.Add(regexState);
                //}
            }
            previousLine = line;
        }
        if (builder.IsValide())
        {
            result.Add(builder.Finalize());
        }
        return result;

    }


}
