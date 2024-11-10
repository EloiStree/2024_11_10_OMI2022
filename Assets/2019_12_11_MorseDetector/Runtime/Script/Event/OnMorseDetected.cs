using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class OnMorseDetected : UnityEvent<MorseValue> { }

[System.Serializable]
public class OnMorseWithOrigineDetected : UnityEvent<MorseValueWithOrigine> { }

