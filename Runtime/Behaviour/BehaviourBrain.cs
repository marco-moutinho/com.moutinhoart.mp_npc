using UnityEngine;
using MP_Npc.Perception;

// created at  : 12-Apr-2026

/// | 22 May 2026 | 001
/// | 29 Jun 2026 | 002 | FuncTick
/// | 09 Jul 2026 | 003

namespace MP_Npc.Behavior
{
    // the behaviour brain class is responsible to decide the behaviour of a npc (non-playable-character);

    /// <summary>
    /// NPC core behaviour center unit of processing. It is essencial a Manager for all Npc related systems. Like a manager or organizer.
    /// It is responsible to create and build "sub systems" like Perception System and Utility Decidir. I decided to struct like this only be more easy to change stuff.~Seperation of concerns.
    /// </summary>
    /// <remarks>[ Central Unit of Behaviour ]</remarks>
    public class BehaviourBrain
    {
        // of Owner
        //...
        protected NpcPersonalityData _personalityData;
        protected NpcComponent _ownerNpcComponent;

        protected PerceptionSystem _ownerPerceptionSystem; // Perception system is created and owned by the NpcComponent not this class

        // owns :
        //...
        protected NpcBlackboard _blackBoard;
        protected UtilityDecider _utilityDecider;
        //protected StWorldContext _worldContext; // <- should this be only on Blackboard ?

        protected GameObject _ownerGameObject;

        public BehaviourBrain(in NpcComponent inNpcComponent, in NpcPersonalityData inPersonalityData, in GameObject inGameObject, in PerceptionSystem inPerceptionSystem)
        {
            // safety check of : input parameter of type : NpcComponent
            if (inNpcComponent != null)
            {
                _ownerNpcComponent = inNpcComponent;
            }
            else
            {
                Debug.LogError(this + " : [ MARCO ] : BehaviourBrain(constructor...) : inNpcComponent is null !!!");
            }

            // safety check of : set personality data
            if (inPersonalityData != null)
            {
                _personalityData = inPersonalityData;
            }
            else
            {
                Debug.LogError(this + " : [ MARCO ] : BehaviourBrain(constructor...) : inPersonalityData is null !!!");
            }

            // safety check of : in GameObject
            if (inGameObject != null)
            {
                _ownerGameObject = inGameObject;
            }
            else { Debug.LogError(this + " : [ MARCO ] : BehaviourBrain(constructor...) : inGameObject is null !!!"); }

            if (inPerceptionSystem == null) { Debug.LogError(this + " : BehaviourBrain(constructor...) : inPerceptionSystem is null !!!"); }
            else { _ownerPerceptionSystem = inPerceptionSystem; }

            // create a blackboard
            _blackBoard = new NpcBlackboard();

            // create the Utility Decider
            _utilityDecider = new UtilityDecider(_blackBoard);
        }

        // added on 20-Apr-2026
        public virtual void Method_SetBlackboardKeysOfOwnerReferences()
        {
            _blackBoard.OwnerGameObject = _ownerGameObject;
            _blackBoard.OwnerTransform = _ownerGameObject.transform;
            _blackBoard.OwnerBehaviourBrain = this;

            // brain -> NpcComp -> brain -> blackboard
            _blackBoard.OwnerPerceptionSystem = _ownerNpcComponent.Method_ReturnPerceptionSystem();
            _blackBoard.OwnerNavMeshAgent = _ownerNpcComponent.Method_ReturnNavMeshAgent();

        }

        // [ 22 - May - 2026 ] #Created
        public virtual void ProcessPerceivedCharacters()
        {
            /// processa o GO sensitidos pelo sistema de percepção, de forma a perceber se existe alguma ameaça;
            /// ao analizar o que os outros "Characters" estão a fazer... e o que são, pois, deve ser possivel um AI perceber se por exemplo se existe um outro character que possa ser hostil a ele
            /// se forma a querer o evitar ou atacar de surpresa.
        }

        // [ 22 - May - 2026 ] #Created
        public virtual void ProcessStimuli()
        {
            /// processa o que sentiu, este script é o cerebro e é neste script, ou pelo menos atraves deste script que se processa o que acontece com o que se sabe/sente
        }

        // [ 25 - May - 2026 ] #Created
        public NpcBlackboard GetBlackboard()
        {
            if (_blackBoard != null) { return _blackBoard; }
            else { Debug.LogError(this + " : public NpcBlackboard GetBlackboard() :  _blackboard is null !!!"); return null; ; }
        }

        public virtual void ExecuteTick()
        {
            //_utilityDecider.CalculateStatesScore();
        }

        // [ 09 Jul 2026 ] #Added

        /// <summary>
        /// This function should work like a 'notification'
        /// </summary>
        /// <param name="inGameObjects"></param>
        public virtual void MFuncOnPerceptionEnter(in GameObject inGameObjects)
        {
            // interrupt something?
        }

        // [ 10 Jul 2026 ] #Added
        public virtual bool MfuncHasPerceivedAnyEnemy()
        {
            return false; // temp
        }
    }
    // end of class
}   