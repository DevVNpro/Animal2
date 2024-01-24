using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveObjectWithPoint : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private float moveTime;
    [SerializeField] private float delayTime = 0.1f;
    private Tween tween;

    public Ease Ease;
    
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
            StartCoroutine(MoveTranform());
        }
        
    }

    IEnumerator MoveTranform()
    {
        
        for(int i =0 ; i< listPoint.Capacity; i++)
        {
            tween =  transform.DOMove(listPoint[i].transform.position, moveTime).SetEase(Ease); 
            yield return  new WaitForSeconds(moveTime);
            yield return  new WaitForSeconds(delayTime);
            if (listPoint.Capacity == i + 1)
            { 
                flagMove = true;
            }
        }
    }

    private void OnDisable()
    {
        tween.Kill();
    }
}
