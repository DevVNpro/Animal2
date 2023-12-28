using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapControl : MonoBehaviour
{
    public   delegate void  OnActiceSwitch();

    public static OnActiceSwitch OnActive;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Switch"))
        {
            other.GetComponent<BoxCollider2D>().isTrigger = true;
            OnActive();
        }
    }
}
