
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Source: https://github.com/daseyb/ShaylaGames/blob/master/Assets/Audio/Atomtwist/ATMicInput/MicInput.cs
public class MicInput : MonoBehaviour
{
    public Image m_imageDebug;
    public string m_tryToSelect="Microphone";

    [Header("Debug")]
    [Range(0,1)]
    public float m_micLoudness;

    public string m_device="";
    public string [] m_listOfDevices;

    public AudioClip m_clipRecord;
    int m_sampleWindow = 128;
    void InitMic()
    {
        m_listOfDevices = Microphone.devices;
        if (m_listOfDevices.Length > 0) {
            if (string.IsNullOrEmpty(m_device) ) {

                for (int i = 0; i < m_listOfDevices.Length; i++)
                {
                    if (m_listOfDevices[i].ToLower().IndexOf(m_tryToSelect.ToLower()) >= 0) { 
                        m_device = m_listOfDevices[i];
                        break;
                    }
                }        
                if(string.IsNullOrEmpty(m_device))
                    m_device = Microphone.devices[0];
            }
            m_clipRecord = Microphone.Start(m_device, true, 999, 44100);
        }
    }

    void StopMicrophone()
    {
        Microphone.End(m_device);
    }

    //get data from microphone into audioclip
    float LevelMax()
    {
        float levelMax = 0;
        float[] waveData = new float[m_sampleWindow];
        int micPosition = Microphone.GetPosition(null) - (m_sampleWindow + 1); // null means the first microphone
        if (micPosition < 0) return 0;
        m_clipRecord.GetData(waveData, micPosition);
        // Getting a peak on the last 128 samples
        for (int i = 0; i < m_sampleWindow; i++)
        {
            float wavePeak = waveData[i] * waveData[i];
            if (levelMax < wavePeak)
            {
                levelMax = wavePeak;
            }
        }
        return levelMax;
    }


    void Update()
    {
        // levelMax equals to the highest normalized value power 2, a small number because < 1
        // pass the value to a static var so we can access it from anywhere
        m_micLoudness = LevelMax();
        if (m_imageDebug != null)
            m_imageDebug.fillAmount = m_micLoudness;
    }

    bool _isInitialized;
    // start mic when scene starts
    void OnEnable()
    {
        InitMic();
        _isInitialized = true;
    }

    //stop mic when loading a new level or quit application
    void OnDisable()
    {
        StopMicrophone();
    }

    void OnDestroy()
    {
        StopMicrophone();
    }


    // make sure the mic gets started & stopped when application gets focused
    /*
    void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            //Debug.Log("Focus");

            if (!_isInitialized)
            {
                //Debug.Log("Init Mic");
                InitMic();
                _isInitialized = true;
            }
        }
        if (!focus)
        {
            //Debug.Log("Pause");
            StopMicrophone();
            //Debug.Log("Stop Mic");
            _isInitialized = false;

        }
    }*/
}