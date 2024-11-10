using System.Collections.Generic;
using System.Text;

[System.Serializable]
public class TimedCommandLines
{
    public TimedCommandLines(string name, string callId)
    {
        m_name = name;
        m_callId = callId;
    }
    public TimedCommandLines(string name) : this(name, name)
    { }

    public TimedCommandLines()
    {
    }

    public string m_name;
    public string m_callId;
    public List<Item> m_toSend = new List<Item>();

    [System.Serializable]
    public class Item
    {
        public string m_commandLine;
        public ulong m_timeInMs;

        public Item(ulong milliseconds, string rawJomiToSend)
        {
            m_commandLine = rawJomiToSend;
            m_timeInMs = milliseconds;
        }
    }

    public void AddKey(ulong milliseconds, string rawJomiToSend)
    {
        m_toSend.Add(new Item(milliseconds, rawJomiToSend)); ;

    }

    public static string ConvertToFile(TimedCommandLines macro)
    {
        StringBuilder lines = new StringBuilder();
        foreach (var item in macro.m_toSend)
        {
            lines.Append(string.Format("{0}♦{1}\n", item.m_timeInMs, item.m_commandLine));
        }
        return string.Format("☗name {0}\n☗callid {1}\n☗description {2}\n{3}", macro.m_name, macro.m_callId, "", lines.ToString());
    }



}
