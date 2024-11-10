using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct MorseValueWithOrigine : MorseValueInterface {

    public MorseValueWithOrigine(MorseEmittorInterface emittor, params MorseKey[] keys): this(emittor, new MorseValue(keys))
    {}
    public MorseValueWithOrigine(MorseEmittorInterface emittor, MorseValue value) {
        m_emittorName = emittor.GetSourceName();
        m_morse = value;
        m_emittor = emittor;
    }

    private MorseEmittorInterface m_emittor;
    public MorseEmittorInterface GetEmittorSource() { return m_emittor; }

    [SerializeField]
    MorseValue m_morse;
    public MorseValue GetMorseValue() { return m_morse;}

    [SerializeField]
    string m_emittorName;

    public string GetEmittorName() {
        return m_emittorName;
    }
    public MorseKey[] GetValue()
    {
        return m_morse.GetValue();
    }
}
