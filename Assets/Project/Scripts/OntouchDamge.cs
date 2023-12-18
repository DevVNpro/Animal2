using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  UniRx;
[RequireComponent(typeof(Collider2D))]
public class OntouchDamge : MonoBehaviour
{
    [Header("Properties")]
    public Vector2 vertorForce;
    public GameObject vectorOrigin;
    public float timeDelay;
    [Header("DebugTime set =0 ")]
    public float countTime;
    [Header("Ref")]
    [SerializeField] private List<OntouchDamge> ontouchDamges;
    private CharacterController characterController;



    private void Update()
    {
        countTime += Time.deltaTime;
    }

    protected virtual void TakeDameOnTouch(GameObject other)
    {

        if (countTime >= timeDelay)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                characterController = other.gameObject.GetComponent<CharacterController>();
                characterController.AddForce(vertorForce, new Vector2(vectorOrigin.transform.position.x, vectorOrigin.transform.position.y));
                Rxmanager.DeDuctHpPlayer.OnNext(true);
                if (ontouchDamges.Count > 0)
                {
                    foreach (var onTouchDamage in ontouchDamges)
                    {
                        onTouchDamage.countTime = 0;
                    }
                }
                else
                {
                    countTime = 0;
                }
            }

        }
    }


}
