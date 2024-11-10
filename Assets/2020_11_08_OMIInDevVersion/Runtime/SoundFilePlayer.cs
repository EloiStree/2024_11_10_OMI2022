using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFilePlayer : MonoBehaviour
{
    public Queue<ExecutableFile> m_nextToPlay = new Queue<ExecutableFile>();
    public Queue<ExecutableFile> m_playInstance = new Queue<ExecutableFile>();
    public AudioSource m_audioSource;
    public Dictionary<string ,AudioClip> m_shortclip = new Dictionary<string, AudioClip>();

    public void PlayOneShot(ExecutableFile file)
    {
        m_playInstance.Enqueue(file);
    }
    public void PlayAsMain(ExecutableFile file)
    {
        m_nextToPlay.Enqueue(file);
    }

    private void Update()
    {
        if (m_nextToPlay.Count > 0)
        {
            ExecutableFile e = m_nextToPlay.Dequeue();
            StartCoroutine(loadFile(e.GetAbsolutePath(), e.GetFileName()));

        }
        if (m_playInstance.Count > 0)
        {
            ExecutableFile e = m_playInstance.Dequeue();
            StartCoroutine(loadFileAsOneShot(e.GetAbsolutePath(), e.GetFileName()));
        }
    }

    IEnumerator loadFile(string path, string name)
    {

        AudioClip myAudioClip = null;
        if (m_shortclip.ContainsKey(path))
        {
            myAudioClip = m_shortclip[path];
        }
        else
        {
            WWW www = new WWW("file://" + path);

            // while (myAudioClip.isReadyToPlay)
            yield return www;
            myAudioClip = www.GetAudioClip(false, false);
            myAudioClip.name = name;
            if (myAudioClip.length < 60)
            {
                m_shortclip.Add(path, myAudioClip);
            }
        }

        myAudioClip.name = name;
        m_audioSource.clip = myAudioClip;
        m_audioSource.Stop();
        m_audioSource.Play();

        //m_audioSource.PlayOneShot(clip);
    }

    IEnumerator loadFileAsOneShot(string path, string name)
    {
        AudioClip myAudioClip = null;
        if (m_shortclip.ContainsKey(path))
        {
            myAudioClip = m_shortclip[path];
        }
        else { 
            WWW www = new WWW("file://" + path);

            // while (myAudioClip.isReadyToPlay)
            yield return www;
             myAudioClip = www.GetAudioClip(false ,false);
            myAudioClip.name = name;
            if (myAudioClip.length < 60) { 
                m_shortclip.Add(path, myAudioClip);
            }
        }

        GameObject sound = new GameObject("Sound: "+ name);
        AudioSource scriptAudio = sound.AddComponent<AudioSource>();

        scriptAudio.clip = myAudioClip;
        scriptAudio.Play();
        Destroy(sound, myAudioClip.length + 0.05f);

        //m_audioSource.PlayOneShot(clip);
    }

    public void Play(ExecutableFile sound)
    {

        PlayOneShot(sound);
        m_audioSource.Play();
       // sound:play:Chopin
    }

    public void Pause(ExecutableFile sound)
    {
        m_audioSource.Pause();
    }

    public void Stop(ExecutableFile sound)
    {
        m_audioSource.Stop();
    }

    internal void Play()
    {
        m_audioSource.Play();
    }

    internal void Pause()
    {
        m_audioSource.Pause();
    }

    internal void SwitchPause()
    {
        if (m_audioSource.isPlaying)
            m_audioSource.Pause();
        else
            m_audioSource.Play();
    }

    internal void Stop()
    {
        m_audioSource.Stop();
    }
}
