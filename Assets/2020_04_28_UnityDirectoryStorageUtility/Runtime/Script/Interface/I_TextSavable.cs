using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface I_TextSavable
{
    string GetSavableText();
    string GetSavableDefaultText();
    void SetTextFromLoad(string text);
}
