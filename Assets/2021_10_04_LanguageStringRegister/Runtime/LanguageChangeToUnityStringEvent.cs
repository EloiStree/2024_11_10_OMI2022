using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageChangeToUnityStringEvent : AbstractLanguageChangeToText
{

    public TextFromLanguageToInject m_textChangeRequest;

    protected override void OnTextChangeRequested(in string text)
    {
        m_textChangeRequest.Invoke(text);
    }
    protected override void TryToAutoFill()
    {
        base.TryToAutoFill();
    }
}