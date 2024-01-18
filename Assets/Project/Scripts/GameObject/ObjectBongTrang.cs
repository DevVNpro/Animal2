using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ObjectBongTrang : MonoBehaviour
{
    [SerializeField] private GameObject particalBumBum;
    [SerializeField] private GameObject display;
    [SerializeField] private AudioClip soundBreak;
    [SerializeField] private AudioClip soundbuble;
    [SerializeField] private SimpleSound simpleSound;

    public bool checkDuplicate;
    [SerializeField] private CircleCollider2D circleCollider2D;

    private void Awake()
    {
        simpleSound = transform.GetComponent<SimpleSound>();
        circleCollider2D = transform.GetComponent<CircleCollider2D>();
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
        yield return  new WaitForSeconds(1.5f);
        simpleSound.Play(soundbuble);
        simpleSound.Play(soundBreak);
        Instantiate(particalBumBum, transform.position, Quaternion.Euler(0f, 0f, 0f));
        transform.DOScale(new Vector3(0f, 0f, 0f), 0.3f).SetEase(Ease.InOutBack);
        circleCollider2D.enabled = false;
        yield return  new WaitForSeconds(3f);
        transform.DOScale(new Vector3(1f, 1f, 1f), 0.3f).SetEase(Ease.InOutBack);
        circleCollider2D.enabled = true;
        checkDuplicate = true;
    }
}
