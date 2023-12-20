using System;
using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class CharacterController : MonoBehaviour
{

    [Header("Properties")] public float timeRelaxState;
    public int state;
    public bool holdButtonRight, holdButtonLeft;
    private bool isDead;


    [Header("Ref")] public CharacterAnimation _characterAnimation;
    public CharacterMoverment _characterMoverment;
    public CharacterHealth characterHealth;
    public AnimationReferenceAsset[] AnimationReferenceAsset;

    void Start()
    {
        timeRelaxState = 0;
    }

    void FixedUpdate()
    {
        if (!_characterMoverment.effectMoveByForce && !isDead)
        {
            Move();
        }
    }

    private void Move()
    {
#if UNITY_EDITOR
        if (Input.GetKey(KeyCode.A))
        {
            _characterMoverment.MoveLeft();
        }

        else if (Input.GetKey(KeyCode.D))
        {
            _characterMoverment.MoveRight();
        }
#endif
#if !UNITY_EDITOR
                if (holdButtonRight)
        {
            _characterMoverment.x = 1;
            _characterMoverment.MoveRight(); 
        }
        else
        {
            _characterMoverment.x = 0;
        }
        if (holdButtonLeft)
        {
            _characterMoverment.x = -1;
            _characterMoverment.MoveLeft();

        }
        else
        {
            _characterMoverment.x = 0;
        }
#endif
    }

    private void Update()
    {
#if UNITY_EDITOR
        Jump();
#endif
        RandomAnimIdle();
        UpdateAnimation();
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            _characterMoverment.Jump();
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            _characterMoverment.Jump(true );

        }
    }

    private void RandomAnimIdle()
    {
        if (state == (int) MovermentState.idle)
        {
            timeRelaxState += Time.deltaTime;
        }
        else
        {
            timeRelaxState = 0;
        }

        if (timeRelaxState >= 10)
        {
            state = Random.Range(4, 6);
            _characterAnimation.PlayAnimation(AnimationReferenceAsset[state], true, 1);
            timeRelaxState = 0;
        }
    }

    private void UpdateAnimation()
    {
        if (!isDead)
        {
            #region MoveAndIdle

            if (_characterMoverment.rigidbody2d.velocity.x > 2f && _characterMoverment.isGround)
            {
                state = (int) MovermentState.running;

            }
            else if (_characterMoverment.rigidbody2d.velocity.x < -2f && _characterMoverment.isGround)
            {

                state = (int) MovermentState.running;

            }
            else
            {
                if (state != 4 && state != 5 && (Math.Abs(_characterMoverment.rigidbody2d.velocity.y) < 0.5f))
                {
                    if (!_characterMoverment.isGround) return;
                    state = (int) MovermentState.idle;

                }
            }

            #endregion
            #region JumpAndFall

            if (_characterMoverment.rigidbody2d.velocity.y > 0.5f && !_characterMoverment.isBrige)
            {
                state = (int) MovermentState.jumping;
            }
            else if (_characterMoverment.rigidbody2d.velocity.y < -0.2f && !_characterMoverment.isBrige)
            {

                state = (int) MovermentState.falling;
            }

            if ((state == (int) MovermentState.jumping || state == (int) MovermentState.falling) &&
                _characterMoverment.isBrige && Math.Abs(_characterMoverment.rigidbody2d.velocity.x) < 2f)
            {
                state = (int) MovermentState.idle;

            }

            #endregion
            #region PushBox

            if (_characterMoverment.isPushBox && (Math.Abs(_characterMoverment.rigidbody2d.velocity.x) > 0.5f))
                state = (int) MovermentState.PushBox;

            #endregion
        }
        else
        {
            state = (int) MovermentState.dead;
        }

        #region ActiveAnimation

        _characterAnimation.PlayAnimation(AnimationReferenceAsset[state], true, 1);

        #endregion
        
    }
    #region Force
    public void AddForce(Vector2 vecterForce, Vector2 vectorOrigin)
    {
        _characterMoverment.AddForceTouch(vecterForce, vectorOrigin);
    }

    public void StopPhysics()
    {
        isDead = true;
        Debug.Log("isdeadtrue");
        transform.GetComponent<CapsuleCollider2D>().enabled = false;
    }
    #endregion
    
#region health
    public void DeductHelth()
    {
        characterHealth.DeductHealth();
    }
    #endregion

#region Control

#endregion
    public void Right(bool hold= false)
    {
        if (hold) {
            holdButtonRight = true;
        }
        else
        {
            holdButtonRight = false;

        }
    }
    public void Left(bool hold = false)
    {
        if (hold)
        {
            holdButtonLeft = true;
        }
        else
        {
            holdButtonLeft = false;
        }
    }
}
public enum MovermentState { idle, running, jumping, falling, Wait1, Wait2, PushBox,dead }