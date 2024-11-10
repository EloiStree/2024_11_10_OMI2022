using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TDD_MacroAddAndPlay : MonoBehaviour
{
    public MacroCoroutineExecution m_executer;
    public NameListOfCommandLinesRegister m_storeTest = new NameListOfCommandLinesRegister();
    public List<TextLinkedToId> m_textToCommandsMacro = new List<TextLinkedToId>();

    [Header("Params")]
    public string m_testLoop = "Hello World";
    public MacroCoroutineExecution.LoopTurnOffButton m_loopOffButton;
    public string m_testRandom = "Change Color Randomly";
    public int m_randomInteration = 10;
    public string m_testPlayReverse = "123";


    public string m_testOneByCall = "Countdown";
    public MacroCoroutineExecution.RequestNext m_nextRemote;
    [Header("Debug")]
    public int m_macroRegistered;
    public string[] m_names;
    private void Start()
    {

        for (int i = 0; i < m_textToCommandsMacro.Count; i++)
        {
            string id = m_textToCommandsMacro[i].m_nameId;
            string txt = m_textToCommandsMacro[i].m_text;
            List<ICommandLine> lines = CommandLine.GetLinesFromCharSplite(txt);
            m_storeTest.AddList(id, new NamedListOfCommandLines(id, lines));
            m_names = m_storeTest.GetNameRegistered();
        }
        List<ICommandLine> cmds;
        if (m_storeTest.GetCommandLinesOf(m_testLoop, out cmds))
        {
            StartCoroutine(m_executer.ExecuteLoop(m_loopOffButton, cmds));
        }
        if (m_storeTest.GetCommandLinesOf(m_testRandom, out cmds))
        {
            StartCoroutine(m_executer.ExecuteRandomCommands(m_randomInteration, cmds));
        }
        if (m_storeTest.GetCommandLinesOf(m_testPlayReverse, out cmds))
        {
            IEnumerator up = m_executer.ExecuteStepByStep(cmds, false);
            IEnumerator down = m_executer.ExecuteStepByStep(cmds, true);
            PlayCoroutines(up, down);

        }
        if (m_storeTest.GetCommandLinesOf(m_testOneByCall, out cmds))
        {
            IEnumerator up = m_executer.ExecuteOneLineAtTime(m_nextRemote, cmds);
            PlayCoroutines(up);

        }
    }

    public void PlayCoroutines(params IEnumerator[] coroutines){
        StartCoroutine(GoThroughCoroutines(coroutines));
    }

    private IEnumerator GoThroughCoroutines(params IEnumerator [] coroutines)
    {
        for (int i = 0; i < coroutines.Length; i++)
        {
            yield return coroutines[i];
        }
        yield break;
    }
}

public class NameListOfCommandLinesRegister
{

    private Dictionary<string, NamedListOfCommandLines> m_registered = new Dictionary<string, NamedListOfCommandLines>();

    public bool GetList(string nameId, out NamedListOfCommandLines foundList)
    {
        bool hasList = m_registered.ContainsKey(nameId);
        if (hasList)
            foundList = m_registered[nameId];
        else foundList = null;
        return hasList;
    }

    public void AddList(string nameId, NamedListOfCommandLines listOfCommands)
    {
        if (m_registered.ContainsKey(nameId))
            m_registered[nameId] = listOfCommands;
        else
            m_registered.Add(nameId, listOfCommands);
    }

    public string[] GetNameRegistered()
    {
        return m_registered.Keys.ToArray();
    }

    public bool GetCommandLinesOf(string listNameId, out List<ICommandLine> lines)
    {

        lines = null;
        NamedListOfCommandLines listOfCommands;
        if (GetList(listNameId, out listOfCommands))
        {
            lines = listOfCommands.GetCommands();
            return true;
        }
        return false;
    }
    public bool GetCommandLinesOf(string listNameId, out NamedListOfCommandLines lines)
    {

        lines = null;
        NamedListOfCommandLines listOfCommands;
        if (GetList(listNameId, out listOfCommands))
        {
            lines = listOfCommands;
            return true;
        }
        return false;
    }

    public void Clear()
    {
        m_registered.Clear();
    }

    public int GetCount()
    {
        return m_registered.Keys.Count;
    }

    public NamedListOfCommandLines[] GetNameRegisteredFull()
    {
        return m_registered.Values.ToArray();
    }
}





public class CharListOfCommandLinesRegister
{

    private Dictionary<char, CharToCommands> m_registered = new Dictionary<char, CharToCommands>();

    public bool GetList(char charValue, out CharToCommands foundList)
    {
        bool hasList = m_registered.ContainsKey(charValue);
        if (hasList)
            foundList = m_registered[charValue];
        else foundList = null;
        return hasList;
    }

    public void AddList(char charValue, CharToCommands listOfCommands)
    {
        if (m_registered.ContainsKey(charValue))
            m_registered[charValue] = listOfCommands;
        else
            m_registered.Add(charValue, listOfCommands);
    }

    public char[] GetNameRegistered()
    {
        return m_registered.Keys.ToArray();
    }

    public bool GetCommandLinesOf(char charValue, out List<ICommandLine> lines)
    {

        lines = null;
        CharToCommands listOfCommands;
        if (GetList(charValue, out listOfCommands))
        {
            lines = listOfCommands.GetCommands();
            return true;
        }
        return false;
    }
    public bool GetCommandLinesOf(char charValue, out CharToCommands lines)
    {

        lines = null;
        CharToCommands listOfCommands;
        if (GetList(charValue, out listOfCommands))
        {
            lines = listOfCommands;
            return true;
        }
        return false;
    }

    public void Clear()
    {
        m_registered.Clear();
    }

    public int GetCount()
    {
        return m_registered.Keys.Count;
    }

    public CharToCommands[] GetNameRegisteredFull()
    {
        return m_registered.Values.ToArray();
    }
}






