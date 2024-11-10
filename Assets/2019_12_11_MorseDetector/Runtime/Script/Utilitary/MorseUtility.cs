using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class MorseUtility  {

    public static MorseValueToText[] ClassicMorseTable= new MorseValueToText[] {
        new MorseValueToText("A", ".-"),
        new MorseValueToText("B", "-..."),
        new MorseValueToText("C", "-.-."),
        new MorseValueToText("D", "-.."),
        new MorseValueToText("E", "."),
        new MorseValueToText("F", "..-."),
        new MorseValueToText("G", "--."),
        new MorseValueToText("H", "...."),
        new MorseValueToText("I", ".."),
        new MorseValueToText("J", ".---"),
        new MorseValueToText("K", "-.-"),
        new MorseValueToText("L", ".-.."),
        new MorseValueToText("M", "--"),
        new MorseValueToText("N", "-."),
        new MorseValueToText("O", "---"),
        new MorseValueToText("P", ".--."),
        new MorseValueToText("Q", "--.-"),
        new MorseValueToText("R", ".-."),
        new MorseValueToText("S", "..."),
        new MorseValueToText("T", "-"),
        new MorseValueToText("U", "..-"),
        new MorseValueToText("V", "...-"),
        new MorseValueToText("W", ".--"),
        new MorseValueToText("X", "-..-"),
        new MorseValueToText("Y", "-.--"),
        new MorseValueToText("Z", "--.."),
        new MorseValueToText("0", "-----"),
        new MorseValueToText("1", ".----"),
        new MorseValueToText("2", "..---"),
        new MorseValueToText("3", "...--"),
        new MorseValueToText("4", "....-"),
        new MorseValueToText("5", "....."),
        new MorseValueToText("6", "-...."),
        new MorseValueToText("7", "--..."),
        new MorseValueToText("8", "---.."),
        new MorseValueToText("9", "----."),
        new MorseValueToText("0", "-----")

    };

    public static MorseValue GetRandomClassicMorseCode()
    {
        return ClassicMorseTable[UnityEngine.Random.Range(0, ClassicMorseTable.Length)].GetValue();
    }

    public static string GuessValueInClassicMorse(MorseValue morseValue)
    {
        return GuessValue(morseValue, ClassicMorseTable);
    }
    public static string GuessValue(MorseValue morseValue, MorseValueToText[] possibleMatches)
    {
        List<MorseValueToText> matchs = possibleMatches.Where(k => k.GetValue().ToString().IndexOf(morseValue.ToString())==0).OrderBy(k=>k.GetValue().ToString().Length).ToList();

        if (matchs.Count > 0) {
           // Debug.Log(matchs[0].GetValue().ToString() + "  -  " + morseValue.ToString());
            return matchs[0].GetTextAssociated();
        }
        else return "?";
    }

    public struct MorseValueToText {
        public MorseValueToText(string text, string keys)
        {
            m_text = text;
            m_morse = new MorseValue(keys);
        }
        public MorseValueToText(string text, params MorseKey[] keys)
        {
            m_text = text;
            m_morse = new MorseValue(keys);
        }
        [SerializeField]
        MorseValue m_morse;
        [SerializeField]
        string m_text;
        public MorseValue GetValue() { return m_morse; }
        public string GetTextAssociated() { return m_text; }

    }
}
