using Eloi;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationConfStringKeyToParseValueMono : MonoBehaviour
{
    public DefaultApplicationConfigurationStringKeyValueMono m_source;

    public ListenToStrigKeyValueType m_listeners;

    public void Reset() {
        Eloi.E_SearchInSceneUtility.TryToFetchWithInScene( ref m_source); 
    }

 
    public void AccessAndPushObserveValue()
    {
        AccessAndPush_String();
        AccessAndPush_Bool();
        AccessAndPush_Int();
        AccessAndPush_ULong();
        AccessAndPush_Float();
        AccessAndPush_Double();
    }

    private void AccessAndPush_Double()
    {
        bool found;
        string id;
        double valueFloat;
        double defaultValueFloat;
        foreach (var item in m_listeners.m_doubleEvent)
        {
            item.GetKeyId(out id);
            m_source.m_application.GetFromStringCollectionAsDouble(
                in id, out found, out valueFloat);
            if (!found)
            {
                item.GetNotFoundDefaultValue(out defaultValueFloat);
                item.SetValueAndNotifyAsChanged(in defaultValueFloat);
            }
            else
            {
                item.SetValueAndNotifyAsChanged(in valueFloat);
            }
        }
    }

    private void AccessAndPush_Float()
    {
        bool found;
        string id;
        float valueFloat;
        float defaultValueFloat;
        foreach (var item in m_listeners.m_floatEvent)
        {
            item.GetKeyId(out id);
            m_source.m_application.GetFromStringCollectionAsFloat(
                in id, out found, out valueFloat);
            if (!found)
            {
                item.GetNotFoundDefaultValue(out defaultValueFloat);
                item.SetValueAndNotifyAsChanged(in defaultValueFloat);
            }
            else
            {
                item.SetValueAndNotifyAsChanged(in valueFloat);
            }
        }
    }

    private void AccessAndPush_ULong()
    {
        bool found;
        string id;
        ulong valueFloat;
        ulong defaultValueFloat;
        foreach (var item in m_listeners.m_ulongEvent)
        {
            item.GetKeyId(out id);
            m_source.m_application.GetFromStringCollectionAsUlong(
                in id, out found, out valueFloat);
            if (!found)
            {
                item.GetNotFoundDefaultValue(out defaultValueFloat);
                item.SetValueAndNotifyAsChanged(in defaultValueFloat);
            }
            else
            {
                item.SetValueAndNotifyAsChanged(in valueFloat);
            }
        }
    }

    private void AccessAndPush_Int()
    {
        bool found;
        string id;
        int valueFloat;
        int defaultValueFloat;
        foreach (var item in m_listeners.m_intEvent)
        {
            item.GetKeyId(out id);
            m_source.m_application.GetFromStringCollectionAsInt(
                in id, out found, out valueFloat);
            if (!found)
            {
                item.GetNotFoundDefaultValue(out defaultValueFloat);
                item.SetValueAndNotifyAsChanged(in defaultValueFloat);
            }
            else
            {
                item.SetValueAndNotifyAsChanged(in valueFloat);
            }
        }
    }

    private void AccessAndPush_Bool()
    {
        bool found;
        string id;
        bool value;
        bool defaultValue;
        foreach (var item in m_listeners.m_boolEvent)
        {
            item.GetKeyId(out id);
            m_source.m_application.GetFromStringCollectionAsBool(
                in id, out found, out value);
            if (!found)
            {
                item.GetNotFoundDefaultValue(out defaultValue);
                item.SetValueAndNotifyAsChanged(in defaultValue);
            }
            else
            {
                item.SetValueAndNotifyAsChanged(in value);
            }
        }
    }

    private void AccessAndPush_String()
    {
        string id;
        string valueString;
        string defaultString;
        bool found;
        foreach (var item in m_listeners.m_stringEvent)
        {
            item.GetKeyId(out id);
            m_source.m_application.GetFromStringCollection(
                in id, out found, out valueString);
            if (!found)
            {
                item.GetNotFoundDefaultValue(out defaultString);
                item.SetValueAndNotifyAsChanged(in defaultString);
            }
            else
            {
                item.SetValueAndNotifyAsChanged(in valueString);
            }
        }
    }

    public void Push(in string key, in float value, in bool createIfNotExisting) {

        if(createIfNotExisting)
            m_source.m_application.SetOrAddStringInCollectionAsText(in key, value.ToString());
        else 
            m_source.m_application.SetStringInCollectionAsTextIfFound(in key, value.ToString());
    }

}


