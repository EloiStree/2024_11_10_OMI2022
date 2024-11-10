using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StupidScript_ResetToZeroAndImport : MonoBehaviour
{

    public UnityEvent m_requestToReset;
    public UnityEvent m_startImporting;


    public void TriggerReimportOfFiles() {
        m_requestToReset.Invoke();
        m_startImporting.Invoke();
    }

}
