using System.Collections.Generic;

public class NamedListOfCommandLines : ListOfCommandLines
{
    public NamedListOfCommandLines(string nameId, IEnumerable<string> command) : base(command)
    {
        m_namedId = nameId;
    }
    public NamedListOfCommandLines(string nameId, IEnumerable<ICommandLine> command) : base(command)
    {
        m_namedId = nameId;
    }
    private string m_namedId = "";
    public string GetIdentifiantName() { return m_namedId; }
}