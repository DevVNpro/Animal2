using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (countTime >= timeDelay)
        {
            if (collision.CompareTag("Player"))
            {
                characterController = collision.gameObject.GetComponent<CharacterController>();
               if (characterController == null) return;
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
                characterController.AddForce(vertorForce, new Vector2(vectorOrigin.transform.position.x, vectorOrigin.transform.position.y));
                characterController.DeductHelth();

            }
        }
    }

}
