using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Eloi;
public class Debug_LanguageInjectionInScene : MonoBehaviour
{
    public UILangInjectionMono [] m_textInjectionsInScene;
    public string [] m_alias;
    public ulong[] m_ids;


    private void OnValidate()
    {
        if (!Application.isPlaying)
        {
            Refresh();
        }
    }

    [ContextMenu("Manual Refresh")]
    public void Refresh()
    {
       
        
            m_textInjectionsInScene = GameObject.FindObjectsOfType<UILangInjectionMono>();
            m_alias = m_textInjectionsInScene.Where(k => k.m_useAlias)
                .Select(k => (k.m_alias)).ToArray();
            m_ids = m_textInjectionsInScene.Where(k => k.m_useId)
               .Select(k => (k.m_id)).ToArray();
        
    }
}
