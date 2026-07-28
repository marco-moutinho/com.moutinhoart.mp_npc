using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
namespace MP_Npc.Behavior
{
    // | 08 Jul 2026 | Created
    // | 23 Jul 2026 | 001

    [CreateAssetMenu(fileName = "VigilanceBehavior", menuName = "[ MP_NPC ]/Behaviors/Vigilance")]
    public class VigilanceBehavior : Behavior
    {
        [Header("Vigilance Subclass")]
        public VigilanceBehaviorMod _mod;

        // OVERRIDES of base class
        public override float FuncCalculateScore()
        {
            throw new System.NotImplementedException();
        }

        public override void MfuncRunBehavior(in BehaviourBrain inBrain)
        {
            // Ask if had perceive any character
            // if so : go until min attack distance
            // if not : patrol
            bool hasVision; bool hasSound;
            bool hasPerceivedAnyEnemy = inBrain.GetBlackboard().OwnerPerceptionSystem.MfuncHasPerceptionOfSomething(out hasVision, out hasSound);


            List<GameObject> copy = inBrain.GetBlackboard().OwnerPerceptionSystem.Method_GetPerceivedGO();
            int num = copy.Count;


            //...

            // inBrain.GetBlackboard().OwnerNavMeshAgent
        }
    }
}