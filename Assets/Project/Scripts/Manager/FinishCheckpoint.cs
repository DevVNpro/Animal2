using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using DG.Tweening;
public class FinishCheckpoint : MonoBehaviour
{

    [SerializeField] private List<GameObject> particalSystem;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Rxmanager.PlayWin.OnNext(true);
            StartCoroutine(ActivePartical());  
           // collision.GetComponent<CharacterController>().enabled = false;

        }
    }
    IEnumerator ActivePartical()
    {
        foreach(var partical in particalSystem)
        {
            yield return new WaitForSeconds(0.5f);
            partical.SetActive(true);
        }
    }
}
