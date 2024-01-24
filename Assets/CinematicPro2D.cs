using System;
using System.Collections;
using System.Collections.Generic;
using Com.LuisPedroFonseca.ProCamera2D;
using UnityEngine;

public class CinematicPro2D : MonoBehaviour
{
   [SerializeField] private ProCamera2D ProCamera2D;
   public bool check = false;
   private const string KeyFisrtPlay = "KeyFisrtPlay";
   private void Start()
   {
      if(PlayerPrefs.HasKey(KeyFisrtPlay)) gameObject.SetActive(false);
      ProCamera2D.GetComponent<ProCamera2DCinematics>().OnCinematicStarted.AddListener(LockCharacter);
      ProCamera2D.GetComponent<ProCamera2DCinematics>().OnCinematicFinished.AddListener(FreeCharacter);
   }

   private void OnTriggerEnter2D(Collider2D other)
   {
      if (other.CompareTag("Player") && !check)
      {
         PlayerPrefs.SetInt(KeyFisrtPlay,0);
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
