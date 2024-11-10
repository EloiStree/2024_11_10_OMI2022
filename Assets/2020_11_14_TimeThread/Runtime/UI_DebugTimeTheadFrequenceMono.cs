using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_DebugTimeTheadFrequenceMono : MonoBehaviour
{
    public TimeTheadFrequenceMono m_frequence;
    public UI_SwitchImageColor m_60FPS     ;
    public UI_SwitchImageColor m_30FPS      ;
    public UI_SwitchImageColor m_20FPS     ;
    public UI_SwitchImageColor m_10FPS     ;
    public UI_SwitchImageColor m_5FPS      ;
    public UI_SwitchImageColor m_4FPS      ;
    public UI_SwitchImageColor m_2FPS      ;
    public UI_SwitchImageColor m_1Second   ;
    public UI_SwitchImageColor m_5Second   ;
    public UI_SwitchImageColor m_10Second  ;
    public UI_SwitchImageColor m_20Second  ;
    public UI_SwitchImageColor m_30Second  ;
    public UI_SwitchImageColor m_60Second  ;
    public UI_SwitchImageColor m_300Second ;
    public UI_SwitchImageColor m_600Second;


    private void Start()
    {
        m_frequence.SubscribeToChannel(TimeTheadFrequenceMono.ChannelRange.FPS60, m_60FPS.Switch, true);
        m_frequence.SubscribeToChannel(TimeTheadFrequenceMono.ChannelRange.FPS30, m_30FPS.Switch, true);
        m_frequence.SubscribeToChannel(TimeTheadFrequenceMono.ChannelRange.FPS20, m_20FPS    .Switch, true);
        m_frequence.SubscribeToChannel(TimeTheadFrequenceMono.ChannelRange.FPS10, m_10FPS    .Switch, true);
        m_frequence.SubscribeToChannel(TimeTheadFrequenceMono.ChannelRange.FPS5, m_5FPS     .Switch, true);
        m_frequence.SubscribeToChannel(TimeTheadFrequenceMono.ChannelRange.FPS4, m_4FPS     .Switch, true);
        m_frequence.SubscribeToChannel(TimeTheadFrequenceMono.ChannelRange.FPS2, m_2FPS     .Switch, true);
        m_frequence.SubscribeToChannel(TimeTheadFrequenceMono.ChannelRange.S1, m_1Second  .Switch, true);
        m_frequence.SubscribeToChannel(TimeTheadFrequenceMono.ChannelRange.S5, m_5Second  .Switch, true);
        m_frequence.SubscribeToChannel(TimeTheadFrequenceMono.ChannelRange.S10, m_10Second .Switch, true);
        m_frequence.SubscribeToChannel(TimeTheadFrequenceMono.ChannelRange.S20, m_20Second .Switch, true);
        m_frequence.SubscribeToChannel(TimeTheadFrequenceMono.ChannelRange.S30, m_30Second .Switch, true);
        m_frequence.SubscribeToChannel(TimeTheadFrequenceMono.ChannelRange.S60, m_60Second .Switch, true);
        m_frequence.SubscribeToChannel(TimeTheadFrequenceMono.ChannelRange.M5, m_300Second.Switch, true);
        m_frequence.SubscribeToChannel(TimeTheadFrequenceMono.ChannelRange.M10, m_600Second.Switch, true);

    }
}