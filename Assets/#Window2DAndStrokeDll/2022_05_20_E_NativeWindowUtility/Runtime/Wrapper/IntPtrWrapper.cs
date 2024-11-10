
using System;
using UnityEngine;

[System.Serializable]
public class IntPtrWrapper
{
    [SerializeField] IntPtr m_ref;
    [SerializeField] int m_debugValueOfRef;
    public IntPtrWrapper()
    {
        m_ref = IntPtr.Zero;
    }
    public IntPtrWrapper(IntPtr value)
    {
        SetRefTo(value);
    }
    public bool IsNotZero() { return m_ref != null && m_ref != IntPtr.Zero; }
    public void SetWith(int value)
    {
        m_ref = new IntPtr(value);
        m_debugValueOfRef = (int)m_ref;
    }
    public void SetRefTo(IntPtr value)
    {
        m_ref = value;
        m_debugValueOfRef = (int)m_ref;
    }

    public void GetRef(out IntPtr value)
    {
        value = m_ref;
    }
    public IntPtr GetRef()
    {
        return m_ref;
    }
}