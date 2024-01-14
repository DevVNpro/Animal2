using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;
using Sequence = DG.Tweening.Sequence;

public class Character : MonoBehaviour
{
    [Header("References")]
    public Moverment moverment;
    public Attack attack;
    public BulletSpawnerDragon bulletSpawnerDragon;
    public Health health;
    public Animator animator;
    public AIBrain AiBrain;
    public SimpleSound simpleSound;
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
        bulletSpawnerDragon.GetReferenceCharacter(this);
        AiBrain.Init(this);
        AiBrain.ActiveBrain();
        AiBrain.ResetBrain();
        transform.GetComponent<SimpleSound>();
    }

    public void Dead()
    {
        StartCoroutine(DeadDeactive());
    }
    IEnumerator DeadDeactive()
    {
        moverment.collider2D.enabled = false;
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOMoveY(transform.position.y + 2f,0.2f).SetEase(Ease.OutSine));
        sequence.Append(transform.DOMoveY(transform.position.y -15f,0.7f).SetEase(Ease.InSine)).OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
        AiBrain.DeActiveBrain();
        yield return null;
    }
}
