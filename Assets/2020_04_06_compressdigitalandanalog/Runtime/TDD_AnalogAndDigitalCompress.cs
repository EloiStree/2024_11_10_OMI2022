using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class TDD_AnalogAndDigitalCompress : MonoBehaviour
{
    public string m_message="#|";
    public AnalogDigitalCompressionUnityObserver m_observer = new AnalogDigitalCompressionUnityObserver();
    private AnalogDigitalCompressionSerialized m_compression= new AnalogDigitalCompressionSerialized();
    [Header("Debug")]
     public    bool isValide;

    public string m_lastChange;


    private void Awake()
    {
        m_compression.m_sizeChange += ChangedSize;
        m_observer.SetCompression(m_compression, true);
        m_observer.m_digitalChange.AddListener(DigitChanged);
        m_observer.m_analogChange.AddListener(AnalogChanged);
    }

    private void AnalogChanged(int arg0, short arg1, short arg2)
    {
        m_lastChange = arg0 + " " + arg1 + " > a >" + arg2; 
    }


    private void DigitChanged(int arg0, bool arg1, bool arg2)
    {
        m_lastChange = arg0 + " " + arg1 + " > d >" + arg2;
    }

    private void OnDestroy()
    {
        m_compression.m_sizeChange -= ChangedSize;
        m_observer.m_digitalChange.RemoveListener(DigitChanged);
        m_observer.m_analogChange.RemoveListener(AnalogChanged);
    }

    private void ChangedSize()
    {
        Debug.Log("Change of size: "+ m_message);
    }

    private void OnValidate()
    {
        
        m_compression.SetWithCompressMessage(m_message, out isValide);
    }

   
}


[System.Serializable]
public class AnalogDigitalCompressionUnityObserver {

    public AnalogDigitalCompression m_compression= new AnalogDigitalCompression();
    public bool[]  m_digit;
    public short[] m_analog;
    public UnityEvent m_onChanged;
    public DigitalCompressChange m_digitalChange;
    public AnalogCompressChange m_analogChange;

    public void StartListening() {
        m_compression.m_refreshed += CheckChange ;
    }
    public void StopListening()
    {
        m_compression.m_refreshed -= CheckChange;

    }
    public void SetCompression(AnalogDigitalCompression compression, bool forceCheck) {
        if (m_compression != null)
            m_compression.m_refreshed -= CheckChange;
        m_compression = compression;
        if (m_compression != null)
            m_compression.m_refreshed += CheckChange;
        if(forceCheck)
            CheckChange();
    }


    private void CheckChange()
    {
        if (m_compression == null) return;
        bool[] digit= m_compression.GetDigitCopy(), previousDigit = m_digit ;
        short[] analog = m_compression.GetAnalogCopy(), previousAnalog= m_analog;

        for (int i = 0; i < digit.Length && i < previousDigit.Length; i++)
        {
            if (digit[i] != previousDigit[i])
                m_digitalChange.Invoke(i, previousDigit[i], digit[i]);


        }
        for (int i = 0; i < analog.Length && i < previousAnalog.Length; i++)
        {
            if (analog[i] != previousAnalog[i])
                m_analogChange.Invoke(i ,  previousAnalog[i], analog[i]);

        }
        m_digit = digit;
        m_analog = analog;
        m_onChanged.Invoke();
    }
}

[System.Serializable]
public class DigitalCompressChange : UnityEvent<int, bool, bool>
{
   
}
[System.Serializable]
public class AnalogCompressChange : UnityEvent<int, short, short> { }


[System.Serializable]
public class AnalogDigitalCompressionSerialized : AnalogDigitalCompression
{ 
}
 public class AnalogDigitalCompression
    {

        public bool[] m_digitalValues= new bool[0];
    public short[] m_analogValues = new short[0];

    public ChangeDetected m_refreshed;
    public ChangeDetected m_sizeChange;
    public delegate void ChangeDetected();

    public void SetWithCompressMessage(string message, out bool isValide) {
        if (string.IsNullOrEmpty(message)) { isValide = false; return; }
        
    message = message.Trim();
        if (message.Length < 1) { isValide = false; return; }
        if (message[0] != '#') { isValide = false; return; }
        if (message.IndexOf('|') < 0) { isValide = false; return; }
        message = message.Substring(1);
        string[] tokens = message.Split('|');

        bool[] digitalValues = new bool[tokens[0].Length];
        short[] analogValues = new short[tokens[1].Length];
        if (tokens.Length != 2) { isValide = false; return; }

        int index = 0;
        foreach (char c in tokens[0].ToCharArray())
        {
            if(c != '1' && c != '0') {
                isValide = false; 
                return; 
            }

            digitalValues[index] = c == '1';

            index++;
        }
        index = 0;
        foreach (char c in tokens[1].ToCharArray())
        {

            try
            {
                analogValues[index] = short.Parse(c.ToString());
            }
            catch {
                isValide = false; return;
            }
            index++;
        }
        if (m_sizeChange!=null && (digitalValues.Length != m_digitalValues.Length || 
            analogValues.Length != m_analogValues.Length)  )
            m_sizeChange();
        
        m_digitalValues = digitalValues ;
        m_analogValues= analogValues;
        isValide = true;
        if(m_refreshed!=null)
        m_refreshed();
       
    }

    public int GetDigitCount() { return m_digitalValues.Length; }
    public int GetAnalogCount() { return m_analogValues.Length; }

    public bool GetDigitValue(uint index) { return m_digitalValues[index]; }
    public short GetAnalogValue(uint index) { return m_analogValues[index]; }

    public bool[] GetDigitCopy() { return m_digitalValues.ToArray(); }
    public short[] GetAnalogCopy() { return m_analogValues.ToArray(); }
    public string GetCompressedMessage() {
        StringBuilder sb = new StringBuilder();
        sb.Append('#');
        for (int i = 0; i < m_digitalValues.Length; i++)
        {
            sb.Append(m_digitalValues[i] ? '1' : '0');
        }
        sb.Append('|'); 
        for (int i = 0; i < m_analogValues.Length; i++)
        {
            sb.Append(m_analogValues[i] );
        }
        return sb.ToString();

    }

}


public abstract class LabelReference<T>
{


    [SerializeField] protected string m_label;
    [SerializeField] protected AnalogDigitalCompression m_source;
    [SerializeField] protected uint m_index;

    public LabelReference(string label, AnalogDigitalCompression source, uint index) {
        m_label = label;
        m_source = source;
        m_index = index;
    }

        public abstract T GetValue(bool isValide);
    public void ChangeIndex(uint newIndex) {
        m_index = newIndex;
    }
    public void ChangeLabel(string newName) {
        m_label = newName;
    }
    public void ChangeCompression(AnalogDigitalCompression compression) {
        m_source = compression;
    }
    public uint GetIndex() { return m_index; }
    public string GetLabel() { return m_label; }

}
public class AnalogLabelReference : LabelReference<short>
{
    public AnalogLabelReference(string label, AnalogDigitalCompression source, uint index) : base(label, source, index)
    {
    }

    public override short GetValue(bool isValide)
    {
         isValide= m_source != null && m_index < m_source.GetAnalogCount();
        return m_source.GetAnalogValue(m_index) ;

    }
}
public class DigitalLabelReference : LabelReference<bool>
{
    public DigitalLabelReference(string label, AnalogDigitalCompression source, uint index) : base(label, source, index)
    {
    }

    public override bool GetValue(bool isValide)
    {
        isValide= m_source != null && m_index < m_source.GetDigitCount();

        return m_source.GetDigitValue(m_index);


    }
}