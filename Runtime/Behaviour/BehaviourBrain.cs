using UnityEngine;
using MP_Npc.Perception;

// created at  : 12-Apr-2026

/// | 22 May 2026 | 001
/// | 29 Jun 2026 | 002 | FuncTick
/// | 09 Jul 2026 | 003
/// | 18 Ago 2026 | 005 | create MfuncRunStateMachine
/// | 19 Ago 2026 | 006 | re factor XfuncRunStateMachine

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
            _blackBoard.bbk_OwnerGameObject = _ownerGameObject;
            _blackBoard.bbk_OwnerTransform = _ownerGameObject.transform;
            _blackBoard.OwnerBehaviourBrain = this;

            // brain -> NpcComp -> brain -> blackboard
            _blackBoard.OwnerPerceptionSystem = _ownerNpcComponent.Method_ReturnPerceptionSystem();
            _blackBoard.bbk_OwnerNavMeshAgent = _ownerNpcComponent.Method_ReturnNavMeshAgent();

            // on this line I had try to set a variable on Blackboard and at the same time to make it a validation check by set it inside a Tryget function on a if statment
            if(_ownerNpcComponent.XfuncTryGetBSM(out _blackBoard.bbk_BSM))
            {
                Debug.Log("[ MARCO ] : " + this + " : " +  _blackBoard.bbk_BSM.name);

                // Set Current Behaviour as the root Behaviour ( slot struct ), this should be the first thing the BSM runs when it first starts
                _blackBoard.bbk_CurrentBehaviourSlot = _blackBoard.bbk_BSM.XfuncReturnRootSlot();
            }
            else
            {
                Debug.LogError("[ ERROR ] : " + this + " : Method_SetBlackboardKeysOfOwnerReferences() : if(_ownerNpcComponent.XfuncTryGetBSM(out _blackBoard.bbk_BSM)) was FALSE !!!");
            }

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

            XfuncRunBSM();
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

        // [ 18 Ago 2026 ] #Added | [ 19 Ago 2026 ] #Complete Refactor
        public virtual void XfuncRunBSM()
        {
            // safety check
            if(_blackBoard.bbk_CurrentBehaviourSlot.behaviour == null)
            {
                Debug.LogError("[ MARCO ] : " + this + " : public virtual void XfuncRunBSM() : if(_blackBoard.bbk_CurrentBehaviourSlot.behaviour == null) !!!");
                // TO DO : Initialize "default" _blackBoard.bbk_CurrentBehaviourSlot.behaviour...
                //          BUT it suposed to be on line 94
            }

            EBehaviourPhase eBehaviourPhase;
            _blackBoard.bbk_CurrentBehaviourSlot.behaviour.MfuncRunBehavior(this, out eBehaviourPhase);

            /// TO DO :
            /// Like this I only set the currentBehavior a class ptr, but i need to point the struct that contain the Behaviour, and the pointers on Success and on Failure
           
            // var used to search what behaviour slot is to Transit
            Behavior nextBehaviourRef;

            switch (eBehaviourPhase)
            {

                case EBehaviourPhase.Running:
                    // Give some Debug Feedback, maybe even create a Debug in game HUD/UI
                    Debug.Log("[ MARCO ] : " + this + " : XfuncRunBSM() : EBehaviourPhase.Running...");
                    break;

                case EBehaviourPhase.Completed:
                    nextBehaviourRef = _blackBoard.bbk_CurrentBehaviourSlot.behaviourOnSucceded;
                    if(_blackBoard.bbk_BSM.XfuncTryGetSlotByBehavior(nextBehaviourRef, out _blackBoard.bbk_CurrentBehaviourSlot))
                    {
                        _blackBoard.bbk_CurrentBehavior = _blackBoard.bbk_CurrentBehaviourSlot.behaviourOnSucceded;
                    }
                    break;

                case EBehaviourPhase.Failed:
                    nextBehaviourRef = _blackBoard.bbk_CurrentBehavior = _blackBoard.bbk_CurrentBehaviourSlot.behaviourOnFailed;
                    if(_blackBoard.bbk_BSM.XfuncTryGetSlotByBehavior(nextBehaviourRef, out _blackBoard.bbk_CurrentBehaviourSlot))
                    {
                        _blackBoard.bbk_CurrentBehavior = _blackBoard.bbk_CurrentBehaviourSlot.behaviourOnFailed;
                    }
                    break;
            }
        }
    }
    // end of class
}

/// TO DO : 
/// Add a Full UI/HUD to debug a selected/focused NPC/Ai/BRAIN,
///     this could be even a game mechanic for the ExilionNexus Metaverse game project