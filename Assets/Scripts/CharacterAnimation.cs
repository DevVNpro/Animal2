using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
public class CharacterAnimation : MonoBehaviour
{
    
    
    private SkeletonAnimation _skeletonAnimation;
     [SerializeField]private string animName;
    
    private void Awake()
    {
        _skeletonAnimation = transform.GetComponent<SkeletonAnimation>();
    }

 

    public void PlayAnimation(AnimationReferenceAsset AnimationReferenceAsset, bool loop, float timeScale)
    {
        if (animName == _skeletonAnimation.skeletonDataAsset.name) return;
        Debug.Log("PlayAnim");
        _skeletonAnimation.AnimationName = AnimationReferenceAsset.name;
        _skeletonAnimation.loop = loop;
        _skeletonAnimation.timeScale = timeScale;
        animName = AnimationReferenceAsset.name;
        
    }
}
