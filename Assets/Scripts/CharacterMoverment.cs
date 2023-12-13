using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Apple;

public class CharacterMoverment : MonoBehaviour
{
    [Header("Referent")]
     public Rigidbody2D rigidbody2d;
    [Header("Properties")]
    [SerializeField] private float speedRun;
    [SerializeField] private float speedJump;
    [SerializeField] private LayerMask layerJump;
    public bool isGround;
    private float x;

    private void Awake()
    {
        rigidbody2d = transform.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        CheckGround();
        x = Input.GetAxisRaw("Horizontal");
    }

    private void CheckGround()
    {
        isGround = Physics2D.BoxCast(transform.position, new Vector2(1,1), 0f, Vector2.down,0.2f,layerJump);
    }
    public void MoveLeft()
    {
        rigidbody2d.velocity = new Vector2(x*speedRun,rigidbody2d.velocity.y );
        transform.localScale = new Vector3(-1,1,1);
        
    }

    public void MoveRight()
    {
        rigidbody2d.velocity = new Vector2(x*speedRun,rigidbody2d.velocity.y );
        transform.localScale = new Vector3(1,1,1);
        
    }

    public void Jump()
    {
        if (isGround )
        {
            rigidbody2d.velocity = (new Vector2(0, speedJump));
        }
        
        
    }


    //Debug Boxcast jump
    private void OnDrawGizmos()
    {
        if (isGround)
        {
            Gizmos.color = new Color(1, 1, 0, 0.5f);
            Gizmos.DrawCube(transform.position, new Vector2(1, 1));
        }
        else
        {
            Gizmos.color = new Color(1, 0, 0, 0.5f);
            Gizmos.DrawCube(transform.position, new Vector2(1, 1));
        }

    }
}
