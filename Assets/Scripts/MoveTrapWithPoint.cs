using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveTrapWithPoint : MonoBehaviour
{
    [Header("Ref")]
    [SerializeField] private List<GameObject> listPoint;
    public bool flagMove;
    private void Awake()
    {
        flagMove = true;
    }
    private void Update()
    {
        if (flagMove)
        {
            flagMove = false;
            transform.DOMove(listPoint[0].transform.position, 2f).OnComplete(() =>
            {
                 transform.DOMove(listPoint[1].transform.position, 2f).OnComplete(() =>
                {
                    transform.DOMove(listPoint[2].transform.position, 2f).OnComplete(() =>
                    {
                        transform.DOMove(listPoint[3].transform.position, 2f).OnComplete(() => flagMove = true);
                    });
                });
            });

        }
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            CharacterHeath characterHeath = collision.transform.GetComponent<CharacterHeath>();
            if (characterHeath == null) return;
            characterHeath.DeductHeath();
        }
    }



}
