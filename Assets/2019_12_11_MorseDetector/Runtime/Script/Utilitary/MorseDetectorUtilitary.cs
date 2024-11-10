using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MorseDetectorUtilitary  {

    private static MorseDetectorUtilitaryMono m_monoInstance;
    public static MorseDetectorUtilitaryMono InstanceInScene {
        get {
            if (m_monoInstance == null)
            {
                m_monoInstance = GameObject.FindObjectOfType<MorseDetectorUtilitaryMono>();
            }
            if (m_monoInstance == null)
            {
                GameObject createMono = new GameObject("#MorseDetectorUtility");
                m_monoInstance = createMono.AddComponent<MorseDetectorUtilitaryMono>();
            }
            return m_monoInstance;
        }
    }

    public static int GetDetectorsCount()
    {
        return m_detectors.Length;
    }

    public static OnMorseWithOrigineDetected OnMorseDetected = new OnMorseWithOrigineDetected();

    private static MorseDetector[] m_detectors= new MorseDetector[0];

    public static void RefreshAvailableDetectorsInScene() {
        m_detectors =  GameObject.FindObjectsOfType<MorseDetector>();

        for (int i = 0; i < m_detectors.Length; i++)
        {
            MorseDetector detecotor = m_detectors[i];
            if (detecotor != null)
            {
                detecotor.m_onMorseDetected.RemoveListener(FowardMorseDetected);
                detecotor.m_onMorseDetected.AddListener(FowardMorseDetected);
            }

        }
    }

    private static void FowardMorseDetected(MorseValueWithOrigine morseValue)
    {
        OnMorseDetected.Invoke(morseValue);
    }

    public static void ListenToMouse(int buttonId)
    {

        GameObject obj = GameObject.Instantiate(Resources.Load("MorseEmittorMouse") as GameObject);
        MorseEmittor_MouseListener mouseListener = obj.GetComponent<MorseEmittor_MouseListener>();
        if (mouseListener == null)
            mouseListener = obj.GetComponentInChildren<MorseEmittor_MouseListener>();
        if (mouseListener != null)
            mouseListener.SetMouseListened(buttonId);
    }
    public static void ListenToKeyboard(KeyCode keyCodeId)
    {

        GameObject obj = GameObject.Instantiate(Resources.Load("MorseEmittorKeyboard") as GameObject);
        MorseEmittor_KeypressListener keyboardListener = obj.GetComponent<MorseEmittor_KeypressListener>();
        if (keyboardListener == null)
            keyboardListener = obj.GetComponentInChildren<MorseEmittor_KeypressListener>();
        if (keyboardListener != null)
            keyboardListener.SetKeyboardListened(keyCodeId);
    }
}
