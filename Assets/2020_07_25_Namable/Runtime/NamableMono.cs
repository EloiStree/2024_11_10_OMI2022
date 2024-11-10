using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NamableMono : MonoBehaviour, INamable
{
    public string m_choosedName;
    public string GetName()
    {
        return m_choosedName;
    }

    public void SetName(string name)
    {
        m_choosedName = name;
    }

    
}
