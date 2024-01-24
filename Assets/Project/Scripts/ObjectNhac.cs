using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ObjectNhac : MonoBehaviour
{

    [SerializeField] private AudioClip music;
    private SimpleSound simpleSound;

    private void Awake()
    {
        simpleSound = transform.GetComponent<SimpleSound>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            simpleSound.Play(music); 
            transform.DOLocalMove(new Vector3(0f, -0.3f, 0f), 0.3f);
        }
    }
    

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            transform.DOLocalMove(new Vector3(0f, 0f, 0f), 0.3f);
        }
    }
}
