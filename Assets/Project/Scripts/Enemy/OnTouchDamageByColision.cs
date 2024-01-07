using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class OnTouchDamageByColision : OntouchDamge
{
    public int damage;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            TakeDameOnTouch(other.gameObject,damage);
        }
    }

    /*
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            TakeDameOnTouch(other.gameObject, damage);
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            transform.GetComponent<Collider2D>().isTrigger = false;
        }
    }
    */

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            TakeDameOnTouch(other.gameObject, damage);
        }
    }
}
