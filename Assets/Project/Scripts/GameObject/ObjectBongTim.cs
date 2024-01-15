using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBongTim : MonoBehaviour
{
 //   [SerializeField] private Animator animator;
    [SerializeField] private GameObject particalBumBum;
    [SerializeField] private GameObject animator;
    [SerializeField] private AudioClip soundBreak;
    [SerializeField] private AudioClip soundbuble;
    [SerializeField] private SimpleSound simpleSound;

    private void Awake()
    {
        simpleSound = transform.GetComponent<SimpleSound>();
    }

    private void OnTriggerEnter2D(Collider2D other)    
    {
        StartCoroutine(CountDownBreake());
    }

    IEnumerator CountDownBreake()
    {
        simpleSound.Play(soundbuble);
        simpleSound.Play(soundBreak);
        Instantiate(particalBumBum, transform.position, Quaternion.Euler(0f, 0f, 0f));
        animator.GetComponent<UnityEngine.Animator>().enabled = true;
        yield return  new WaitForSeconds(0.2f);
        gameObject.SetActive(false);
    }
}
