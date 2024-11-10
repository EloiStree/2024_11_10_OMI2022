using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractMidiInRelayMono : MonoBehaviour, IAbstractMidiInRelay
{
    public AbstractMidiInRelay m_relay;
    public Eloi.StringClampHistory m_history;

    public void PushIn(IMidiNoteEventGet note)
    {
        m_relay.PushIn(note);
        MidiOneLinerDebug.GetFrom(in note, out string description);
        m_history.PushIn(description);
    }

    public void PushIn(IMidiPitchChangeEventGet pitch)
    {
        m_relay.PushIn(pitch);
        MidiOneLinerDebug.GetFrom(in pitch, out string description);
        m_history.PushIn(description);
    }

    public void PushIn(IMidiControlChangeEventGet control)
    {
        m_relay.PushIn(control);
        MidiOneLinerDebug.GetFrom(in control, out string description);
        m_history.PushIn(description);
    }

    public void PushIn(IMidiPatchChangeEventGet patch)
    {
        m_relay.PushIn(patch);
        MidiOneLinerDebug.GetFrom(in patch, out string description);
        m_history.PushIn(description);
    }

    public void PushIn(IMidiShortWithSourceGet shortValue)
    {
        m_relay.PushIn(shortValue);
        MidiOneLinerDebug.GetFrom(in shortValue, out string description);
        m_history.PushIn(description);
    }
}

public interface IAbstractMidiInRelay
{
    void PushIn(IMidiShortWithSourceGet shortValue);
    void PushIn(IMidiNoteEventGet note);
    void PushIn(IMidiPitchChangeEventGet pitch);
     void PushIn(IMidiControlChangeEventGet control);
     void PushIn(IMidiPatchChangeEventGet patch);
}

[System.Serializable]
public class AbstractMidiInRelay : IAbstractMidiInRelay
{
    public MidiInAbstractEventShortValue m_onShortReceived;
    public MidiInAbstractEventNote m_onNoteReceived;
    public MidiInAbstractEventPitch m_onPitchReceived;
    public MidiInAbstractEventControl m_controlReceived;
    public MidiInAbstractEventPatch m_patchReceived;

    public void PushIn(IMidiShortWithSourceGet shortValue)
    {
        if (shortValue != null)
            m_onShortReceived.Invoke(shortValue);
    }
    public void PushIn(IMidiNoteEventGet note)
    {
        if (note != null)
            m_onNoteReceived.Invoke(note);
    }
    public void PushIn(IMidiPitchChangeEventGet pitch)
    {
        if (pitch != null)
            m_onPitchReceived.Invoke(pitch);
    }
    public void PushIn(IMidiControlChangeEventGet control)
    {
        if (control != null)
            m_controlReceived.Invoke(control);
    }
    public void PushIn(IMidiPatchChangeEventGet patch)
    {
        if (patch != null)
            m_patchReceived.Invoke(patch);
    }
}
