using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileImport_MidiToBoolean : MonoBehaviour
{

    public MidiToBooleanMono m_register;
    public void ClearRegister()
    {

        m_register.Clear();
    }

    public void Load(params string[] filePath)
    {
        for (int i = 0; i < filePath.Length; i++)
        {
            if (File.Exists(filePath[i]))
            {
                LoadMidiToBoolean(File.ReadAllText(filePath[i]), m_register);
            }
        }
    }

    private void LoadMidiToBoolean(string textToLoad, MidiToBooleanMono register)
    {


        string[] lines = textToLoad.Split('\n');
        TileLine tokens;
        for (int i = 0; i < lines.Length; i++)
        {
            char[] l = lines[i].Trim().ToCharArray();
            if (l.Length > 0 && (l[0] == '#' || (l[0] == '/' && l[1] == '/')))
                continue;
            bool useTrim=true, useIgnoreCase=true;
            tokens = new TileLine(lines[i]);
            //MPK Mini Play♦Shorten♦6321♦FootSustain♦SetFalse
            if (tokens.GetCount() == 5 && Eloi.E_StringUtility.AreEquals("Shorten", tokens.GetValue(1), in useTrim, in useIgnoreCase))
            {

                int shortenToDetect = 0;
                if (int.TryParse(tokens.GetValue(2), out shortenToDetect))
                {
                    string deviceName = tokens.GetValue(0).Trim();
                    string booleanName = tokens.GetValue(3).Trim();
                    string setType = tokens.GetValue(4).Trim();
                    MidiToBooleanObserved.SetBooleanType t = Eloi.E_StringUtility.AreEquals("SetTrue", setType, true, true)
                        ? MidiToBooleanObserved.SetBooleanType.SetTrue : MidiToBooleanObserved.SetBooleanType.SetFalse;
                    MidiToBooleanObserved.ShortenId value =
                        new MidiToBooleanObserved.ShortenId(deviceName, shortenToDetect, booleanName, t);

                    m_register.Append(value);
                }
            }




            //MPK Mini Play♦CC♦5♦0.1♦0.4♦AttackMode♦SetTrue
            if (tokens.GetCount() == 7 && Eloi.E_StringUtility.AreEquals("cc", tokens.GetValue(1), in useTrim, in useIgnoreCase))
            {


                string deviceName = tokens.GetValue(0).Trim();
                string controlName = tokens.GetValue(2).Trim();
                string booleanName = tokens.GetValue(5).Trim();
                    string setType = tokens.GetValue(6).Trim();
                    float.TryParse(tokens.GetValue(3).Trim(), out float min);
                    float.TryParse(tokens.GetValue(4).Trim(), out float max);
                    MidiToBooleanObserved.SetBooleanType t = Eloi.E_StringUtility.AreEquals("SetTrue", setType, true, true)
                        ? MidiToBooleanObserved.SetBooleanType.SetTrue : MidiToBooleanObserved.SetBooleanType.SetFalse;
                    MidiToBooleanObserved.ControlCommand value =
                        new MidiToBooleanObserved.ControlCommand(deviceName,controlName, min, max, booleanName, t);

                    m_register.Append(value);
                
            }
            //
            //MPK Mini Play♦NoteVelocity♦A#2♦DoubleJump♦0.9♦1♦SetTrue
            if (tokens.GetCount() == 7 && Eloi.E_StringUtility.AreEquals("notevelocity", tokens.GetValue(1), in useTrim, in useIgnoreCase))
            {

                string deviceName = tokens.GetValue(0).Trim();
                string controlName = tokens.GetValue(2).Trim();
                string booleanName = tokens.GetValue(5).Trim();
                string setType = tokens.GetValue(6).Trim();
                float.TryParse(tokens.GetValue(3).Trim(), out float min);
                float.TryParse(tokens.GetValue(4).Trim(), out float max);
                MidiToBooleanObserved.SetBooleanType t = Eloi.E_StringUtility.AreEquals("SetTrue", setType, true, true)
                    ? MidiToBooleanObserved.SetBooleanType.SetTrue : MidiToBooleanObserved.SetBooleanType.SetFalse;
                MidiToBooleanObserved.NoteVelocityNoChannel value =
                    new MidiToBooleanObserved.NoteVelocityNoChannel(deviceName, controlName, min, max, booleanName, t);

                m_register.Append(value);
                
            }
            if (tokens.GetCount() == 5 && Eloi.E_StringUtility.AreEquals("note", tokens.GetValue(1), in useTrim, in useIgnoreCase))
            {

                string deviceName = tokens.GetValue(0).Trim();
                string noteName = tokens.GetValue(2).Trim();
                string booleanName = tokens.GetValue(3).Trim();
                string setType = tokens.GetValue(4).Trim();
                MidiToBooleanObserved.SetBooleanType t = Eloi.E_StringUtility.AreEquals("SetTrue", setType, true, true)
                    ? MidiToBooleanObserved.SetBooleanType.SetTrue : MidiToBooleanObserved.SetBooleanType.SetFalse;
                MidiToBooleanObserved.NoteNotChannel value =
                    new MidiToBooleanObserved.NoteNotChannel(deviceName, noteName, booleanName, t);

                    m_register.Append(value);
            }
            if (tokens.GetCount() == 7 && Eloi.E_StringUtility.AreEquals("pitch", tokens.GetValue(1), in useTrim, in useIgnoreCase))
            {

                //int shortenToDetect = 0;
                //if (int.TryParse(tokens.GetValue(2), out shortenToDetect))
                //{
                //    string deviceName = tokens.GetValue(0).Trim();
                //    string booleanName = tokens.GetValue(3).Trim();
                //    string setType = tokens.GetValue(4).Trim();
                //    MidiToBooleanObserved.SetBooleanType t = Eloi.E_StringUtility.AreEquals("SetTrue", setType, true, true)
                //        ? MidiToBooleanObserved.SetBooleanType.SetTrue : MidiToBooleanObserved.SetBooleanType.SetFalse;
                //    MidiToBooleanObserved.NoteNotChannel value =
                //        new MidiToBooleanObserved.NoteNotChannel(deviceName, shortenToDetect, booleanName, t);

                //    m_register.Append(value);
                //}
            }
            if (tokens.GetCount() == 7 && Eloi.E_StringUtility.AreEquals("patch", tokens.GetValue(1), in useTrim, in useIgnoreCase))
            {

                //int shortenToDetect = 0;
                //if (int.TryParse(tokens.GetValue(2), out shortenToDetect))
                //{
                //    string deviceName = tokens.GetValue(0).Trim();
                //    string booleanName = tokens.GetValue(3).Trim();
                //    string setType = tokens.GetValue(4).Trim();
                //    MidiToBooleanObserved.SetBooleanType t = Eloi.E_StringUtility.AreEquals("SetTrue", setType, true, true)
                //        ? MidiToBooleanObserved.SetBooleanType.SetTrue : MidiToBooleanObserved.SetBooleanType.SetFalse;
                //    MidiToBooleanObserved.NoteNotChannel value =
                //        new MidiToBooleanObserved.NoteNotChannel(deviceName, shortenToDetect, booleanName, t);

                //    m_register.Append(value);
                //}
            }
        }
    }

}
[System.Serializable]
public class MidiToBooleanObserved {


