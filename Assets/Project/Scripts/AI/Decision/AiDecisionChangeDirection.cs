using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;
using Sirenix.OdinInspector;
[CreateAssetMenu(fileName = "ChangeDirection", menuName = "Data/AI/Decision/ChangeDirection", order = 0)]

public class AiDecisionChangeDirection : AIDecision
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override bool Decide(AIBrain _brain)
    {
        return _brain.Player.moverment.CheckChangeDirection();
    }
}
