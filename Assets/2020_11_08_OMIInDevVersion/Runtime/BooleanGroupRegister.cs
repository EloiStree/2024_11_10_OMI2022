using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BooleanGroupRegister : MonoBehaviour
{
    public Dictionary<string,NamedBooleanGroup> m_groupRegister = new Dictionary<string, NamedBooleanGroup>();
    public List<NamedBooleanGroup> m_debug = new List<NamedBooleanGroup>();

    public void Clear()
    {
        m_groupRegister.Clear();
    }

    internal IEnumerable<NamedBooleanGroup> GetGroups()
    {
        return m_groupRegister.Values;
    }

    public void Add(NamedBooleanGroup group)
    {
        if (!m_groupRegister.ContainsKey(group.m_groupName)) { 
            m_groupRegister.Add(group.m_groupName, group);
             m_debug.Add(group);
        }
        else
            m_groupRegister[group.m_groupName]= group;
    }

    public void GetGroup(string groupName, out NamedBooleanGroup group) {

        if ( !m_groupRegister.ContainsKey(groupName) )
        {
            group = null; 
        }
        else {
            group = m_groupRegister[groupName];
        }
    }
}
