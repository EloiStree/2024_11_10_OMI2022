using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MockMusicUI : MonoBehaviour
{
    public AudioSource m_audioSource;
    public AudioClip m_do;
    public AudioClip m_re;
    public AudioClip m_mi;
    public AudioClip m_fa;
    public AudioClip m_sol;
    public AudioClip m_la;
    public AudioClip m_si;
    public Image m_doPanel;
    public Image m_rePanel;
    public Image m_miPanel;
    public Image m_faPanel;
    public Image m_solPanel;
    public Image m_laPanel;
    public Image m_siPanel;
    public Color m_onColor = Color.green;
    public Color m_offColor = Color.gray;

    public enum Note:int{Do, Re, Mi, Fa, Sol, La, Si }

    public void Tap(Note note) {
        m_doPanel.color = m_offColor;
        m_rePanel.color = m_offColor; 
        m_miPanel.color = m_offColor; 
        m_faPanel.color = m_offColor; 
        m_solPanel.color = m_offColor;
        m_laPanel.color = m_offColor; 
        m_siPanel.color = m_offColor; 
        switch (note)
        {
            case Note.Do:
        m_doPanel.color = m_onColor;
                m_audioSource.PlayOneShot(m_do);
                break;
            case Note.Re:
        m_rePanel.color = m_onColor;
                m_audioSource.PlayOneShot(m_re);
                break;
            case Note.Mi:
        m_miPanel.color = m_onColor;
                m_audioSource.PlayOneShot(m_mi);
                break;
            case Note.Fa:
        m_faPanel.color = m_onColor;
                m_audioSource.PlayOneShot(m_fa);
                break;
            case Note.Sol:
        m_solPanel.color = m_onColor;
                m_audioSource.PlayOneShot(m_sol);
                break;
            case Note.La:
        m_laPanel.color = m_onColor;
                m_audioSource.PlayOneShot(m_la);
                break;
            case Note.Si:
        m_siPanel.color = m_onColor;
                m_audioSource.PlayOneShot(m_si);
                break;
            default:
                break;
        }

    }
}
