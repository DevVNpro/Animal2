using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class TrapControl : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Switch"))
        {
            other.GetComponent<BoxCollider2D>().isTrigger = true;
            Rxmanager.SwitchTrap.OnNext(true);

        }
    }
}
