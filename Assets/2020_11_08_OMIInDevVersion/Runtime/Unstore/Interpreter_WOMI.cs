using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WindowsInput.Native;

public class Interpreter_WOMI : AbstractInterpreterMono
{
    public WindowSimWriter m_keyWriter;

    public override bool CanInterpreterUnderstand(ref ICommandLine command)
    {
        return StartWith(ref command, "winkey:", true);
    }

    public override string GetName()
    {
        return "Window Keyboard Input";
    }

    public override void TranslateToActionsWithStatus(ref ICommandLine command, ref ExecutionStatus succedToExecute)
    {
        string cmd = command.GetLine().Trim();
        string cmdLow = command.GetLine().ToLower();
        string[] token = cmd.Split(':');


        if (token.Length >= 3)
        {
            string toPress = token[2];
            VirtualKeyCode key ;
            bool found;
            ConvertStringToEnum(toPress, out found, out key);
            PressType pressType= PressType.Both;
            if (cmdLow.IndexOf("winkey:press:") == 0 || cmdLow.IndexOf("winkey:p:") == 0)
            {
                pressType = PressType.Down;
            }
            else if (cmdLow.IndexOf("winkey:release:") == 0 || cmdLow.IndexOf("winkey:r:") == 0)
            {
                pressType = PressType.Up;
            }
            else if (cmdLow.IndexOf("winkey:stroke:") == 0 || cmdLow.IndexOf("winkey:s:") == 0)
            {
                pressType = PressType.Both;
            }
            if (found) {
                m_keyWriter.VirtualKeyStroke(key, pressType);
            }
        }
        
        succedToExecute.SetAsFinished(true);
    }

    private void ConvertStringToEnum(string toPress, out bool found, out VirtualKeyCode key)
    {
        toPress=toPress.ToLower().Trim();
        for (int i = 0; i < m_keysLow.Length; i++)
        {
            if (m_keysLow[i].keyAsString == toPress) {
                found = true;
                key = m_keysLow[i].key;
                return;
            }
        }
        found = false;
        key = VirtualKeyCode.NONCONVERT;
    }
    public void Reset()
    {
        m_keys = GetListOfVirtualKey();
        m_keysLow = new KeyLow[m_keys.Length];
        List<string> tmp = new List<string>();
        for (int i = 0; i < m_keys.Length; i++)
        {
            string low = m_keys[i].ToString().ToLower();
            tmp.Add(low);
            m_keysLow[i] = new KeyLow() { keyAsString = low, key = m_keys[i] };
        }
        m_copyPastable = string.Join("\n", tmp);
    }
    [TextArea(0,5)]
    public string m_copyPastable;
    [System.Serializable]
    public class KeyLow {
        public string keyAsString;
        public VirtualKeyCode key;
    }
    public KeyLow[] m_keysLow = new KeyLow[0];
    public VirtualKeyCode[] m_keys= new VirtualKeyCode[0];
    public VirtualKeyCode[] GetListOfVirtualKey() {
      return   Enum.GetValues(typeof(VirtualKeyCode)).Cast<VirtualKeyCode>().ToArray();
    }
    public override void WhatIsYourRequirementFor(ref ICommandLine command, out ICommandExecutioninformation executionInfo)
    {
        executionInfo= new CommandExecutionInformation(false, false, false, false);
    }

    public override string WhatWillYouDoWith(ref ICommandLine command)
    {
        return "";
    }

   
}
