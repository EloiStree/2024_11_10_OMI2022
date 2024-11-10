using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class TargetProcessIntPtrMono : AbstractIntPtrWrapMono, IntPtrWrapGet, IntPtrWrapPointerSet, IntPtrWrapParentingSet
{
    [SerializeField] TargetSwitchableProcessIntPtr m_processId;
    public override void GetAsInt(out int pointer)
    {
        m_processId.GetAsInt(out pointer);
    }
    public override int GetAsInt()
    {
        return m_processId.GetAsInt();
    }
    public override IntPtr GetAsIntPtr()
    {
        return m_processId.GetAsIntPtr();
    }
    public override void GetAsIntPtr(out IntPtr pointer)
    {
        m_processId.GetAsIntPtr(out pointer);
    }

    public override bool IsParent()
    {
        return m_processId.IsParent();
    }

    public override void IsParent(out bool isParent)
    {
         m_processId.IsParent(out isParent);
    }

    public void SetAsInt(int pointer)
    {
        m_processId.SetAsInt( pointer);
    }

    public void SetAsIntAsRef(in int pointer)
    {
        m_processId.SetAsIntAsRef (in pointer);
    }

    public void SetAsIntPtr(IntPtr pointer)
    {
        m_processId.SetAsIntPtr(pointer);
    }

    public void SetAsIntPtrAsRef(in IntPtr pointer)
    {
        m_processId.SetAsIntPtrAsRef (pointer);
    }

    public void SetAsParent(bool isParent)
    {
        m_processId.SetAsParent ( isParent);
    }

    public void SetAsParentAsRef(in bool isParent)
    {
        m_processId.SetAsParentAsRef( in isParent);
    }
}





[System.Serializable]
public class TargetSwitchableProcessIntPtr : TargetUnkownProcessIntPtr, IntPtrWrapParentingSet
{
    public bool m_isParent;
    public override bool IsParent()
    {
        return m_isParent;
    }
    public override void IsParent(out bool isParent)
    {
        isParent = m_isParent;
    }

    public void SetAsParent(bool isParent)
    {
        m_isParent = isParent;
    }

    public void SetAsParentAsRef(in bool isParent)
    {
        m_isParent = isParent;
    }
}


[System.Serializable]
public class TargetParentProcessIntPtr : TargetUnkownProcessIntPtr
{
    public override bool IsParent()
    {
        return true;
    }
    public override void IsParent(out bool isParent)
    {
        isParent = true;
    }

    
}
[System.Serializable]
public class TargetChildrenProcessIntPtr : TargetUnkownProcessIntPtr
{
    public override bool IsParent()
    {
        return false;
    }
    public override void IsParent(out bool isParent)
    {
        isParent = false;
    }
}



[System.Serializable]
public abstract class TargetUnkownProcessIntPtr : IntPtrWrapGet, IntPtrWrapPointerSet
{


    public void SetToZero()
    {
        SetAsInt(0);
    }
    public int m_proccessId=0;
    public void GetAsInt(out int pointer)
    {
        pointer = m_proccessId;
    }
    public int GetAsInt()
    {
        return m_proccessId;
    }
    public IntPtr GetAsIntPtr()
    {
        return (IntPtr)m_proccessId;
    }
    public void GetAsIntPtr(out IntPtr pointer)
    {
        pointer = (IntPtr)m_proccessId;
    }

    public abstract bool IsParent();

    public abstract void IsParent(out bool isParent);

    public void SetAsInt(int pointer)
    {
        m_proccessId = pointer;
    }

    public void SetAsIntAsRef(in int pointer)
    {
        m_proccessId = pointer;
    }

    public void SetAsIntPtr(IntPtr pointer)
    {
        m_proccessId = (int)pointer;
    }

    public void SetAsIntPtrAsRef(in IntPtr pointer)
    {
        m_proccessId = (int)pointer;
    }

}
