using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Action_Real_CopyClipboard : IUser32Action
{
    public Action_Real_CopyClipboard(int millisecondsBetween = 5)
    {
        m_millisecondsBetweenKey = millisecondsBetween;
    }

    public int m_millisecondsBetweenKey;
}
