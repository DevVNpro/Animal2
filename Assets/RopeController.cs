using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class RopeController : MonoBehaviour
{

    [Header("Properties")]
    public float swingForce= 45f;
    public float timeDelay = 0.1f;
    public bool onRope = false;
    public float direction;
    public bool rightFall;
    public bool leftFall;
    public float Anchorright;
    public float Anchorleft;

    [Header("reference")]
    public AnimationReferenceAsset animSwing;
    public Transform transformRope;
    public CharacterAnimation characterAnimation;
    private CharacterController characterController;
    private Transform transformCharacter;
    private void Start()
    {
        Anchorright = transformRope.position.x + 2;
        Anchorleft = transformRope.position.x - 1;
        transformCharacter = transform;
        characterController = transform.gameObject.GetComponent<CharacterController>();

    }
    private void Update()
    {
     //   direction = Input.GetAxisRaw("Horizontal");
        if (onRope)
        {
            characterAnimation.PlayAnimation(animSwing, true, 1f);
            characterController.enabled = false;
#if  UNITY_EDITOR
            if (Input.GetKey(KeyCode.W))
            {
                StartCoroutine(OffRope());
            }
            direction = Input.GetAxisRaw("Horizontal");
#endif            
            if (!rightFall && !leftFall)
            {
                transform.localScale = new Vector2(1, 1);
                transformCharacter.position = new Vector2(transformRope.position.x - 1.5f, transformRope.position.y - 2.5f);
            }
            if (direction == 1)
            {
                transform.localScale = new Vector2(1, 1);
                transformCharacter.position = new Vector2(transformRope.position.x - 1.5f, transformRope.position.y - 2.5f);
            }
            else if (direction == -1)
            {
                transformCharacter.position = new Vector2(transformRope.position.x + 1.5f, transformRope.position.y - 2.5f);
                transform.localScale = new Vector2(-1, 1);
            }
            else
            {
                if (transformRope.position.x >Anchorright)
                {
                    if(transformRope.GetComponent<Rigidbody2D>().velocity.y > 1f )
                    {
                        transformCharacter.position = new Vector2(transformRope.position.x - 1.5f, transformRope.position.y - 2.5f);
                        transform.localScale = new Vector2(1, 1);//phai
                        rightFall = true;
                        leftFall = false;

                    }
                    else if(!leftFall)
                    {
                        transformCharacter.position = new Vector2(transformRope.position.x + 1.5f, transformRope.position.y - 2.5f);
                       transform.localScale = new Vector2(-1, 1);//trai
                    }
                    else if (leftFall)
                    {
                        transformCharacter.position = new Vector2(transformRope.position.x- 1.5f , transformRope.position.y - 2.5f);
                    }

                }
                else if (transformRope.position.x < Anchorleft)
                {
                    
                    if (transformRope.GetComponent<Rigidbody2D>().velocity.y < 1f && rightFall)
                    {
                   //     transformCharacter.position = new Vector2(transformRope.position.x + 1.5f,
                   //         transformRope.position.y - 2.5f);
                    //    transform.localScale = new Vector2(-1, 1); //trai
                    Debug.Log("sssssssssssssssssssssssssssssss");

                    }
                    else
                    {
                        rightFall = false;
                        leftFall = true;

                    }

                    if (transformRope.GetComponent<Rigidbody2D>().velocity.y > 1f && leftFall)
                    {
                        transformCharacter.position = new Vector2(transformRope.position.x + 1.5f, transformRope.position.y - 2.5f);
                        transform.localScale = new Vector2(-1, 1); 
                        rightFall = false;
                        leftFall = true;

                        
                    }
                  

                if (transformRope.GetComponent<Rigidbody2D>().velocity.y < 1f && !rightFall)
                    {
                        transformCharacter.position = new Vector2(transformRope.position.x -1.5f, transformRope.position.y - 2.5f);
                        transform.localScale = new Vector2(1, 1); //phai
                    }
             
            
                }
                else
                {
                    if (rightFall )
                    {
                        
                        transformCharacter.position = new Vector2(transformRope.position.x + 1.5f, transformRope.position.y - 2.5f);
                        transform.localScale = new Vector2(-1, 1);
                    }

                    if (leftFall)
                    {
                        transformCharacter.position = new Vector2(transformRope.position.x - 1.5f, transformRope.position.y - 2.5f);
                        transform.localScale = new Vector2(1, 1);
                    }
                }
            }

            transformRope.GetComponent<Rigidbody2D>().AddForce(Vector2.right * direction * swingForce);
        }


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Rope"))
        {
            onRope = true;
            collision.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            transformRope.GetComponent<Rigidbody2D>().AddForce(Vector2.right  * swingForce*2);
            leftFall = true;
        }
    }
    IEnumerator OffRope()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        onRope = false;
        characterController.enabled = true;
        yield return new  WaitForSeconds(timeDelay);
            
    }

    #region Control
    public void Right(bool hold= false)
    {
        if (onRope)
        {
            if (hold)
            {
                direction = 1;
            }
            else
            {
                direction = 0;

            }
        }
    }
    public void Left(bool hold = false)
    {
        if (onRope)
        {
            if (hold)
            {
                direction = -1;
            }
            else
            {
                direction = 0;
            }
        }
    }

    public void jumpRope()
    {
        if (onRope)
        {
            StartCoroutine(OffRope());
        }
    }

    #endregion


}
