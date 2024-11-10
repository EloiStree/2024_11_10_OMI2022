using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demo_Macros : MonoBehaviour
{
    public SingleMacro m_singleMacro;
    public GroupMacro m_groupMacro;
    public SequenceMacro m_sequenceMacro;
    

    void Start()
    {
        MacroCoroutine.Execute(m_singleMacro);
        MacroCoroutine.Execute(m_groupMacro);
        MacroCoroutine.Execute(m_sequenceMacro);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
