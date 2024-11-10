using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class User32ActionAbstractCatchToExecuteMono :MonoBehaviour
{

    public abstract void TryToExecute(IUser32Action actionToExecute);
    public abstract void TryToExecute(int milliseconds, IUser32Action actionToExecute);
    public abstract void TryToExecute(DateTime specificTime, IUser32Action actionToExecute);
}
public abstract class User32ActionAbstractCatchToExecute : Eloi.GenericSingletonOfMono<User32ActionAbstractCatchToExecuteMono>
{
    public static void PushAction(IUser32Action actionToExecute)
    {
        Instance.TryToExecute(actionToExecute);
    }
    public static void PushActionIn(int milliseconds, IUser32Action actionToExecute)
    {
        Instance.TryToExecute(milliseconds,actionToExecute);
    }
    public static void PushActionAt(DateTime specificTime, IUser32Action actionToExecute)
    {
        Instance.TryToExecute(specificTime,actionToExecute);
    }
}
