using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;
public class ObjectBong : MonoBehaviour
{
    [SerializeField] private GameObject particalBumBum;
    [SerializeField] private GameObject display;
    [SerializeField] private AudioClip soundBreak;
    [SerializeField] private AudioClip soundbuble;
    [SerializeField] private SimpleSound simpleSound;
    
    public bool notReused;
    [SerializeField] private Collider2D Collider2D;

    private void Awake()
    {
        simpleSound = transform.GetComponent<SimpleSound>();
        Collider2D = transform.GetComponent<Collider2D>(); 

    }

    private void OnTriggerEnter2D(Collider2D other)    
    {
        if (other.CompareTag("BulletPlayer"))
        {
            StartCoroutine(CountDownBreake());
        }
    }

    IEnumerator CountDownBreake()
    {
        if(soundBreak!= null)  {simpleSound.Play(soundBreak);}
        if(soundbuble!= null) {  simpleSound.Play(soundbuble);}
        Instantiate(particalBumBum, transform.position, Quaternion.Euler(0f, 0f, 0f));
        if (notReused)
        {
            display.GetComponent<UnityEngine.Animator>().enabled = true;
            yield return  new WaitForSeconds(0.2f);
            gameObject.SetActive(false);
        }
        else
        {
            transform.DOScale(new Vector3(0f, 0f, 0f), 0.3f).SetEase(Ease.InOutBack);
            Collider2D.enabled = false;
            yield return  new WaitForSeconds(3);
            transform.DOScale(new Vector3(1f, 1f, 1f), 0.3f).SetEase(Ease.InOutBack);
            Collider2D.enabled = true;


        }
    }
}
