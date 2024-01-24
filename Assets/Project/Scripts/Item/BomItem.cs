using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
public class BomItem : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Rxmanager.UseBom.OnNext(10);
            gameObject.SetActive(false);
        }
    }
}
