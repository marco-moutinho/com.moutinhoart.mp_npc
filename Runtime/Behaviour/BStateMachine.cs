using System;
using UnityEngine;
namespace MP_Npc.Behavior
{
    // [ 18 Ago 2026 ] #Created / actually start to work on it, till then was just a SO
    [CreateAssetMenu(fileName = "BStateMachine", menuName = "[ MP_NPC ]/BStateMachine")]
    public class BStateMachine : ScriptableObject
    {
        [SerializeField]
        protected FBehaviourSlot[] _BehaviourArray;
        //List<FBehaviourSlot> BehaviourList; // List<> or FBehaviourSlot[] (array) for my use case???

        // [ 19 Ago 2026 ] #Added
        public FBehaviourSlot XfuncReturnRootSlot()
        {
            if(_BehaviourArray.Length == 0)
            {
                Debug.LogError("[ MARCO ] : " + this + " : public FBehaviourSlot XfuncReturnRootSlot() : _BehaviourArray.Length == 0 !!!");
            }
            return _BehaviourArray[0];
        }

        // [ 19 Ago 2026 ] #Added
        public FBehaviourSlot[] XfuncReturnBehaviourArray()
        {
            return _BehaviourArray;
        }

        // [ 19 Ago 2026 ] #Added
        public bool XfuncTryGetSlotByBehavior(Behavior inBehaviour, out FBehaviourSlot outBehaviourSlot)
        {
            if(inBehaviour != null)
            {
                for(int i = 0; i <  _BehaviourArray.Length; i++)
                {
                    if(inBehaviour == _BehaviourArray[i].behaviour)
                    {
                        outBehaviourSlot = _BehaviourArray[i];
                        return true;
                    }
                }
            }

            Debug.LogError("[ MARCO ] : " + this + "XfuncTryGetSlotByBehavior(...) : inBehaviour is null !!!");
            outBehaviourSlot = default; // check what "default" does exactly
            return false;
        }
    }


    // [ 18 Ago 2026 ] #Created
    [Serializable]
    public struct FBehaviourSlot
    {
        public Behavior behaviour;
        public Behavior behaviourOnSucceded;
        public Behavior behaviourOnFailed;
    }

    // [ 18 Ago 2026 ] #Created
    public enum EBehaviourPhase { Running, Completed, Failed, };

    /// TO DO:
    /// [ 18 Ago 2026 ]
    /// Add a way of running a complete new/seperate BSM, maybe the best place to that without much refact is on FBehaviourSlot, by adding a new OnSucceded and OnFailed vars but of type BSM
}