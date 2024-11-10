using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Experiment_InOutMemoryMutex : MonoBehaviour
{
    public TargetMemoryFileWithMutexInfoWithFormat m_connectionPointText;
    public TargetMemoryFileWithMutexInfoWithFormat m_connectionPointImage;
    private TargetMemoryFileWithMutex m_textMemoryTunnel;
    private TargetMemoryFileWithMutex m_imageMemoryTunnel;

    [TextArea(0,5)]
    public string m_fetchText;
    public Text m_text;
    public Texture2D m_toPushTexture;
    public Texture2D m_debugTexture;
    public RawImage m_debugTextureUI;

    void Start()
    {
        m_textMemoryTunnel = new TargetMemoryFileWithMutex(m_connectionPointText);
        m_imageMemoryTunnel = new TargetMemoryFileWithMutex(m_connectionPointImage);
        GenerateNewTexture();
    }

    [ContextMenu("New Texture")]
    private void GenerateNewTexture()
    {
        m_toPushTexture = new Texture2D(256, 256, TextureFormat.ARGB32, false);
        Color[] c = m_toPushTexture.GetPixels();
        for (int i = 0; i < c.Length; i++)
        {
            float r = UnityEngine.Random.value;
            c[i] = new Color(i*r % 255, i % 20, i % 3);

        }
        m_toPushTexture.SetPixels(c);
        m_toPushTexture.Apply();
    }

    public void AppendTextThrowMemory(string text) {
        m_textMemoryTunnel.SetText(text);
    }

    [ContextMenu("Read Text")]
    public void ReadText()
    {
        m_textMemoryTunnel.TextRecovering(out string text, false);
        m_text.text = text;
        m_fetchText = text;
    }
    [ContextMenu("Read Text And Flush")]
    public void ReadTextAndFlush()
    {
        m_textMemoryTunnel.TextRecovering(out string text, true);
        m_text.text = text;
        m_fetchText = text;
    }

    [ContextMenu("PushTexture2DTest as bytes")]
    public void PushTexture2DTest()
    {
        PushTexture2D(m_toPushTexture);
    }

    public void PushTexture2D(Texture2D texture)
    {
        byte[] t = texture.EncodeToPNG();
        m_imageMemoryTunnel.SetAsBytes(t);
    }

    public int m_byteCountTexture;
    [ContextMenu("Read Bytes")]
    public void ReadBytes()
    {
        m_imageMemoryTunnel.BytesRecovering(out byte[] bytes, false);
        m_byteCountTexture = bytes.Length;
        m_debugTexture = new Texture2D(2, 2, TextureFormat.RGBA32, false);
        m_debugTexture.LoadImage(bytes);
        m_debugTexture.Apply();
        m_debugTextureUI.texture = m_debugTexture;
    }
    [ContextMenu("Read Bytes And Flush")]
    public void ReadBytesAndFlush()
    {
        m_imageMemoryTunnel.BytesRecovering(out byte[] bytes, true);
        m_byteCountTexture = bytes.Length;
        m_debugTexture = new Texture2D(2, 2, TextureFormat.RGBA32, false);
        m_debugTexture.LoadImage(bytes);
        m_debugTexture.Apply();
        m_debugTextureUI.texture = m_debugTexture;
    }
}
