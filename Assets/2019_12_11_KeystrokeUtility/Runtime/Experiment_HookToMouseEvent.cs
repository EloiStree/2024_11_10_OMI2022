using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using static InterceptMouse;

public class Experiment_HookToMouseEvent : MonoBehaviour
{
    public static Queue<ToProcess> toProcess = new Queue<ToProcess>();
    void Awake()
    {
        Humm();
        //Humm(toProcess);
    }
    void OnDestroy() {
        InterceptMouse.m_wantThreadAlive = false;
        InterceptMouse.StopApp();
    }
    public ulong m_mouseTick;
    public ulong m_count;
    private void Update()
    {
        m_mouseTick = InterceptMouse.m_mouseTickEvent;
        m_count = (ulong) toProcess.Count;
    }

    public void Humm() {
        Task.Factory.StartNew(new Action(() =>
        {
            InterceptMouse.toProcess = toProcess;
            InterceptMouse.m_wantThreadAlive = true;
            InterceptMouse.StartApp();

            while (true)
            {
                Thread.Sleep(200);
              }
        }));
    }


}