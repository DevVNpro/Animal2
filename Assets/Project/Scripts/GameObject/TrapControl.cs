using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class TrapControl : MonoBehaviour
{
    private SimpleSound simpleSound;
    public AudioClip soundOpenDoor;
    private void Awake()
    {
        simpleSound = transform.GetComponent<SimpleSound>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Switch"))
        {
            simpleSound.Play(soundOpenDoor);
            other.GetComponent<BoxCollider2D>().isTrigger = true;
            Rxmanager.SwitchTrap.OnNext(true);

        }
    }
}
