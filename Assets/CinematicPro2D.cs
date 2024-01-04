using System;
using System.Collections;
using System.Collections.Generic;
using Com.LuisPedroFonseca.ProCamera2D;
using UnityEngine;

public class CinematicPro2D : MonoBehaviour
{
   [SerializeField] private ProCamera2D ProCamera2D;
   public bool check = false;

   private void Start()
   {
      ProCamera2D.GetComponent<ProCamera2DCinematics>().OnCinematicStarted.AddListener(LockCharacter);
      ProCamera2D.GetComponent<ProCamera2DCinematics>().OnCinematicFinished.AddListener(FreeCharacter);
   }

   private void OnTriggerEnter2D(Collider2D other)
   {
      if (other.CompareTag("Player") && !check)
      {
         Debug.Log("!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
         check = true;
         ProCamera2D.GetComponent<ProCamera2DCinematics>().Play();
      }
   }

   private void LockCharacter()
   {
      Rxmanager.LockCharater.OnNext(true);
   }

   private void FreeCharacter()
   {
      Rxmanager.FreeCharater.OnNext(true);
   }
}
