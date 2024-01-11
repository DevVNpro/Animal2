using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;
using Sirenix.OdinInspector;
[CreateAssetMenu(fileName = "ChangeDirection", menuName = "Data/AI/Decision/ChangeDirection", order = 0)]

public class AiDecisionChangeDirection : AIDecision
{

    public override bool Decide(AIBrain _brain)
    {
        return _brain.Player.moverment.CheckChangeDirection();
    }
}
