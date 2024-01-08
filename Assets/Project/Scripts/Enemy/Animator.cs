using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class Animator : MonoBehaviour
{
    [SerializeField] private AnimationReferenceAsset[] animationReferenceAssets;
    private Character character;
    public void GetReferenceCharacter(Character character)
    {
        this.character = character;
    }
    void Update()
    {
        
    }
}
