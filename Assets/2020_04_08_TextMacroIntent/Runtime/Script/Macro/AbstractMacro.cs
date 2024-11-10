using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MacroCoroutine : MonoBehaviour {


    public static FinishChecker Execute(IMacro macro) {
        GameObject obj = new GameObject("Executing Macro");
        MacroCoroutine corou =obj.AddComponent< MacroCoroutine>();
        return corou.StartExecutreMacro(macro);
        
    }

    public IMacro m_macroToExecutre;
    public Coroutine m_currentCoroutine;
    public FinishChecker m_isFinished = new FinishChecker();

    public FinishChecker StartExecutreMacro(IMacro macro) {
        m_isFinished.AddListener(StopNow);
        m_currentCoroutine = StartCoroutine(macro.Execute(m_isFinished));
        return m_isFinished;
        
    }

    public void StopNow() {
        if(m_currentCoroutine!=null)
        StopCoroutine(m_currentCoroutine);
        Destroy(this.gameObject);
    }
}

public class FinishChecker {
    private bool m_isFinished=false;
    private OnFinish m_onFinish;
    public delegate void OnFinish();
    public void AddListener(OnFinish toDo) {
        m_onFinish += toDo;
    }
    public bool IsFinished() { return m_isFinished; }
    public void SetAsFinished() { 
        m_isFinished = true;
        if(m_onFinish!=null)
            m_onFinish();
    }
}
public interface IMacro
{
    IEnumerator Execute(FinishChecker isExecuted);
}
public abstract class AbstractMacro : IMacro
{
    public abstract IEnumerator Execute(FinishChecker isExecuted);
    public static void TryToInterpretCommand(string cmd)
    {

        throw new System.NotImplementedException("Was but is not implemented for the moment");
//        CommandInterpreter.TryToTranslate(cmd);
    }
}
[System.Serializable]
public class SingleMacro : AbstractMacro
{
    public string m_command = "";
    public override IEnumerator Execute(FinishChecker isExecuted)
    {
        TryToInterpretCommand(m_command);
        if(isExecuted!=null)
        isExecuted.SetAsFinished();
        yield break ;
    }
}
[System.Serializable]
public class GroupMacro : AbstractMacro
{
    public string[] m_commands = new string[] { };
    public override IEnumerator Execute(FinishChecker isExecuted)
    {
        for (int i = 0; i < m_commands.Length; i++)
        {
            TryToInterpretCommand(m_commands[i]);
        }
        if (isExecuted != null)
            isExecuted.SetAsFinished();
        yield break;
    }
}
[System.Serializable]
public class SequenceMacro : AbstractMacro
{
    public class TimeBeforeMacro {
        public float m_timeBefore=0.05f;
        public AbstractMacro m_macro;
        public bool m_parallelsExecution;
    }
    public List<TimeBeforeMacro> m_commands = new List<TimeBeforeMacro>();
    public override IEnumerator Execute(FinishChecker isExecuted)
    {
        for (int i = 0; i < m_commands.Count; i++)
        {
            yield return new WaitForSeconds(m_commands[i].m_timeBefore);
            if (m_commands[i].m_parallelsExecution)
                MacroCoroutine.Execute(m_commands[i].m_macro);
            else yield return m_commands[i].m_macro.Execute(null);
        }
        if (isExecuted != null)
            isExecuted.SetAsFinished();
       yield break;
    }
}