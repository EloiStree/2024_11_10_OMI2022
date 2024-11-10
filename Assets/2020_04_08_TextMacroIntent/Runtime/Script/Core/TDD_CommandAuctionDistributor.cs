using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDD_CommandAuctionDistributor : MonoBehaviour
{
    public CommandAuctionDistributor m_auction;
    public CommandAuctionCoroutineExecuter m_executer;
    public string [] m_commands;
    

    IEnumerator Start()
    {
        ExecutionStatus status = null;
        foreach (string cmd in m_commands)
        {
            CommandLine cl = new CommandLine(cmd);
            m_executer.TryToExecutre(cl, out  status);
            yield return new WaitWhile(() => !status.HasFinish());
        }
    }
}
