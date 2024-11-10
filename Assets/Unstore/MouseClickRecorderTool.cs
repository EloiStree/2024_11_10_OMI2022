using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClickRecorderTool : MonoBehaviour
{
    public TimeThreadMono m_time;
    public MouseInformationAbstract m_mouseInformation;

    public List<MouseRecord> m_currentRecord = new List<MouseRecord>();


    private void Awake()
    {
       // m_mouseInformation.m
    }

    public void Save(float l2r, float b2t, MouseRecord.ClickType clickType) { 
    
    }

}

[System.Serializable]
public class MouseRecord {
    public ScreenPositionAsPourcent m_clickPosition;
    public enum ClickType { Left, Middle, Right}
    public ClickType m_clickType;
}
