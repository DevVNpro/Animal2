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
    public float timeSmokeRun;
    public float timeSmokeRunCouter;

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
    public bool isSwing;
    public bool CanShot;
    public bool effectMoveByForce;
    public float xValue;
    private bool isBlockMoverMent0;
    private bool isBlockMoverMent1;
    private bool isBlockMoverMent2;
    private bool isBlockMoverMent3;
    private bool isBlockMoverMent4;
    bool enableSmoke = true; 
    bool enableFallSmoke; 
    bool checkduplicate = true;



    [Header("Referent")]
    public Rigidbody2D rigidbody2d;
    public CharacterController CharacterController;
    [SerializeField] private GameObject runSmoke;
    [SerializeField] private GameObject jumpSmoke;


    private void Awake()
    {
        timeSmokeRunCouter = timeSmokeRun;
        timeEffectByForceCount = timeEffectByForce;
        rigidbody2d = transform.GetComponent<Rigidbody2D>();
        CharacterController = transform.GetComponent<CharacterController>();
    }

    void Update()
    {
        CheckFallParticle();
        CheckBufferTime();
        CheckCoyoteTime();
        CheckGround();
        CheckBox();
        CheckWall();
        CheckBrige();
        DebuRayJump();
        ReseteffectMoveByforce();
        CountDownTimeSmoke();
        IntantiateFallGround();
#if UNITY_EDITOR
        xValue = Input.GetAxisRaw("Horizontal");
        if (xValue == 1)
        {
            CharacterController.holdButtonRight = true;
        }
        else if (xValue == -1)
        {
            CharacterController.holdButtonLeft = true;
        }
        else
        {
            CharacterController.holdButtonLeft = false;
            CharacterController.holdButtonRight = false;

        }

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
    public void CheckBufferTimeButton(bool Active)
    {
        if (Active)
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
        if (Mathf.Abs(rigidbody2d.velocity.y) > 5f) isGround = false;
    }

    private void CheckFallParticle()
    {
        enableFallSmoke = isGround;
        if (!isGround)
        {
            checkduplicate = false;
        }
        
    }
    private void CheckBrige()
    {
        isBrige = Physics2D.BoxCast(transform.position, new Vector2(1, 1), 0f, Vector2.down, 0.2f, layerBrige);
    }

    private void CheckBox()
    {
        var position = transform.position;
        isPushBox = Physics2D.Raycast(new Vector2(position.x, position.y + 1.1f),
            Vector2.right * CharacterController.CharaterDirection, 1, layerBox);
        if (isPushBox)
        {
            CanShot = false;
        }
        else
        {
            CanShot = true;
        }
    }

    private void CheckWall()
    {
        var position = transform.position;
        isBlockMoverMent0 =  Physics2D.Raycast(new Vector2(position.x, position.y+2.5f), Vector2.right *CharacterController.CharaterDirection,1, layerWall);
        isBlockMoverMent1 = Physics2D.Raycast(new Vector2(position.x, position.y+2f), Vector2.right *CharacterController.CharaterDirection,1, layerWall);
        isBlockMoverMent2 = Physics2D.Raycast(new Vector2(position.x, position.y+1.5f), Vector2.right *CharacterController.CharaterDirection,1, layerWall);
        isBlockMoverMent3 = Physics2D.Raycast(new Vector2(position.x, position.y+1f), Vector2.right *CharacterController.CharaterDirection,1, layerWall);
        isBlockMoverMent4 = Physics2D.Raycast(new Vector2(position.x, position.y+0.1f), Vector2.right *CharacterController.CharaterDirection,1, layerWall);
        isWall = (isBlockMoverMent0||isBlockMoverMent1 || isBlockMoverMent2 || isBlockMoverMent3 || isBlockMoverMent4);
        //    BoxCast(new Vector2(position.x, position.y + 1.7f), new Vector2(2f, 3f), 0f, Vector2.one, 0f, layerWall);
    }
    

    #endregion
    

    #region VfxInstantiate
    public void CountDownTimeSmoke()
    {
        if (!enableSmoke)
        {
            timeSmokeRunCouter -= Time.deltaTime;
            if(timeSmokeRunCouter <= 0)
            {
                enableSmoke = true;
                timeSmokeRunCouter = timeSmokeRun;
            }
        }
    }
    private void IntantiateSmoke()
    {
        if (enableSmoke)
        {
            Vector2 vector2 = new Vector2(transform.position.x,transform.position.y +0.2f);
            Instantiate(runSmoke, vector2, Quaternion.EulerRotation(0f, 0f, 0f));
            enableSmoke = false;

        }

    }

    private void IntantiateFallGround()
    {
        if (enableFallSmoke && !checkduplicate && !CharacterController.RopeController.onRope)
        {
            if(rigidbody2d.velocity.y != 0) return;
            Vector2 vector2 = new Vector2(transform.position.x,transform.position.y -0.7f);
            Instantiate(jumpSmoke, vector2, Quaternion.EulerRotation(0f, 0f, 0f));
            enableFallSmoke = false;
            checkduplicate = true;
        }
    }
    #endregion
  
    #region Moverment

    public void MoveLeft()
    {
        transform.localScale = new Vector3(-1,1,1);
        if ((!isGround && isWall)||(isWall&& CharacterController.holdButtonLeft&&!isPushBox)) return;
        rigidbody2d.velocity = new Vector2(xValue*speedRun,rigidbody2d.velocity.y );
        if (isGround)
        {
            IntantiateSmoke();

        }
    }

    public void MoveRight()
    {
        transform.localScale = new Vector3(1,1,1);
        if ((!isGround && isWall)||(isWall&& CharacterController.holdButtonRight&&!isPushBox)) return;
        rigidbody2d.velocity = new Vector2(xValue*speedRun,rigidbody2d.velocity.y );
        if (isGround)
        {
            IntantiateSmoke();

        }

    }

    public void Jump(bool active = false)
    {
        Debug.Log("JumpCharacter");
        
        if (active)
        {
            coyoteTimeCounter = 0;
        }
        else
        {
            if ( coyoteTimeCounter > 0f && !effectMoveByForce && !CharacterController.isDead && !CharacterController.isWin)
            {
                SoundManager.Intance.PlayVfxJump();
                rigidbody2d.velocity = (new Vector2(0, speedJump));

            }

        }
        
    }
    public void AddForceTouch(Vector2 vectorFore,Vector2 vectorOrigin)
    {
        rigidbody2d.velocity= new Vector2(0f,0f);
        if (transform.position.x >= vectorOrigin.x)
        {
            effectMoveByForce = true;
            rigidbody2d.AddForce(new Vector2(Math.Abs(vectorFore.x),vectorFore.y),ForceMode2D.Impulse);
        }
        else if(transform.position.x < vectorOrigin.x)
        {
            effectMoveByForce = true;
            rigidbody2d.AddForce(new Vector2(-Math.Abs(vectorFore.x), vectorFore.y), ForceMode2D.Impulse);
            
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
        
        
      //  if (isWall)
       // {
     //       Gizmos.color = new Color(0.5f, 1, 0.5f, 0.5f);
     //       Gizmos.DrawCube(new Vector2(transform.position.x,transform.position.y+ 1.7f), new Vector2(2f, 3f)); 
     //   }
     //   else
    //    {
      //      Gizmos.color = new Color(1, 0, 0, 0.5f);
     //       Gizmos.DrawCube(new Vector2(transform.position.x,transform.position.y+ 1.7f), new Vector2(2f, 3f));
      //  }
     
    }

    private void DebuRayJump()
    {
        if (isWall)
        {
            Debug.DrawRay(new Vector2(transform.position.x, transform.position.y+2.5f),Vector2.right*CharacterController.CharaterDirection,Color.black);
            Debug.DrawRay(new Vector2(transform.position.x, transform.position.y+2f),Vector2.right *CharacterController.CharaterDirection,Color.black);
            Debug.DrawRay(new Vector2(transform.position.x, transform.position.y+1.5f),Vector2.right *CharacterController.CharaterDirection,Color.black);
            Debug.DrawRay(new Vector2(transform.position.x, transform.position.y+1f),Vector2.right *CharacterController.CharaterDirection,Color.black);
            Debug.DrawRay(new Vector2(transform.position.x, transform.position.y+0.1f),Vector2.right*CharacterController.CharaterDirection,Color.black);


        }
        else
        {
            Debug.DrawRay(new Vector2(transform.position.x, transform.position.y+2.5f),Vector2.right*CharacterController.CharaterDirection,Color.blue);
            Debug.DrawRay(new Vector2(transform.position.x, transform.position.y+2f),Vector2.right*CharacterController.CharaterDirection,Color.blue);
            Debug.DrawRay(new Vector2(transform.position.x, transform.position.y+1.5f),Vector2.right*CharacterController.CharaterDirection,Color.blue);
            Debug.DrawRay(new Vector2(transform.position.x, transform.position.y+1f),Vector2.right*CharacterController.CharaterDirection,Color.blue);
            Debug.DrawRay(new Vector2(transform.position.x, transform.position.y+0.1f),Vector2.right*CharacterController.CharaterDirection,Color.blue);

        }
    }
    
    #endregion

    #region OtherPhysic2d
    public void SetKinematicBody()
    {
        rigidbody2d.bodyType = RigidbodyType2D.Kinematic;
        rigidbody2d.velocity = new Vector2(0, 0);

    }
    public void SetDynamicBody()
    {
        rigidbody2d.bodyType = RigidbodyType2D.Dynamic;
    }
    public void TurnOnColider()
    {
        transform.GetComponent<CapsuleCollider2D>().enabled = true;
    }
    public void TurnOffColider()
    {
        transform.GetComponent<CapsuleCollider2D>().enabled = false;
    }


    #endregion

}
