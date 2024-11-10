using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public struct MorseValue :MorseValueInterface 
{
    public static char SHORT = '.';
    public static char LONG = '-';

    public MorseValue(string keys): this(keys.ToCharArray())
    {}
    public MorseValue(char [] keys):this (ConvertCharsToKeys(keys))
    {}

    private static MorseKey[] ConvertCharsToKeys(char[] keys)
    {
        MorseKey[] finalkeys = new MorseKey[keys.Length];
        for (int i = 0; i < keys.Length; i++)
        {
            finalkeys[i] = (keys[i] == LONG) ? MorseKey.Long : MorseKey.Short;
        }

        return finalkeys;
    }

    public MorseValue(params MorseKey[] keys)
    {
        m_value = keys;
    }
    
    [SerializeField]
    private MorseKey[] m_value;

    public override string ToString()
    {
        string value = "";
        for (int i = 0; i < m_value.Length; i++)
        {
            switch (m_value[i])
            {
                case MorseKey.Short:
                    value += SHORT;
                    break;
                case MorseKey.Long:
                    value += LONG;
                    break;
                default:
                    break;
            }
        }
        return value;
    }

    public static bool operator ==(MorseValue b1, MorseValue b2)
    {
        return b1.Equals(b2);
    }

    public static bool operator !=(MorseValue b1, MorseValue b2)
    {
        return !(b1 == b2);
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;
       

        var b2 = (MorseValue)obj;

        if (m_value == null || b2.m_value == null)
            return false;
        if (m_value.Length != b2.m_value.Length)
            return false;
        for (int i = 0; i < m_value.Length; i++)
        {
            if (m_value[i] != b2.m_value[i])
                return false;
        }
        return true;
    }

    public override int GetHashCode()
    {
        return ToString().GetHashCode();
    }

    public MorseKey[] GetValue()
    {
        return m_value;
    }
}
[System.Serializable]
public class MorsePlus
{
    public MorsePlus(params MorseKey[] keys)
    {
        SetValue("", keys);
    }
    public MorsePlus(string description, params MorseKey[] keys)
    {
        SetValue(description, keys);

    }
    private void SetValue(string description, MorseKey[] keys)
    {
        m_morsValue = new MorseValue(keys);
        m_description = description;
    }

    [SerializeField]
    public string m_description;
    public MorseValue m_morsValue;

}
