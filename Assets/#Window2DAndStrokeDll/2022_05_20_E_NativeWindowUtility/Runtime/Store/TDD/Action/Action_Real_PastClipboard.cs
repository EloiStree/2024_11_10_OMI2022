using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Action_Real_PastClipboard : IUser32Action
{
    public Action_Real_PastClipboard(int millisecondsBetween=5)
    {
        m_millisecondsBetweenKey = millisecondsBetween;
    }

    public int m_millisecondsBetweenKey;
}
