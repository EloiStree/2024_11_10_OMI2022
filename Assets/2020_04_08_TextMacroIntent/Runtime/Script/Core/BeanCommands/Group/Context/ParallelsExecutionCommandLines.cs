using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;


/// <summary>
/// Just execute them all at once.
/// </summary>
public class ParallelsExecutionCommandLines : IGroupOfChonogicalCommandLine
{
    private GroupOfCommandLines m_toExecute;
    private ThreadTriggerType m_orderChoose;
    public ParallelsExecutionCommandLines(IEnumerable<ICommandLine> toExecute, ThreadTriggerType startToEnd)
    {
        this.m_toExecute = new GroupOfCommandLines(toExecute);  
        this.m_orderChoose = startToEnd;
    }
    public ParallelsExecutionCommandLines(GroupOfCommandLines toExecute, ThreadTriggerType startToEnd)
    {
        this.m_toExecute = toExecute;
        this.m_orderChoose = startToEnd;
    }

    public enum ThreadTriggerType {  StartToEnd, EndToStart, Randomly}

    public List<ICommandLine> GetCommandLines() {
        return GetCommandLines(m_orderChoose);
    }
    public List<ICommandLine> GetCommandLines(ThreadTriggerType priority) {
        if (priority == ThreadTriggerType.StartToEnd)
            return m_toExecute.GetLines().ToList();
        if (priority == ThreadTriggerType.EndToStart)
            return m_toExecute.GetLines().Reverse().ToList();
        return Shuffle<ICommandLine>(m_toExecute.GetLines().ToList());

    }
    // SOURCE: https://stackoverflow.com/questions/273313/randomize-a-listt
    public static List<T> Shuffle<T>(List<T> list)
    {
        RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
        int n = list.Count;
        while (n > 1)
        {
            byte[] box = new byte[1];
            do provider.GetBytes(box);
            while (!(box[0] < n * (Byte.MaxValue / n)));
            int k = (box[0] % n);
            n--;
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
        return list;
    }

    public ICommandLine[] GetLines()
    {
        return m_toExecute.GetLines();
    }
}
