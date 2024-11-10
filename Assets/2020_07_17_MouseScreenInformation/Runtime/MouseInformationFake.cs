using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInformationFake : MonoBehaviour
{
    public MouseInformationAbstract m_toAffect;
    public float m_leftToRightPixel;
    public float m_botToTopPixel;
    [Range(0 , 4500)]
    public float m_screenWidth=1600;
    [Range(0 , 4500)]
    public float m_screenHeight=900;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
