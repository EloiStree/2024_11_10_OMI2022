using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UI_QuickChangeAOE : MonoBehaviour
{

    public UI_AoeUtilityToJOMI[] m_elements;
    public List<Key> m_keys;

    [System.Serializable]
    public class Key {
        public string m_toCallId;
        public Sprite m_icon;
    
    }


    private void OnValidate()
    {
        while(m_keys.Count < m_elements.Length)
            m_keys.Add(new Key());
        for (int i = 0; i < m_elements.Length; i++)
        {
            m_elements[i].SetCallId(m_keys[i].m_toCallId);
            m_elements[i].SetIcon(m_keys[i].m_icon);
        }
    }

}
