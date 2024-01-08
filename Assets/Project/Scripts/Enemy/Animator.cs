using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class Animator : MonoBehaviour
{
    public SkeletonAnimation _skeletonAnimation;
    [SerializeField]private string animName;

    public AnimationReferenceAsset[] animationReferenceAssets;
    private Character character;
    public void GetReferenceCharacter(Character character)
    {
        this.character = character;
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
