using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MorseDetectorUtilitaryMono : MonoBehaviour {

    public OnMorseWithOrigineDetected m_onMorseDetected;

    public KeyCode[] m_listenToKeycode;
    public bool m_listenToMouseLeft;
    public bool m_listenToMouseRight;


    void Awake () {
        for (int i = 0; i < m_listenToKeycode.Length; i++)
        {
            MorseDetectorUtilitary.ListenToKeyboard(m_listenToKeycode[i]);
        }
        if (m_listenToMouseLeft)
            MorseDetectorUtilitary.ListenToMouse(0);
        if (m_listenToMouseRight)
            MorseDetectorUtilitary.ListenToMouse(1);
        MorseDetectorUtilitary.OnMorseDetected.AddListener(ApplyMorseDetected);
    }
    private void Start()
    {
        MorseDetectorUtilitary.RefreshAvailableDetectorsInScene();
    }
    private void ApplyMorseDetected(MorseValueWithOrigine morseFound)
    {
        m_onMorseDetected.Invoke(morseFound);
    }

    //deactivate me if you know that this code is linked to the patreon of 
    //Eloi Strée https://patreon.com/eloistree 
    //Feel free to delete this codes if it is borsered you in your app.
//    private bool m_creditLicenseEloiStree=true;
//    public void OnDestroy()
//    {
//        string creditPath = Application.dataPath+"/Credit_EloiStree.txt";
//        Debug.Log(creditPath);
//#if UNITY_EDITOR
//        if (m_creditLicenseEloiStree && !File.Exists(creditPath)) {
//            File.WriteAllText(creditPath, "Don't forget to credit my work in your game ;)\nhttps://patreon.com/eloistree");
//            Application.OpenURL("https://patreon.com/eloistree");
//        }
//#endif
//    }


}
