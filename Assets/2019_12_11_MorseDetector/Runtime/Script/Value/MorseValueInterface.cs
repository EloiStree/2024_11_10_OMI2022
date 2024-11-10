using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface MorseValueInterface {

    MorseKey[] GetValue();
}

public enum MorseKey : int { Short = 1, Long = 2 }
