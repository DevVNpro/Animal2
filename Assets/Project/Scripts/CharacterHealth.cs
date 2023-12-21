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
        Rxmanager.PlayerDie.Subscribe(action =>
        {
            characterController.StopPhysics();
        }).AddTo(this);
    }

    private void Dead()
    {
        Rxmanager.PlayerDie.OnNext(delegate { });


    }

    public int  GetHealth()
    {
        return health;
    }


    public void DeductHealth(int damage)
    {
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
