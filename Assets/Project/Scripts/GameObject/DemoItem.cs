using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoItem : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }
    }
}
