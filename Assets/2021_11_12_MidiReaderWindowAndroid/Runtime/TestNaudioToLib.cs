using NAudio.Midi;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestNaudioToLib : MonoBehaviour
{



    [SerializeField] int mInDeviceIndex = -1;
    [SerializeField] int mOutDeviceIndex = -1;

    [SerializeField] MidiIn mMidiIn = null;

    public List<string> m_inDevices = new List<string>();
    public List<int> m_inDevicesId = new List<int>();
    public List<string> m_outDevices = new List<string>();

    //public void Start()
    //{
    //    RefreshListOfDevices();

    //    if (m_inDevices.Count>0)
    //    {
    //        SubscribeToMidiIn();
    //    }

    //}

    //private void OnDestroy()
    //{
    //    UnsubscriveToMidiIn();
    //}

    //public void OnApplicationQuit()
    //{
    //    UnsubscriveToMidiIn();
    //}



    [ContextMenu("Refresh List of Devices")]
    public void RefreshListOfDevices()
    {

        m_inDevices.Clear();
        for (int device = 0; device < MidiIn.NumberOfDevices; device++)

        {
            string name = MidiIn.DeviceInfo(device).ProductName;
            m_inDevicesId.Add(device);
            m_inDevices.Add(name);


        }

        mInDeviceIndex = m_inDevices.Count;

        m_outDevices.Clear();
        for (int device = 0; device < MidiOut.NumberOfDevices; device++)
        {

            string name = MidiOut.DeviceInfo(device).ProductName;
            m_outDevices.Add(name);

        }

        mOutDeviceIndex = m_outDevices.Count;

    }




    public void SubscribeToMidiIn()
    {
        if (mMidiIn != null)
        {
            mMidiIn.Stop();
            mMidiIn.Dispose();
        }

        mMidiIn = new MidiIn(m_inDevicesId[0]);

        mMidiIn.MessageReceived += midiIn_MessageReceived;
        mMidiIn.ErrorReceived += midiIn_ErrorReceived;
        mMidiIn.Start();
    }

    private void UnsubscriveToMidiIn()
    {
        try
        {
            if (mMidiIn != null)
            {
                mMidiIn.Stop();
                mMidiIn.Dispose();
            }
        }
        catch {  }
    }


    static void midiIn_ErrorReceived(object sender, MidiInMessageEventArgs e)

    {

        Debug.Log(String.Format("Time {0} Message 0x{1:X8} Event {2}",

           e.Timestamp, e.RawMessage, e.MidiEvent));



    }



   
    static void midiIn_MessageReceived(object sender, MidiInMessageEventArgs e)
    {


        MidiEvent me = e.MidiEvent;
        if (me == null)
            return;
       

        if (me is NoteEvent) 
        {
            NoteEvent noteEvt = me as NoteEvent;
            Debug.Log(String.Format("Note {0}, {1}, {2}, {3}, {4}, {5}, {6}",

           noteEvt.NoteName,
           noteEvt.NoteNumber,
           noteEvt.Velocity,
           noteEvt.DeltaTime,
           noteEvt.Channel,
           noteEvt.AbsoluteTime,
           noteEvt.GetAsShortMessage()));
            return;
        }
            else if (me is ControlChangeEvent) 
        {
                    ControlChangeEvent noteEvt = me as ControlChangeEvent;
            Debug.Log(String.Format("Control {0}, {1}, {2}, {3}, {4}, {5}, {6}",

           noteEvt.CommandCode,
           noteEvt.Controller,
           noteEvt.ControllerValue,
           noteEvt.DeltaTime,
           noteEvt.Channel,
           noteEvt.AbsoluteTime,
           noteEvt.GetAsShortMessage()));
            return;
            } else if (me is PatchChangeEvent)
            {

                PatchChangeEvent noteEvt = me as PatchChangeEvent;
                    Debug.Log(String.Format("Patch {0}, {1}, {2}, {3}",

                   noteEvt.DeltaTime,
                   noteEvt.Channel,
                   noteEvt.AbsoluteTime,
                   noteEvt.GetAsShortMessage()));
                    return;
                }



        else {

            Debug.Log(String.Format("Time {0} Message 0x{1:X8} Event {2} Command:{3}",
            e.Timestamp, e.RawMessage, e.MidiEvent, e.MidiEvent.CommandCode));
        }

    }
}



