using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StackerRegisterMono : MonoBehaviour
{
    public Dictionary<string, CommandLineStacker> m_register = new Dictionary<string, CommandLineStacker>();
    public List<CommandLineStacker> m_stackList= new List<CommandLineStacker>();
   
    public void Clear()
    {
        m_register.Clear();

            m_stackList.Clear();
    }


    public void Remove(string nameId)
    {
        if (m_register.ContainsKey(nameId))
        {
            m_stackList.Remove(m_register[nameId]);
            m_register.Remove(nameId);
        }
    }


    public void Add(string nameId, CommandLineStacker stack)
    {
        if (!m_register.ContainsKey(nameId))
        {
            m_stackList.Add( stack);
            m_register.Add(nameId, stack);
        }
    }


    public void Create(string nameId, params string[] actions)
    {
        Remove(nameId);
        Add(nameId, new CommandLineStacker(actions));
    }
    public void Append(string nameId, params string[] actions)
    {
        if (m_register.ContainsKey(nameId))
        {
            CommandLineStacker select = m_register[nameId];
            select.AddCommand(actions);
        }
        else {
            Create(nameId, actions);
        }
    }

    public List<ICommandLine> DoNext(string stackName, int count)
    {
        List<ICommandLine> found = new List<ICommandLine>();
        if (m_register.ContainsKey(stackName))
        {
            bool order = count>=0;
            for (int i = 0; i < Mathf.Abs(count); i++)
            {
                if (order)
                    m_register[stackName].GoNext();
                else
                    m_register[stackName].GoPrevious();
                found.Add(m_register[stackName].GetCurrent());
            }
        }
        return found;
    }
    public void GoNext(string stackName, int count)
    {
        if (m_register.ContainsKey(stackName))
        {
            bool order = count >= 0;
            for (int i = 0; i < Mathf.Abs(count); i++)
            {
                if (order)
                    m_register[stackName].GoNext();
                else
                    m_register[stackName].GoPrevious();
            }
        }
    }


    public void SetIndex(string stackName, int index)
    {
       
        if (m_register.ContainsKey(stackName))
        {
                    m_register[stackName].SetIndex(index); 
        }
    }

   

    public ICommandLine GetCurrent(string stackName)
    {
        if (m_register.ContainsKey(stackName))
        {

            return m_register[stackName].GetCurrent();
        }
        else return null;
    }

    public void Clear(string stackName)
    {
        if (m_register.ContainsKey(stackName))
        {

             m_register[stackName].Clear();
        }
     
    }

    public void MoveAtEnd(string stackName)
    {
        if (m_register.ContainsKey(stackName))
        {

            m_register[stackName].MoveAtEnd();
        }
    }
    public void MoveAtStart(string stackName)
    {
        if (m_register.ContainsKey(stackName))
        {

            m_register[stackName].MoveAtStart();
        }
    }
   

    
}

[System.Serializable]
public class NamedCommandLineStackerRef {

    public string m_name;
    public CommandLineStacker m_stacker;
}

[System.Serializable]
public class CommandLineStacker
{
    public List<CommandLine> m_commands= new List<CommandLine>();
    public int m_index=0;

    public CommandLineStacker() { }
    public CommandLineStacker(params string[] cmds) {
        AddCommand(cmds);
    }
    public void Clear() {
        m_commands.Clear();
        m_index=0;
    }

    public void AddCommand(params string [] cmds)
    {
        for (int i = 0; i < cmds.Length; i++)
        {
            if(cmds.Length>0)
                m_commands.Add(new CommandLine(cmds[i]));
        }
    }

    public ICommandLine GetCurrent()
    {
        if (m_commands.Count <= 0) 
            return new CommandLine("");
        return m_commands[m_index];
    }
    public List<ICommandLine> GetCommands()
    {
        return m_commands.ToList<ICommandLine>();
    }

    public void ResetIndex() {
        m_index = 0;
    }
    public void GoNext()
    {
        m_index++;
        if (m_index >= m_commands.Count)
            m_index = 0;
    }
    public void GoPrevious()
    {

        m_index--;
        if (m_index <0)
            m_index = m_commands.Count-1;
    }
    public void GoNext(uint value)
    {
        for (int i = 0; i < value; i++)
        {
            GoNext();
        }
    }
    public void GoPrevious(uint value)
    {
        for (int i = 0; i < value; i++)
        {
            GoPrevious();
        }
    }

    public void SetIndex(int index)
    {
        if (index < 0) 
            m_index = 0;
        if (m_commands.Count <= 0)
            m_index = 0;
        if (index >= m_commands.Count )
            m_index = m_commands.Count - 1; 
    }

    public void MoveAtStart()
    {
        SetIndex(0);
    }

    public void MoveAtEnd()
    {
        SetIndex(GetMaxIndex());
    }

    private int GetMaxIndex()
    {
        if (m_commands.Count == 0)
            return 0;
        return m_commands.Count - 1;
    }
}
