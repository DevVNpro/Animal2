using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTouchDamageByTrigger : OntouchDamge
{
    private void OnTriggerStay2D(Collider2D other)
    {
        TakeDameOnTouch(other.gameObject);
    }
}
