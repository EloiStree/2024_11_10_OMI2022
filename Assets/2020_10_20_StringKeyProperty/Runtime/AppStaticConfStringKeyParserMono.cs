using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppStaticConfStringKeyParserMono : MonoBehaviour
{
  
    public ListenToStrigKeyValueType m_listeners;
   

    [ContextMenu("Access and parse push")]
    public void AccessAndPushObserveValue()
    {
        AccessAndPush_String();
        AccessAndPush_Bool();
        AccessAndPush_Int();
        AccessAndPush_ULong();
        AccessAndPush_Float();
        AccessAndPush_Double();
    }

    public void Test(string id) => Debug.Log("Quick Test string: " + id, this.gameObject);
    public void Test(bool id) => Debug.Log("Quick Test bool: " + id, this.gameObject);
    public void Test(int id) => Debug.Log("Quick Test int: " + id, this.gameObject);
    public void Test(ulong id) => Debug.Log("Quick Test ulong: " + id, this.gameObject);
    public void Test(float id) => Debug.Log("Quick Test float: " + id, this.gameObject);
    public void Test(double id) => Debug.Log("Quick Test double: " + id, this.gameObject);

    private void AccessAndPush_Double()
    {
        bool found;
        string id;
        double valueFloat;
        double defaultValueFloat;
        foreach (var item in m_listeners.m_doubleEvent)
        {
            item.GetKeyId(out id);
            StringKeyPropertyStatic.GetFromStringCollectionAsDouble(
                 id, out found, out valueFloat);
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
            StringKeyPropertyStatic.GetFromStringCollectionAsFloat(
                 id, out found, out valueFloat);
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
            item.GetKeyId(out id); StringKeyPropertyStatic.GetFromStringCollectionAsUlong(
                  id, out found, out valueFloat);
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
            StringKeyPropertyStatic.GetFromStringCollectionAsInt(
                  id, out found, out valueFloat);
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
            StringKeyPropertyStatic.GetFromStringCollectionAsBool(
                 id, out found, out value);
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
            StringKeyPropertyStatic.Get(
                  id, out found, out valueString);
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


}
