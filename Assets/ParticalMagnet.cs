using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ParticalMagnet : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private float timeUseMagnet;

    private Tween tween;
    private void OnEnable()
    {
        StartCoroutine(CoundDownTimeMagnet());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            tween = collision.transform.DOMove(transform.position, 0.2f);
        }
    }
    IEnumerator CoundDownTimeMagnet()
    {
        yield return new WaitForSeconds(timeUseMagnet);
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        tween.Kill();
    }
}