[System.Serializable]
public class ListenToStrigKeyValueType {
    public StringKeyBoolEvent [] m_boolEvent = new StringKeyBoolEvent[0];
    public StringKeyIntEvent [] m_intEvent = new StringKeyIntEvent[0];
    public StringKeyULongEvent [] m_ulongEvent = new StringKeyULongEvent[0];
    public StringKeyFloatEvent [] m_floatEvent = new StringKeyFloatEvent[0];
    public StringKeyDoubleEvent [] m_doubleEvent = new StringKeyDoubleEvent[0];
    public StringKeyStringEvent [] m_stringEvent = new StringKeyStringEvent[0];
}

[System.Serializable]
public abstract class StringKeyToValueEvent<T> {

    public static string DEBUGTEXT = "Exception Config: ";
    [SerializeField] string m_keyId;
    [SerializeField] T m_notFoundValue;
    [SerializeField] T m_currentValue;

    public void GetKeyId(out string id)
    {
        id = m_keyId;
    }
    public void GetNotFoundDefaultValue(out T value)
    {
        value = m_notFoundValue;
    }
    public void GetCurrentValue(out T value)
    {
        value = m_currentValue;
    }
    public void SetValueAndNotifyAsChanged(in T value) {
        m_currentValue = value;
        try
        {
            SetValueAndNotifyAsChangeImplementation(value);
        }
        catch (Exception e)
        {
            Debug.Log(DEBUGTEXT + e.StackTrace);
        }
    }
    public abstract void SetValueAndNotifyAsChangeImplementation(in T value);

}

[System.Serializable]
public class StringKeyFloatEvent : StringKeyToValueEvent<float>
{
    [SerializeField] PrimitiveUnityEvent_Float m_onChange;
    public override void SetValueAndNotifyAsChangeImplementation(in float value)
    {
        m_onChange.Invoke(value);
    }
}
[System.Serializable]
public class StringKeyIntEvent : StringKeyToValueEvent<int>
{
    [SerializeField] PrimitiveUnityEvent_Int m_onChange;
    public override void SetValueAndNotifyAsChangeImplementation(in int value)
    {
        m_onChange.Invoke(value);
    }
}

[System.Serializable]
public class StringKeyByteEvent : StringKeyToValueEvent<byte>
{
    [SerializeField] PrimitiveUnityEvent_Byte m_onChange;
    public override void SetValueAndNotifyAsChangeImplementation(in byte value)
    {
        m_onChange.Invoke(value);
    }
}
[System.Serializable]
public class StringKeyBoolEvent : StringKeyToValueEvent<bool>
{
    [SerializeField] PrimitiveUnityEvent_Bool m_onChange;
    public override void SetValueAndNotifyAsChangeImplementation(in bool value)
    {
        m_onChange.Invoke(value);
    }
}
[System.Serializable]
public class StringKeyULongEvent : StringKeyToValueEvent<ulong>
{
    [SerializeField] PrimitiveUnityEvent_ULong m_onChange;
    public override void SetValueAndNotifyAsChangeImplementation(in ulong value)
    {
        m_onChange.Invoke(value);
    }
}
[System.Serializable]
public class StringKeyShortEvent : StringKeyToValueEvent<short>
{
    [SerializeField] PrimitiveUnityEvent_Short m_onChange;
    public override void SetValueAndNotifyAsChangeImplementation(in short value)
    {
        m_onChange.Invoke(value);
    }
}
[System.Serializable]
public class StringKeyStringEvent : StringKeyToValueEvent<string>
{
    [SerializeField] PrimitiveUnityEvent_String m_onChange;
    public override void SetValueAndNotifyAsChangeImplementation(in string value)
    {
        m_onChange.Invoke(value);
    }
}
[System.Serializable]
public class StringKeyDoubleEvent : StringKeyToValueEvent<double>
{

    [SerializeField] PrimitiveUnityEvent_Doube m_onChange;
    public override void SetValueAndNotifyAsChangeImplementation(in double value)
    {
        m_onChange.Invoke(value);
    }
}

