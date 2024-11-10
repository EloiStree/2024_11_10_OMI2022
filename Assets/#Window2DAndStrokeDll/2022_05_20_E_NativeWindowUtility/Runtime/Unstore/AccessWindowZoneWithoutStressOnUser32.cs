
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public interface IAccessWindowZoneWithoutStressOnUser32
{

    public void ForceRefresh(in IntPtrWrapGet processId);
    public void RefreshAll();
    public void GetWindowInfo(in IntPtrWrapGet processId, out DeductedInfoOfWindowSizeWithSource info);

}

public class AccessWindowZoneWithoutStressOnUser32 :MonoBehaviour, IAccessWindowZoneWithoutStressOnUser32
{
    public Dictionary<int, DeductedInfoOfWindowSizeWithSource> m_idToSize = new Dictionary<int, DeductedInfoOfWindowSizeWithSource>();

    public void ForceRefresh(in IntPtrWrapGet processId)
    {
        FetchWindowInfoUtility.Get(processId, out DeductedInfoOfWindowSizeWithSource rect); 
        SetindowInfo(rect);
    }
    public void ForceRefresh(in int processId)
    {
        FetchWindowInfoUtility.Get(new IntPtrProcessId(processId), out DeductedInfoOfWindowSizeWithSource rect);
        SetindowInfo(rect);

    }
    public void SetindowInfo( in DeductedInfoOfWindowSizeWithSource info) {
        int id = info.m_pointer.GetAsInt();
        if (m_idToSize.ContainsKey(id))
        {
            m_idToSize[id]=info;
        }
        else
        {
             m_idToSize.Add(id, info);
        }
    }

    public void GetWindowInfo(in IntPtrWrapGet processId, out DeductedInfoOfWindowSizeWithSource info)
    {
        GetWindowInfo(processId.GetAsInt(), out info);
    }
        public void GetWindowInfo(in int processId, out DeductedInfoOfWindowSizeWithSource info)
    {
        if (m_idToSize.ContainsKey(processId))
        { 
            info = m_idToSize[processId];
        }
        else {
            FetchWindowInfoUtility.Get(new IntPtrProcessId(processId), out DeductedInfoOfWindowSizeWithSource rect);
            m_idToSize.Add(processId, rect);
            info = rect;
        }
    }

    public void RefreshAll()
    {
        List<int> ids=  m_idToSize.Keys.ToList() ;
        for (int i = 0; i < ids.Count; i++)
        {
            ForceRefresh(ids[i]);
        }
       
    }
    public void FlushRegistered() {
        m_idToSize.Clear();
    }


}