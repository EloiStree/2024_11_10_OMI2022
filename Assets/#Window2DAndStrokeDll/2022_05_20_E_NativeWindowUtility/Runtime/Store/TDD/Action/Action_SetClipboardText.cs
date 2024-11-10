using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Action_SetClipboardText : IUser32Action
{
    [TextArea(0,10)]
    public string m_textToPast;
    
}
