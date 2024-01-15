using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  UniRx;

public class healthItem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Rxmanager.UseHealth.OnNext(true);
            gameObject.SetActive(false);

        }
    }
}
