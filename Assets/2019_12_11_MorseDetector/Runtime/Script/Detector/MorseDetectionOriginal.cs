using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FB_MorseDetection : MonoBehaviour
{



    public float m_blankCommitDelay = 0.75f;
    public float m_longDelay = 0.3f;
    public float m_longBecomePressing = 1f;

    [Header("Events")]
    public OnMorseKeyDetected onMorseKeyDetected;
    public OnMorseValueDetected onMorseValueDetected;
    public OnMorsePressingState onMorsePressingState;
    public OnMorseDownState onMorseDownState;

    [Serializable]
    public class OnMorseKeyDetected : UnityEvent<MorseKey> { };
    [Serializable]
    public class OnMorseValueDetected : UnityEvent<MorseValue> { };
    [Serializable]
    public class OnMorsePressingState : UnityEvent<bool> { };
    [Serializable]
    public class OnMorseDownState : UnityEvent<bool> { };


    [Header("Debug View")]
    public List<MorseKey> m_detectedKeys = new List<MorseKey>();

    [Header("Debug Intern")]
    public bool m_isDown;
    public bool m_isPressing;
    public float m_isDownTime;
    public float m_isUpTime;

    public bool m_startDetectiong;
    public float m_firstDetectionTime;

    public bool m_previousState;
    public void Update()
    {
        bool wasDown = m_previousState;
        bool isDown = m_isDown;
        bool wasPressing = m_isPressing;
        // Add time
        float delta = Time.deltaTime;
        if (m_isDown)
        {
            m_isDownTime += delta;
        }
        else
        {
            m_isUpTime += delta;
        }



        if (m_startDetectiong)
            m_firstDetectionTime += delta;


        //Check Pressing
        m_isPressing = m_isDownTime > m_longBecomePressing;





        //DETECT CHANGE

        if (wasPressing != m_isPressing && m_isPressing)
        {

            onMorsePressingState.Invoke(true);
            //  Debug.Log("Start Pressing: " + m_isDownTime);
        }

        if (wasDown != isDown)
        {
            onMorseDownState.Invoke(isDown);

            if (isDown && !m_isPressing)
            { }
            else if (!isDown && !m_isPressing)
            {
                if (m_isDownTime < m_longDelay)
                {

                    // Debug.Log("Short");
                    m_detectedKeys.Add(MorseKey.Short);
                    onMorseKeyDetected.Invoke(MorseKey.Short);
                }
                else
                {

                    m_detectedKeys.Add(MorseKey.Long);
                    onMorseKeyDetected.Invoke(MorseKey.Long);
                    //  Debug.Log("Long");
                }


            }
            else if (!isDown && m_isPressing)
            {
                Debug.Log("Stop Pressing: " + m_isDownTime);
                onMorsePressingState.Invoke(false);
                ResetMorseDetection();


            }


            if (m_isDown)
            {
                m_isUpTime = 0;
            }
            else
            {
                m_isDownTime = 0;
            }
        }


        if (m_startDetectiong && !m_isPressing && m_isUpTime > m_blankCommitDelay)
        {
            //Debug.Log("Notify morse code");
            MorseValue val = new MorseValue(m_detectedKeys.ToArray());
            onMorseValueDetected.Invoke(val);
            ResetMorseDetection();
        }



        m_previousState = isDown;
    }

    private void ResetMorseDetection()
    {
        m_isPressing = false;
        m_isDownTime = 0;
        m_isUpTime = 0;

        m_startDetectiong = false;
        m_firstDetectionTime = 0;
        m_detectedKeys.Clear();

    }

    public void NotifyDown()
    {
        m_isDown = true;
        m_startDetectiong = true;

    }
    public void NotifyUp()
    {

        m_isDown = false;
    }
}
