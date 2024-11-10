using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class AnalogDigitalCompressionToBoolean : MonoBehaviour
{

    public BooleanStateRegisterMono m_register;
    public List<NomenclatureLinkedToPort> m_loadedPortToNomenclature= new List<NomenclatureLinkedToPort>();
    public List<AnalogDigitalCompressorNomenclature> m_portNomenclatures= new List<AnalogDigitalCompressorNomenclature>();
    public List<NomenclatureLinkedToPortReference> m_portRequired= new List<NomenclatureLinkedToPortReference>();

  
    public void ClearLoadedInformation() {
        m_loadedPortToNomenclature.Clear();
        m_portNomenclatures.Clear();
        m_portRequired.Clear();
    }

    public void AddInformationFromFiles(string[] paths) {
        for (int i = 0; i < paths.Length; i++)
        {
            AddInformationFromFile(paths[i]);
        }
    }

    private void AddInformationFromFile(string pathFile)
    {
        if (!File.Exists(pathFile))
            return;
        string text = File.ReadAllText(pathFile);
        XmlToAnalogDigitalCompressConfig.LoadFromXML(text, out m_portRequired, out m_portNomenclatures);

        for (int i = 0; i < m_portRequired.Count; i++)
        {
            NomenclatureLinkedToPort info = new NomenclatureLinkedToPort(
                    m_portRequired[i].m_portId,
                    GetMaskIfExist(m_portRequired[i].m_maskName));
            if (info.m_mask != null)
            {
                m_loadedPortToNomenclature.Add(
                    info);
            }

        }
    }

    public void StopThePortConnections()
    {
            AnalogPortCompressionObserver.CloseConnections();

    }
    public void StartThePortConnections()
    {
        for (int i = 0; i < m_loadedPortToNomenclature.Count; i++)
        {
            AnalogPortCompressionObserver.ResetConnectionWithDefault(m_loadedPortToNomenclature[i].m_portId);

        }
    }

    private AnalogDigitalCompressorNomenclature GetMaskIfExist(string maskName)
    {
        List<AnalogDigitalCompressorNomenclature> d = m_portNomenclatures.Where(k => k.m_nomenclatureName == maskName).ToList();
        if (d.Count < 1)
            return null;
        return d[0];
    }

    public void Update()
    {
        BooleanStateRegister register = m_register.m_register;
        for (int i = 0; i < m_loadedPortToNomenclature.Count; i++)
        {
            uint id = m_loadedPortToNomenclature[i].m_portId;
            foreach (DigitToLabel item in m_loadedPortToNomenclature[i].m_mask.m_digitLabel)
            {
                bool value;
                AnalogPortCompressionObserver.GetDigitalState(id, item.m_index, out value, false);
                register.Set(item.m_labelId, item.m_inverseValue ? !value : value);
            }
            foreach (AnalogToLabelWithRange item in m_loadedPortToNomenclature[i].m_mask.m_analogLabel)
            {
                short value;
                AnalogPortCompressionObserver.GetAnalogState(id, item.m_index, out value, 0);

                bool boolValue = value >= item.m_min && value <= item.m_max;
                register.Set(item.m_labelId, item.m_inverseValue ? !boolValue : boolValue);

            }

        }
    }
    private void OnDestroy()
    {
        AnalogPortCompressionObserver.CloseConnections();
    }
}

[System.Serializable]
public class NomenclatureLinkedToPortReference
{

    public string m_maskName;
    public uint m_portId;
    public NomenclatureLinkedToPortReference(uint portId, string mask)
    {
        m_portId = portId;
        m_maskName = mask;
    }
}

[System.Serializable]
public class NomenclatureLinkedToPort
{

    public uint m_portId;
    public AnalogDigitalCompressorNomenclature m_mask;

    public NomenclatureLinkedToPort(uint portId, AnalogDigitalCompressorNomenclature mask)
    {
        m_portId = portId;
        m_mask = mask;
    }
}



