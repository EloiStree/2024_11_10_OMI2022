using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Eloi;
using UnityEngine.Events;

[System.Serializable]
public class MidiInAbstractEventSourceDevice : UnityEvent<IMidiNoteEventGet> { }
[System.Serializable]
public class MidiInAbstractEventNote : UnityEvent<IMidiNoteEventGet> { }
[System.Serializable]
public class MidiInAbstractEventPitch : UnityEvent<IMidiPitchChangeEventGet> { }
[System.Serializable]
public class MidiInAbstractEventControl : UnityEvent<IMidiControlChangeEventGet> { }
[System.Serializable]
public class MidiInAbstractEventPatch : UnityEvent<IMidiPatchChangeEventGet> { }
[System.Serializable]
public class MidiInAbstractEventShortValue : UnityEvent<IMidiShortWithSourceGet> { }



public interface IMidiShortWithSourceGet{

    void GetWhenReceived(out DateTime whenReceived);
    void GetSource(out IMidiInSourceGet source);
    void GetShortId(out IMidiShortGet midiShort);
    void GetShortId(out int midiShortId);
}

public interface IMidiInSourceGet
{
     void GetSourceDeviceName(out string deviceNameId);
     void GetSourceDeviceUniqueId(out uint uniqueId);
}

public interface IMidiEventBasicGet {

     void GetSourceDeviceName(out string deviceNameId);
     void GetSourceDeviceUniqueId(out uint uniqueId);
     void GetWhenReceived(out DateTime whenReceived);
     void GetShortenId(out int shortId);
     void GetShortenId(out IMidiShortGet shortenId);
     void GetUsedChannel(out int channel);
     void GetAbsoluteTime(out long absoluteTime);

}

public interface IMidiEventNamedElementGet {

    bool IsNameIsAnInteger();
    void GetNameAsInteger(out int uniqueId);
    void GetGivenName(out string givenName);
}

public interface IMidiNoteEventGet : IMidiEventBasicGet , IMidiEventNamedElementGet
{
    void GetVelocity(out IPercent01Get pourcentVelocity);
    void GetVelocity(out IPercentN1P1Get pourcentVelocity);
    void IsOn(out bool isOn);
    void IsOff(out bool isOff);
    bool HasVelocityData();
}


public interface IMidiControlChangeEventGet : IMidiEventBasicGet, IMidiEventNamedElementGet
{
     void GetControlRawValue(out int value);
     void GetControlPercentValue(out IPercent01Get pourcentOnValue);
     void GetControlPercentValue(out IPercentN1P1Get pourcentOnValue);
}
public interface IMidiPitchChangeEventGet : IMidiEventBasicGet, IMidiEventNamedElementGet
{
     void GetPitchRawValue(out int value);
     void GetPitchPourcentValue(out IPercent01Get pourcentValue);
     void GetPitchPourcentValue(out IPercentN1P1Get pourcentValue);
}

public interface IMidiPatchChangeEventGet : IMidiEventBasicGet, IMidiEventNamedElementGet
{
     void GetPatchRawValue(out int value);
}



// Detect that note has been touch genlty, averagely, heavily



// Detect that a specific short appeared
// Detect that two specifics short appeared and diseapeared

public interface IOwnOneShortId { void GetOwnShortId(out int shortId); }
public interface IOwnTwoShortId { void GetOwnShortIds(out int shortIdA, out int shortIdB); }
public interface IOwnSeveralShortId {
    void GetOwnShortIds(out int[] shortIds);
    int[] GetOwnShortIds();
}


[System.Serializable]
public struct TwoMidiShortAsBoolean : IOwnTwoShortId, IOwnSeveralShortId
{
    public int m_trueShortId;
    public int m_falseShortId;

    public void GetOwnShortIds(out int shortIdA, out int shortIdB)
    {
        shortIdA = m_trueShortId;
        shortIdB = m_falseShortId;
    }

    public void GetOwnShortIds(out int[] shortIds)
    {
        shortIds = new int[] { m_trueShortId, m_falseShortId };
    }
    public int[] GetOwnShortIds()
    {
        return new int[] { m_trueShortId, m_falseShortId };
    }
}

[System.Serializable]
public struct GroupOfMidiShortIds : IOwnSeveralShortId
{
    public int [] m_shortIds;

    public void GetOwnShortIds(out int[] shortIds)
    {
        shortIds = m_shortIds;
    }
    public int[] GetOwnShortIds()
    {
        return m_shortIds;
    }
}


public interface IMidiShortGet
{
    void GetMidiShort(out int value);
}
[System.Serializable]
public struct MidiShort : IMidiShortGet, IOwnOneShortId
{
    [SerializeField] int m_shortId;

    public MidiShort(int shortId)
    {
        m_shortId = shortId;
    }
    public MidiShort(string shortId)
    {
       if(!int.TryParse(shortId, out m_shortId))
            throw new System.ArgumentException("Short id must be convertable to int");
    }

    public void GetMidiShort(out int value)
    {
        value= m_shortId;
    }

    public void GetOwnShortId(out int shortId)
    {
        shortId = m_shortId;
    }
}