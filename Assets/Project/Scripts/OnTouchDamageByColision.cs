using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTouchDamageByColision : OntouchDamge
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        TakeDameOnTouch(other.gameObject);
    }
}
