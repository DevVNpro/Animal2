using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;
[CreateAssetMenu(fileName = "AiActionAttack", menuName = "Data/AI/Action/Attack")]

public class AiActionAttack : AIAction
{
   public override void PerformAction(AIBrain _brain)
   {
     Debug.Log("Attack");
   }

   public override void OnEnterState(AIBrain _brain)
   {

      Debug.Log("EnterAttack");
   }

   public override void OnExitState(AIBrain _brain)
   {
      Debug.Log("ExitAttack");
   }
}
