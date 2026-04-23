using UnityEngine;
using UnityEngine.AI;
using MP_Npc.Perception;
// | created     | 11-Apr-2026
// | change | 12-Apr-2026
// | change | 20-Apr-2026
namespace MP_Npc.Behavior
{
    public class NpcBlackboard
    {
        // owner
        //...
        public GameObject bbKeyOwnerGameObject;
        public Transform bbKeyOwnerTransform;

        public BehaviourBrain bbKeyOwnerBehaviourBrain;
        public PerceptionSystem bbKeyOwnerPerceptionSystem;

        public NavMeshAgent bbKeyOwnerNavMeshAgent;
        public NavMeshPath bbKeyAgentNavMeshPath;
        
        // behaviour tasks
        public BaseTask bbKeyCurrentTask;


        // other target / other npc / opponent / ally?
        public GameObject bbKeyTargetGameObject;
        public Transform bbKeyTargetTransform;
        public Vector3 bbKeyTargetPosition;

        public StimuliEmitter bbKeyStimuliEmitter;

        // public BlackboardKeyData[] _keysArray;
    }
}