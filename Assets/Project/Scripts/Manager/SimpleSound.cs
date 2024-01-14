using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSound : BaseAudio
{
    [SerializeField] public bool autoPlay, loop;

    private void Start()
    {
        AudioSource.loop = loop;
        if (autoPlay)
        {
            AudioSource.Play();
        }
    }
    public void Play()
    {
        AudioSource.Play();
    }

    public void Play(AudioClip clip, bool isLoop = false)
    {
        if (AudioSource.isPlaying && AudioSource.loop)
        {
            return;
        }
        AudioSource.loop = isLoop;
        AudioSource.clip = clip;
        AudioSource.Play();
    }

    public void SetLoop(bool auto)
    {
        AudioSource.loop = auto;
    }
}

