using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interpreter_Network : AbstractInterpreterMono
{

    public UDPThreadSender m_sender;

    public override bool CanInterpreterUnderstand(ref ICommandLine command)
    {
        string s = command.GetLine().ToLower().Trim();
       return s.IndexOf("udp ") == 0 && s.IndexOf("|") > 0 || s.IndexOf("udpfocus ") == 0 && s.IndexOf("|") > 0;
    }

    public override string GetName()
    {
        return "Network interpeter";
    }

    public override void TranslateToActionsWithStatus(ref ICommandLine command, ref ExecutionStatus succedToExecute)
    {
        string s = command.GetLine().ToLower().Trim();
        int startIndex = 0;
        if (s.IndexOf("udp ") == 0)
            startIndex = 4;
        int endIndex = s.IndexOf("|") ;

        //Debug.Log("DD:" + s + "DD" );
        if (startIndex > 1 && endIndex > startIndex && endIndex+1<s.Length)
        {
            string target = s.Substring(startIndex, endIndex - startIndex);
            string value = s.Substring(endIndex+1);
            //Debug.Log("T:" + target +" V:"+value+"<>");
            if (target == "all" || target.Trim().Length == 0)
                m_sender.AddMessageToSendToAll(value);
            else { 
                m_sender.TryToSend(target, value);
            
            }
        }


        //udpfocus name|focusname focusname
        //udpfocus target|MainComputer LeftComputer
        //udpfocus target|LeftComputer
        //udprelaytofocus target|Debuglog Hello
    }

    public override void WhatIsYourRequirementFor(ref ICommandLine command, out ICommandExecutioninformation executionInfo)
    {
        executionInfo = new CommandExecutionInformation(false, false, true, false);
    }

    public override string WhatWillYouDoWith(ref ICommandLine command)
    {
        return "";
    }

}

[System.Serializable]
public class UDPFocusGroup {


    public Dictionary<string,AliasToGroupOfAlias> m_groupAlias = new Dictionary<string,AliasToGroupOfAlias>();

    public void SetFocus(string name, params string[] focusAlias) {

        if (!m_groupAlias.ContainsKey(name))
            m_groupAlias.Add(name, new AliasToGroupOfAlias(name, focusAlias));
        m_groupAlias[name].Set( focusAlias);
    }
    public void Clear(string name) {

        if (!m_groupAlias.ContainsKey(name))
            m_groupAlias.Add(name, new AliasToGroupOfAlias(name,new string[0]));
        if (m_groupAlias.ContainsKey(name))
            m_groupAlias[name].Clear();
    }
}