using Eloi;
using NAudio.Midi;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowMidiInConverterToAbstractMono : MonoBehaviour, IAbstractMidiInRelay
{
    public AbstractMidiInRelay m_relay;
    public WindowMidiDeviceSourceRef m_monoSource;

    public void PushIn(NoteOnEvent note)
    {
        PushIn(new WindowMidiNoteWarpper(m_monoSource, note));
    }

    public void PushIn(NoteEvent note)
    {
        PushIn(new WindowMidiNoteWarpper(m_monoSource, note));
    }

    public void PushIn(PitchWheelChangeEvent note)
    {
        PushIn(new WindowMidiPitchWarpper(m_monoSource, note));
    }

    public void PushIn(ControlChangeEvent note)
    {
        PushIn(new WindowMidiControlWarpper(m_monoSource, note));
    }

    public void PushIn(PatchChangeEvent note)
    {
        PushIn(new WindowMidiPatchWarpper(m_monoSource, note));
    }
    public void PushIn(int id)
    {
        PushIn(new ShortenWindowMidiWarpper(m_monoSource, id));
    }



    public void PushIn(IMidiNoteEventGet note)
    {
        m_relay.PushIn(note);
    }

    public void PushIn(IMidiPitchChangeEventGet note)
    {
        m_relay.PushIn(note);
    }

    public void PushIn(IMidiControlChangeEventGet note)
    {
        m_relay.PushIn(note);
    }

    public void PushIn(IMidiPatchChangeEventGet note)
    {
        m_relay.PushIn(note);
    }

    public void PushIn(IMidiShortWithSourceGet shortValue)
    {
        m_relay.PushIn(shortValue);
    }

    [System.Serializable]
    public class WindowMidiDeviceSourceRef : IMidiInSourceGet
    {
        public MidiInMono m_source;
        public void GetSourceDeviceName(out string deviceNameId)
        {
            m_source.GetSourceName(out deviceNameId);
        }

        public void GetSourceDeviceUniqueId(out uint uniqueId)
        {
            uniqueId = (uint)m_source.GetInstanceID();
        }
    }


    public abstract class WindowMidiWarpper : IMidiEventNamedElementGet, IMidiEventBasicGet
    {
        public IMidiInSourceGet m_source;
        public DateTime m_whenReceived;
        public WindowMidiWarpper(IMidiInSourceGet source, DateTime receivedTime)
        {
            m_source = source;
            m_whenReceived = receivedTime;
        }
        public WindowMidiWarpper(IMidiInSourceGet source)
        {
            m_source = source;
            m_whenReceived = DateTime.Now;
        }


        //All Midi Event
        public abstract void GetShortenId(out int shortId);
        public abstract void GetShortenId(out IMidiShortGet shortenId);
        public abstract void GetUsedChannel(out int channel);
        public abstract void GetAbsoluteTime(out long absoluteTime);


        //Specific to event
        public abstract bool IsNameIsAnInteger();
        public abstract void GetNameAsInteger(out int uniqueId);
        public abstract void GetGivenName(out string givenName);

        public void GetSourceDeviceName(out string deviceNameId)
        {
            m_source.GetSourceDeviceName(out deviceNameId);
        }

        public void GetSourceDeviceUniqueId(out uint uniqueId)
        {
            m_source.GetSourceDeviceUniqueId(out uniqueId);
        }

        public void GetWhenReceived(out DateTime whenReceived)
        {
            whenReceived = m_whenReceived;
        }


    }

    public class ShortenWindowMidiWarpper : IMidiShortWithSourceGet
    {
        public IMidiInSourceGet m_source;
        public DateTime m_whenReceived;
        public int m_shorten;
        public ShortenWindowMidiWarpper(IMidiInSourceGet source, MidiEvent genericInformation) 
        {
            m_shorten = genericInformation.GetAsShortMessage();
            Set(source);
        }



        public ShortenWindowMidiWarpper(IMidiInSourceGet source, DateTime receivedTime, MidiEvent genericInformation) 
        {
            m_shorten =  genericInformation.GetAsShortMessage();
            Set(source, receivedTime);
        }


        public ShortenWindowMidiWarpper(IMidiInSourceGet source, int shortenId) 
        {
            m_shorten = shortenId;
            Set(source);
        }
        public ShortenWindowMidiWarpper(IMidiInSourceGet source, DateTime receivedTime,  int shortenId)
        {
            m_shorten = shortenId;
            Set(source, receivedTime);
        }

        private void Set(IMidiInSourceGet source)
        {
            m_source = source;
            m_whenReceived = DateTime.Now;
        }
        private void Set(IMidiInSourceGet source, DateTime receivedTime)
        {
            m_source = source;
            m_whenReceived = receivedTime;
        }
       

        public void GetShortId(out int midiShortId)
        {
            midiShortId = m_shorten;
        }

        public void GetWhenReceived(out DateTime whenReceived)
        {
            whenReceived= m_whenReceived;
        }

        public void GetSource(out IMidiInSourceGet source)
        {
            source = m_source;
        }

        public void GetShortId(out IMidiShortGet midiShort)
        {
            midiShort = new MidiShort(m_shorten);
        }
    }


    public abstract class WindowMidiEventGenericWarpper : WindowMidiWarpper
    {
        public MidiEvent m_genericInformation;
        protected WindowMidiEventGenericWarpper(IMidiInSourceGet source, MidiEvent genericInformation) :base(source)
        {
            m_genericInformation = genericInformation;
        }
        protected WindowMidiEventGenericWarpper(IMidiInSourceGet source, DateTime receivedTime, MidiEvent genericInformation): base(source, receivedTime)
        {
            m_genericInformation = genericInformation;
        }
        //All Midi Event
        public  override void GetShortenId(out int shortId) {
            shortId= m_genericInformation.GetAsShortMessage();
        }
        public override void GetShortenId(out IMidiShortGet shortenId)
        {
            shortenId =new MidiShort( m_genericInformation.GetAsShortMessage());
        }
        public override void GetUsedChannel(out int channel)
        {
            channel = m_genericInformation.Channel;
        }
        public override void GetAbsoluteTime(out long absoluteTime)
        {
            absoluteTime = m_genericInformation.AbsoluteTime;
        }
    }


    public class WindowMidiControlWarpper : WindowMidiEventGenericWarpper, IMidiControlChangeEventGet
    {


        public WindowMidiControlWarpper(IMidiInSourceGet source, ControlChangeEvent control) : base(source, control)
        {
            m_control = control;
            SetValue(control);
        }
        public WindowMidiControlWarpper(IMidiInSourceGet source, DateTime receivedTime, ControlChangeEvent control) : base(source, receivedTime, control)
        {
            m_control = control; 
            SetValue(control);
        }
        private void SetValue(ControlChangeEvent pitch)
        {
            float p = pitch.ControllerValue / (float)127;
            m_p = new Percent01(p);
            m_pn = new PercentN1P1((p - 0.5f) * 2f);
        }
        Percent01 m_p;
        PercentN1P1 m_pn;
        #region Access Name and ID
        public NAudio.Midi.ControlChangeEvent m_control;
        public bool m_isOn;


        public override void GetGivenName(out string givenName)
        {
            givenName = m_control.Controller.ToString() ;
        }

        public override void GetNameAsInteger(out int uniqueId)
        {
            uniqueId = (int) m_control.Controller;
        }

        public override bool IsNameIsAnInteger()
        {
            return true;
        }

        #endregion

        public void GetControlPercentValue(out IPercent01Get pourcentOnValue)
        {
            pourcentOnValue = m_p;
        }

        public void GetControlPercentValue(out IPercentN1P1Get pourcentOnValue)
        {
            pourcentOnValue = m_pn;
        }

        public void GetControlRawValue(out int value)
        {
            value = m_control.ControllerValue;
        }

    }
    public class WindowMidiPatchWarpper : WindowMidiEventGenericWarpper, IMidiPatchChangeEventGet
    {

        public WindowMidiPatchWarpper(IMidiInSourceGet source, PatchChangeEvent patch) : base(source, patch)
        {
            m_patchEvent = patch;
        }
        public WindowMidiPatchWarpper(IMidiInSourceGet source, DateTime receivedTime, PatchChangeEvent patch) : base(source, receivedTime, patch)
        {
            m_patchEvent = patch;
        }


        #region Access Name and ID
        public NAudio.Midi.PatchChangeEvent m_patchEvent;
        public const  int m_maxValue=127;

        public override void GetGivenName(out string givenName)
        {
            givenName = ""+m_patchEvent.Patch ;
        }

        public override void GetNameAsInteger(out int uniqueId)
        {
            uniqueId = m_patchEvent.Patch;
        }

        public override bool IsNameIsAnInteger()
        {
            return true;
        }

        #endregion

       

        public void GetPatchRawValue(out int value)
        {
            value = m_patchEvent.Patch    ;
        }

        
    }
    public class WindowMidiPitchWarpper : WindowMidiEventGenericWarpper, IMidiPitchChangeEventGet
    {

        public WindowMidiPitchWarpper(IMidiInSourceGet source, PitchWheelChangeEvent pitch) : base(source, pitch)
        {
            m_pitchWheel = pitch;
            SetValue(pitch);
        }
        public WindowMidiPitchWarpper(IMidiInSourceGet source, DateTime receivedTime, PitchWheelChangeEvent pitch) : base(source, receivedTime, pitch)
        {
            m_pitchWheel = pitch;
            SetValue(pitch);
        }

        private void SetValue(PitchWheelChangeEvent pitch)
        {
            float p = pitch.Pitch / (float)16383;
            m_p = new Percent01(p);
            m_pn = new PercentN1P1((p - 0.5f) * 2f);
        }


        #region Access Name and ID
        public NAudio.Midi.PitchWheelChangeEvent m_pitchWheel;
        public Percent01 m_p;
        public PercentN1P1 m_pn;


        public override void GetGivenName(out string givenName)
        {
            givenName = "Pitch Wheel";
        }

        public override void GetNameAsInteger(out int uniqueId)
        {
            uniqueId = 0;
        }

        public override bool IsNameIsAnInteger()
        {
            return true;
        }

        #endregion

        public void GetPitchPourcentValue(out IPercent01Get pourcentValue)
        {
            pourcentValue = m_p;
        }

        public void GetPitchPourcentValue(out IPercentN1P1Get pourcentValue)
        {
            pourcentValue = m_pn;
        }

        public void GetPitchRawValue(out int value)
        {
           value = m_pitchWheel.Pitch;
        }

    }
    public class WindowMidiNoteWarpper : WindowMidiEventGenericWarpper, IMidiNoteEventGet
    {
        public WindowMidiNoteWarpper(IMidiInSourceGet source, NoteEvent note) : base(source, note)
        {
            m_note = note;
            m_isOn = m_note.CommandCode == MidiCommandCode.NoteOn && note.Velocity!=0;
            m_velocity = note.Velocity/127f;
        }
        public WindowMidiNoteWarpper(IMidiInSourceGet source, DateTime receivedTime, NoteEvent note): base(source, receivedTime, note)
        {
            m_note = note;
            m_isOn = m_note.CommandCode == MidiCommandCode.NoteOn && note.Velocity != 0;
            m_velocity = note.Velocity / 127f;
        }


        #region Access Name and ID
        public NAudio.Midi.NoteEvent m_note;
        public bool m_isOn;
        public float m_velocity;
    

        public override void GetGivenName(out string givenName)
        {
            givenName = m_note.NoteName;
        }

        public override void GetNameAsInteger(out int uniqueId)
        {
            uniqueId = m_note.NoteNumber;
        }

        public override bool IsNameIsAnInteger()
        {
            return true;
        }

        #endregion

        public void GetVelocity(out IPercent01Get pourcentVelocity)
        {
            pourcentVelocity = new Percent01(m_velocity);
        }

        public void GetVelocity(out IPercentN1P1Get pourcentVelocity)
        {
            pourcentVelocity = new PercentN1P1((m_velocity-0.5f)*2f);
        }

        public void IsOn(out bool isOn)
        {
            isOn = m_isOn;
        }

        public void IsOff(out bool isOff)
        {
            isOff = !m_isOn;
        }

        public bool HasVelocityData()
        {
            return true;
        }
    }



}
