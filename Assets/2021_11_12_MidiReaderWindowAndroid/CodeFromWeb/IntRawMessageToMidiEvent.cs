using NAudio.Midi;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntRawMessageToMidiEvent : MonoBehaviour
{
    /// <summary>
    /// Creates a MidiEvent from a raw message received using
    /// the MME MIDI In APIs
    /// </summary>
    /// <param name="rawMessage">The short MIDI message</param>
    /// <returns>A new MIDI Event</returns>
    public static MidiEvent FromRawMessage(int rawMessage)
    {
    // Source https://csharp.hotexamples.com/fr/examples/NAudio.Midi/ControlChangeEvent/-/php-controlchangeevent-class-examples.html
        long absoluteTime = 0;
        int b = rawMessage & 0xFF;
        int data1 = (rawMessage >> 8) & 0xFF;
        int data2 = (rawMessage >> 16) & 0xFF;
        MidiCommandCode commandCode;
        int channel = 1;

        if ((b & 0xF0) == 0xF0)
        {
            // both bytes are used for command code in this case
            commandCode = (MidiCommandCode)b;
        }
        else
        {
            commandCode = (MidiCommandCode)(b & 0xF0);
            channel = (b & 0x0F) + 1;
        }

        MidiEvent me;
        switch (commandCode)
        {
            case MidiCommandCode.NoteOn:
            case MidiCommandCode.NoteOff:
            case MidiCommandCode.KeyAfterTouch:
                if (data2 > 0 && commandCode == MidiCommandCode.NoteOn)
                {
                    me = new NoteOnEvent(absoluteTime, channel, data1, data2, 0);
                }
                else
                {
                    me = new NoteEvent(absoluteTime, channel, commandCode, data1, data2);
                }
                break;
            case MidiCommandCode.ControlChange:
                me = new ControlChangeEvent(absoluteTime, channel, (MidiController)data1, data2);
                break;
            case MidiCommandCode.PatchChange:
                me = new PatchChangeEvent(absoluteTime, channel, data1);
                break;
            case MidiCommandCode.ChannelAfterTouch:
                me = new ChannelAfterTouchEvent(absoluteTime, channel, data1);
                break;
            case MidiCommandCode.PitchWheelChange:
                me = new PitchWheelChangeEvent(absoluteTime, channel, data1 + (data2 << 7));
                break;
            case MidiCommandCode.TimingClock:
            case MidiCommandCode.StartSequence:
            case MidiCommandCode.ContinueSequence:
            case MidiCommandCode.StopSequence:
            case MidiCommandCode.AutoSensing:
                me = new MidiEvent(absoluteTime, channel, commandCode);
                break;
            case MidiCommandCode.MetaEvent:
            case MidiCommandCode.Sysex:
            default:
                throw new FormatException(String.Format("Unsupported MIDI Command Code for Raw Message {0}", commandCode));
        }
        return me;

    }
}
