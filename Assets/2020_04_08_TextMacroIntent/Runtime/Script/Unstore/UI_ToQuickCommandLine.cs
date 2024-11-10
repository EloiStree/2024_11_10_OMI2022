using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ToQuickCommandLine : MonoBehaviour
{
    public CommandAuctionExecuter m_executer;
    public InputField m_input;
    public void TryToExecute() {
        m_executer.ExecuteStringCommand(m_input.text);
    }
}
