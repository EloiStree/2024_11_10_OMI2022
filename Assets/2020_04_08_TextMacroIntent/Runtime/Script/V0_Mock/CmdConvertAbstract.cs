using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICmdConverter {
    bool CanTakeResponsability(string command);
    void DoTheThing(string command, SuccessChecker hasBeenConverted, FinishChecker hasFinish);
}
public abstract class CmdConvertAbstract : MonoBehaviour, ICmdConverter
{

    public abstract bool CanTakeResponsability(string command);
    public abstract void DoTheThing(string command, SuccessChecker hasBeenConverted, FinishChecker hasFinish);

}
public class SuccessChecker {

    public enum State {Processing, Done, Fail}
    public State m_state= State.Processing;
    public string m_errorMessage="";
    public void SetState(State state) { m_state = state; }
    public void SetAsFail() { m_state = State.Fail; }
    public void SetAsFail(string messageError) {
        SetErrorMessage(messageError);
        SetAsFail();
    }
    public void SetAsDone() { m_state = State.Done; }
    public void SetErrorMessage(string info) { m_errorMessage = info; }
    public State GetState() { return m_state; }
    public string GetErroMessage() { return m_errorMessage; }
}