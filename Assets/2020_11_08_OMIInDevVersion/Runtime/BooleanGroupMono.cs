using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BooleanGroupMono : MonoBehaviour
{
    public BooleanGroupRegister m_groupRegister;
    public BooleanStateRegisterMono m_boolRegister;

    public List<NamedBooleanRefGroup> m_groupRef= new List<NamedBooleanRefGroup>();



    public void ReloadGroup()
    {
        foreach (var item in m_groupRegister.GetGroups() )
        {
            m_groupRef.Add(new NamedBooleanRefGroup(m_boolRegister, item));
        }
    }

    public void SelectAs(bool value, string groupname, string boolname)
    {
        NamedBooleanRefGroup group;
        bool found;
        TryToGetGroup(groupname, out group, out found);
        if (found)
        {
            List<string> inverse = group.m_booleanGroup.GetAllBooleanNames();
            inverse.Remove(boolname);
            m_boolRegister.Set(boolname, value, true);
            m_boolRegister.Set(!value, inverse);

        }
    }

    public void SwitchAll(string groupname)
    {
        NamedBooleanRefGroup group;
        bool found;
        TryToGetGroup(groupname, out group, out found);
        if (found)
        {
            m_boolRegister.Switch( group.m_booleanGroup.m_targetNames);

        }
    }
    //boolgroup:false:mapping
    //boolgroup:false:mapping:map2
    public void SetAll(bool value, string groupname)
    {
        NamedBooleanRefGroup group;
        bool found;
        TryToGetGroup(groupname, out group, out found);
        if (found) {
            m_boolRegister.Set(value, group.m_booleanGroup.m_targetNames);
        }

    }

    public void TryToGetGroup(string nameOfGroup, out NamedBooleanRefGroup group, out bool found) {
        group = null;
        NamedBooleanRefGroup[] foundGroup= m_groupRef.Where(k => k.m_booleanGroup.m_groupName == nameOfGroup).ToArray();
        found = foundGroup.Length > 0;
        if (found)
            group = foundGroup[0];
    }
}
