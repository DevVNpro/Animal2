using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;
[CreateAssetMenu(fileName = "CheckPlayerInZoneHorizontal",menuName = "Data/AI/Decision/CheckPlayerInZoneHorizontal", order = 0)]
public class AIDecisionCheckPlayerInZoneHorizontal : AIDecision
{
    public override bool Decide(AIBrain _brain)
    {
       return _brain.Player.attack.CheckPlayerInZoneHorizontal();
    }
}
