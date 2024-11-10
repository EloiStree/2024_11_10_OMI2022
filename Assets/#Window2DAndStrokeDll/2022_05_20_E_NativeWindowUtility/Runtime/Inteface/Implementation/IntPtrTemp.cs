
using System;

[System.Serializable]
public struct IntPtrTemp : IntPtrWrapGet
{
    public int m_pointer;
    public bool m_isParent;

    public static IntPtrWrapGet Zero { get; internal set; }

    public IntPtrTemp(int pointer, bool isParent) { m_pointer = pointer; m_isParent = isParent; }
    public IntPtrTemp(IntPtr pointer, bool isParent) { m_pointer = (int)pointer; m_isParent = isParent; }
    public IntPtrTemp(IntPtrWrapGet pointer) {
        pointer.GetAsInt(out m_pointer);
        pointer.IsParent(out m_isParent);
    }

    public static IntPtrWrapGet Int(int value, bool isParent= true)
    {
        return new IntPtrTemp(value,isParent);
    }

    public void GetAsInt(out int pointer)
    {
        pointer = m_pointer;
    }

    public int GetAsInt()
    {
        return m_pointer;
    }

    public void GetAsIntPtr(out IntPtr pointer)
    {
        pointer = (IntPtr)m_pointer;
    }

    public IntPtr GetAsIntPtr()
    {
        return (IntPtr)m_pointer;
    }

    public bool IsParent()
    {
        return m_isParent;
    }

    public void IsParent(out bool isParent)
    {
        isParent = m_isParent;
    }

    public static IntPtrWrapGet Int(IntPtr intPtr, bool isParent=true)
    {
        return new IntPtrTemp(intPtr,isParent);
    }

    public  void Set(int processId)
    {
        m_pointer = processId;
    }
}



[System.Serializable]
public struct IntPtrProcessId : IntPtrWrapGet
{
    public int m_pointer;
    public IntPtrProcessId(int pointer) { m_pointer = pointer;  }
    public IntPtrProcessId(IntPtr pointer) { m_pointer = (int)pointer; }
    public IntPtrProcessId(IntPtrWrapGet pointer)
    {
        pointer.GetAsInt(out m_pointer);
    }

    public static IntPtrWrapGet Int(int value)
    {
        return new IntPtrProcessId(value);
    }

    public void GetAsInt(out int pointer)
    {
        pointer = m_pointer;
    }

    public int GetAsInt()
    {
        return m_pointer;
    }

    public void GetAsIntPtr(out IntPtr pointer)
    {
        pointer = (IntPtr)m_pointer;
    }

    public IntPtr GetAsIntPtr()
    {
        return (IntPtr)m_pointer;
    }

    public bool IsParent()
    {
        throw new Exception("No parent define for this class");
    }

    public void IsParent(out bool isParent)
    {
        throw new Exception("No parent define for this class");
    }

    public static IntPtrWrapGet Int(IntPtr intPtr)
    {
        return new IntPtrProcessId(intPtr);
    }

    public void Set(int processId)
    {
        m_pointer = processId;
    }
}