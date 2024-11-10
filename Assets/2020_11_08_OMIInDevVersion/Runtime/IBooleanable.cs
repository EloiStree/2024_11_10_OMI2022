using System;
using UnityEngine;
public interface IBooleanable
{
     bool IsTrue();
     bool IsFalse();
}

public interface INamedBooleanable : IBooleanable
{
     string GetIdName();

}
public class BooleanableAbstract : IBooleanable
{
    [SerializeField]
    protected bool m_isTrueValue;
    public bool IsTrue() { return m_isTrueValue; }
    public bool IsFalse() { return !m_isTrueValue; }
}
public class SettableBooleanableAbstract : BooleanableAbstract
{
    public void SetBooleanValue(bool isTrue) { m_isTrueValue = isTrue; }
}

public class NamedBooleanableAbstract : BooleanableAbstract, INamedBooleanable
{
    [SerializeField]
    protected string m_idName;

    public NamedBooleanableAbstract(string name, bool startValue)
    {
        m_idName = name;
        m_isTrueValue = startValue;
    }
    public string GetIdName()
    {
        return m_idName;
    }
}

public class NamedBooleanableRef : INamedBooleanable
{
    

    public IBooleanable m_linkedBoolean;
    [SerializeField]
    protected string m_idName;

    public NamedBooleanableRef(string name, IBooleanable boolean)
    {
        m_linkedBoolean = boolean;
        m_idName = name;
    }
    public string GetIdName()
    {
        return m_idName;
    }

    public bool IsFalse()
    {
        return m_linkedBoolean.IsFalse();
    }

    public bool IsTrue()
    {
        return m_linkedBoolean.IsTrue();
    }
}