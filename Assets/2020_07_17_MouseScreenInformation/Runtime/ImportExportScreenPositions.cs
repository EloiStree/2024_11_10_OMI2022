using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImportExportScreenPositions : MonoBehaviour
{
    [TextArea(0, 5)]
    [SerializeField] string m_jsonStored;
    private string m_previousJson;
    public ScreenPositionsRegisterEvent m_newValue;
    private bool m_avoidValideInEditorAtStart=true;
    public bool m_notifyAtStart=true;
    public void Awake()
    {
        m_avoidValideInEditorAtStart = false;
        m_previousJson = m_jsonStored;
    }
    public void Start()
    {
        if(m_notifyAtStart)
            m_newValue.Invoke(GetCurrent());
    }
    public void SetWithoutNotification(ScreenPositionsRegister collection)
    {
        m_jsonStored = JsonUtility.ToJson(collection,true);
        m_previousJson = m_jsonStored;
    }
  
    public void SetWithNotification(ScreenPositionsRegister collection)
    {
        SetWithoutNotification(collection);
        m_newValue.Invoke(GetCurrent());
    }
    public ScreenPositionsRegister GetCurrent()
    {
        return JsonUtility.FromJson<ScreenPositionsRegister>(m_jsonStored);
    }

    public void OnValidate()
    {
        if (m_avoidValideInEditorAtStart && Application.isPlaying)
            return;
        if (m_previousJson != m_jsonStored) {
            m_newValue.Invoke(GetCurrent());
            m_previousJson = m_jsonStored;
        }
    }
}
