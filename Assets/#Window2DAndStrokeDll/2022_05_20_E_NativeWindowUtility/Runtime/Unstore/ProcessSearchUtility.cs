using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ProcessSearchUtility
{

    public static void FindWindowParentProcess(string windowName, out IntPtrWrapGet found)
        => throw new System.Exception(); //found = new IntPtrTemp(WindowIntPtrUtility. FindWindowParentProcess (null, windowName), true);
    public static void FindChildrenOf(IntPtrWrapGet pointer, out List<IntPtrWrapGet> found)
    {
        FindChildrenOf(pointer, out found);
    }
    public static void GetParentWithChildrensOf(IntPtrWrapGet parentPointer, out List<IntPtrWrapGet> found)
    {
        FindChildrenOf(parentPointer, out found);
        found.Add(parentPointer);
    }
}