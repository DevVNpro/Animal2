using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoxObject : MonoBehaviour
{
    [SerializeField] private bool BlockWay;
    public LayerMask layerBlock;
    public Vector2 sizeCheck;
    public Rigidbody2D Rigidbody2D;

    private void Awake()
    {
        Rigidbody2D = transform.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        CheckBlockWay();
    }


    public void CheckBlockWay()
    {
        BlockWay = Physics2D.BoxCast(transform.position, new Vector2(sizeCheck.x, sizeCheck.y), 0f, Vector2.one, 0f, layerBlock);
        if (BlockWay)
        {
            gameObject.layer = 6;
            Rigidbody2D.bodyType = RigidbodyType2D.Static;
        }
    }

    private void OnDrawGizmos()
    {
        if (BlockWay)
        {
            Gizmos.color = new Color(1,1,0,0.5f);
            Gizmos.DrawCube(transform.position,new Vector2(sizeCheck.x, sizeCheck.y));    
        }
        else
        {
            Gizmos.color = new Color(1,0,0,0.5f);
            Gizmos.DrawCube(transform.position,new Vector2(sizeCheck.x, sizeCheck.y));   
        }
        
    }
}
