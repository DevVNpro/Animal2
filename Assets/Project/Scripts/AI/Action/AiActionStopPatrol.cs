using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;
[CreateAssetMenu(fileName = "AiActionStopPatrol", menuName ="Data/AI/Action/AiActionStopPatrol" )]
public class AiActionStopPatrol : AIAction
{
    
    public override void PerformAction(AIBrain _brain)
    {
        _brain.Player.moverment.StopPatrol();
    }

    protected override void Initialization()
    {
        base.Initialization();
    }

    protected override void OneTimeAction(AIBrain _brain)
    {
        base.OneTimeAction(_brain);
    }

    public override void OnEnterState(AIBrain _brain)
    {
        Debug.Log("EnterStopPatrol");
    }

    public override void OnExitState(AIBrain _brain)
    {
        Debug.Log("ExitsStopPatrol");
    }
}
