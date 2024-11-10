using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFakeCursorWithScreenPosition : MonoBehaviour
{

    public Transform m_affected;
    public Camera m_cameraTargeted;
    public float m_cameraDistance = 1;
    [SerializeField]
    ScreenPositionInPourcentBean m_lastReceived;
    public void MoveTo(ScreenPositionFullRecord position)
    {
        MoveTo(position.GetAsPourcent());
    }
    public void MoveTo(ScreenPositionInPourcentBean position)
    {
        if (m_cameraTargeted == null)
            m_cameraTargeted = Camera.main;
        m_lastReceived = position;

        Vector3 positionOnScreen = new Vector3(0.5f, 0.5f);
        positionOnScreen.y = position.GetBotToTopValue();
        positionOnScreen.x = position.GetLeftToRightValue();
        positionOnScreen.z = m_cameraDistance;

        Vector3 newPosition = m_cameraTargeted.ViewportToWorldPoint(positionOnScreen);
        m_affected.rotation = m_cameraTargeted.transform.rotation;
        m_affected.position = newPosition + m_cameraTargeted.transform.forward * m_cameraDistance;

    }
}
