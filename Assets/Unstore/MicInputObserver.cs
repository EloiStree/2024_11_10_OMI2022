using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicInputObserver : MonoBehaviour
{


    public string[] m_listOfDevices;
    public List<MicObserved> m_observedDevices= new List<MicObserved>();
    public Dictionary<string, MicObserved> m_directRef = new Dictionary<string, MicObserved>();

    [System.Serializable]
    public class MicObserved {
       public string m_devicename;
       public AudioClip m_observedClip;
        int m_sampleWindow = 128;

        public MicObserved(string devicename) {

            m_devicename = devicename;
            StartIfNotStart();
        }

        public void StartIfNotStart() { 
        
            if(m_observedClip==null)
                m_observedClip = Microphone.Start(m_devicename, true, 999, 44100);

        }
        public void Stop() {

            if (m_observedClip) { 
                Microphone.End(m_devicename);
                m_observedClip = null;
            }
        }
        public bool HasMic() {
            return m_observedClip != null;
        }
        public float GetLevelMax()
        {
            if (m_observedClip == null)
                StartIfNotStart();

           
            float levelMax = 0;
            float[] waveData = new float[m_sampleWindow];
            int micPosition = Microphone.GetPosition(m_devicename) - (m_sampleWindow + 1); 
            if (micPosition < 0) 
                return 0;
            m_observedClip.GetData(waveData, micPosition);
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



    }

    public float GetValueOf(string realName)
    {
        if (m_directRef.ContainsKey(realName)) {
            return m_directRef[realName].GetLevelMax();
        } 
        return 0;
    }

   

    public bool Contain(string name)
    {
        bool found;
        string realName;
        TryToGetCorrespondingName(name, out found, out realName);
        return found;
    }

    public void TryToAddMicToListen(string nameOfDevice) {
        m_listOfDevices = Microphone.devices;
        bool found;
        string realName;
        TryToGetCorrespondingName(nameOfDevice,out found, out realName);
        if (found) {
            if (!AlreadyHaveTheMic(realName))
            { 
                MicObserved m = new MicObserved(realName);
                m_observedDevices.Add(m);
                m_directRef.Add(realName, m);
            }
        }
    }

    private bool AlreadyHaveTheMic(string realName)
    {
        foreach (var item in m_observedDevices)
        {
            if (item.m_devicename == realName)
                return true;
        }
        return false;
    }

    public void TryToGetCorrespondingName(string nameOfDevice, out bool found, out string realName)
    {
        realName = "";
        found = false;
        for (int i = 0; i < m_listOfDevices.Length; i++)
        {
            if (m_listOfDevices[i].ToLower().IndexOf(nameOfDevice.ToLower()) >= 0)
            {
                realName= m_listOfDevices[i];
                found = true;
            }
        }
    }

   

    private void StopMicrophone()
    {
        foreach (var item in m_observedDevices)
        {
            item.Stop();
        }
    }

    void OnDisable()
    {
        StopMicrophone();
    }

    
    void OnDestroy()
    {
        StopMicrophone();
    }
}
