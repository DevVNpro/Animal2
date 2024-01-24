using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharactorDirection
{
    Right = 1,
    left = -1,
}
public class Moverment : MonoBehaviour
{
    public Rigidbody2D rigidbody2D;
    private Character character;
    public Collider2D collider2D; 
    public  CharacterDirection characterDirection;
    public LayerMask LayerMaskGround;

    private void Start()
    {
        characterDirection = CharacterDirection.Right;
        collider2D = transform.GetComponent<Collider2D>();
        rigidbody2D = transform.GetComponent<Rigidbody2D>();
    }

    public void GetReferenceCharacter(Character character)
    {
        this.character = character;
    }

    private void Update()
    {
        Debug.DrawRay(transform.position+ new Vector3(1f*(int) characterDirection,0,0f), Vector2.down * 2, Color.red,0f);
    }
    public void AddForceDrop()
    {
        rigidbody2D.AddForce(new Vector2(0, 20f));
        rigidbody2D.gravityScale = 10;
    }

    public void MoveHorizontal()
    {
        rigidbody2D.velocity = new Vector2(3*(int)characterDirection,0);
    }

    public void StopPatrol()
    {
        rigidbody2D.velocity = new Vector2(0,0);
    }
     public bool CheckChangeDirection()
    {
        bool ray = Physics2D.Raycast(transform.position + new Vector3(1 * (int) characterDirection, 0f, 0f), Vector2.down, 2f, LayerMaskGround);
        if (!ray)
        {
            if ((int)characterDirection == -1)
            {

                characterDirection = CharacterDirection.Right;
                transform.localScale = new Vector3(1, 1, 1);
                return false;
            }
            if ((int) characterDirection == 1)
            {
                characterDirection = CharacterDirection.Left;
                transform.localScale = new Vector3(-1, 1, 1);
                return false;
            }
        }

        return true;
    }
}
