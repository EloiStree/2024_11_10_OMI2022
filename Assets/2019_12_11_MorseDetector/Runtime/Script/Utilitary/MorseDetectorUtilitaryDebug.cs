using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorseDetectorUtilitaryDebug : MonoBehaviour {

    [Header("Morse")]
    public List<string> m_morsesDetected;
    public int m_maxDisplay = 10;

    [Header("Detector")]
    public int m_detectorCout;
    public List<string> m_dectector;
    public void AddMorseFound (MorseValueWithOrigine morse) {
        m_morsesDetected.Add(string.Format("{0}:{1}", morse.GetMorseValue(), morse.GetEmittorName()));
        if (m_morsesDetected.Count > m_maxDisplay)
            m_morsesDetected.RemoveAt(0);

        //Bad Code, Should not be call at morse found but I am sleepy.
        m_detectorCout = MorseDetectorUtilitary.GetDetectorsCount();
    }
	
}
