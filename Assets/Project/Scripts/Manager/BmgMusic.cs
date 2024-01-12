using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BmgMusic : BaseAudio
{
    private static BmgMusic _instance;
    public static BmgMusic Instance => _instance;
    protected override void Awake()
    {
        base.Awake();
        if (_instance != null)
        {
            Destroy(this);
            return;
        }
        _instance = this;
    }
    public bool IsPlaying()
    {
        return AudioSource.isPlaying;
    }

    public bool IsPlayingClip(AudioClip clip)
    {
        return AudioSource.clip == clip;
    }

    public void Play(AudioClip clip)
    {
        PlayLoop(clip);
    }
    public new void Stop()
    {
        if (AudioSource)
        {
            AudioSource.Stop();
            AudioSource.clip = null;
        }
    }
}
