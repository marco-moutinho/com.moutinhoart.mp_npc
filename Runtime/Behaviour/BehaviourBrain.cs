using UnityEngine;
// created at  : 12-Apr-2026
// last change : 13-Apr-2026
namespace MP_Npc.Behavior
{
    // the behaviour brain class is responsible to decide the behaviour of a npc (non-playable-character);
    public class BehaviourBrain
    {
        protected NpcPersonalityData _personalityData;

        protected NpcBlackboard _blackBoard;
        protected NpcComponent _ownerNpcComponent;
        protected BaseTask _currentAction;

        protected GameObject _ownerGameObject;

        protected int _driveToAttack;
        protected int _driveToDefend;

        protected int _driveToFlee; // how to calculate this?
        protected int _driveToFight; // how to calculate this?
        
        // a more open ended drive, maybe usefull for non combat games, maybe for cozy games, or open world games
        protected int _driveToExplore;

        // use to investigate for exemple sensed stimulus
        protected int _driveToInvestigate;

        // World context
        protected bool _bIsFighting;

        // use to calculate what should do when on a survival/combat context. HOW?
        protected int _DangerLevel;

        // for exemple is being attacked, this is a "split second" event so it is considered apart of the overall danger, cause in the middle of a ex: fight maybe times that are more criticals than others
        protected int _firstDegreeDangerLevel;

        protected int _danger;

        public BehaviourBrain(in NpcComponent inNpcComponent, in NpcPersonalityData inPersonalityData, in GameObject inGameObject)
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

            // create a blackboard
            _blackBoard = new NpcBlackboard();
        }

        protected virtual void Method_BehaviorSelector()
        {
            // Combat
            // Q: where / when do the character moves?
            // A: inside the selected action! Depending on the selected action it have diferent moves requirments(directions/speeds/targets)
            if (_bIsFighting)
            {
                // attack vs defend/block
                if (_driveToAttack > _driveToDefend)
                {
                    // call atack action - select better possible attack action option
                }
                else
                {
                    if (_driveToAttack == _driveToDefend)
                    {

                    }
                }
            }


        }

        // added on 13-Apr-2026
        // start to work on 20-Apr-2026
        protected virtual void Method_ComputeDrives()
        {
            _blackBoard.bbKeyStimuliEmitter.Method_GetMenaceValue(out _DangerLevel);
        }

        // added on 20-Apr-2026
        public virtual void Method_SetBlackboardKeysOfOwnerReferences()
        {
            _blackBoard.bbKeyOwnerGameObject = _ownerGameObject;
            _blackBoard.bbKeyOwnerTransform = _ownerGameObject.transform;
            _blackBoard.bbKeyOwnerBehaviourBrain = this;

            // brain -> NpcComp -> brain -> blackboard
            _blackBoard.bbKeyOwnerPerceptionSystem = _ownerNpcComponent.Method_ReturnPerceptionSystem();
            _blackBoard.bbKeyOwnerNavMeshAgent = _ownerNpcComponent.Method_ReturnNavMeshAgent();
            
        }
    }
}