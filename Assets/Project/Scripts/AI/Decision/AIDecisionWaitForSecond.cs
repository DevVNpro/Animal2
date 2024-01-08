using Sirenix.OdinInspector;
using UnityEngine;

namespace MoreMountains.Tools.Decision
{
    [CreateAssetMenu(fileName = "WaitForXSecond", menuName = "Data/AI/Decision/WaitForSecond", order = 0)]
    public class AIDecisionWaitForSecond : AIDecision
    {
        public float time;
        public bool random;
        [ShowIf(nameof(random))] public float min, max;
        
        public override bool Decide(AIBrain _brain)
        {
            var waitTime = time;
            if (random)
            {
                waitTime = Random.Range(min, max);
            }
            
            return _brain.TimeInThisState >= waitTime;
        }
    }
}