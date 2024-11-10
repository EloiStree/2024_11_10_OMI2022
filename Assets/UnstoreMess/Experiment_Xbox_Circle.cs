using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Experiment_Xbox_Circle : MonoBehaviour
{

    public UDPThreadSender m_xomi;

    public float m_periodeTime=10;
    public Vector2 m_circle;

    void FixedUpdate()
    {
       float pct = (Time.timeSinceLevelLoad% m_periodeTime) / m_periodeTime;
        pct = (pct - 0.5f) * 2f;

        float Y = Mathf.Cos(pct *90*Mathf.Deg2Rad );
        m_circle.x = pct;
        m_circle.y = Y*Mathf.Sign(pct);

        string msg = string.Format(" 🎮r:{0}:{1}", m_circle.x, m_circle.y);
        m_xomi.AddMessageToSendToAll(msg);
        m_xomi.SendAllAsSoonAsPossible();

    }
}
