using NAudio.Midi;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class E_MidiUtility
{
    public static void GetMidiOutIdOf(in string midiNameId, out bool found, out int id)
    {

        for (int device = 0; device < MidiOut.NumberOfDevices; device++)
        {
            string name = MidiOut.DeviceInfo(device).ProductName;
            if (name.Length == midiNameId.Length && name.ToLower().IndexOf(midiNameId.ToLower()) == 0)
            {
                found = true;
                id = device;
                return;
            }
        }
        found = false;
        id = -1;
    }

    public static void GetListOfMidiOutDevices(out string[] midiList)
    {
        midiList = new string [MidiOut.NumberOfDevices];
        for (int i = 0; i < MidiOut.NumberOfDevices; i++)
        {
            midiList[i]= MidiOut.DeviceInfo(i).ProductName; 
        }
    }
    public static void GetListOfMidiOutDevices(ref Dropdown midiOutList)
    {
        if (midiOutList == null)
            return;
        GetListOfMidiOutDevices(out string[] devices);
        midiOutList.ClearOptions();
        midiOutList.AddOptions(devices.ToList());

    }

    public static void GetListOfMidiInDevices(out string[] midiList)
    {
        midiList = new string[MidiIn.NumberOfDevices];
        for (int i = 0; i < MidiIn.NumberOfDevices; i++)
        {
            midiList[i] = MidiIn.DeviceInfo(i).ProductName;
        }
    }
    public static void GetListOfMidiInDevices(ref Dropdown midiOutList)
    {
        if (midiOutList == null)
            return;
        GetListOfMidiInDevices(out string[] devices);
        midiOutList.ClearOptions();
        midiOutList.AddOptions(devices.ToList());
    }


    public static void GetMidiInIdOf(in string midiNameId, out bool found, out int id)
    {

        for (int device = 0; device < MidiIn.NumberOfDevices; device++)
        {
            string name = MidiIn.DeviceInfo(device).ProductName;
            if (name.Length == midiNameId.Length && name.ToLower().IndexOf(midiNameId.ToLower()) == 0)
            {
                found = true;
                id = device;
                return;
            }
        }
        found = false;
        id = -1;
    }
}