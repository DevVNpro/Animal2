using System.Collections;
using System.Collections.Generic;
using Com.LuisPedroFonseca.ProCamera2D.Platformer;
using MoreMountains.Tools;
using UnityEngine;

[CreateAssetMenu(fileName = "AiActionPatrolHorizontal", menuName = "Data/AI/Action/AiActionPatrolHorizontal")]
public class AiActionPatrolHorizontal : AIAction
{
    public override void PerformAction(AIBrain _brain)
    {
        _brain.Player.animator.PlayAnimation(_brain.Player.animator.animationReferenceAssets[0],true,1);
        _brain.Player.moverment.MoveHorizontal();

    }

    public override void OnEnterState(AIBrain _brain)
    {
    }

    public override void OnExitState(AIBrain _brain)
    {
    }
}
