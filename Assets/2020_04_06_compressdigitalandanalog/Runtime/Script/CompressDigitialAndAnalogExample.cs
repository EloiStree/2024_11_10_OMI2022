//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

using System;
using UnityEngine;

public class CompressDigitialAndAnalogExample : MonoBehaviour
{
    public CompressDigitalAndAnalogIndexListener m_input;
    public Transform m_spawnPoint;
    public Camera m_camera;
    public Light m_light;

    [Header("Debug")]
    public Color m_currentColorLight;
    public Color m_wantedColorLight;
    public Color m_currentColorCamera;
    public Color m_wantedColorCamera;
    public float m_lerpFactor = 1f;

    void Start()
    {
       //First way
        ListenToDigital spawnblock = new ListenToDigital() { m_index = 0 };
        spawnblock.m_onChange.AddListener(SpawnBlock);
        ListenToAnalog changeColor = new ListenToAnalog() { m_index = 0 };
        changeColor.m_onAnalogChange.AddListener(ChangeCameraGreen);
        //Second way 
        m_input.AddListener(spawnblock);
        m_input.AddListener(changeColor);
        m_input.m_state.AddAnalogListener(AnalogChangeWithIndex);
        m_input.m_state.AddDigitalListener(DigitalChangeWithIndex);



    }

    private void DigitalChangeWithIndex(string givenName, int index, bool newValue)
    {
        if(newValue)
        switch (index)
        {
            case 0: SpawnCylinder();break;
            case 1: SpawnBlock(); break;
            case 2: SpawnCapsule(); break;
            case 3: SpawnSphere(); break;
        }
    }

    private void AnalogChangeWithIndex(string givenName, int index, Digit oldValue, Digit newValue)
    {
        switch (index)
        {
            case 0: ChangeCameraRed((int)newValue); break;
            case 1: ChangeCameraGreen((int)newValue); break;
            case 2: ChangeCameraBlue((int)newValue); break;
            case 3: ChangeLightRed((int)newValue); break;
            case 4: ChangeLightGreen((int)newValue); break;
            case 5: ChangeLightBlue((int)newValue); break;
        }
    }

    public void Update()
    {
        m_currentColorLight = Color.Lerp(m_currentColorLight, m_wantedColorLight, Time.deltaTime * m_lerpFactor);
        m_currentColorCamera = Color.Lerp(m_currentColorCamera, m_wantedColorCamera, Time.deltaTime * m_lerpFactor);
        m_light.color = m_currentColorLight;
        m_camera.backgroundColor = m_currentColorCamera;
    }
    public void SpawnBlock(bool value)
    {

        CreatePrimitive(PrimitiveType.Cube);
    }
    public void SpawnBlock()
    {

        CreatePrimitive( PrimitiveType.Cube);
    }
    public void SpawnCylinder()
    {
        CreatePrimitive( PrimitiveType.Cylinder);
    }
    public void SpawnCapsule()
    {
        CreatePrimitive( PrimitiveType.Capsule);
    }
    public void SpawnSphere()
    {
        CreatePrimitive( PrimitiveType.Sphere);
    }
    
    private void CreatePrimitive( PrimitiveType primitive)
    {
        GameObject obj = GameObject.CreatePrimitive(primitive);
        SpawnParameters(obj);
    }

    private void SpawnParameters(GameObject obj)
    {
        obj.transform.position = m_spawnPoint.transform.position;
        Renderer render = obj.GetComponent<Renderer>();
        render.material.color = m_currentColorLight;
        obj.AddComponent<Rigidbody>();
    }



    public void ChangeCameraRed(int value)
    {
        m_wantedColorCamera.r = 1f - (float)value / 10f;
    }
    public void ChangeCameraGreen(int value)
    {
        m_wantedColorCamera.g = 1f - (float)value / 10f;
    }
    public void ChangeCameraBlue(int value)
    {
        m_wantedColorCamera.b = 1f - (float)value / 10f;
    }

    public void ChangeLightRed(int value)
    {
        m_wantedColorLight.r = 1f - (float)value / 10f;
    }
    public void ChangeLightGreen(int value)
    {

        m_wantedColorLight.g = 1f - (float)value / 10f;
    }
    public void ChangeLightBlue(int value)
    {
        m_wantedColorLight.b = 1f - (float)value / 10f;

    }
}
