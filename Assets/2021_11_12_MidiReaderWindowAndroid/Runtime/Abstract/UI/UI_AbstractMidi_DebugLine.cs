using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_AbstractMidi_DebugLine : MonoBehaviour, IAbstractMidiGetHandler
{

    public object m_data;

   public UI_AbstractMidi_DebugLine_PitchChange   m_pitchDisplay;
   public UI_AbstractMidi_DebugLine_ControlChange m_controlDisplay;
   public UI_AbstractMidi_DebugLine_NoteChange    m_noteDisplay;
   public UI_AbstractMidi_DebugLine_PatchChange   m_patchDisplay;
   public UI_AbstractMidi_DebugLine_ShortChange   m_shortDisplay;
    public string m_receivedDebug;

    private void Update()
    {
        if (m_data is IMidiControlChangeEventGet)
        {
            DisableAll();
            m_controlDisplay.gameObject.SetActive(true);
            m_controlDisplay.PushIn((IMidiControlChangeEventGet)m_data);
            MidiOneLinerDebug.GetFrom((IMidiControlChangeEventGet) m_data, out m_receivedDebug);
        }
        else if (m_data is IMidiShortWithSourceGet)
        {
            DisableAll();
            m_controlDisplay.gameObject.SetActive(true);
            m_shortDisplay.PushIn((IMidiShortWithSourceGet)m_data);
            MidiOneLinerDebug.GetFrom((IMidiShortWithSourceGet)m_data, out m_receivedDebug);
        }
        else if (m_data is IMidiPitchChangeEventGet)
        {
            DisableAll();
            m_pitchDisplay.gameObject.SetActive(true);
            m_pitchDisplay.PushIn((IMidiPitchChangeEventGet)m_data);
            MidiOneLinerDebug.GetFrom((IMidiPitchChangeEventGet)m_data, out m_receivedDebug);
        }
        else if (m_data is IMidiPatchChangeEventGet)
        {
            DisableAll();
            m_patchDisplay.gameObject.SetActive(true);
            m_patchDisplay.PushIn((IMidiPatchChangeEventGet)m_data);
            MidiOneLinerDebug.GetFrom((IMidiPatchChangeEventGet)m_data, out m_receivedDebug);
        }
        else if (m_data is IMidiNoteEventGet)
        {
            DisableAll();
            m_noteDisplay.gameObject.SetActive(true);
            m_noteDisplay.PushIn((IMidiNoteEventGet)m_data);
            MidiOneLinerDebug.GetFrom((IMidiNoteEventGet)m_data, out m_receivedDebug);
        }
        m_data = null;
    }


    public void PushIn(IMidiNoteEventGet value)
    {

        m_data = value;
    }


    public void PushIn(IMidiPitchChangeEventGet value)
    {
        m_data = value;
      

    }
    public void PushIn(IMidiControlChangeEventGet value)
    {

        m_data = value;
    }
    public void PushIn(IMidiPatchChangeEventGet value)
    {

        m_data = value;
    }
    public void PushIn(IMidiShortWithSourceGet value)
    {
        m_data = value;

    }
    public void DisableAll()
    {
        m_pitchDisplay.gameObject.SetActive(false);
        m_controlDisplay.gameObject.SetActive(false);
        m_noteDisplay.gameObject.SetActive(false);
        m_patchDisplay.gameObject.SetActive(false);
        m_shortDisplay.gameObject.SetActive(false);
    }
}



public interface IAbstractMidiGetHandler {
   void PushIn(IMidiNoteEventGet value);
   void PushIn(IMidiPitchChangeEventGet value);
   void PushIn(IMidiControlChangeEventGet value);
   void PushIn(IMidiPatchChangeEventGet value);
   void PushIn(IMidiShortWithSourceGet value);

}