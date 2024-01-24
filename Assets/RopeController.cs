using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using UniRx;
using Sirenix.OdinInspector;

public class RopeController : MonoBehaviour
{

    [Header("Properties")]
    public float swingForce= 350f;
    public bool onRope = false;
    public float direction;
    public float timeSpam=0.5f;
    public int lastDiriction=0;

    [Header("reference")]
    public AnimationReferenceAsset animSwing;
    private Transform transformRope;
    public CharacterAnimation characterAnimation;
    private CharacterController characterController;
    private void Start()
    {
        characterController = transform.gameObject.GetComponent<CharacterController>();
        Rxmanager.PlayerDie.Subscribe((tmp) =>
        {
            if (onRope)
            {
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
   
            if (direction == 1)
            {
                transform.localScale = new Vector2(1, 1);
                transform.position = new Vector2(transformRope.position.x - 1.5f, transformRope.position.y - 2.5f);
                lastDiriction = 1;
            }
            else if (direction == -1)
            {
                transform.position = new Vector2(transformRope.position.x + 1.5f, transformRope.position.y - 2.5f);
                transform.localScale = new Vector2(-1, 1);
                lastDiriction = -1;
            }
            else
            {
                if (lastDiriction == 0)
                {
                    transform.localScale = new Vector2(1, 1);
                    transform.position = new Vector2(transformRope.position.x - 1.5f, transformRope.position.y - 2.5f);
                }
                else
                {
                    if (lastDiriction == 1)
                    {
                        transform.localScale = new Vector2(1, 1);
                        transform.position = new Vector2(transformRope.position.x - 1.5f, transformRope.position.y - 2.5f);
                    }
                    else if (lastDiriction == -1)
                    {
                        transform.position = new Vector2(transformRope.position.x + 1.5f, transformRope.position.y - 2.5f);
                        transform.localScale = new Vector2(-1, 1);
                    }
                }
            }
            transformRope.GetComponent<Rigidbody2D>().AddForce(Vector2.right * direction * swingForce*2);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Rope")&& timeSpam <=0)
        {
            transformRope = collision.transform;
            characterController._characterMoverment.SetKinematicBody();
            characterController._characterMoverment.TurnOffColider();
            onRope = true;

        }
    }
    IEnumerator OffRope()
    {
        if (!onRope) yield return null;
    
        characterController._characterMoverment.SetDynamicBody();
        characterController._characterMoverment.TurnOnColider();
        onRope = false;
        characterController.enabled = true;
        timeSpam = 0.6f;
       GetComponent<Rigidbody2D>().velocity = new Vector2(0,23);
        yield return null;     
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
