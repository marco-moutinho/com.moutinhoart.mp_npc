using UnityEngine;
using UnityEngine.AI;
using MP_Npc.Perception;
using MP_Gameplay.Vita;
using System.Collections.Generic;
/// | created | 11-Apr-2026
/// | 001 | 12-Apr-2026
/// | 002 | 20-Apr-2026
/// | 003 | 23-Jun-2026
/// | 004 | 20 Jun 2026
/// | 005 | 23 Jul 2026
namespace MP_Npc.Behavior
{
    /// <summary>
    /// Extend this class to add more behaviour keys.
    /// or... make each custom state or StateMachine have its own isolated drives...
    /// </summary>
    public class NpcBlackboard
    {
        // Unity Components
        //...
        public GameObject OwnerGameObject;
        public Transform OwnerTransform;

        public NavMeshAgent OwnerNavMeshAgent;
        public NavMeshPath AgentNavMeshPath;


        // C# components Extensions
        //...
        public NpcComponent bbkOwnerNpcComponent;
        public BehaviourBrain OwnerBehaviourBrain;
        public PerceptionSystem OwnerPerceptionSystem;
        public UtilityDecider UtilityDecider;

        public UtilityDeciderData UtilityDeciderData; // set from NpcComponent

        // BEHAVIOUR
        //...


        // ...Combat context
        public float DriveToAttack;
        public float DriveToBlock;
        public float DriveToFlee;
        public float DriveToInvade; //dodge
        // ... World ctx
        public float DriveToExplore;
        public float DriveToInvestigate; // investigate refers to contexts of gather info about a situation or other idendity (character), like earing a possible enemy or search for a lost target
        
        public float DangerLevel; // use to calculate how in danger it is

        // other target / other npc / opponent / ally?
        public GameObject bbKeyTargetGameObject;
        public GameObject bbkCurrentOpponent;

        // [ MP_Gameplay Package ]
        public VitaComponent bbkeyTargetsVita;

        public List<BlackboardExtension> blackBoardExtencions;
    }
}