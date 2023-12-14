using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
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
    [SerializeField] private LayerMask layerBox;
    [SerializeField] private LayerMask layerWall;
    public bool isGround;
    public bool isPushBox;
    public bool isWall;
    private float x;

    private void Awake()
    {
        rigidbody2d = transform.GetComponent<Rigidbody2D>();
    }

    #region  MoveAndJump
    void Update()
    {
        CheckGround();
        CheckBox();
        CheckWall();
        x = Input.GetAxisRaw("Horizontal");

    }

    private void CheckGround()
    {
        isGround = Physics2D.BoxCast(transform.position, new Vector2(1,1), 0f, Vector2.down,0.2f,layerJump);
    }

    private void CheckBox()
    {
        isPushBox = Physics2D.BoxCast(new Vector2(transform.position.x,transform.position.y+1.3f), new Vector2(2f, 1.5f), 0f, Vector2.one,0f,layerBox);
    }

    private void CheckWall()
    {
        isWall = Physics2D.BoxCast(new Vector2(transform.position.x, transform.position.y + 1.5f), new Vector2(2f, 3f), 0f, Vector2.one, 0f, layerWall);
    }
    public void MoveLeft()
    {
        if(isWall && (math.abs(rigidbody2d.velocity.y)>0.05f)) return;
        rigidbody2d.velocity = new Vector2(x*speedRun,rigidbody2d.velocity.y );
        transform.localScale = new Vector3(-1,1,1);
    }

    public void MoveRight()
    {
        if(isWall && (math.abs(rigidbody2d.velocity.y)>0.1f)) return;
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
    

    #endregion
    
    #region jumpBoxCast
    private void OnDrawGizmos()
    {
        /*
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

        if (isPushBox)
        {
            Debug.Log("PushBox");
            Gizmos.color = new Color(1, 1, 0, 0.5f);
            Gizmos.DrawCube(new Vector2(transform.position.x,transform.position.y+1.3f), new Vector2(1.8f, 1.5f));
        }
        else
        {
            Gizmos.color = new Color(1, 0, 0, 0.5f);
            Gizmos.DrawCube(new Vector2(transform.position.x,transform.position.y+1.3f), new Vector2(1.8f, 1.5f));
        }
        */
        if (isWall)
        {
            Gizmos.color = new Color(0.5f, 1, 0.5f, 0.5f);
            Gizmos.DrawCube(new Vector2(transform.position.x,transform.position.y+1.5f), new Vector2(2f, 3f)); 
        }
        else
        {
            Gizmos.color = new Color(1, 0, 0, 0.5f);
            Gizmos.DrawCube(new Vector2(transform.position.x,transform.position.y+1.5f), new Vector2(2f, 3f));
        }

        
    }
    

    #endregion

    #region OtherPhysic2d



    #endregion
    
}
