using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swing : MonoBehaviour
{
    [SerializeField] private CharacterMoverment _characterMoverment;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("swing"))
        {
            _characterMoverment.isSwing = true;
            Rigidbody2D rigidbody2D = other.gameObject.GetComponent<Rigidbody2D>();
           FixedJoint2D fixedJoint2D = transform.gameObject.AddComponent(typeof(FixedJoint2D)) as FixedJoint2D;
           fixedJoint2D.connectedBody = rigidbody2D;
        }
    }
}
