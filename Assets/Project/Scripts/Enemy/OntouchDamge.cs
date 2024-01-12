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

     public virtual void TakeDameOnTouch(GameObject other,int damage)
    {

        if (other.transform.Find("ShieldSoftBlue").gameObject.activeSelf)
        {
            transform.GetComponent<Collider2D>().isTrigger = true;
            return;
        }
        if (countTime >= timeDelay)
        {
            SoundManager.Intance.PlayTouchEnemy();
            if (other.gameObject.CompareTag("Player"))
            {
                Debug.Log("OntouchPlayer0");
                characterController = other.gameObject.GetComponent<CharacterController>();
                characterController.AddForce(vertorForce, new Vector2(vectorOrigin.transform.position.x, vectorOrigin.transform.position.y));
                characterController.DeductHelth(damage);
                Rxmanager.DeDuctHpPlayer.OnNext(damage);
                Debug.Log("OntouchPlayer1");
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
