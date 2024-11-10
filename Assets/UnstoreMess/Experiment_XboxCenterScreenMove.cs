using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Experiment_XboxCenterScreenMove : MonoBehaviour
{

    public UDPThreadSender m_xomi;


    public float m_timeBetweenMove=20;
    public Vector2 m_pixelAccumulation;

    public float m_maxPixelSentPerFrame = 10;
    public float m_frameTime = 0.1f;
    public float m_pourcentMaxPerPixel = 10;


    public float m_timeToArriveAtZero = 0.5f;

    public float m_timePer100PixelHorizontal = 0.1f;
    public float m_timePer100PixelVertical = 0.1f;

    public float m_powerPer100PixelHorizontal = 0.2f;
    public float m_powerPer100PixelVertical = 0.2f;

    public float m_powerMinimumHorizontal = 0.2f;
    public float m_powerMinimumVertical = 0.2f;

    public float m_powerMaximumHorizontal = 1f;
    public float m_powerMaximumVertical = 0.4f;

    public float m_powerMaximumPixelCumulation = 500;

    public AnimationCurve m_powerCurvePerPixelHorizontal;
    public AnimationCurve m_powerCurvePerPixelVertical;

    public void Start()
    {
        InvokeRepeating("PushFrame", 0, m_frameTime);
    }

    public void FixedUpdate()
    {
        RemoveTimePast();
    }

    public DateTime m_lastTimeRemove;
    public void RemoveTimePast() {
        DateTime now = DateTime.Now;
        float deltaTime =(float) (now - m_lastTimeRemove).TotalSeconds;
        float removeHorizontal = deltaTime * (100f / m_timePer100PixelHorizontal);
        float removeVertical = deltaTime * (100f / m_timePer100PixelVertical);

        RemoveTorwardZero(ref m_pixelAccumulation.x, in removeHorizontal);
        RemoveTorwardZero(ref m_pixelAccumulation.y, in removeVertical);

        m_lastTimeRemove = now;

    }

    private void RemoveTorwardZero(ref float value, in float toRemove)
    {
        if (value > 0)
        {
            value -= toRemove;
            if (value < 0)
                value = 0;
        }
        if (value < 0)
        {
            value += toRemove;
            if (value > 0)
                value = 0;
        }
    }

    public void AddPixelMove(Vector2 pixelMoved)
    {
        if (pixelMoved.x!=0 && ( Mathf.Sign(m_pixelAccumulation.x) * Mathf.Sign(pixelMoved.x) < -0.01f))
            m_pixelAccumulation.x = 0;
        if (pixelMoved.y != 0 && (Mathf.Sign(m_pixelAccumulation.y) * Mathf.Sign(pixelMoved.y) < -0.01f))
            m_pixelAccumulation.y = 0;

        m_pixelAccumulation.x += pixelMoved.x;
        m_pixelAccumulation.y += pixelMoved.y;

        m_pixelAccumulation.x = Mathf.Clamp(m_pixelAccumulation.x, -m_powerMaximumPixelCumulation, m_powerMaximumPixelCumulation);
        m_pixelAccumulation.y = Mathf.Clamp(m_pixelAccumulation.y, -m_powerMaximumPixelCumulation, m_powerMaximumPixelCumulation);


    }

    public bool m_wasMoving;

    public float m_horizontalPower=1f;
    public float m_verticalPower=0.4f;

    public void PushFrame() {


        int xRange, yRange;

        yRange = (int)m_pixelAccumulation.y;
        xRange = (int)m_pixelAccumulation.x;

        string toSend;
        bool hasValueToChange = (xRange != 0 || yRange != 0);
        if (hasValueToChange)
        {

            float x = ((float)m_pixelAccumulation.x) * ((float)m_powerPer100PixelHorizontal / 100f) ;
            float y = ((float)m_pixelAccumulation.y) * ((float)m_powerPer100PixelVertical / 100f);
            if (Mathf.Abs(x) < m_powerMinimumHorizontal)
                x = m_powerMinimumHorizontal * Mathf.Sign(x);
            if (Mathf.Abs(y) < m_powerMinimumVertical)
                y = m_powerMinimumVertical * Mathf.Sign(y);


            m_wasMoving = true;
            toSend = string.Format(" 🎮r:{1}:{2}",
            m_timeBetweenMove, x, y);

            m_xomi.AddMessageToSendToAll(new MessageToAll(toSend));
            m_xomi.SendAllAsSoonAsPossible();
        }
        if (m_wasMoving &&!hasValueToChange)
        {

            m_wasMoving = false;
            toSend = "🎮r:{0}:{0}";
            m_xomi.AddMessageToSendToAll(new MessageToAll(toSend));
            m_xomi.SendAllAsSoonAsPossible();

        }

    }

}
