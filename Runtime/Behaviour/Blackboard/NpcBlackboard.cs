using MP_Gameplay.Vita;
using MP_Npc.Perception;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/// | 000 | 11-Apr-2026 | created
/// | 001 | 12-Apr-2026
/// | 002 | 20-Apr-2026
/// | 003 | 23-Jun-2026
/// | 004 | 20 Jun 2026
/// | 005 | 23 Jul 2026
/// | 006 | 29 Jul 2026 | added "TryGetExtension"
/// | 007 | 18 Ago 2026 | added " bbk_CurrentBehavior "
/// | 008 | 19 Ago 2026 | added " BStateMachine bbk_BSM "
namespace MP_Npc.Behavior
{
    /// <summary>
    /// This is the run time memory of a npc. Think of this like the memory section of a brain.
    /// The Behavior is on Scriptable Objects, as they can not store (run time) data, the data is stored on each individual Npc, where? On it´s memory (Blackboard).
    /// As it is introduce more behavior to the npc, I create more "Memory Boxes" ( BlackboardExtensions ), each new extension subclass it's responsible to store data for a specific
    /// behavior, like "NavigationBbExtension" was created to store only path fiding / NavMesh related stuff.
    /// </summary>
    /// 
    [Tooltip(
        "This is the run time memory of a npc. Think of this like the memory section of a brain.\r\n" +
        "The Behavior is on Scriptable Objects, as they can not store (run time) data, the data is stored on each individual Npc, where? On it´s memory (Blackboard).\r\n" +
        "As it is introduce more behavior to the npc, I create more \"Memory Boxes\" ( BlackboardExtensions ), each new extension subclass it's responsible to store data for a specific\r\n" +
        "behavior, like \"NavigationBbExtension\" was created to store only path fiding / NavMesh related stuff.")]
    public class NpcBlackboard
    {
        // Unity Components
        //...
        public GameObject bbk_OwnerGameObject;
        public Transform bbk_OwnerTransform;

        public NavMeshAgent bbk_OwnerNavMeshAgent;  // <- remove this from this class, implement on Navigation Extension
        public NavMeshPath bbk_AgentNavMeshPath;    // <- remove this from this class, implement on Navigation Extension


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

        public List<BlackboardExtension> blackBoardExtencions = new List<BlackboardExtension>(); // need to create using new, if not the list does not actualy exist

        // [ 29 Jul 2026 ] #added
        public bool TryGetExtension<T>(out T ext) where T : BlackboardExtension
        {
            if (blackBoardExtencions.Count == 0) { ext = null; return false; }
            foreach (var item in blackBoardExtencions)
            {
                if(item is T typedItem) // "Se este item for do tipo T, guarda-o em typedItem."
                {
                    ext = typedItem;
                    return true;
                }
            }


            ext = null;
            return false;
        }

        public BStateMachine bbk_BSM;
        public FBehaviourSlot bbk_CurrentBehaviourSlot;
        public Behavior bbk_CurrentBehavior;
    }
}