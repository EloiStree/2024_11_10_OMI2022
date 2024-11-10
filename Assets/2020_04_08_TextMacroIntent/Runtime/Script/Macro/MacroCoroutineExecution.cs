using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MacroCoroutineExecution 
{
    public CommandAuctionDistributor m_auction;
    public CommandAuctionCoroutineExecuter m_executer;

    [System.Serializable]
    public class LoopTurnOffButton {
        public LoopTurnOffButton() { }
        [SerializeField] bool m_turnOff=false;
        public void TurnOff() { m_turnOff = true; }
        public bool IsRequestingToTurnOff() { return m_turnOff; }
    }

    [System.Serializable]
    public class RequestNext
    {
        [SerializeField] int m_nextCount;
        public void Next(int count = 1) { m_nextCount += count; }
        public void ResetTokens() { m_nextCount = 0; }

        public bool IsRequestingNext()
        {
            return m_nextCount > 0;
        }
        public void BurnNextToken() {
            if(m_nextCount>0)
            m_nextCount--; 
        }

    
    }

    public IEnumerator ExecuteLoop(LoopTurnOffButton turnOff, List<ICommandLine> commands, float minTimeBetweenLoop=0)
    {
        ExecutionStatus status = null;
      
        while (!IsLoopAskToStop(turnOff) && commands != null && commands.Count > 0)
        {
            for (int i = 0; i < commands.Count; i++)
            {
                if (IsLoopAskToStop(turnOff))
                {
                    yield break;
                }
                else
                {
                    m_executer.TryToExecutre(commands[i], out status);
                    yield return new WaitWhile(() => !status.HasFinish());
                }
            }
            yield return new WaitForEndOfFrame();
            yield return new WaitForSeconds(minTimeBetweenLoop);
        }
        yield break;
    }

    private static bool IsLoopAskToStop(LoopTurnOffButton turnOff)
    {
        return turnOff != null && turnOff.IsRequestingToTurnOff() == true;
    }

    public IEnumerator ExecuteStepByStep(List<ICommandLine> commands, bool reverse = false)
    {
        ExecutionStatus status = null;

        int useIndex;
        for (int i = 0; i < commands.Count; i++)
        {
            useIndex = reverse ? commands.Count - 1 - i : i;
            m_executer.TryToExecutre(commands[useIndex], out status);
            yield return new WaitWhile(() => !status.HasFinish());
        }
    }

    public IEnumerator ExecuteRandomCommands(int iteration, List<ICommandLine> commands)
    {
        if (commands.Count > 0) { 
            ExecutionStatus status = null;
            int useIndex;
            for (int i = 0; i < iteration; i++)
            {
                useIndex = UnityEngine.Random.Range(0, commands.Count);
                m_executer.TryToExecutre(commands[useIndex], out status);
                yield return new WaitWhile(() => !status.HasFinish());
            }
        }
        yield break;
    }

    public IEnumerator ExecuteOneLineAtTime(RequestNext nextRemote, List<ICommandLine> commands, float minTimeBetweenLoop = 0)
    {
        ExecutionStatus status = null;

            for (int i = 0; i < commands.Count; i++)
            {
                yield return new WaitWhile(() => !nextRemote.IsRequestingNext());
                nextRemote.BurnNextToken();

                m_executer.TryToExecutre(commands[i], out status);
                yield return new WaitWhile(() => !status.HasFinish());
            }
        yield break;
    }
}
