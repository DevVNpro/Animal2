using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
public class CharacterAnimation : MonoBehaviour
{
    
    [Header("Ref")]
    private SkeletonAnimation _skeletonAnimation;

    private CharacterController _characterController;
     [SerializeField]private string animName;
     public int index;
    
    private void Awake()
    {
        _skeletonAnimation = transform.GetComponent<SkeletonAnimation>();
        _characterController = transform.GetComponent<CharacterController>();
    }

 

    public void PlayAnimation(AnimationReferenceAsset AnimationReferenceAsset, bool loop, float timeScale)
    {
        if (animName == _skeletonAnimation.skeletonDataAsset.name) return;
        _skeletonAnimation.AnimationName = AnimationReferenceAsset.name;
        _skeletonAnimation.loop = loop;
        _skeletonAnimation.timeScale = timeScale;
        animName = AnimationReferenceAsset.name;
    }

    public void PlayAnimationShot(AnimationReferenceAsset animationReferenceAsset,bool loop)
    {
        Spine.TrackEntry animationEntry = _skeletonAnimation.state.AddAnimation(1, animationReferenceAsset, loop, 0);
        animationEntry.Complete += AnimationEntry_Complete;
    }

    private void AnimationEntry_Complete(Spine.TrackEntry trackEntry) 
    {
        _skeletonAnimation.state.ClearTrack(1);
    }
}
