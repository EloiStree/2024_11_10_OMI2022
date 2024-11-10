using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldToAction : MonoBehaviour
{
    public InputField m_observed;
    public CommandAuctionExecuter m_auctionExecuter;
    public void Push() => m_auctionExecuter.ExecuteStringCommand(m_observed.text);


}
