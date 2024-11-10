using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

public class RegexToCommandsRegisterMono : MonoBehaviour
{

    public RegexToCommandLinesRegister m_registeredRegex = new RegexToCommandLinesRegister();
    public int m_count;

    public void AddRegexToCommands(string regex, string text, char spliter)
    {
        AddRegexToCommands(regex, text.Split(spliter));
    }
    public void AddRegexToCommands(string regex, string [] lines)
    {
        m_registeredRegex.AddRegex(new RegexLinkedToCommandLines(regex, lines));

    }
    private void Update()
    {
        m_count = m_registeredRegex.GetCount() ;
    }

    public void Clear()
    {
        m_registeredRegex.Clear();
    }

    public void AddRegexToCommands(RegexLinkedToCommandLines rc)
    {
        m_registeredRegex.AddRegex(rc);
    }

    public RegexToCommandLinesRegister GetRegister()
    {
        return m_registeredRegex;
    }

    public bool Get(string text, out RegexLinkedToCommandLines regexInfo)
    {
        return m_registeredRegex.GetFirstMatchingRegex(text, out regexInfo);
    }
}

public class RegexToCommandLinesRegister
{
    private List<RegexLinkedToCommandLines> m_registeredRegex = new List<RegexLinkedToCommandLines>();

    public bool GetFirstMatchingRegex(string text, out RegexLinkedToCommandLines regexFound)
    {
        for (int i = 0; i < m_registeredRegex.Count; i++)
        {
            if (m_registeredRegex[i].IsTextMatching(text))
            {
                regexFound = m_registeredRegex[i];
                return true;
            }
        }
        regexFound = null;
        return false;
    }
    public bool GetAllMatchingRegex(string text, out List<RegexLinkedToCommandLines> regexFound)
    {
        regexFound = m_registeredRegex.Where(k => k.IsTextMatching(text)).ToList();
        return regexFound.Count > 0;
    }

    public void RemoveRegex(string regex) {

        m_registeredRegex = m_registeredRegex.Where(k => k.GetTheRegex() != regex).ToList();
    }
    public void RemoveRegex(RegexLinkedToCommandLines regex)
    {
        m_registeredRegex.Remove(regex);
    }

    public void AddRegex(RegexLinkedToCommandLines regexToMacro)
    {
        m_registeredRegex.Add(regexToMacro);
    }
    public void AddRegex(string regex, ListOfCommandLines macro, out RegexLinkedToCommandLines stored)
    {
        stored = new RegexLinkedToCommandLines(regex, macro);
        AddRegex(stored);
    }
    public void AddRegex(string regex, IEnumerable<ICommandLine> commands, out RegexLinkedToCommandLines stored)
    {
        stored = new RegexLinkedToCommandLines(regex, commands);
        AddRegex(stored);
    }

    public bool GetCommandLinesOf(string text, out List<ICommandLine> cmds)
    {
        RegexLinkedToCommandLines found;
       if (GetFirstMatchingRegex(text,out found)){
            cmds = found.GetCommandLines().ToList();
            return true;
        }
        cmds = new List<ICommandLine>();
        return false;
    }

   

    public void Clear()
    {
        m_registeredRegex.Clear();
    }

    public int GetCount()
    {
        return m_registeredRegex.Count;
    }
}

public class RegexLinkedToCommandLines {

    public string m_regex="";
    public ListOfCommandLines m_commands= new ListOfCommandLines();
  
    public RegexLinkedToCommandLines(string regex, params string[] commands)
    {
        m_regex = regex;
        Add(commands);
    }
    public void Add(params string[] commands)
    {
        for (int i = 0; i < commands.Length; i++)
        {
            m_commands.AddAtEnd(new CommandLine(commands[i]));
        }
    }
    public RegexLinkedToCommandLines(string regex, IEnumerable<ICommandLine> commands)
    {
        SetWith(regex, new ListOfCommandLines(commands));
    }
    public RegexLinkedToCommandLines(string regex, ListOfCommandLines commands) {
        SetWith(regex, commands);
    }
    private void SetWith(string regex, ListOfCommandLines commands) {
        m_commands = commands;
        m_regex = regex;
    }
    public string GetTheRegex() { return m_regex; }
    public bool IsTextMatching(string text) { return Regex.IsMatch(text, m_regex); }
    public IEnumerable<ICommandLine> GetCommandLines() { return m_commands.GetCommands(); }
    public ListOfCommandLines GetCommands() { return m_commands; }
}

