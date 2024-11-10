public class ExecutionStatus
{
    public bool m_finishExecuting = false;
    public bool m_succedToExecute = false;
    public string m_errorInformation = null;

    public void SetAsFinished(bool succed) { m_finishExecuting = true; m_succedToExecute = succed; }
    public void StopWithError(string errorDescription = "") { m_errorInformation = errorDescription; SetAsFinished(false); }
    public bool HasFinish() { return m_finishExecuting; }
    public bool HasSucced() { return m_succedToExecute; }
    public bool HadError(out string error) { error = m_errorInformation; return error != null; }
    public bool HadError() { return m_errorInformation != null; }

    public void Reset()
    {
        m_succedToExecute = false;
        m_errorInformation = null;
        m_finishExecuting = false;
    }

}
