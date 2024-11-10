using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlus : MonoBehaviour
{
    public Camera m_camera;

    public void SetColor(Color color)
    {
        m_camera.backgroundColor = color;
    }
    public void SetColor(float r, float g, float b )
    {
        SetColor(new Color(r, g, b));

    }
    private void Reset()
    {
        m_camera = GetComponent<Camera>();
    }
}
