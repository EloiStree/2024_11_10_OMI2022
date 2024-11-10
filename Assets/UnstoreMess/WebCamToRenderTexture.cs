using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebCamToRenderTexture : MonoBehaviour
{
	public int index;
	public RenderTexture targetTexture;
	public int width = 1920, height = 1080, fps = 30;
	int prevIndex;

	WebCamTexture webcamTexture;

	void Start()
	{
		prevIndex = index;
		SetWebCamTexture(index);
	}

	void Update()
	{
		if (index != prevIndex)
		{
			//利用可能だが，処理落ちするためコメントアウト
			//SetWebCamTexture(index);
		}
		//テクスチャをコピー
		Graphics.Blit(webcamTexture, targetTexture);

		prevIndex = index;
	}

	void SetWebCamTexture(int index)
	{
		if (webcamTexture != null && webcamTexture.isPlaying)
			webcamTexture.Stop();
		WebCamDevice[] devices = WebCamTexture.devices;
		try
		{
			webcamTexture = new WebCamTexture(devices[index].name, this.width, this.height, this.fps);
		}
		catch (System.Exception e)
		{
			webcamTexture = new WebCamTexture(devices[0].name, this.width, this.height, this.fps);
		}
		webcamTexture.Play();
	}

	//解像度を設定
	public void SetResolution(int w, int h)
	{
		width = w;
		height = h;
	}

	public Eloi.ClassicUnityEvent_Texture2D m_texturePushOut;

	[ContextMenu("Push Texture out")]
	public void PushOutTexture()
	{
		Texture2D t = GetTexture2DFromWebcamTexture(webcamTexture);
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