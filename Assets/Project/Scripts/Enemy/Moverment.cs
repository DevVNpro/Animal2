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

    public void MoveHorizontal()
    {
        character.animator.PlayAnimation(character.animator.animationReferenceAssets[0],true,1);
        rigidbody2D.velocity = new Vector2(3*(int)characterDirection,0);
        bool Ray = Physics2D.Raycast(transform.position + new Vector3(1 * (int) characterDirection, 0f, 0f), Vector2.down, 1f, LayerMaskGround);
        if (!Ray)
        {
            if ((int)characterDirection == -1)
            {
                characterDirection = CharacterDirection.Right;
            }
            else if ((int) characterDirection == 1)
            {
                characterDirection = CharacterDirection.Left;

            }
        }
    }
        public bool CheckChangeDirection()
    {
        return false;
    }
}
