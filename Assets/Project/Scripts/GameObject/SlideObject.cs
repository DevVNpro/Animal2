using System;
using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;

public class SlideObject : MonoBehaviour
{
   [SerializeField] private AnimationReferenceAsset animationReferenceAsset;
   private CharacterController _characterController;
   private CharacterMoverment _characterMoverment;
   public bool Onslide;

   private void OnCollisionEnter2D(Collision2D other)
   {
      if (other.gameObject.CompareTag("Player") && Mathf.Abs(other.gameObject.transform.position.y - transform.position.y)>0.53f)
      {
         Onslide = true;
         _characterController = other.gameObject.GetComponent<CharacterController>();
         _characterMoverment = other.gameObject.GetComponent<CharacterMoverment>();
         other.gameObject.transform.localScale = new Vector3(1,1,1);
         _characterController._characterAnimation.PlayAnimation(animationReferenceAsset,true,1);
         _characterController.enabled = false;
      }
   }

   private void OnCollisionStay2D(Collision2D other)
   {
      if (other.gameObject.CompareTag("Player")  && Mathf.Abs(other.gameObject.transform.position.y - transform.position.y)>0.53f)
      {
         if (Input.GetKeyDown(KeyCode.W))
         {
            _characterController.enabled = true;
            _characterController.Jump(true);

         }
         float y = _characterMoverment.rigidbody2d.velocity.y; 
         _characterMoverment.rigidbody2d.velocity =new Vector2(10,y);
      }
   }
   private void OnCollisionExit2D(Collision2D other)
   {
      Onslide = false;
      _characterController.enabled = true;
   }

   #region Control

   public void JumpOnSlide(bool active = false)
   {
      if (Onslide)
      {
         _characterController.enabled = true;
         _characterController.Jump(active);
      }
   }
   

   #endregion

 
}
