using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  UniRx;

public class healthItem : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Rxmanager.UseHealth.OnNext(true);
            gameObject.SetActive(false);

        }
    }
}
