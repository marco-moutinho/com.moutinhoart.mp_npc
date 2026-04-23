using System.Collections.Generic;
using UnityEngine;
// created at 04-Apr-2026
// last change 20-Apr-2026
namespace MP_Npc.Perception
{
    public abstract class NpcSense
    {
        protected PerceptionSystem _perceptionSystem;
        protected NpcPerceptionData _perceptionData;
        protected GameObject _ownerGameObject;
        protected Transform _ownerTransform;

        protected List<GameObject> _sensedGameObjects;
        protected Collider[] _OverlapedCollidersBuffer;

        protected bool _enableDebugLog = false;

        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="inPerceptionSystem"></param>
        /// <param name="inGameObject"></param>
        public NpcSense(in PerceptionSystem inPerceptionSystem, in GameObject inGameObject)
        {
            // safety check of : inPerceptionSystem input parameter
            if(inPerceptionSystem != null)
            {
                _perceptionSystem = inPerceptionSystem;
            }
            else
            {
                Debug.LogError(this + " : [ MARCO ] : NpcSense( constructor... ) : ''inPerceptionSystem'' is null !!!");
            }

            // safety check of : inPerceptionData input parameter
            if (inPerceptionSystem != null)
            {
                _perceptionData = inPerceptionSystem.Method_ReturnPerceptionData();
            }
            else
            {
                Debug.LogError(this + " : [ MARCO ] : NpcSense( constructor... ) : ''inPerceptionData'' is null !!!");
            }

            // safety check of : inGameObject input parameter
            if(inGameObject != null)
            {
                _ownerGameObject = inGameObject;
                _ownerTransform = inGameObject.transform;
            }
            else
            {
                Debug.LogError(this + " : [ MARCO ] : NpcSense( constructor... ) : ''inGameObject'' is null !!!");
            }

            // initialize arrays and Lists
            _sensedGameObjects = new List<GameObject>(_perceptionData.visionSenseData.visionBufferSize);
            _OverlapedCollidersBuffer = new Collider[_perceptionData.visionSenseData.visionBufferSize];
        }

        // added on 04-Apr-2026
        public virtual void Method_Execute()
        {
            if (_enableDebugLog)
            {
                Debug.Log(this + " : [ MARCO ] : Method_Execute() {...}");
            }
        }

        // added on 21 - Apr - 2026
        public virtual List<GameObject> Method_ReturnSensedGameObjectsList() { return _sensedGameObjects; }

        // added on 21 - Apr - 2026
        protected virtual void Method_OnEnterSense(in GameObject inGameObject)
        {
            //...
            _sensedGameObjects.Add(inGameObject);
            _perceptionSystem.Method_OnEnterPerception(inGameObject);
        }

        // added on 22 - Apr - 2026
        /// <summary>
        /// This method function should run after the sense execute function ( never before or while ).
        /// Thats why it is not implemented on this ( base class ), so any sub class od NpcSense.cs can call parent/base "Method_Execute()".
        /// </summary>
        protected virtual void Method_ExecuteLoseSenseLoop()
        {
            if (_enableDebugLog) { Debug.Log(this + "virtual void Method_ExecuteLoseSenseLoop()..."); }

            for(int i = 0;  i < _sensedGameObjects.Count; i++)
            {
                GameObject lcGoRef = _sensedGameObjects[i];

                if(lcGoRef == null)
                {
                    Debug.LogWarning(this + " : [ MARCO ] :  Method_ExecuteLoseSenseLoop() : lcGoRef is null !!!"); 
                    return;
                } // this return terminate loop completly or just this loop iteration?

                bool lcCanSense = Method_CheckIfGameObjectIsStillCanStillBeSensed(lcGoRef);

                if (lcCanSense == false)
                {
                    _perceptionSystem.Method_OnSenseLostPerception(lcGoRef);
                    _sensedGameObjects.Remove(lcGoRef); // its best to remove at index?

                }
            }

            /// IMPORTANT NOTE !
            /// every rule of lose sight should have a equivalent to be able to gain sense, if not it will create a endless conflict of enter and lost sense;
            /// this has happened to me when creating the vision sense, what happened?
            /// I had two conditions to lost sight A: be out of range/distance B: the angle between Npc sensing forward and sensed GameObject direction relative to him;
            /// But to gain sense / perception is was only needed to be on range, so every frame it enter cause it was on correct distance but then lost sense cause it my not be on the FOV / angle;
            /// Solution : so i create a new funtion, a method that return a bool value to check if can enter sense;
            
            /*
             *  does this type of comment have a difrent color of the above? 
             *  check it by switching themes!
             *  just to test...
             */
        }

        // added on 21 - Apr - 2026
        /// <summary>
        /// This functions is called by "Method_ExecuteLoseSenseLoop" on the base class of this ( NpcSense.cs ).
        /// Override this function on senses sub class so each created sense has its own lose sense logic.
        /// </summary>
        /// <param name="inGameObject"></param>
        /// <returns></returns>
        protected abstract bool Method_CheckIfGameObjectIsStillCanStillBeSensed(in GameObject inGameObject);

        // added on 22 - Apr - 2026
        /// <summary>
        /// This function should be called when...
        /// </summary>
        /// <param name="inGameObject"></param>
        /// <returns></returns>
        protected abstract bool Method_CheckIfGameObjectCanBeSensed(in GameObject inGameObject);

        // added on 20 - Apr - 2026
        public virtual void Method_DrawGizmos()
        {
            // called by PerceptionSystem.cs by the NpcComponent.cs
            //...
        }
    }
}