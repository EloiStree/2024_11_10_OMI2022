using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_AbstractMidi_DebugLineHistory : MonoBehaviour, IAbstractMidiGetHandler
{


    public UI_AbstractMidi_DebugLine[] m_lines;

    public List<object> m_queue = new System.Collections.Generic.List<object>();




    public void PushIn(IMidiNoteEventGet value)
    {
        PushObject(value);
    }
    public void PushIn(IMidiPitchChangeEventGet value)
    {
        PushObject(value);
    }
    public void PushIn(IMidiControlChangeEventGet value)
    {
        PushObject(value);
    }
    public void PushIn(IMidiPatchChangeEventGet value)
    {
        PushObject(value);
    }
    public void PushIn(IMidiShortWithSourceGet value)
    {
        PushObject(value);
    }


    public void PushIn(IMidiNoteEventGet value, in int index)
    {
        m_lines[index].PushIn(value);
    }
    public void PushIn(IMidiPitchChangeEventGet value, in int index)
    {

        m_lines[index].PushIn(value);
    }
    public void PushIn(IMidiControlChangeEventGet value, in int index)
    {

        m_lines[index].PushIn(value);
    }
    public void PushIn(IMidiPatchChangeEventGet value, in int index)
    {

        m_lines[index].PushIn(value);
    }
    public void PushIn(IMidiShortWithSourceGet value, in int index)
    {

        m_lines[index].PushIn(value);
    }

    public void PushObject( object target)
    {
        Eloi.E_GeneralUtility.ListAsQueueInsert(in target, m_lines.Length, ref m_queue);
        for (int i = 0; i < m_lines.Length; i++)
        {
            if (i < m_queue.Count)
            {
                if (m_queue[i] is IMidiNoteEventGet)
                    m_lines[i].PushIn((IMidiNoteEventGet)m_queue[i]);
                else if (m_queue[i] is IMidiPitchChangeEventGet)
                    m_lines[i].PushIn((IMidiPitchChangeEventGet)m_queue[i]);
                else if (m_queue[i] is IMidiControlChangeEventGet)
                    m_lines[i].PushIn((IMidiControlChangeEventGet)m_queue[i]);
                else if (m_queue[i] is IMidiPatchChangeEventGet)
                    m_lines[i].PushIn((IMidiPatchChangeEventGet)m_queue[i]);
                else if (m_queue[i] is IMidiShortWithSourceGet)
                    m_lines[i].PushIn((IMidiShortWithSourceGet)m_queue[i]);

            }
            else {

                m_lines[i].DisableAll();
            }
        }

    }




}
