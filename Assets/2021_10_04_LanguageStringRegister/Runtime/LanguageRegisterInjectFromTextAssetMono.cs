using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageRegisterInjectFromTextAssetMono : MonoBehaviour
{
    public TextAsset  m_appendFromUnity;
    public bool m_fromUnityConverted;
    public string m_fromUnityError;

    void Start()
    {
        if (m_appendFromUnity != null) {
            ImportExcelToLanguageRegister.Import(m_appendFromUnity.text, ref LanguageRegister.Set, out m_fromUnityConverted, out m_fromUnityError);
            LanguageRegister.NotifyRegisterChange();
        }
    }

}
