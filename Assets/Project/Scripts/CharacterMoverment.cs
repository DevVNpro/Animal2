using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Apple;
public class CharacterMoverment : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private float speedRun;
    [SerializeField] private float speedJump;
    public  float coyoteJumpTime;
    public float coyoteTimeCounter;
    public float bufferJumpTime;
    public float bufferJumpTimeCouter;
    [SerializeField] private float timeEffectByForce;
    public float timeEffectByForceCount;
    [SerializeField] private LayerMask layerJump;
    [SerializeField] private LayerMask layerBox;
    [SerializeField] private LayerMask layerWall;
    [SerializeField] private LayerMask layerBrige;
    public bool isGround;
    public bool isPushBox;
    public bool isWall;
    public bool isBrige;
    public bool effectMoveByForce;
    private float x;
    [Header("Referent")]
    public Rigidbody2D rigidbody2d;

    private void Awake()
    {
        timeEffectByForceCount = timeEffectByForce;
        rigidbody2d = transform.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        CheckBufferTime();
        CheckCoyoteTime();
        CheckGround();
        CheckBox();
        CheckWall();
        CheckBrige();
        ReseteffectMoveByforce();
#if UNITY_EDITOR
        x = Input.GetAxisRaw("Horizontal");
#endif
    }

    private void CheckBufferTime()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            bufferJumpTimeCouter = bufferJumpTime;
        }
        else
        {
            bufferJumpTimeCouter -= Time.deltaTime;
        }
    }
    private void CheckCoyoteTime()
    {
        if (isGround)
        {
            coyoteTimeCounter = coyoteJumpTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }
    }
    public void ReseteffectMoveByforce()
    {
        if (effectMoveByForce)
        {
            timeEffectByForceCount -= Time.deltaTime;
            if (timeEffectByForceCount <= 0)
            {
                timeEffectByForceCount = timeEffectByForce;
                effectMoveByForce = false;
            }
        }
    }
    
    #region CheckBoxCast
    private void CheckGround()
    {
        isGround = Physics2D.BoxCast(transform.position, new Vector2(1,1), 0f, Vector2.down,0.2f,layerJump);
    }
    private void CheckBrige()
    {
        isBrige = Physics2D.BoxCast(transform.position, new Vector2(1, 1), 0f, Vector2.down, 0.2f, layerBrige);
    }

    private void CheckBox()
    {
        var position = transform.position;
        isPushBox = Physics2D.BoxCast(new Vector2(position.x,position.y+1.3f), new Vector2(2f, 1.5f), 0f, Vector2.one,0f,layerBox);
    }

    private void CheckWall()
    {
        var position = transform.position;
        isWall = Physics2D.BoxCast(new Vector2(position.x, position.y + 1.7f), new Vector2(2f, 3f), 0f, Vector2.one, 0f, layerWall);
    }
    

    #endregion

    #region Moverment

        public void MoveLeft()
    {
        if (!isGround && isWall) return;
        rigidbody2d.velocity = new Vector2(x*speedRun,rigidbody2d.velocity.y );
        transform.localScale = new Vector3(-1,1,1);
    }

    public void MoveRight()
    {
        if (!isGround && isWall) return;
        rigidbody2d.velocity = new Vector2(x*speedRun,rigidbody2d.velocity.y );
        transform.localScale = new Vector3(1,1,1);
        
    }

    public void Jump(bool active = false)
    {
        if (active)
        {
            coyoteTimeCounter = 0;
        }
        else
        {
            if ( coyoteTimeCounter >0f && bufferJumpTimeCouter >0f )
            {
                rigidbody2d.velocity = (new Vector2(0, speedJump));

            }

        }
        
    }
    public void AddForceTouch(Vector2 vectorFore,Vector2 vectorOrigin)
    {
        if (transform.position.x >= vectorOrigin.x)
        {
            effectMoveByForce = true;
            rigidbody2d.AddForce(new Vector2(Math.Abs(vectorFore.x),vectorFore.y),ForceMode2D.Impulse);
            Debug.Log("Add Force Right");
        }
        else if(transform.position.x < vectorOrigin.x)
        {
            effectMoveByForce = true;
            rigidbody2d.AddForce(new Vector2(-Math.Abs(vectorFore.x), vectorFore.y), ForceMode2D.Impulse);
            Debug.Log("Add Force Left");
            
        }

    }

    #endregion

    #region DrawBoxCast
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
        
        
        if (isWall)
        {
            Gizmos.color = new Color(0.5f, 1, 0.5f, 0.5f);
            Gizmos.DrawCube(new Vector2(transform.position.x,transform.position.y+ 1.7f), new Vector2(2f, 3f)); 
        }
        else
        {
            Gizmos.color = new Color(1, 0, 0, 0.5f);
            Gizmos.DrawCube(new Vector2(transform.position.x,transform.position.y+ 1.7f), new Vector2(2f, 3f));
        }
     
    }
    

    #endregion

    #region OtherPhysic2d



    #endregion
    
}
