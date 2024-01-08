using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("References")]
    public Moverment moverment;
    public Attack attack;
    public Health health;
    public Animator animator;
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
    }

    public void Dead()
    {
        Debug.Log("CharacterDead");
    }
}
