using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimeTheadFrequenceMono : MonoBehaviour
{
    public TimeThreadMono m_timeThread;
    public Channel m_60FPS       = new Channel(16);
    public Channel m_30FPS       = new Channel(33);
    public Channel m_20FPS       = new Channel(50);
    public Channel m_10FPS       = new Channel(100);
    public Channel m_5FPS        = new Channel(200);
    public Channel m_4FPS        = new Channel(250);
    public Channel m_2FPS       = new Channel(500);
    public Channel m_1Second    = new Channel(1.0);
    public Channel m_5Second    = new Channel(5.0);
    public Channel m_10Second    = new Channel(10.0);
    public Channel m_20Second    = new Channel(20.0);
    public Channel m_30Second   = new Channel(30.0);
    public Channel m_60Second   = new Channel(60.0);
    public Channel m_300Second  = new Channel(300.0);
    public Channel m_600Second  = new Channel(600.0);

    public enum ChannelRange { FPS60 , FPS30, FPS20, FPS10, FPS5, FPS4, FPS2, S1, S5, S10, S20, S30, S60, M5, M10 }


    private void Awake()
    {

     AddChannelAsLoop(m_60FPS       );
     AddChannelAsLoop(m_30FPS       );
     AddChannelAsLoop(m_20FPS       );
     AddChannelAsLoop(m_10FPS       );
     AddChannelAsLoop(m_5FPS        );
     AddChannelAsLoop(m_4FPS        );
     AddChannelAsLoop(m_2FPS        );
     AddChannelAsLoop(m_1Second     );
     AddChannelAsLoop(m_5Second     );
     AddChannelAsLoop(m_10Second    );
     AddChannelAsLoop(m_20Second    );
     AddChannelAsLoop(m_30Second    );
     AddChannelAsLoop(m_60Second    );
     AddChannelAsLoop(m_300Second   );
     AddChannelAsLoop(m_600Second   );

    }

    private void AddChannelAsLoop(Channel channel)
    {
        m_timeThread.SubscribeLoop(
            new TimeThreadMono.LoopPing(channel.m_milliseconds, PingThreadType.InUnityThread)
            {
                m_callBackInThread = channel.PingListenerInUnityThread,
            }); 
        m_timeThread.SubscribeLoop(
             new TimeThreadMono.LoopPing(channel.m_milliseconds, PingThreadType.InTimeThread)
             {
                 m_callBackInThread = channel.PingListenerInTimeThread,
             });

    }



    public void SubscribeToChannel(ChannelRange channel, Callback action, bool useUnityThread)
    {
        Channel selection = null;
        GetChannelRef(channel, out selection);
        if (selection != null)
            if (useUnityThread)
            {
                selection.m_callbacksInUnity += action;
            }
            else { 
                selection.m_callbacksInThread += action;
            }
    }
   
    public void UnsubscribeToChannel(ChannelRange channel, Callback action, bool useUnityThread)
    {
        Channel selection = null;
        GetChannelRef(channel, out selection);
        if (selection != null)
            if (useUnityThread)
            {
                selection.m_callbacksInUnity -= action;
            }
            else
            {
                selection.m_callbacksInThread -= action;
            }
    }
   
    public void GetChannelRef(ChannelRange channel , out Channel selectedChannel) {
        selectedChannel = null;
        switch (channel)
        {
            case ChannelRange.FPS60:
                selectedChannel = m_60FPS;
                break;
            case ChannelRange.FPS30:
                selectedChannel = m_30FPS;
                break;
            case ChannelRange.FPS20:
                selectedChannel = m_20FPS;
                break;
            case ChannelRange.FPS10:
                selectedChannel = m_10FPS;
                break;
            case ChannelRange.FPS5:
                selectedChannel = m_5FPS;
                break;
            case ChannelRange.FPS4:
                selectedChannel = m_4FPS;
                break;
            case ChannelRange.FPS2:
                selectedChannel = m_2FPS;
                break;
            case ChannelRange.S1:
                selectedChannel = m_1Second;
                break;
            case ChannelRange.S5:
                selectedChannel = m_5Second;
                break;
            case ChannelRange.S10:
                selectedChannel = m_10Second;
                break;
            case ChannelRange.S20:
                selectedChannel = m_20Second;
                break;
            case ChannelRange.S30:
                selectedChannel = m_30Second;
                break;
            case ChannelRange.S60:
                selectedChannel = m_60Second;
                break;
            case ChannelRange.M5:
                selectedChannel = m_300Second;
                break;
            case ChannelRange.M10:
                selectedChannel = m_600Second;
                break;
            default:
                break;
        }
    }


    public delegate void Callback();

    [System.Serializable]
    public class Channel
    {
        public Callback m_callbacksInThread;
        public Callback m_callbacksInUnity;
        public UnityEvent m_unityEvent= new UnityEvent();
        public uint m_milliseconds;

        public Channel(uint ms)
        {
            this.m_milliseconds = ms;
        }
        public Channel(double seconds)
        {
            this.m_milliseconds = (uint) seconds*1000;
        }
        public void PingListenerInTimeThread()
        {

            if (m_callbacksInThread != null)
                m_callbacksInThread();

        }
        public void PingListenerInUnityThread() {

            if (m_callbacksInUnity != null)
                m_callbacksInUnity();
            if(m_unityEvent!=null)
                m_unityEvent.Invoke();
        }
    }
}
