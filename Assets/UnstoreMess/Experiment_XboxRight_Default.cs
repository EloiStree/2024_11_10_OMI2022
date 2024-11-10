using UnityEngine;

public class Experiment_XboxRight_Default : MonoBehaviour
{

    public UDPThreadSender m_xomi;
    public void SetJoystick(float horizontal, float vertical) {
        XOMIUtility.Text_RightJoystick(in horizontal, in vertical, out string msg, 0);
        m_xomi.AddMessageToSendToAll(msg);
        m_xomi.SendAllAsSoonAsPossible();
    }
    public void StopJoystick() {
        SetJoystick(0, 0);
    }

    private void Reset()
    {
        m_xomi=  Eloi.E_SearchInSceneUtility.FindObjectOfType<UDPThreadSender>(true);
    }
}


public class XOMIUtility {


    public static void Text_RightJoystick(in float percentHorizontal, in float percentVertical, out string toSend, int millisecondsDelay = 0)
    {
        toSend = string.Format("tms:{2}:🎮r:{0}:{1}", percentHorizontal, percentVertical, millisecondsDelay);

    }
    public static void Text_RightJoystick(in float percentHorizontal, in float percentVertical, out string toSend)
    {
        toSend = string.Format(" 🎮r:{0}:{1} ", percentHorizontal, percentVertical);

    }
    public static void Text_LeftJoystick(in float percentHorizontal, in float percentVertical, out string toSend)
    {
        toSend = string.Format(" 🎮l:{0}:{1} ", percentHorizontal, percentVertical);
    }

    public static void Text_RightTrigger(in float percentValue, out string toSend)
    {
        toSend = string.Format(" 🎮r:{0} ", percentValue);

    }
    public static void Text_LeftTrigger(in float percentValue, out string toSend)
    {
        toSend = string.Format(" 🎮l:{0} ", percentValue);
    }
    
}