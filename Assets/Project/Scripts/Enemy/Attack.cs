using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{

    private Character character;
    public LayerMask LayerCanAttack;
    public bool checkCanAttackBoxHorizontal;

    [Header("BulletProperty")] [SerializeField]
    private float countTimeSpam = 1;

    [SerializeField] private float distanceCheckPlayer;

    [SerializeField] private float timeSpam = 1;
    private bool shot;
     public void GetReferenceCharacter(Character character)
    {
        this.character = character;
    }

     private void Update()
     {
         if (shot)
         {
             countTimeSpam -= Time.deltaTime;
         }

         if (countTimeSpam <= 0)
         {
             shot = false;
             countTimeSpam = timeSpam;
         }
     }


     public bool CheckPlayerInZoneHorizontal()
     {
          checkCanAttackBoxHorizontal = Physics2D.BoxCast(new Vector2(transform.position.x + distanceCheckPlayer * (int) character.moverment.characterDirection,transform.position.y),new Vector2(6,1),0f ,new Vector2(1, 1),0.1f,LayerCanAttack);
          return checkCanAttackBoxHorizontal;
     }

     public void ShootBulletDragon()
     {
         if (!shot)
         {
             shot = true;
             character.bulletSpawnerDragon.shot();
             
         }
     }

     public void DeductHpPlayer(GameObject player,int value)
     {
         player.GetComponent<CharacterHealth>().DeductHealth(value);
     }

     #region DrawGrimoz

     private void OnDrawGizmos()
     {
         if (checkCanAttackBoxHorizontal)
         {
            Gizmos.color = new Color(1,0,0,0.5f);
            Gizmos.DrawCube(new Vector2(transform.position.x + distanceCheckPlayer* (int) character.moverment.characterDirection,transform.position.y), new Vector2(6,1));
         }
         else
         {
            Gizmos.color = new Color(1,1,0,0.5f);
          //   Gizmos.DrawCube(new Vector2(transform.position.x + distanceCheckPlayer* (int) character.moverment.characterDirection,transform.position.y), new Vector2(6,1));
         }
     }

     #endregion
}
