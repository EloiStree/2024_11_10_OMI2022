using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface FunctionCallAsString{

     void Call(string methodeName, params string[] parameters);
}

[System.Serializable]
public class StringAnonymeCall : UnityEvent<string, string[]> { }

public class Interpreter_CallFunction : AbstractInterpreterMono
{
    public List<MonoBehaviour> m_linkedScripts = new List<MonoBehaviour>();
    public StringAnonymeCall m_listener;

    public override bool CanInterpreterUnderstand(ref ICommandLine command)
    {
        return StartWith(ref command, "fct:", true);
    }

    public override string GetName()
    {
        return "Call Function";
    }

    public override void TranslateToActionsWithStatus(ref ICommandLine command, ref ExecutionStatus succedToExecute)
    {
        string cmd = command.GetLine().Trim();
        string cmdLow = command.GetLine().ToLower();
        string[] token = cmd.Split(':');
        bool wasManaged = false;
        
        if (cmdLow.IndexOf("fct:") == 0 && token.Length>2)
        {
            string methodeName = token[1].Trim();
            int count = token.Length - 2;
            string[] parameters = count > 0? new string[count]: new string[0];
            for (int i = 0; i < count; i++)
            {
                parameters[i] = token[2 + i];
            }

            m_listener.Invoke(methodeName, parameters);
            for (int i = 0; i < m_linkedScripts.Count; i++)
            {
                m_linkedScripts[i].SendMessage(methodeName, SendMessageOptions.DontRequireReceiver);
            }

            wasManaged = true;
        }
        succedToExecute.SetAsFinished(wasManaged);

    }

    public override void WhatIsYourRequirementFor(ref ICommandLine command, out ICommandExecutioninformation executionInfo)
    {
        executionInfo=  new CommandExecutionInformation(false, false, false, true);
    }

    public override string WhatWillYouDoWith(ref ICommandLine command)
    {
        return "";
    }
}
