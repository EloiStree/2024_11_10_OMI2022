using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MidiToBooleanRegisterMono : MonoBehaviour
{
    public MidiToBooleanMono m_midiToBooleanRegister;
    public BooleanStateRegisterMono m_booleanRegister;


    private bool ignore=true, trim=true;
    public void PushIn(IMidiNoteEventGet value)
    {
        if (value == null)
            return;
        value.GetGivenName(out string givenName);
        value.GetNameAsInteger(out int givenNameId);
        string givenNameIdString = ""+givenNameId;
        value.GetSourceDeviceName(out string deviceName);
        value.IsOn(out bool isOn);

        value.GetVelocity(out Eloi.IPercent01Get pctValue);
        float pct = pctValue.GetPercent();

        IEnumerable<MidiToBooleanObserved.NoteNotChannel> notes =  m_midiToBooleanRegister.m_noteNoChannel.Where(
            k =>
            Eloi.E_StringUtility.AreEquals(in k.m_midiDeviceName, in deviceName, in ignore, in trim) &&
            (Eloi.E_StringUtility.AreEquals(in k.m_noteId,  in givenName, in ignore, in trim)|| Eloi.E_StringUtility.AreEquals(in k.m_noteId, in givenNameIdString, in ignore, in trim)));



        foreach (var item in notes)
        {
            if (item.m_setType == MidiToBooleanObserved.SetBooleanType.SetTrue)
                m_booleanRegister.Set(item.m_booleanNameToAffect, isOn, true);
            else if (item.m_setType == MidiToBooleanObserved.SetBooleanType.SetFalse)
                m_booleanRegister.Set(item.m_booleanNameToAffect, !isOn, true);
        }

        IEnumerable<MidiToBooleanObserved.NoteVelocityNoChannel> notesVelocity = m_midiToBooleanRegister.m_noteNoChannelVelocity.Where(
          k =>
          Eloi.E_StringUtility.AreEquals(in k.m_midiDeviceName, in deviceName, in ignore, in trim) &&
          (Eloi.E_StringUtility.AreEquals(in k.m_noteId, in givenName, in ignore, in trim) || Eloi.E_StringUtility.AreEquals(in k.m_noteId, in givenNameIdString, in ignore, in trim)));




        foreach (var item in notesVelocity)
        {
            bool isInRange = item.m_pourcentMinVelocity <= pct && item.m_pourcentMaxVelocity >= pct;
            if (item.m_setType == MidiToBooleanObserved.SetBooleanType.SetTrue)
                m_booleanRegister.Set(item.m_booleanNameToAffect, isInRange, true);
            else if (item.m_setType == MidiToBooleanObserved.SetBooleanType.SetFalse)
                m_booleanRegister.Set(item.m_booleanNameToAffect, !isInRange, true);
        }
    }
    public void PushIn(IMidiControlChangeEventGet value)
    {
        if (value == null)
            return;
        value.GetGivenName(out string givenName);
        value.GetNameAsInteger(out int givenNameId);
        string givenNameIdString = "" + givenNameId;
        value.GetSourceDeviceName(out string deviceName);
        value.GetControlPercentValue(out Eloi.IPercent01Get percent);
        percent.GetPercent(out float pct);
        IEnumerable<MidiToBooleanObserved.ControlCommand> note = m_midiToBooleanRegister.m_controlCommand.Where(
              k =>
              Eloi.E_StringUtility.AreEquals(in k.m_midiDeviceName, in deviceName, in ignore, in trim) &&
            (Eloi.E_StringUtility.AreEquals(in k.m_controllerName, in givenName, in ignore, in trim) || Eloi.E_StringUtility.AreEquals(in k.m_controllerName, in givenNameIdString, in ignore, in trim))

              );
        foreach (var item in note)
        {
            bool isInRange = item.m_pourcentMin <= pct && item.m_pourcentMax >= pct;
            if (item.m_setType == MidiToBooleanObserved.SetBooleanType.SetTrue)
                m_booleanRegister.Set(item.m_booleanNameToAffect, isInRange, true);
            else if (item.m_setType == MidiToBooleanObserved.SetBooleanType.SetFalse)
                m_booleanRegister.Set(item.m_booleanNameToAffect, !isInRange, true);
        }

    }
    public void PushIn(IMidiPitchChangeEventGet value)
    {

    }
    public void PushIn(IMidiPatchChangeEventGet value)
    {

    }
    public void PushIn(IMidiShortWithSourceGet value)
    {
        if (value == null)
            return;
        value.GetShortId(out int shortenId);
        value.GetSource(out IMidiInSourceGet deviceSource);
        deviceSource.GetSourceDeviceName(out string deviceName);

      //  Eloi.E_DebugLog.A(shortenId +" - - "+deviceName);

        IEnumerable<MidiToBooleanObserved.ShortenId> note = m_midiToBooleanRegister.m_shortenId.Where(
              k =>
              Eloi.E_StringUtility.AreEquals(in k.m_midiDeviceName, in deviceName, in ignore, in trim) &&
              k.m_shortenValue== shortenId);
       // Eloi.E_DebugLog.G();

        foreach (var item in note)
        {
            if (item.m_setType == MidiToBooleanObserved.SetBooleanType.SetTrue)
            {
                Eloi.E_DebugLog.E();
                m_booleanRegister.Set(item.m_booleanNameToAffect, true, true);
            }
            else if (item.m_setType == MidiToBooleanObserved.SetBooleanType.SetFalse) {
                Eloi.E_DebugLog.F();
                m_booleanRegister.Set(item.m_booleanNameToAffect, false,true);
            }
        }
    }
}
