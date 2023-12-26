using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class RopeController : MonoBehaviour
{

    [Header("Properties")]
    public float swingForce= 10f;
    public float timeDelay = 0.4f;
    public bool onRope = false;
    public float direction;

    [Header("reference")]
    public AnimationReferenceAsset animSwing;
    private CharacterController characterController;
    public CharacterAnimation characterAnimation;
    private Transform transformCharacter;
    private Transform transformRope;

    private void Start()
    {
        transformCharacter = transform;
        characterController = transform.gameObject.GetComponent<CharacterController>();

    }


    private void Update()
    {
        direction = Input.GetAxisRaw("Horizontal");
        if (onRope)
        {
            characterAnimation.PlayAnimation(animSwing, true, 0.7f);
            characterController.enabled = false;
         //   transformCharacter.position = new Vector2 (transformRope.position.x + 2*direction,transformRope.position.y-2.5f); 
            if (Input.GetKey(KeyCode.W))
            {
                StartCoroutine(OffRope());
            }
            direction = Input.GetAxisRaw("Horizontal");
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
                if (transformRope.position.x >=40)
                {
                    if(transformRope.GetComponent<Rigidbody2D>().velocity.y > 1f)
                    {
                        transformCharacter.position = new Vector2(transformRope.position.x - 1.5f, transformRope.position.y - 2.5f);
                        transform.localScale = new Vector2(1, 1);

                    }
                    else
                    {
                        transformCharacter.position = new Vector2(transformRope.position.x + 1.5f, transformRope.position.y - 2.5f);
                        transform.localScale = new Vector2(-1, 1);

                    }

                }
                else
                {
                    if (transformRope.GetComponent<Rigidbody2D>().velocity.y < 1f)
                    {
                        transformCharacter.position = new Vector2(transformRope.position.x - 1.5f, transformRope.position.y - 2.5f);
                        transform.localScale = new Vector2(1, 1);

                    }
                    else
                    {

                        transformCharacter.position = new Vector2(transformRope.position.x + 1.5f, transformRope.position.y - 2.5f);
                        transform.localScale = new Vector2(-1, 1);

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
     
            transformRope = collision.transform;
            if (direction == 1)
            {
                transform.localScale = new Vector2(1, 1);
                transformCharacter.position = new Vector2(transformRope.position.x - 2f, transformRope.position.y - 2.5f);
            }
            else if (direction == -1)
            {
                transformCharacter.position = new Vector2(transformRope.position.x + 2f, transformRope.position.y - 2.5f);
                transform.localScale = new Vector2(-1, 1);
            }
            onRope = true;

        }
    }
    IEnumerator OffRope()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        onRope = false;
        characterController.enabled = true;
        yield return new  WaitForSeconds(0.2f);
            
    }
}
