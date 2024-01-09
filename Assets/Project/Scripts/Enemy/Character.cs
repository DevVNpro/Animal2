using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using Unity.VisualScripting;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("References")]
    public Moverment moverment;
    public Attack attack;
    public Health health;
    public Animator animator;
    public AIBrain AiBrain;
    [Header("Option")] 
    public bool MoveHorizontal;
    public bool JumpHorizontal;
    public bool Fly;
    


    private void Start()
    {
        moverment.GetReferenceCharacter(this);
        health.GetReferenceCharacter(this);
        attack.GetReferenceCharacter(this);
        animator.GetReferenceCharacter(this);
        AiBrain.Init(this);
        AiBrain.ActiveBrain();
        AiBrain.ResetBrain();
    }

    public void Dead()
    {
        StartCoroutine(DeadDeactive());
    }
    IEnumerator DeadDeactive()
    {
        moverment.collider2D.enabled = false;
        moverment.AddForceDrop();
        AiBrain.DeActiveBrain();
        yield return new WaitForSeconds(3);
        gameObject.SetActive(false);
    }
}
