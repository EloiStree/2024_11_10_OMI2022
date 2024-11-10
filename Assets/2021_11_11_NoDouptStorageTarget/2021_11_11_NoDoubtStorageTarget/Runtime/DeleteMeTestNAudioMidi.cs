using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NAudio;
using NAudio.Midi;
using System;
using System.Linq;


public class DeleteMeTestNAudioMidi : MonoBehaviour
{
    public string m_path;
    [TextArea(2, 20)]
    public string m_loaded;
    [TextArea(2, 20)]
    public string m_loadedD;
    [ContextMenu("Load File")]
    public void LoadFile()
    {
        MidiFile midi = new MidiFile(m_path);
        List<TempoEvent> tempo = new List<TempoEvent>();

        for (int i = 0; i < midi.Events.Count(); i++)
        {
            foreach (MidiEvent note in midi.Events[i])
            {
                TempoEvent tempoE;

                m_loadedD += note.ToString()+ "\n";
                try { tempoE = (TempoEvent)note; tempo.Add(tempoE); 
                }
                catch { }
                if (note.CommandCode == MidiCommandCode.NoteOn)
                {
                    var t_note = (NoteOnEvent)note;
                    if (t_note != null) { 
                    var noteOffEvent = t_note.OffEvent;

                   string t = string.Format("{0}-{1}-{2}\n", t_note.NoteName,
                       t_note.NoteNumber,
                       t_note.Velocity
                       );

                    double d = (t_note.AbsoluteTime / midi.DeltaTicksPerQuarterNote) * tempo[tempo.Count() - 1].Tempo;

                  t+="--"+TimeSpan.FromSeconds(d);
                    }
                }

            }
        }
    }

}
