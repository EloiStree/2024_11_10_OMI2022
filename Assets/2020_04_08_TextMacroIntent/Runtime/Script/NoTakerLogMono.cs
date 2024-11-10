using System.Collections;
using System.Collections.Generic;
using System.Windows.Input;
using UnityEngine;
using UnityEngine.Events;

public class NoTakerLogMono : MonoBehaviour
{

    public CommandLineAsStringUnityEvent m_onParsingFailCommand;
    public ParsingFailUnityEvent         m_onParsingFail;

    public CommandLineAsStringUnityEvent m_onExecutingFailCommand;
    public ExecutingFailUnityEvent       m_onExecutingFail;

    public CommandLineAsStringUnityEvent m_onNoAuctionFailCommand;
    public NoAuctionFailUnityEvent       m_onNoAuctionFail;

    private void OnEnable()
    {
        NoTakerLog.m_auctionFailListener += NotifyAuctionFail;
        NoTakerLog.m_parsingFailListener += NotifyParsingFail;
        NoTakerLog.m_executingFailListener += NotifyExecutingFail;
    }

    private void OnDisable()
    {
        NoTakerLog.m_auctionFailListener -= NotifyAuctionFail;
        NoTakerLog.m_parsingFailListener -= NotifyParsingFail;
        NoTakerLog.m_executingFailListener -= NotifyExecutingFail;
    }

    public void NotifyParsingFail(FailParsingThisCommand fail)
    {
        m_onParsingFailCommand.Invoke(fail.GetCommand().GetLine());
        m_onParsingFail    .Invoke(fail);
    }
    public void NotifyExecutingFail(FailExecutingCommand fail)
    {
        m_onExecutingFailCommand.Invoke(fail.GetCommand().GetLine());
        m_onExecutingFail      .Invoke(fail);

    }
    public void NotifyAuctionFail(NoAuctionForThisCommand noAuction)
    {
        m_onNoAuctionFailCommand.Invoke(noAuction.GetCommand().GetLine());
        m_onNoAuctionFail       .Invoke(noAuction);
      
    }

    [System.Serializable]
    public class CommandLineAsStringUnityEvent : UnityEvent<string> { }
    [System.Serializable]
    public class CommandLineUnityEvent : UnityEvent<ICommand> { }

    [System.Serializable]
    public class ParsingFailUnityEvent : UnityEvent<FailParsingThisCommand> { }
    [System.Serializable]
    public class ExecutingFailUnityEvent : UnityEvent<FailExecutingCommand> { }
    [System.Serializable]
    public class NoAuctionFailUnityEvent : UnityEvent<NoAuctionForThisCommand> { }
}

public class NoTakerLog {


    public static NotifyParsingFailEvent   m_parsingFailListener;
    public static NotifyExecutingFailEvent m_executingFailListener;
    public static NotifyAuctionFailEvent   m_auctionFailListener;

    public void NotifyParsingFail(FailParsingThisCommand fail) {
        if (fail != null)
            m_parsingFailListener(fail);

    }
    public void NotifyExecutingFail(FailExecutingCommand fail)
    {
        if (fail != null)
            m_executingFailListener(fail);
    }
    public void NotifyAuctionFail(NoAuctionForThisCommand noAuction)
    {
        if (noAuction != null)
            m_auctionFailListener(noAuction);
    }

    public delegate void NotifyParsingFailEvent(FailParsingThisCommand fail);
    public delegate void NotifyExecutingFailEvent(FailExecutingCommand fail);
    public delegate void NotifyAuctionFailEvent(NoAuctionForThisCommand noAuction);

}

