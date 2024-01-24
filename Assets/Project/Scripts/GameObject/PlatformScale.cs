using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;
public class PlatformScale : MonoBehaviour
{
    [SerializeField] private GameObject particalBumBum;
    [SerializeField] private UnityEngine.Animator animator;
    [SerializeField] private AudioClip soundBreak;
    [SerializeField] private AudioClip soundbuble;
    [SerializeField] private SimpleSound simpleSound;
    [SerializeField] private Collider2D Collider2D;
    public bool checkDuplicate;
    public bool useAnimator;
    
    
    private void Awake()
    {
        simpleSound = transform.GetComponent<SimpleSound>();
        Collider2D = transform.GetComponent<Collider2D>();
        checkDuplicate = true;

    }

 
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") &&checkDuplicate)
        {
            if (((other.transform.position.y - other.gameObject.GetComponent<CapsuleCollider2D>().bounds.size.y / 2) >=
                 transform.position.y-1.3f))
            {
                checkDuplicate = false;
                StartCoroutine(CountDownBreake());
            }
        }
    }

    IEnumerator CountDownBreake()
    {
        if (useAnimator)
        {
            if (animator != null)
            {
                animator.enabled = true;
            }
        }
        yield return  new WaitForSeconds(1.5f);
        if (useAnimator)
        {
            if (animator != null)
            {
                animator.enabled = false;
                animator.transform.rotation = Quaternion.Euler(0f,0f,0f);
            }
        }
        if(soundbuble != null) {simpleSound.Play(soundbuble);}
        if(soundBreak != null){  simpleSound.Play(soundBreak); }
        Instantiate(particalBumBum, transform.position, Quaternion.Euler(0f, 0f, 0f));
        transform.DOScale(new Vector3(0f, 0f, 0f), 0.3f).SetEase(Ease.InOutBack);
        Collider2D.enabled = false;
        yield return  new WaitForSeconds(3f);
        transform.DOScale(new Vector3(1f, 1f, 1f), 0.3f).SetEase(Ease.InOutBack);
        Collider2D.enabled = true;
        checkDuplicate = true;
    }
}
