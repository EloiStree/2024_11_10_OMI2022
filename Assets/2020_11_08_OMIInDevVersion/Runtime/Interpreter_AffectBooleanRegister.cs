using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class Interpreter_AffectBooleanRegister : AbstractInterpreterMono
{
    public BooleanStateRegisterMono m_booleanRegister;
    public BooleanGroupMono m_booleanGroupMono;
    public override bool CanInterpreterUnderstand(ref ICommandLine command)
    {

        return StartWith(ref command, "bool:", true) || StartWith(ref command, "boolswitch:", true) || StartWith(ref command, "boolgroup:", true);
    }
                  
    public override string GetName()
    {
        return "Affect boolean register";
    }

    public override void TranslateToActionsWithStatus(ref ICommandLine command, ref ExecutionStatus succedToExecute)
    {
        bool succed=false;
        string cmd = command.GetLine().Trim().ToLower();
        string[] tokens = cmd.Split(':');
        if (tokens.Length >=2) {
            string action = tokens[0].Trim(), name = tokens[1].Trim();
            if (!string.IsNullOrWhiteSpace(action) && !string.IsNullOrWhiteSpace(name) )
            {
                //"bool: a🀲 b c d "
                //"bool: a 🀸 b c d "

                if (tokens.Length == 2 && tokens[1].Length>=3)
                {
                   
                    string dominotext = tokens[1];
                    bool falseTrueDominor= Regex.IsMatch(dominotext,".*🀲.*"); ;
                    int dominoIndex = dominotext.IndexOf("🀲");
                    if (dominoIndex > -1)
                    {
                        string left = dominotext.Substring(0, dominoIndex);
                        string right = dominotext.Substring(dominoIndex + 2 );
                        string[] boolLeft = left.Split(' ');
                        string[] boolRight = right.Split(' ');
                        for (int i = 0; i < boolLeft.Length; i++)
                        {
                            if (boolLeft[i].Trim().Length > 0)
                            {
                                m_booleanRegister.Set(boolLeft[i].Trim() , !falseTrueDominor, !falseTrueDominor);
                            }

                        }
                        for (int i = 0; i < boolRight.Length; i++)
                        {
                            if (boolRight[i].Trim().Length > 0)
                            {
                                m_booleanRegister.Set(boolRight[i].Trim(), falseTrueDominor, falseTrueDominor);
                            }

                        }
                    }



                }

                    if (tokens.Length == 3 )
                {
                    string  value = tokens[2].Trim();

                    if (action == "bool" && !string.IsNullOrWhiteSpace(value)) {
                        BooleanStateRegister reg = m_booleanRegister.GetRegister();
                        if (!reg.Contains(name))
                        {
                            reg.Set(name, false);
                        }
                        if (name.Length > 0) {
                            if (value.ToLower().Trim() == "switch")
                                reg.SwitchValue(name);
                            else 
                                reg.Set(name, IsTrue(value.ToLower()));
                        }
                    }
                }
                if (tokens.Length == 2)
                {
                    if (action == "boolswitch")
                    {
                       // Debug.Log("???");
                        BooleanStateRegister reg = m_booleanRegister.GetRegister();
                        if (!reg.Contains(name)) {
                            reg.Set(name, false);
                        }
                        if (name.Length > 0) { 
                            reg.Set(name , ! reg.GetValueOf(name));
                        }
                    }

                }

                if (action == "boolgroup")
                {
                    //boolgroup:false:groupname
                    //boolgroup:true:groupname
                    //boolgroup:switch:groupname
                    //boolgroup:false:groupname:boolname
                    //boolgroup:true:groupname:boolname
                    if (tokens.Length == 3 || tokens.Length==4)
                    {
                        string value = tokens[1].Trim().ToLower();
                        string groupname = tokens[2].Trim().ToLower();
                        string boolname = "";
                        if(tokens.Length>=4)
                           boolname= tokens[3].Trim().ToLower();
                        BoolAction requested = BoolAction.Switch;
                        if (value == "true" || value == "1")
                            requested = BoolAction.True;
                        if (value == "false" || value == "0")
                            requested = BoolAction.False;

                        // Debug.Log("???");
                        BooleanStateRegister reg = m_booleanRegister.GetRegister();
                        
                        if (groupname.Length > 0)
                        {
                            if (boolname.Length == 0)
                            {
                                if (requested == BoolAction.True)
                                {
                                    m_booleanGroupMono.SetAll(true, groupname);
                                }
                               else if (requested == BoolAction.False)
                                {
                                    m_booleanGroupMono.SetAll(false, groupname);
                                }
                               else if (requested == BoolAction.Switch)
                                {
                                    m_booleanGroupMono.SwitchAll( groupname);
                                }
                            }
                            else {
                                if (requested == BoolAction.True)
                                {
                                    m_booleanGroupMono.SelectAs(true, groupname, boolname);
                                }
                                else if (requested == BoolAction.False)
                                {
                                    m_booleanGroupMono.SelectAs(false, groupname, boolname);
                                }
                               
                            }

                        }
                    }

                }
            }
           

            
        }
        



        succedToExecute.SetAsFinished(succed);
    }

    public enum BoolAction { True, False, Switch}
    private bool IsTrue(string value)
    {
        value = value.ToLower();
        return value == "1" || value == "true";
    }

    public override void WhatIsYourRequirementFor(ref ICommandLine command, out ICommandExecutioninformation executionInfo)
    {
        executionInfo = new CommandExecutionInformation(false, false, false, false);
    }

    public override string WhatWillYouDoWith(ref ICommandLine command)
    {
        return "It will affect the boolean register by executing the following command: " + command;
    }
}
