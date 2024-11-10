using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortcutsInterpreterAuctionMono : MonoBehaviour
{

    public ShortcutsGroupEvent m_groupReceived;
    public ShortcutTextEvent m_shortcutSoloReceived;

    public void PushToAuctionAllGroupAsItem( ShortcutsGroup group)
    {
        PushToAuctionAllGroupAsItem(group);

    }
    public void PushToAuctionAllGroupAsItem( IEnumerable<ShortcutAsText> group)
    {
        foreach (ShortcutAsText item in group)
        {
            PushToAuction(item);
        }
    }
    public void PushToAuctionAsGroupSensitive( ShortcutsGroup group) {
        m_groupReceived.Invoke(group);

    }
    public void PushToAuction( ShortcutAsText shortcut)
    {
        m_shortcutSoloReceived.Invoke(shortcut);

    }
}


