using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class AbstractIntPtrWrapMono : MonoBehaviour, IntPtrWrapGet
{
    public abstract void GetAsInt(out int pointer);
    public abstract int GetAsInt();
    public abstract void GetAsIntPtr(out IntPtr pointer);
    public abstract IntPtr GetAsIntPtr();
    public abstract bool IsParent();
    public abstract void IsParent(out bool isParent);
}

public abstract class AbstractContaintIntPtrWrapMono : AbstractIntPtrWrapMono
{
    public abstract void GetIntPtrInChildren(out IntPtrWrapGet info);
    public override void GetAsInt(out int pointer) {
        GetIntPtrInChildren(out IntPtrWrapGet info);
        info.GetAsInt(out pointer);
    }
    public override int GetAsInt()
    {
        GetIntPtrInChildren(out IntPtrWrapGet info);
        return info.GetAsInt();
    }
    public override void GetAsIntPtr(out IntPtr pointer)
    {
        GetIntPtrInChildren(out IntPtrWrapGet info);
        info.GetAsIntPtr(out pointer);
    }
    public override IntPtr GetAsIntPtr()
    {
        GetIntPtrInChildren(out IntPtrWrapGet info);
        return info.GetAsIntPtr();
    }
    public override bool IsParent()
    {
        GetIntPtrInChildren(out IntPtrWrapGet info);
        return info.IsParent();
    }
    public override void IsParent(out bool isParent)
    {
        GetIntPtrInChildren(out IntPtrWrapGet info);
        info.IsParent(out isParent);
    }
}




public interface IntPtrWrapGet
{
    void GetAsIntPtr(out IntPtr pointer);
    IntPtr GetAsIntPtr(); 
    void GetAsInt(out int pointer);
    int GetAsInt();
    bool IsParent();
    void IsParent(out bool isParent);
}

public interface IntPtrWrapPointerSet
{
    void SetAsIntPtr(IntPtr pointer);
    void SetAsInt(int pointer);

    void SetAsIntPtrAsRef(in IntPtr pointer);
    void SetAsIntAsRef(in int pointer);
}
public interface IntPtrWrapParentingSet
{
    void SetAsParent(bool isParent);
    void SetAsParentAsRef(in bool isParent);
}
