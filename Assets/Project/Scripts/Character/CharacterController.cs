using System;
using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;
using UniRx;
public class CharacterController : MonoBehaviour
{

    [Header("Properties")] public float timeRelaxState;
    public int state;
    public bool holdButtonRight, holdButtonLeft;
    public bool isDead;
    public bool isWin;
    public int CharaterDirection;

    [Header("Ref")] public CharacterAnimation _characterAnimation;
    public CharacterMoverment _characterMoverment;
    public CharacterController characterController;
    public CharacterHealth characterHealth;
    public BulletSpawner BulletSpawner;
    public AnimationReferenceAsset[] AnimationReferenceAsset;
    Vector3 test;
    
    
    private void Awake()
    {
        test = transform.GetComponent<CapsuleCollider2D>().bounds.size;
        Debug.Log(test);
        characterController = this;
        Rxmanager.PlayWin.Subscribe((value)=>
        {
            isWin = value;
        }).AddTo(this);
        timeRelaxState = 0;
    }



    void FixedUpdate()
    {
        if (!_characterMoverment.effectMoveByForce && !isDead&&!isWin)
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
            _characterMoverment.xValue = 1;
            _characterMoverment.MoveRight(); 
        }
       else if (holdButtonLeft)
        {
            _characterMoverment.xValue = -1;
            _characterMoverment.MoveLeft();
        }
        else
        {

            _characterMoverment.xValue = 0;
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
        CheckDirection();
    }

    public void CheckDirection()
    {
        if (transform.localScale.x == 1)
        {
            CharaterDirection = (int) CharacterDirection.Right;
        }
        else if (transform.localScale.x == -1)
        {
            CharaterDirection = (int)CharacterDirection.Left;

        }
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
        if (!isWin)
        {
            if (!isDead)
            {

                if (!_characterMoverment.isSwing)
                {


                    #region MoveAndIdle

                    if ( Mathf.Abs(_characterMoverment.rigidbody2d.velocity.x) > 2f && _characterMoverment.isGround && !(_characterMoverment.xValue ==0))
                    {
                        if (_characterMoverment.isBrige)
                        {                        
                            state = (int)MovermentState.running;

                        }
                        else if (Mathf.Abs(_characterMoverment.rigidbody2d.velocity.y) < 3f)
                        {
                            state = (int)MovermentState.running;

                        }
                    }
                    else
                    {
                        if (state != 4 && state != 5 && ((Math.Abs(_characterMoverment.rigidbody2d.velocity.y) < 0.2f && Mathf.Abs(_characterMoverment.rigidbody2d.velocity.x) < 1.8f)|| _characterMoverment.isBrige))
                        {
                            if (!_characterMoverment.isGround) return;
                            state = (int)MovermentState.idle;

                        }
                    }

                    #endregion

                    #region JumpAndFall

                    if (_characterMoverment.rigidbody2d.velocity.y > 0.5f && !_characterMoverment.isBrige)
                    {
                        state = (int)MovermentState.jumping;
                    }
                    else if (_characterMoverment.rigidbody2d.velocity.y < -0.2f && !_characterMoverment.isBrige && !_characterMoverment.isGround)
                    {

                        state = (int)MovermentState.falling;
                    }

                    if ((state == (int)MovermentState.jumping || state == (int)MovermentState.falling) &&
                        _characterMoverment.isBrige && Math.Abs(_characterMoverment.rigidbody2d.velocity.x) < 2f)
                    {
                        state = (int)MovermentState.idle;

                    }


                    #endregion

                    #region PushBox

                    if (_characterMoverment.isPushBox &&
                        (Math.Abs(_characterMoverment.rigidbody2d.velocity.x) > 0.5f) && _characterMoverment.isGround)
                        state = (int)MovermentState.PushBox;

                    #endregion

                }
                else
                {
                    state = (int)MovermentState.swing;
                }
            }
        
            else
            {
                state = (int)MovermentState.dead;
            }
        }
        else
        {
            state = (int)MovermentState.Win;
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
        transform.gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
        Debug.Log("Tat colider");
       // transform.GetComponent<CircleCollider2D>().enabled = false;
    }
    #endregion
    
#region health
    public void DeductHelth(int Damage)
    {
        characterHealth.DeductHealth(Damage);
    }
    #endregion

#region Control


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

    public void Shoot()
    {
        if (_characterMoverment.CanShot && !BulletSpawner.isShot && characterController.enabled)
        {
            SoundManager.Intance.PlayVfxShot();
            _characterAnimation.PlayAnimationShot(AnimationReferenceAsset[9],false);
            BulletSpawner.shot();

        }
    }

    public void Jump(bool active = false)
    {
        if (characterController.enabled)
        {
            _characterMoverment.Jump(active);

        }
    }
#endregion
}
public enum MovermentState { idle, running, jumping, falling, Wait1, Wait2, PushBox,dead,swing,attack,Win }
public enum CharacterDirection {Left =-1,Right=1}