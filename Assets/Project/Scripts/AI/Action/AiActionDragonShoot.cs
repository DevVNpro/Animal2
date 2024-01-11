using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;

[CreateAssetMenu(fileName = "AiActionDragonShoot",menuName = "Data/AI/Action/AiActionDragonShoot", order = 0)]

public class AiActionDragonShoot : AIAction
{
    public override void PerformAction(AIBrain _brain)
    {
        _brain.Player.attack.ShootBulletDragon();
    }
}
