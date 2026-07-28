using System.Collections.Generic;
using UnityEngine;
// created at 20-Apr-2026
/// | 29 Apr 2026 | 001
/// | 08 Jul 2026 | 002
/// | 09 Jul 2026 | 003
/// | 23 Jul 2026 | 004
namespace MP_Npc.Perception
{
    public class PerceptionSystem : IStimulusReceiver
    {
        protected GameObject _ownerGameObject;
        protected Transform _ownerTransform;
        protected NpcPerceptionData _npcPerceptionData;

        protected List<GameObject> _perceivedGameObjectsList;

        protected VisionSense _visionSense;
        protected HearingSense _hearingSense;

        public Transform storedTransform {  get; private set; }
        public Vector3 storedPosition => storedTransform.position;

        protected bool _enableDebugMsg;
        protected bool _showGizmos = true;

        protected GlobalPerceptionSystem _globalPerceptionSystem;


        public PerceptionSystem(in NpcPerceptionData inNpcPerceptionData, in GameObject inGameObject)
        {
            // Safety check of : NpcPerceptionData
            if(inNpcPerceptionData != null)
            {
                _npcPerceptionData = inNpcPerceptionData;
            }
            else
            {
                Debug.LogError("[ MARCO ] : " + this + " : inNpcPerceptionData is null !!!");
            }

            // Safety check of : input parameter GameObject
            if(inGameObject != null)
            {
                _ownerGameObject = inGameObject;
                _ownerTransform = _ownerGameObject.transform;
                storedTransform = _ownerTransform;
            }
            else
            {

            }

            // initialize array
            _perceivedGameObjectsList = new List<GameObject>(_npcPerceptionData.perceivedBufferSize);

            if(inNpcPerceptionData.hasVision == true)
            {
                // create vision sense
                _visionSense = new VisionSense(inPerceptionSystem: this, inGameObject: _ownerGameObject);
            }
            
            if(inNpcPerceptionData.hasHearing == true)
            {
                // create hearing sense
                _hearingSense = new HearingSense(this, _ownerGameObject);
            }
        }

        // added on 20 - Apr - 2026
        public NpcPerceptionData Method_ReturnPerceptionData()
        {
            if(_npcPerceptionData != null)
            {
                return _npcPerceptionData;
            }
            else { return null; }
        }

        public virtual void Method_ExecutePerceptionSystem()
        {
            _visionSense.Method_Execute();

            if (_enableDebugMsg)
            {
                Debug.Log(this + " : _perceivedGameObjectsList.Count = " + _perceivedGameObjectsList.Count);
            }
        }

        // added on 20-Apr-2026
        public virtual void Method_OnEnterPerception(in GameObject inGameObject)
        {
            if (_enableDebugMsg) { Debug.Log(this + " : [ MARCO ] : Method_OnEnterPerception(in GameObject inGameObject);"); }

            if (_perceivedGameObjectsList.Contains(inGameObject) == false)
            {
                _perceivedGameObjectsList.Add(inGameObject);
            }
            else
            {

            }
        }

        // added on 22 - Apr - 2026
        /// <summary>
        /// Call this function from any sense when the sense lost perception of a already sensed game object.
        /// </summary>
        /// <param name="inGameObject"></param>
        public virtual void Method_OnSenseLostPerception(in GameObject inGameObject)
        {
            if (_enableDebugMsg) { Debug.Log(this + "virtual void Method_OnSenseLostPerception(in GameObject inGameObject);"); }
            //Debug.Log(this + "virtual void Method_OnSenseLostPerception(in GameObject inGameObject);");

            if (_perceivedGameObjectsList.Contains(inGameObject))
            {
                _perceivedGameObjectsList.Remove(inGameObject);
            }
            else
            {
                Debug.LogError(this + " : [ MARCO ] : Method_OnSenseLostPerception('inGameObject') : _perceivedGameObjectsList does not contain received GameObject reference !!!");
            }
        }

        // addded on 21 - Apr - 2026
        public void Method_ReturnPerceivedGO(out List<GameObject> outList)
        {
            outList = _perceivedGameObjectsList;
        }

        // [ 17 - May - 2026 ] #Added
        public VisionSense Method_ReturnVisionSense() // I add this function so i can on behavior tasks acess what vision had sensed; maybe better to just has a list of each sense sensed GO? but if so need to handle it
        {
            return _visionSense;
        }

        // [ 09 Jul 2026 ] #Added
        public bool MfuncHasPerceptionOfSomething(out bool outHasSee, out bool outHasHear)
        {
            // General bool;
            bool returnValue;
            if (_perceivedGameObjectsList.Count > 0) { returnValue = true; }
            else { returnValue = false; }

            // vision bool...
            if(_visionSense.Method_ReturnSensedGameObjectsList().Count > 0) { outHasSee = true; }
            else {  outHasSee = false; }

            // sound bool
            if(_hearingSense.Method_ReturnSensedGameObjectsList().Count > 0) { outHasHear = true; }
            else { outHasHear = false; }

            return returnValue;
        }
        // GIZMOS
        public virtual void Method_DrawPerceptionGizmos()
        {
            if (_showGizmos)
            {
                _visionSense.Method_DrawGizmos();
            }
            
            //for(int i = 0; i < _perceivedGameObjectsList.Count;i++)
            //{
            //    if (_perceivedGameObjectsList[i] != null)
            //    {
            //        Gizmos.color = _npcPerceptionData.gizmoColorDetection;
            //        Gizmos.DrawWireSphere(_perceivedGameObjectsList[i].transform.position, 1.5f);
            //        Gizmos.DrawLine(_ownerGameObject.transform.position, _perceivedGameObjectsList[i].transform.position);
            //    }
            //}
        }
        // IStimulusReceiver | IStimulusReceiver | IStimulusReceiver | IStimulusReceiver | IStimulusReceiver | IStimulusReceiver | IStimulusReceiver | IStimulusReceiver | IStimulusReceiver | IStimulusReceiver |

        // implemented at 29 - Apr - 2026
        public void IMethod_ReceiveSoundStimuli(in StSoundStimuli inSountStimuli)
        {
            _hearingSense.Method_ReceiveSoundStimuli(inSountStimuli);
        }

        public List<GameObject> Method_GetPerceivedGO() // [ 23 Jul 2026 ] #Added
        {
            return _perceivedGameObjectsList;
        }
    }
}