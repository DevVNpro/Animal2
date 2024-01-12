using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sfx : BaseAudio
{ 
    public void Play(AudioClip clip, bool isLoop = false, float delay = 0f, Action complete = null)
    {
        if (isLoop)
        {
            PlayLoop(clip);
            return;
        }
        PlayOnceShot(clip,delay,complete);
    }
}
