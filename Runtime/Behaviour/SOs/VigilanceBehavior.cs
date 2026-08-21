using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
namespace MP_Npc.Behavior
{
    // | 08 Jul 2026 | Created
    // | 23 Jul 2026 | 001
    // | 21 Ago 2026 | 002 | add _tolorance; Refactor / bug fix on MfuncRunBehavior; add

    [CreateAssetMenu(fileName = "VigilanceBehavior", menuName = "[ MP_NPC ]/Behaviors/Vigilance")]
    public class VigilanceBehavior : Behavior
    {
        [Header("Vigilance Subclass")]
        public VigilanceBehaviorMod _mod;

        public float searchRadius = 10f;
        public float _tolorance = 1;

        [Min(0.0f)] [Tooltip("If value == 0 { don't event wait }")]
        public float _waitTime = 0; // how to add wait time support??? cause on this class can not hold that timer cause its a "stateless" & shared object

        // OVERRIDES of base class
        public override float FuncCalculateScore()
        {
            throw new System.NotImplementedException();
        }

        public override void MfuncRunBehavior(in BehaviourBrain inBrain, out EBehaviourPhase outBehaviourPhase)
        {
            Debug.Log("[ MARCO ] : " + this + " : MfuncRunBehavior(...) : Start..");
            // safety check
            if(inBrain == null)
            {
                Debug.LogError("[ MARCO ] : " + this + " : MfuncRunBehavior(...) : in BehaviourBrain is null !!!");

                // return
                outBehaviourPhase = EBehaviourPhase.Failed;
            }

            // Ask if had perceive any character
            // if so : go until min attack distance
            // if not : patrol
            bool hasVision; bool hasSound;
            //bool hasPerceivedAnyEnemy = inBrain.GetBlackboard().OwnerPerceptionSystem.MfuncHasPerceptionOfSomething(out hasVision, out hasSound);


            // should i use a 'ref', cause i think like this i am creating a copy at every call... using a ref I can fight memory alloc by pass a reference instead creating a new copy
            // well, acording to Gemini a list is pass by ref and not a copy, what it makes sense since its a class and not a struct... I am dumb...
            List<GameObject> copy = inBrain.GetBlackboard().OwnerPerceptionSystem.Method_GetPerceivedGO();
            int num = copy.Count;
            

            // prepere the parameters to call static function
            NavigationBbExtension bbExtension;
            NpcBlackboard blackboard = inBrain.GetBlackboard();
            NavMeshAgent agent = blackboard.bbk_OwnerNavMeshAgent;

            if (blackboard.TryGetExtension<NavigationBbExtension>(out bbExtension))
            {
                if(bbExtension.bbk_HasADestinationPoint == false)
                {
                    Vector3 origin = inBrain.GetBlackboard().bbk_OwnerTransform.position;
                    XAiFunctionLibrary.TryGetRandomReachablePoint(origin, searchRadius, agent, bbExtension.bbk_NavMeshPath, out bbExtension.bbk_DestinationPosition, out bbExtension.bbk_NavMeshHit);
                    agent.SetDestination(target: bbExtension.bbk_DestinationPosition);
                    bbExtension.bbk_HasADestinationPoint = true; // dont forget to set this to true, if not, each tick will TryGetRandomReachablePoint(...) and agent.SetDestination(...)
                    
                    Debug.Log("{ MARCO } : " + this + " : after agent.SetDestination...");
                    // return Running ( only if this behaviour does handle the move to)
                    outBehaviourPhase = EBehaviourPhase.Running;
                    return;
                }
                else
                {
                    bool hasReach = XAiFunctionLibrary.HasReachedDestination(inBrain, _tolorance);
                    if(hasReach == true)
                    {
                        outBehaviourPhase = EBehaviourPhase.Completed;
                        bbExtension.bbk_HasReachDestination = true;
                        bbExtension.bbk_HasADestinationPoint = false;
                    }
                    else
                    {
                        outBehaviourPhase = EBehaviourPhase.Running;
                        bbExtension.bbk_HasReachDestination = false;
                    }
                    return;
                }

            }
            Debug.LogError("[ MARCO ] : " + this + " MfuncRunBehavior(...) : if (blackboard.TryGetExtension<NavigationBbExtension>(out bbExtension)) is FALSE !!!");
            outBehaviourPhase = EBehaviourPhase.Failed; return;
        }
    }
}