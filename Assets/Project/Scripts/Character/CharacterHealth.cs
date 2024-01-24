using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
public class CharacterHealth : MonoBehaviour
{
    [Header("properties")]
    [SerializeField] private int health;
    [SerializeField] private int maxHealth;
    [Header("Ref")]
    [SerializeField] private CharacterController characterController;

    private void Awake()
    {
        health = 3;
        Rxmanager.PlayerDie.Subscribe(tmp =>
        {
            characterController.StopPhysics();
        }).AddTo(this);
    }

    private void Dead()
    {
        SoundManager.Intance.PlayVfxDead();
        Rxmanager.PlayerDie.OnNext(true);
    }

    public int  GetHealth()
    {
        return health;
    }


    public void DeductHealth(int damage)
    {
        SoundManager.Intance.PlayVfxDeducthp();
        health -= damage;
        if (health <= 0)
        {
            Dead();
        }

    }
    public void AddHealth()
    {
        if (health == maxHealth) return;
        health += 1;
    }
}
