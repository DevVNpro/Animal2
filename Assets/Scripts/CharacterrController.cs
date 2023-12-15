using System;
using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class CharacterController : MonoBehaviour
{

    [SerializeField] private CharacterAnimation _characterAnimation;
    [SerializeField] private CharacterMoverment _characterMoverment;
    public AnimationReferenceAsset[] AnimationReferenceAsset;
    int state;
    private float time;

    void Start()
    {
        time = 0;
    }

    void FixedUpdate()
    {
#if UNITY_EDITOR
        Move();
#endif
        
    }

    private void Move()
    {
        if (Input.GetKey(KeyCode.A))
        {
            _characterMoverment.MoveLeft();
        }

        else if (Input.GetKey(KeyCode.D))
        {
            _characterMoverment.MoveRight();
        }
    }
    private void Update()
    {
        RandomAnimIdle();
        Jump();
        UpdateAnimation();
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _characterMoverment.Jump();
        }
    }

    private void RandomAnimIdle()
    {
        if (state == 0)
        {
            time += Time.deltaTime;
        }
        if (time >= 10)
        {    
            state = Random.Range(4, 6);
            Debug.Log(state);
            _characterAnimation.PlayAnimation(AnimationReferenceAsset[state],true,1);
            Debug.Log("10s");
            time = 0;
        }
    }

    private void UpdateAnimation()
    {
        #region MoveAndIdle
        
        if (_characterMoverment.rigidbody2d.velocity.x > 2f  && state != (int) MovermentState.falling  && state != (int) MovermentState.jumping  )
        {
            state = (int) MovermentState.running;

        }
        else if (_characterMoverment.rigidbody2d.velocity.x <  -2f && state != (int) MovermentState.falling&& state != (int) MovermentState.jumping)
        {

            state = (int) MovermentState.running;

        }
        else
        {
            if (state != 4 && state != 5 && (Math.Abs(_characterMoverment.rigidbody2d.velocity.y) < 0.2f)) 
            {
                state = (int) MovermentState.idle;

            }
        }
        #endregion

        #region JumpAndFall
        if(_characterMoverment.rigidbody2d.velocity.y > 2f && !_characterMoverment.isBrige)
        {
            state = (int) MovermentState.jumping;
        }
        else if(_characterMoverment.rigidbody2d.velocity.y < -1.2f) {
            state = (int) MovermentState.falling;
        }
        
        #endregion

        #region PushBox
        if (_characterMoverment.isPushBox && (_characterMoverment.rigidbody2d.velocity.x > 0.2f || _characterMoverment.rigidbody2d.velocity.x < 0.2f)) state = (int) MovermentState.PushBox;
        #endregion
        
        #region ActiveAnimation
        _characterAnimation.PlayAnimation(AnimationReferenceAsset[state], true, 1);
        #endregion 
        if((state == (int)MovermentState.falling || state == (int)MovermentState.idle) && _characterMoverment.isBrige)
        {
            state = (int)MovermentState.idle;
        }
        
    }

    #region Control

    #endregion


}
public enum MovermentState {idle, running, jumping,falling,Wait1,Wait2,PushBox }
