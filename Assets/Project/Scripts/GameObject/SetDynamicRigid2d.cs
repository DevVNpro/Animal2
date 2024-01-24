using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDynamicRigid2d : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;

    private void Awake()
    {
        rigidbody2D = transform.GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        }
    }
}
