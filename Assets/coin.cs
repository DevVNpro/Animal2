using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class coin : MonoBehaviour
{
    [SerializeField] private GameObject particalSystem;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            particalSystem.SetActive(true);
            Rxmanager.CollectCoin.OnNext(true);
            transform.GetComponent<Collider2D>().enabled = false;
            transform.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

}
