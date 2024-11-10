using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MorseEmittorAbstract : MonoBehaviour, MorseEmittorInterface
{
    protected string m_emittorName = "Defaut Undefined";
    public string GetSourceName()
    {
        return m_emittorName;
    }

    public abstract bool IsEmitting();
}


public interface MorseEmittorInterface {
    string GetSourceName();
    bool IsEmitting();

}
