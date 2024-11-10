using NAudio.Midi;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MidiHelperMono : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
}

namespace MidiService
{
    public class MidiHelper
    {
        public enum MidiEventType
        {
            NoteOn,
            AfterTouchKey,
            Pitch,
            ControlChange,
            PatchChange
        }

        /// <summary>
        /// Get the name list of enuerated midi input devices. 
        /// </summary>
        /// <param name="items">The collection for storing the devices'name</param>
        /// <returns>return -1 as index when nothing is found.</returns>
        public static int EnumerateInputDevices(List<string> items)
        {
            for (int device = 0; device < MidiIn.NumberOfDevices; device++)
            {
                items.Add(MidiIn.DeviceInfo(device).ProductName);
            }
            if (items.Count > 0)
            {
                return 0;
            }
            return -1;
        }
        /// <summary>
        /// Get the name list of enuerated midi output devices. 
        /// </summary>
        /// <param name="items">The collection for storing the devices'name</param>
        /// <returns>return -1 as index when nothing is found.</returns>
        public static int EnumerateOutputDevices(List<string> items)
        {
            for (int device = 0; device < MidiOut.NumberOfDevices; device++)
            {
                items.Add(MidiOut.DeviceInfo(device).ProductName);
            }
            if (items.Count > 0)
            {
                return 0;
            }
            return -1;
        }

        /// <summary>
        /// Send a event over midi output.
        /// </summary>
        /// <param name="device_index">The index of the selected output device</param>
        /// <param name="channel">The desired midi channel</param>
        /// <param name="note">The desired midi note to play</param>
        /// <param name="velocity">The current note velocity. (Default 100)</param>
        /// <param name="duration">The current note duration. (Default 50)</param>
        /// <param name="abstime"></param>
        public void SendMidiEvent(MidiEventType eventType, int device_index, byte channel, byte note, byte velocity = 100, byte duration = 50, int abstime = 0, int aftertouchpressure = 0, int pitch = 8192)
        {
            using (MidiOut midiOut = new MidiOut(device_index))
            {
                switch (eventType)
                {
                    case MidiEventType.NoteOn:
                        NoteOnEvent noteOnEvent = new NoteOnEvent(abstime, channel, note, velocity, duration);
                        midiOut.Send(noteOnEvent.GetAsShortMessage());
                        break;
                    case MidiEventType.AfterTouchKey:
                        ChannelAfterTouchEvent aftertouchEvent = new ChannelAfterTouchEvent(abstime, channel, aftertouchpressure);
                        midiOut.Send(aftertouchEvent.GetAsShortMessage());
                        break;
                    case MidiEventType.Pitch:
                        PitchWheelChangeEvent pitchwheelEvent = new PitchWheelChangeEvent(abstime, channel, pitch);
                        midiOut.Send(pitchwheelEvent.GetAsShortMessage());
                        break;
                }

            }
        }

        public MidiEvent CreateNoteOnEvent(long abstime, byte channel, byte note, byte velocity = 100, byte duration = 50)
        {
            return new NoteOnEvent(abstime, channel, note, velocity, duration);
        }

        public MidiEvent CreateAfterTouchEvent(long abstime, byte channel, int aftertouchpressure = 0)
        {
            return new ChannelAfterTouchEvent(abstime, channel, aftertouchpressure);
        }

        public MidiEvent CreatePitchEvent(long abstime, byte channel, int pitch = 8192)
        {
            return new PitchWheelChangeEvent(abstime, channel, pitch);
        }

        public MidiEvent CreatePatchChangeEvent(long abstime, byte channel, int patchnumber)
        {
            return new PatchChangeEvent(abstime, channel, patchnumber);
        }

        public MidiEvent CreateControlChangeEvent(BinaryReader br)
        {
            return new ControlChangeEvent(br);
        }

        public MidiEvent CreateSysexEvent(BinaryReader br)
        {
            return new SysexEvent();
        }
    }
}
