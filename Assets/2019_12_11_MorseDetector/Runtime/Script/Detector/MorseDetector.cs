using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorseDetector : MonoBehaviour
{
    [Header("In")]
    public MorseEmittorAbstract m_morseSource;

    [Header("Out")]
    public OnMorseWithOrigineDetected m_onMorseDetected;

  
}
