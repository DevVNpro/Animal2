using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ObjectMushRoom : MonoBehaviour
{
    [SerializeField] private  float JumpForce;
    [SerializeField] private  GameObject anim;
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private SimpleSound simpleSound;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            simpleSound.Play(audioClip);
            anim.transform.DOLocalMoveY(0.2f, 0.1f);
            other.GetComponent<CharacterMoverment>().rigidbody2d.velocity = new Vector2(0f,JumpForce);

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            anim.transform.DOLocalMoveY(0.5f, 0.1f);
        }
    }
}
