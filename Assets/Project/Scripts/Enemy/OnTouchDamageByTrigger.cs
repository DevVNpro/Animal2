using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTouchDamageByTrigger : OntouchDamge
{
    public int damage;
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            TakeDameOnTouch(other.gameObject,damage);
        }
    }
}