    public enum SetBooleanType { SetTrue, SetFalse }
    [System.Serializable]
    public class ShortenId {
        public string m_booleanNameToAffect;
        public string m_midiDeviceName;
        public int m_shortenValue;
        public SetBooleanType m_setType;

        public ShortenId(string midiDeviceName, int shortenValue, string booleanNameToAffect, SetBooleanType setType)
        {
            m_midiDeviceName = midiDeviceName;
            m_shortenValue = shortenValue;
            m_booleanNameToAffect = booleanNameToAffect;
            m_setType = setType;
        }
    }
    [System.Serializable]
    public class ControlCommand
    {
        public string m_booleanNameToAffect;
        public string m_midiDeviceName;
        public string m_controllerName;
        [Range(0, 1)]
        public float m_pourcentMin;
        [Range(0, 1)]
        public float m_pourcentMax;
        public SetBooleanType m_setType;

        public ControlCommand(string midiDeviceName,string controllerName, float pourcentMin, float pourcentMax, string booleanNameToAffect, SetBooleanType setType)
        {
            m_midiDeviceName = midiDeviceName;
            m_controllerName = controllerName;
            m_booleanNameToAffect = booleanNameToAffect;
            m_pourcentMin = pourcentMin;
            m_pourcentMax = pourcentMax;
            m_setType = setType;
        }
    }
    [System.Serializable]
    public class NoteNotChannel
    {
        public string m_booleanNameToAffect;
        public string m_midiDeviceName;
        public string m_noteId;
        public SetBooleanType m_setType;

        public NoteNotChannel(string midiDeviceName, string noteId, string booleanNameToAffect, SetBooleanType setType)
        {
            m_midiDeviceName = midiDeviceName;
            m_booleanNameToAffect = booleanNameToAffect;
            m_noteId = noteId;
            m_setType = setType;
        }
    }
    [System.Serializable]
    public class NoteVelocityNoChannel
    {
        public string m_booleanNameToAffect;
        public string m_midiDeviceName;
        public string m_noteId;

        [Range(0, 1)]
        public float m_pourcentMinVelocity;
        [Range(0, 1)]
        public float m_pourcentMaxVelocity;

        public SetBooleanType m_setType;

        public NoteVelocityNoChannel(string midiDeviceName, string noteId, float pourcentMinVelocity, float pourcentMaxVelocity, string booleanNameToAffect, SetBooleanType setType)
        {
            m_midiDeviceName = midiDeviceName;
            m_noteId= noteId;
            m_booleanNameToAffect = booleanNameToAffect;
            m_pourcentMinVelocity = pourcentMinVelocity;
            m_pourcentMaxVelocity = pourcentMaxVelocity;
            m_setType = setType;
        }
    }
}

