using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UI_SerialPortListDropDown : MonoBehaviour
{
    public Dropdown m_target;
    public string[] m_portList;
    public float m_timeBetweenCheck=5f;



    private void Start()
    {
        InvokeRepeating("Refresh", 0, m_timeBetweenCheck);    
    }
    private void OnValidate()
    {
        Refresh();
    }
    private void Refresh()
    {
        m_portList = SerialPortUtility.GetListOfAvailaiblePort().ToArray();
        m_target.ClearOptions();
        m_target.AddOptions(m_portList.ToList());
    }
}
