using System.Collections.Generic;
using UnityEngine;
// created at 20-Apr-2026
namespace MP_Npc.Perception
{
    public class PerceptionSystem
    {
        protected GameObject _ownerGameObject;
        protected Transform _ownerTransform;
        protected NpcPerceptionData _npcPerceptionData;

        protected List<GameObject> _listGo;

        protected VisionSense _visionSense;

        protected bool _enableDebugMsg;
        protected bool _showGizmos = true;


        public PerceptionSystem(in NpcPerceptionData inNpcPerceptionData, in GameObject inGameObject)
        {
            // Safety check of : NpcPerceptionData
            if(inNpcPerceptionData != null)
            {
                _npcPerceptionData = inNpcPerceptionData;
            }
            else
            {

            }

            // Safety check of : input parameter GameObject
            if(inGameObject != null)
            {
                _ownerGameObject = inGameObject;
                _ownerTransform = _ownerGameObject.transform;
            }
            else
            {

            }

            // initialize array
            _listGo = new List<GameObject>(_npcPerceptionData.perceivedBufferSize);

            // create vision sense
            _visionSense = new VisionSense(inPerceptionSystem: this, inGameObject: _ownerGameObject);
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
        }

        // added on 20-Apr-2026
        public virtual void Method_OnEnterPerception(in GameObject inGameObject)
        {
            if (_enableDebugMsg) { Debug.Log(this + " : [ MARCO ] : Method_OnEnterPerception(in GameObject inGameObject);"); }

            if (_listGo.Contains(inGameObject) == false)
            {
                _listGo.Add(inGameObject);
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

            if (_listGo.Contains(inGameObject))
            {
                _listGo.Remove(inGameObject);
            }
            else
            {
                Debug.LogError(this + " : [ MARCO ] : Method_OnSenseLostPerception('inGameObject') : _listGo does not contain received GameObject reference !!!");
            }
        }

        // added on 20-apr-2026
        public virtual void Method_PerceiveDanger(in StimuliEmitter inEmitter, out int outMenace)
        {
            if (inEmitter == null)
            {
                Debug.LogError(this + " : [ MARCO ] : Method_PerceiveDanger(...) : ''inEmitter'' is null!!!");
                outMenace = 0;
                return;
            }
            else
            {
                inEmitter.Method_GetMenaceValue(out outMenace);
            }
        }

        // addded on 21 - Apr - 2026
        public void Method_ReturnPerceivedGO(out List<GameObject> outList)
        {
            outList = _listGo;
        }

        // addes on 20-Apr-2026
        //public virtual void Method_ExecuteVisionSense()
        //{
        //    Collider[] lcSensedColliders = Physics.OverlapSphere(position: _ownerTransform.position, radius: _npcPerceptionData.visionSenseData.distance, layerMask: _npcPerceptionData.visionSenseData.visionLayerMask, queryTriggerInteraction: QueryTriggerInteraction.Ignore);
            
        //    if(lcSensedColliders.Length != 0)
        //    {
        //        for(int i = 0; i < lcSensedColliders.Length; i++)
        //        {
        //            GameObject lcGameObject = lcSensedColliders[i].gameObject;

        //            if (_listGo.Contains(lcGameObject) == false)
        //            {
        //                this.Method_OnEnterPerception(lcGameObject);
        //            }
        //        }
        //    }

        //    // "forget" / lost sight
        //    for(int i = 0; i < _listGo.Count; i++)
        //    {
        //        if(_listGo[i] != null)
        //        {
        //            float lcDistance = (_ownerTransform.position - _listGo[i].transform.position).magnitude;
        //            if(lcDistance <= _npcPerceptionData.visionSenseData.lostSightDistance)
        //            {
        //                _listGo.RemoveAt(i);
        //            }
        //        }
        //    }
        //}


        // GIZMOS
        public virtual void Method_DrawPerceptionGizmos()
        {
            if (_showGizmos)
            {
                _visionSense.Method_DrawGizmos();
            }
            
            //for(int i = 0; i < _listGo.Count;i++)
            //{
            //    if (_listGo[i] != null)
            //    {
            //        Gizmos.color = _npcPerceptionData.gizmoColorDetection;
            //        Gizmos.DrawWireSphere(_listGo[i].transform.position, 1.5f);
            //        Gizmos.DrawLine(_ownerGameObject.transform.position, _listGo[i].transform.position);
            //    }
            //}
        }
    }
}