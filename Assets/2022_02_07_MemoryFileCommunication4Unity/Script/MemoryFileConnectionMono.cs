using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MemoryFileConnectionMono : MonoBehaviour
{
    public MemoryFileConnection m_connection;


    public void GetConnection(out MemoryFileConnection connection) { connection = m_connection; }
    public MemoryFileConnection GetConnection() { return m_connection; }
    public MemoryFileConnection Connection
    {
        get { return m_connection; }
        set { m_connection = value; }
    }
    

    void Reset()
    {
        Connection.m_setupInfo = new TargetMemoryFileWithMutexInfoWithFormat();
        Connection.m_setupInfo.m_fileName = Guid.NewGuid().ToString();

    }

    public void SetAsTexture(Texture2D texture) => Connection.SetAsTexture2D(texture);
    public void SetAsJson(object targetObject) => Connection.SetAsOjectInJsonFromat(targetObject);
    public void SetAsText(string text) => Connection.SetText(text);
    public void AppendTextAtStart(string text) => Connection.AppendTextAtEnd(text);
    public void AppendTextAtEnd(string text) => Connection.AppendTextAtStart(text);
    public void SetAsBytes(string text) => Connection.AppendTextAtStart(text);
    public void Flush() => Connection.Flush();

}
[System.Serializable]
    public class MemoryFileConnection 
{
    public TargetMemoryFileWithMutexInfoWithFormat m_setupInfo;
    private TargetMemoryFileWithMutex m_connection  ;

    public void CheckThatConnectionExist()
    {
        m_connection = new TargetMemoryFileWithMutex(m_setupInfo);
    }
    public void SetNameThenReset(string fileName)
    {
        m_setupInfo.m_fileName = fileName;
        m_connection = new TargetMemoryFileWithMutex(m_setupInfo);
    }
    public void SetMaxSizeThenReset(string fileName)
    {
        m_setupInfo.m_fileName = fileName;
        m_connection = new TargetMemoryFileWithMutex(m_setupInfo);
    }
    public void SetNameAndSizeThenReset(string fileName, int maxSize)
    {
        m_setupInfo.m_fileName = fileName;
        m_setupInfo.m_maxMemorySize = maxSize;
        m_connection = new TargetMemoryFileWithMutex(m_setupInfo);
    }
    


    public TargetMemoryFileWithMutex Connection() {
        CheckThatConnectionExist();
        return m_connection;
    }
    public void SetText(string text)
    {
        Connection().SetText(text);
    }
    public void AppendTextAtEnd(string text)
    {
        Connection().AppendTextAtEnd(text);
    }
    public void AppendTextAtStart(string text)
    {
        Connection().AppendTextAtStart(text);
    }
    public void GetAsText(out string text)
    {
        Connection().TextRecovering(out  text, false);
    }
    public void GetAsTextAndFlush(out string text)
    {
        Connection().TextRecovering(out  text, true);
    }
    
    public void SetAsTexture2D(Texture2D texture)
    {
        byte[] t = texture.EncodeToPNG();
        Connection().SetAsBytes(t);
    }

    public void GetAsBytes(out byte[] bytes)
    {
        Connection().BytesRecovering(out bytes, false);
    }
    public void GetAsBytesAndFlush(out byte[] bytes)
    {
        Connection().BytesRecovering(out bytes, true);
    }
    [ContextMenu("Read Bytes")]
    public void GetAsTexture2D(out Texture2D texture)
    {
        Connection().BytesRecovering(out byte[] bytes, false);
        texture = new Texture2D(2, 2, TextureFormat.RGBA32, false);
        texture.LoadImage(bytes);
        texture.Apply();
    }
    [ContextMenu("Read Bytes And Flush")]
    public void GetAsTexture2DAndFlush(out Texture2D texture)
    {
        Connection().BytesRecovering(out byte[] bytes, true);
        texture = new Texture2D(2, 2, TextureFormat.RGBA32, false);
        texture.LoadImage(bytes);
        texture.Apply();
    }


    public void SetAsOjectInJsonFromat<T>(T target) {
        string json = JsonUtility.ToJson(target);
        SetText(json);
    }
    public T GetInObjectFromJsonFormat<T>()
    {
        GetAsText(out string json);
        return JsonUtility.FromJson<T>(json);
    }
    public void  GetInObjectFromJsonFormat<T>(out T recovered)
    {
        GetAsText(out string json);
        recovered= JsonUtility.FromJson<T>(json);
    }

    internal void Flush()
    {
        m_connection.ResetMemory();
    }
}


