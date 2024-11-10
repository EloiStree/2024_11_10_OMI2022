using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FindHarddriveSaveUse : MonoBehaviour
{
    public List<MonoBehaviour> m_inTheScene = new List<MonoBehaviour>();
    public bool m_saveOnHarddrive;

    private void Reset()
    {
        
        GameObject [] objs= GameObject.FindObjectsOfType<GameObject>();
        foreach (var item in objs)
        {
            m_inTheScene.AddRange( item.GetComponents<MonoBehaviour>().Where(k=> k is I_UseHarddriveSave).ToArray() ) ;
        }

    }
    private void OnValidate()
    {
        for (int i = 0; i < m_inTheScene.Count; i++)
        {
            I_UseHarddriveSave t = m_inTheScene[i]as I_UseHarddriveSave;
            t.SetSwitchTo(m_saveOnHarddrive);
        }
    }
}
