using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveTrapWithPoint : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private float moveTime;
    public bool flagMove;
    
    [Header("Ref")]
    [SerializeField] private List<GameObject> listPoint;
    private void Awake()
    {
        flagMove = true;
    }
    private void Update()
    {
        if (flagMove)
        {
            flagMove = false;
            transform.DOMove(listPoint[0].transform.position, moveTime).OnComplete(() =>
            {
                 transform.DOMove(listPoint[1].transform.position, moveTime).OnComplete(() =>
                {
                    transform.DOMove(listPoint[2].transform.position, moveTime).OnComplete(() =>
                    {
                        transform.DOMove(listPoint[3].transform.position, moveTime).OnComplete(() => flagMove = true);
                    });
                });
            });

        }
        
    }


}