[System.Serializable]
public class AnalogDigitalCompressorNomenclature {
    public string m_nomenclatureName="";
    public List<DigitToLabel> m_digitLabel = new List<DigitToLabel>();
    public List<AnalogToLabelWithRange> m_analogLabel = new List<AnalogToLabelWithRange>();
    public string m_documentationInfoUrl;

   

}
[System.Serializable]
public class DigitToLabel
{
    public string m_labelId;
    public uint m_index;
    public bool m_inverseValue;
}
[System.Serializable]
public class AnalogToLabelWithRange
{
    public string m_labelId;
    public uint m_index;
    public short m_min = 0, m_max = 5;
    public bool m_inverseValue;
}
public class AnalogPortCompressionObserver {

    public static SerialPortRequestCommunication m_defaultSetterValue = new SerialPortRequestCommunication {
        m_baudRate=9600, m_dataBite=8,
        m_stopBits= System.IO.Ports.StopBits.One,
        m_handShake= System.IO.Ports.Handshake.None,
        m_parity = System.IO.Ports.Parity.None};
    public static Dictionary<uint, UnityThreadPortCommunication> m_establishConnections= new Dictionary<uint, UnityThreadPortCommunication>();
    public static Dictionary<uint, AnalogDigitalCompression> m_collectedCompression = new Dictionary<uint, AnalogDigitalCompression>();

   

    public static void EstablishConnectionWithDefault(uint portId)
    {
        ResetConnectionWithDefault(portId);
    }
    public static void ResetConnectionWithDefault(uint portId)
    {
        SerialPortRequestCommunication defaultSetter = m_defaultSetterValue.GetCopy();
        defaultSetter.m_portId = portId;
        UnityThreadPortCommunication thread = null;
        if (!m_establishConnections.ContainsKey(portId))
        { 
            m_establishConnections[portId] = thread = new UnityThreadPortCommunication(defaultSetter, false);
        }
        else {
            UnityThreadPortCommunication reco;
            m_establishConnections[portId].CloseAndGetReconnection(out reco, false);
            m_establishConnections[portId]= thread = reco;
        }

        if (!m_collectedCompression.ContainsKey(portId)) {
            m_collectedCompression.Add(portId, new AnalogDigitalCompression());
        }
        thread.AddMessageListener(ProcessMessage);
        thread.StartThread();

    }

    private static void ProcessMessage(uint portId, TimedMessage message)
    {
        bool isMsgValide;
        m_collectedCompression[portId].SetWithCompressMessage(message.m_message, out isMsgValide);
    }


    public static bool IsObservingPort(uint portId) { return m_collectedCompression.ContainsKey(portId); }
    public static AnalogDigitalCompression GetCompressionState(uint portId) { return m_collectedCompression[portId]; }
    public static bool HasConnection(uint portId) { return m_collectedCompression.ContainsKey(portId); }
    public static bool HasConnectionStillValide(uint portId) {
        if (!m_establishConnections.ContainsKey(portId)) return false;
        return m_establishConnections[portId].IsStillConnected();
        }
    public static UnityThreadPortCommunication GetConnection(uint portId) { return m_establishConnections[portId]; }
    public static void GetDigitalState(uint portId, uint index, out bool value, bool defaultIfNone = false) {
        if (!IsObservingPort(portId)) { value = defaultIfNone; return; }
        AnalogDigitalCompression state = GetCompressionState(portId);
        if (index < 0 || index >= state.GetDigitCount())
        { value = defaultIfNone; return; }
        value = state.GetDigitValue(index);
    }
    public static void GetAnalogState(uint portId, uint index, out short value, short defaultIfNone = 0) {
        if (!IsObservingPort(portId)) 
        { value = defaultIfNone; return; }
        AnalogDigitalCompression state = GetCompressionState(portId);
        if(index<0 || index >= state.GetAnalogCount()) 
        { value = defaultIfNone; return; }
        value = state.GetAnalogValue(index);
    
    }
    public static void GetStateAsCompressString(uint portId, out string value, string defaultIfNone = "#|")
    {
        if (!IsObservingPort(portId))
        { value = defaultIfNone; return; }
        AnalogDigitalCompression state = GetCompressionState(portId);
        value = state.GetCompressedMessage();

    }

    public  static uint[] GetPortObserved()
    {
        return m_establishConnections.Keys.ToArray();
    }

    public static void CloseConnections()
    {
        foreach (var item in m_establishConnections.Values)
        {
            item.StopThread();
        }
        
    }

    
}