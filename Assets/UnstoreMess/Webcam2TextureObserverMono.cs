using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Webcam2TextureObserverMono : MonoBehaviour
{

    public WebCamTexture webcamTexture;
    public Eloi.ClassicUnityEvent_Texture2D m_texturePushOut;
    public string [] m_deviceName;
    public int m_deviceIndex;

    public RenderTexture m_debugRenderTexture;
    [ContextMenu("Refresh Names")]
    public void RefreshName() {
        m_deviceName = WebCamTexture.devices.Select(k=>k.name).ToArray();
    }
    // Start is called before the first frame update
    void Start()
    {
        RefreshName();
        if (WebCamTexture.devices.Length ==0)
            return;

       
        webcamTexture = new WebCamTexture(WebCamTexture.devices[m_deviceIndex].name, 1280,720);
        
        webcamTexture.Play();

    }

    [ContextMenu("Push Texture out")]
    public void PushOutTexture() {
       Texture2D t= GetTexture2DFromWebcamTexture(webcamTexture);
        m_texturePushOut.Invoke(t);
    }
    public int m_width;
    public int m_height;
    public Texture2D GetTexture2DFromWebcamTexture(WebCamTexture webCamTexture)
    {
        m_width = webCamTexture.width;
        m_height = webCamTexture.height;
        // Create new texture2d
        Texture2D tx2d = new Texture2D(webCamTexture.width, webCamTexture.height);
        // Gets all color data from web cam texture and then Sets that color data in texture2d
        tx2d.SetPixels(webCamTexture.GetPixels());
        // Applying new changes to texture2d
        tx2d.Apply();
        return tx2d;
    }
}
