using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoesAPIToBooleanRegister : MonoBehaviour
{
    public BooleanStateRegisterMono m_register;
    public Transform m_accelerometer;
    public Transform m_gyroscope;
    public Transform m_lasterStartPoint;
    public Transform m_lasterMesh;
    public float m_maxLaserDistance;
    public float m_deathZone=5f;
    public LayerMask m_layerHit;
    [Header("Debug")]
    Quaternion m_initialDirection;
    Quaternion m_currentDirection;
    public float m_angleHorizontal = 0f;
    public float m_angleVertical = 0f;
    public float m_angleAxial = 0f;
    void Start()
    {
        m_initialDirection = m_gyroscope.rotation;
    }
    private void Update()
    {
        m_currentDirection = m_gyroscope.rotation;
        Quaternion direction = m_currentDirection * Quaternion.Inverse(m_initialDirection);
        m_angleHorizontal = direction.eulerAngles.y;
        m_angleVertical = direction.eulerAngles.x;
        m_angleAxial = direction.eulerAngles.z;
        if (m_angleHorizontal > 180f) m_angleHorizontal = m_angleHorizontal - 360f;
        if (m_angleVertical > 180f) m_angleVertical = m_angleVertical - 360f;
        if (m_angleAxial > 180f) m_angleAxial = m_angleAxial - 360f;

        BooleanStateRegister reg=  m_register.GetRegister();
        reg.Set("isDirectionLeft", m_angleHorizontal<0f && Mathf.Abs(m_angleHorizontal) > m_deathZone);
        reg.Set("isDirectionRight", m_angleHorizontal >= 0f && Mathf.Abs(m_angleHorizontal) > m_deathZone);

        reg.Set("isTiltingLeft", m_angleAxial >= 0f && Mathf.Abs(m_angleAxial)> m_deathZone);
        reg.Set("isTiltingRight", m_angleAxial < 0f && Mathf.Abs(m_angleAxial) > m_deathZone);
        reg.Set("isTiltingFowrard", m_angleVertical >= 0f && Mathf.Abs(m_angleVertical) > m_deathZone);
        reg.Set("isTiltingBackward", m_angleVertical < 0f && Mathf.Abs(m_angleVertical) > m_deathZone);
        bool hasHit;
        float d = GetDistanceOfLaser(out hasHit);
        reg.Set("isLaserDistanceAlmostNone",d < 0.05f);
        reg.Set("isLaserDistanceSmall", d < 0.1f);
        reg.Set("isLaserDistanceMedium", d < 0.2f);
        reg.Set("isLaserDistanceLessMax", d < m_maxLaserDistance);
        reg.Set("isLaserDistanceSupMax", d >= m_maxLaserDistance);
        reg.Set("isLaserHit", hasHit);
    }

    public float GetDistanceOfLaser(out bool hasHit) {
        RaycastHit hit;
        hasHit = Physics.Raycast(m_lasterStartPoint.position, m_lasterStartPoint.forward, out hit, m_maxLaserDistance, m_layerHit);
        if (hasHit)
        {
            Debug.DrawLine(m_lasterStartPoint.position, hit.point, Color.red);
            m_lasterMesh.position = m_lasterStartPoint.position;
            m_lasterMesh.rotation = m_lasterStartPoint.rotation;
            m_lasterMesh.localScale = new Vector3(0.01f, 0.01f, Vector3.Distance(m_lasterStartPoint.position,hit.point));
            m_lasterMesh.Translate(Vector3.forward * hit.distance/2f );
            
            return hit.distance;
        }
        else {

            Debug.DrawLine(m_lasterStartPoint.position, m_lasterStartPoint.position+ m_lasterStartPoint.forward* m_maxLaserDistance, Color.red);
            m_lasterMesh.position = m_lasterStartPoint.position;
            m_lasterMesh.rotation = m_lasterStartPoint.rotation;
            m_lasterMesh.localScale = new Vector3(0.01f, 0.01f, m_maxLaserDistance);
            m_lasterMesh.Translate(Vector3.forward * m_maxLaserDistance/2f );

            return m_maxLaserDistance;
        }
    }
}
