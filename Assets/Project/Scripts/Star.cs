using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UniRx;

public class Star : MonoBehaviour
{
    private void Update()
    {
        Debug.Log(transform.position);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Rxmanager.PickStar.OnNext(transform.position);
            gameObject.SetActive(false);
        }
    }
}
