using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelCheckToBoolean : MonoBehaviour
{

    public List<PixelObservedState> m_state= new List<PixelObservedState>();
   

    public void NewDisplayLoaded(NewDisplayLoaded display) {

        for (int i = 0; i < m_state.Count; i++)
        {
            if (m_state[i].m_ref.m_displayIndex == display.m_displayIndex) {
                m_state[i].SetWithColor(display.GetPixel(
                    m_state[i].m_ref.m_leftRightPourcent,
                    m_state[i].m_ref.m_botTopPourcent));

            }

        }
    }

    public void Clear() {
        m_state.Clear();
    }
}
