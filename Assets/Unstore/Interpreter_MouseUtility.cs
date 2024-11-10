using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Interpreter_MouseUtility : AbstractInterpreterMono
{

    public CommandLineEvent m_relay;
    public CommandAuctionExecuter m_auction;
    public int m_timeBetweenMouseActions = 20;

    public ScreenZoneAndPointRegisterMono m_screenRegister;

    public override bool CanInterpreterUnderstand(ref ICommandLine command)
    {
        return StartWith(ref command, "mouseutility:", true);
    }

    public override string GetName()
    {
        return "Mouse Utility";
    }

    public override void WhatIsYourRequirementFor(ref ICommandLine command, out ICommandExecutioninformation executionInfo)
    {
        executionInfo = new CommandExecutionInformation(false, false, false, false);
    }

    public override string WhatWillYouDoWith(ref ICommandLine command)
    {
        return "";

    }
    public override void TranslateToActionsWithStatus(ref ICommandLine command, ref ExecutionStatus succedToExecute)
    {

        string cmd = command.GetLine().Trim();
        string cmdLow = command.GetLine().ToLower();
        string[] token = cmd.Split(':');

        //mouseutility:click:lrbt:pct:pct
        //mouseutility:click:nameofpoint
        //mouseutility:click:lrbt:pct:pct

        //mouseutility:moveto:lrbt:pct:pct
        //mouseutility:moveto:nameofpoint

        //mouseutility:randomclick:nameofzone
        //mouseutility:randommoveto:nameofzone


        //////DO LATER
        ////mouseutility:gridclicks:nameofzone:counthorizontal:countvertical:timebetweeninMs
        ////mouseutility:gridmoves:nameofzone:counthorizontal:countvertical
        ////When add to jomi
        ////mouseutility:movefromto:lrbt:pct%:pct%:pct%:pct%:time

        //UnityEngine.Debug.Log("DDDiii:"+string.Join("#",token ));
        if (token.Length >= 2)
        {
            string action = token[1].Trim().ToLower();
            bool rightClick = action.IndexOf("rightclick") > -1;
            bool leftClick =!rightClick ;
            
            if (action.IndexOf("click") == 0 || action.IndexOf("leftclick") >= 0 || action.IndexOf("rightclick") >= 0)
            {
                if (token.Length == 3)
                {
                    
                    ScreenPositionInPourcentBean position=null;
                    if (GetPourcent(token[2].Trim().ToLower(),out position)) { 
                    
                        MoveAndClick(position, leftClick, rightClick);
                    }
                }
                if (token.Length == 5) {
                    bool valide;
                    ScreenPositionInPourcentBean position;
                    ConvertStringToPoint(token[2].Trim().ToLower(), token[3].Trim(), token[4].Trim(), out valide, out position);
                    if (valide) {
                        MoveAndClick(position,  leftClick, rightClick);
                    }
                }
            }
            if (action.IndexOf("moveto")==0 )
            {
                //mouseutility:moveto:cornerbotleft
                if (token.Length == 3 )
                {

                    ScreenPositionInPourcentBean position = null;
                    if (GetPourcent(token[2].Trim().ToLower(), out position))
                    {
                        Move(position);
                    }
                }
                if (token.Length == 5)
                {
                    bool valide;
                    ScreenPositionInPourcentBean position;
                    ConvertStringToPoint(token[2].Trim().ToLower(), token[3].Trim(), token[4].Trim(), out valide, out position);

                    if (valide)
                    {
                        Move(position);
                    }
                }

            }
            if (action.IndexOf("randomclick")== 0 || action.IndexOf("randomleftclick") >= 0 || action.IndexOf("randomrightclick") >= 0)
            {
                if (token.Length == 3)
                {

                    ScreenPositionInPourcentBean pct=null;
                    ScreenZoneInPourcentBean zone = null;
                    if (GetPourcent(token[2].Trim().ToLower(), out zone))
                    {
                        pct = zone.GetSquareRandom();
                        MoveAndClick(pct, leftClick,rightClick);
                    }
                }
            }
            if (action.IndexOf("randommoveto")==0)
            {
                if (token.Length == 3)
                {

                    ScreenPositionInPourcentBean pct = null;
                    ScreenZoneInPourcentBean zone = null;
                    if (GetPourcent(token[2].Trim().ToLower(), out zone))
                    {
                        pct = zone.GetSquareRandom();
                        Move(pct);
                    }
                }
            }


            ////mouseutility:gridclicks:nameofzone:counthorizontal:countvertical:timebetweeninMs
            if (action == "gridclick" || action == "gridrightclick" || action == "gridleftclick" || action == "gridmove")
            {
                bool useClick = action.IndexOf("click") > -1;

                if (token.Length == 6)
                {
                    int column=1, line=1;
                    int msBetween=50;
                    if (int.TryParse(token[3], out column) && int.TryParse(token[4], out line) && int.TryParse(token[5], out msBetween)) {
                        
                        ScreenZoneInPourcentBean position = null;
                        if (GetPourcent(token[2].Trim().ToLower(), out position))
                        {
                            float timeInMs = 0;
                            List<string> cmds = new List<string>();
                            foreach (ScreenPositionInPourcentBean pct in position.GetGrid(column, line, true) ) {

                                cmds.Add("In " + timeInMs + "|" + GetMoveCommand(pct));
                                timeInMs += msBetween;
                                if (useClick) { 
                                    cmds.Add("In "+timeInMs+"|"+GetClickCommand(leftClick, rightClick,0));
                                    timeInMs += msBetween;
                                }

                            }
                            m_auction.Execute(cmds);
                        }
                    }
                }
            }


        }


        succedToExecute.SetAsFinished(true);
    }

   

    public bool GetPourcent(string name, out ScreenPositionInPourcentBean position)
    {
        bool isFOund=false;
        NamedScreenPourcentPosition np;
        m_screenRegister.Get(name, out isFOund, out np);
        if (isFOund)
            position = np.m_position;
        else position = null;
        return isFOund;
    }
    public bool GetPourcent(string name, out ScreenZoneInPourcentBean position)
    {
        bool isFOund = false;
        NamedScreenPourcentZone np;
        m_screenRegister.Get(name, out isFOund, out np);
        if (isFOund)
            position = np.m_zone;
        else position = null;
        return isFOund;
    }

    private void Click( bool leftClick, bool rightClick, float timeDelay=0)
    {
        m_relay.Invoke(new CommandLine(GetClickCommand(leftClick, rightClick, timeDelay)));  
    }
    private void Move(ScreenPositionInPourcentBean pct)
    {
        m_relay.Invoke(new CommandLine(GetMoveCommand(pct)));
        
    }
    private string GetClickCommand(bool leftClick, bool rightClick, float timeDelayInMs = 0)
    {
        return (timeDelayInMs > 0f ? "In " + timeDelayInMs + "|" : "") + "jomiraw:ms:" + (leftClick ? "l" : "r");
    }

    private string GetMoveCommand(ScreenPositionInPourcentBean pct)
    {
        return string.Format("jomiraw:mm:{0}%:{1}%", pct.GetLeftToRightValue(), 1f - pct.GetBotToTopValue());
    }

    private void MoveAndClick(ScreenPositionInPourcentBean pct, bool leftClick, bool rightClick)
    {
        Move(pct);
        Click(leftClick, rightClick, m_timeBetweenMouseActions);
    }

    private void GetDirection(string directionvalue, out bool dfound, out DirectionOfScreen direction)
    {
        direction = DirectionOfScreen.LRBT;
        dfound = true;
        directionvalue = directionvalue.ToLower().Trim();
        if (directionvalue == "lrbt") direction = DirectionOfScreen.LRBT;
        else if (directionvalue == "rlbt") direction = DirectionOfScreen.RLBT;
        else if (directionvalue == "lrtb") direction = DirectionOfScreen.LRTB;
        else if (directionvalue == "rltb") direction = DirectionOfScreen.RLTB;
        else dfound = false;

    }

    public void ConvertStringToPoint(string screenDirection, string leftToRight, string botToTop,out bool convertable, out ScreenPositionInPourcentBean position) {

        position = null;
        convertable = false;
        bool directionfound;
        DirectionOfScreen orientation;
        GetDirection(screenDirection, out directionfound, out orientation);
        float lr = 0;
        float bt = 0;
        if (directionfound && float.TryParse(leftToRight, out lr) && float.TryParse(botToTop, out bt)) {

           

            if (orientation == DirectionOfScreen.LRTB)
            {
                bt = 1f - bt;
            }
            else if (orientation == DirectionOfScreen.RLBT)
            {
                lr = 1f - lr;
            }
            else if (orientation == DirectionOfScreen.RLTB)
            {
                lr = 1f - lr;
                bt = 1f - bt;
            }

            position = new ScreenPositionInPourcentBean(lr, bt);
            convertable = true;
        }
    }


    public enum DirectionOfScreen { LRBT, RLBT, LRTB, RLTB }
}
