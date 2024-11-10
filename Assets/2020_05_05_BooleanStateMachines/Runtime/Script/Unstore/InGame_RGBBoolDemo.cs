using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGame_RGBBoolDemo : MonoBehaviour
{
    public BooleanStateRegisterMono m_linked;
    public Transform m_selecter;
    public string m_rName="R";
    public SphereCollider m_r;
    public string m_gName = "G";
    public SphereCollider m_g;
    public string m_bName = "B";
    public SphereCollider m_b;

    void Update()
    {
        BooleanStateRegister reg = m_linked.GetRegister();
        reg.Set(m_rName, (Vector3.Distance(m_selecter.position, m_r.transform.position) < m_r.radius));
        reg.Set(m_gName, (Vector3.Distance(m_selecter.position, m_g.transform.position) < m_g.radius));
        reg.Set(m_bName, (Vector3.Distance(m_selecter.position, m_b.transform.position) < m_b.radius));
    }
}
