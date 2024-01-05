using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using UniRx;
public class RopeController : MonoBehaviour
{

    [Header("Properties")]
    public float swingForce= 250f;
    public float timeDelay = 0.1f;
    public bool onRope = false;
    public float direction;
    public bool rightFall;
    public bool leftFall;
    public float Anchorright;
    public float Anchorleft;
    public float timeSpam=0.5f;

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
        Rxmanager.PlayerDie.Subscribe((tmp) =>
        {
            if (onRope)
            {
                Debug.Log("DieOnRope");
                StartCoroutine(OffRope());
            }
        }).AddTo(this);

    }
    private void Update()
    {
        timeSpam -= Time.deltaTime;

        if (onRope&& timeSpam <=0)
        {
            characterAnimation.PlayAnimation(animSwing, true, 1f);
            characterController.enabled = false;
#if  UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.W)&& timeSpam <=0)
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
            /*
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
            */
            transformRope.GetComponent<Rigidbody2D>().AddForce(Vector2.right * direction * swingForce*2);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Rope")&& timeSpam <=0)
        {
            Debug.Log("False Collider2d");
            transform.gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
                onRope = true;

        }
    }
    IEnumerator OffRope()
    {
        if (!onRope) yield return null;
        Debug.Log("Nhay neeeeeeeeeeeeee");
     //   Debug.Break();
        onRope = false;
        characterController.enabled = true;
        transform.gameObject.GetComponent<CapsuleCollider2D>().enabled = true;
     //   transform.gameObject.GetComponent<CapsuleCollider2D>().enabled = true;
        timeSpam = 0.5f;
       GetComponent<Rigidbody2D>().velocity = new Vector2(0,20);
       // onRope = false;
      //  characterController.enabled = true;
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
