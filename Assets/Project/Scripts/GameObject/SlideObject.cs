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
      if (other.gameObject.CompareTag("Player") && (transform.position.y - other.transform.position.y < -0.54f))
      {
         Debug.Log("other.gameObject.transform.position.y:" +other.gameObject.transform.position.y );
         Debug.Log(" transform.position.y:" + transform.position.y );

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
      if (other.gameObject.CompareTag("Player")&& (transform.position.y - other.transform.position.y < -0.54f))
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
      if (other.gameObject.CompareTag("Player"))
      {
         StartCoroutine(ActiveMove());
      }
   }

   IEnumerator ActiveMove()
   {
      yield return  new WaitForSeconds(0.1f);
      Onslide = false;
      _characterController.enabled = true;
      
   }

   #region Control

   public void JumpOnSlide(bool active = false)
   {
      if (Onslide)
      {
         Debug.Log("JumpSlide");
         _characterController.enabled = true;
         _characterController.Jump(active);
      }
   }
   

   #endregion

 
}
