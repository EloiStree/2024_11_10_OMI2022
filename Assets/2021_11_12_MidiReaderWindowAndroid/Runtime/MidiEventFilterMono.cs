using Eloi;
using NAudio.Midi;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidiEventFilterMono : MonoBehaviour
{
     public UnityMidiEventArgsEvent m_relayedMidiEvent;

    [Header("From Command Control Enum")]
    public bool m_allowNoteOff;
    public bool m_allowNoteOn;
    public bool m_allowKeyAfterTouch;
    public bool m_allowControlChange;
    public bool m_allowPatchChange;
    public bool m_allowChannelAfterTouch;
    public bool m_allowPitchWheelChange;
    public bool m_allowSysex;
    public bool m_allowEox;
    public bool m_allowTimingClock;
    public bool m_allowStartSequence;
    public bool m_allowContinueSequence;
    public bool m_allowStopSequence;
    public bool m_allowAutoSensing;

    [Header("From inheritence")]
    public bool m_allowRawMetaEvent                ;
     public bool m_allowChannelAfterTouchEvent      ;
     public bool m_allowControlChangeEvent          ;
     public bool m_allowKeySignatureEvent           ;
     public bool m_allowMetaEvent                   ;
     public bool m_allowNoteEvent                   ;
     public bool m_allowNoteOnEvent                 ;
     public bool m_allowPatchChangeEvent            ;
     public bool m_allowPitchWheelChangeEvent       ;
     public bool m_allowSequencerSpecificEvent      ;
     public bool m_allowSysexEvent                  ;
     public bool m_allowTempoEvent                  ;
     public bool m_allowTextEvent                   ;
     public bool m_allowTimeSignatureEvent          ;
     public bool m_allowTrackSequenceNumberEvent    ;

    public  TypeClampHistory m_typeHistory;



    public void PushIn(MidiInMessageEventArgs e) {
        if (e == null || e.MidiEvent == null)
            return;
       var eve = e.MidiEvent;
       if(!m_allowRawMetaEvent              &&   eve is NAudio.Midi.RawMetaEvent              ){return;}
       if(!m_allowChannelAfterTouchEvent    &&   eve is NAudio.Midi.ChannelAfterTouchEvent    ){return;}
       if(!m_allowControlChangeEvent        &&   eve is NAudio.Midi.ControlChangeEvent        ){return;}
       if(!m_allowKeySignatureEvent         &&   eve is NAudio.Midi.KeySignatureEvent         ){return;}
       if(!m_allowMetaEvent                 &&   eve is NAudio.Midi.MetaEvent                 ){return;}
       if(!m_allowNoteEvent                 &&   eve is NAudio.Midi.NoteEvent                 ){return;}
       if(!m_allowNoteOnEvent               &&   eve is NAudio.Midi.NoteOnEvent               ){return;}
       if(!m_allowPatchChangeEvent          &&   eve is NAudio.Midi.PatchChangeEvent          ){return;}
       if(!m_allowPitchWheelChangeEvent     &&   eve is NAudio.Midi.PitchWheelChangeEvent     ){return;}
       if(!m_allowSequencerSpecificEvent    &&   eve is NAudio.Midi.SequencerSpecificEvent    ){return;}
       if(!m_allowSysexEvent                &&   eve is NAudio.Midi.SysexEvent                ){return;}
       if(!m_allowTempoEvent                &&   eve is NAudio.Midi.TempoEvent                ){return;}
       if(!m_allowTextEvent                 &&   eve is NAudio.Midi.TextEvent                 ){return;}
       if(!m_allowTimeSignatureEvent        &&   eve is NAudio.Midi.TimeSignatureEvent        ){return;}
       if(!m_allowTrackSequenceNumberEvent  &&   eve is NAudio.Midi.TrackSequenceNumberEvent  ){ return; }

       MidiCommandCode cc = e.MidiEvent.CommandCode;
            if( !m_allowNoteOff            && cc==MidiCommandCode.NoteOff          ){return;}
       else if( !m_allowNoteOn             && cc==MidiCommandCode.NoteOn           ){return;}
       else if( !m_allowKeyAfterTouch      && cc==MidiCommandCode.KeyAfterTouch    ){return;}
       else if( !m_allowControlChange      && cc==MidiCommandCode.ControlChange    ){return;}
       else if( !m_allowPatchChange        && cc==MidiCommandCode.PatchChange      ){return;}
       else if( !m_allowChannelAfterTouch  && cc==MidiCommandCode.ChannelAfterTouch){return;}
       else if( !m_allowPitchWheelChange   && cc==MidiCommandCode.PitchWheelChange ){return;}
       else if( !m_allowSysex              && cc==MidiCommandCode.Sysex            ){return;}
       else if( !m_allowEox                && cc==MidiCommandCode.Eox              ){return;}
       else if( !m_allowTimingClock        && cc==MidiCommandCode.TimingClock      ){return;}
       else if( !m_allowStartSequence      && cc==MidiCommandCode.StartSequence    ){return;}
       else if( !m_allowContinueSequence   && cc==MidiCommandCode.ContinueSequence ){return;}
       else if( !m_allowStopSequence       && cc==MidiCommandCode.StopSequence     ){return;}
       else if( !m_allowAutoSensing        && cc==MidiCommandCode.AutoSensing      ){return;}
       else if( !m_allowMetaEvent          && cc== MidiCommandCode.MetaEvent       ){ return; }
        
        




        m_typeHistory.PushIn(" "+eve.GetType() + " - "+  cc.ToString() );
        m_relayedMidiEvent.Invoke(e);

    }
}
