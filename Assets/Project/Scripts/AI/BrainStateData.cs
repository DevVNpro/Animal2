using System.Collections.Generic;
using MoreMountains.Tools;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "BrainStateData", menuName = "Data/AI/BrainData/BrainStateData", order = 1)]
[InlineEditor]
public class BrainStateData : ScriptableObject
{
    public List<AIState> States;
}