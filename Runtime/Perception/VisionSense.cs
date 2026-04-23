using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;
// created on 20 - Apr - 2026
// last change 22 - Apr - 2026
namespace MP_Npc.Perception
{
    public class VisionSense : NpcSense
    {
        protected float _visionDistance;
        protected float _visionDistanceModifier;
        float _convertedFovAngleToDot;

        // if the sense perceive more than the buffer size, how should it handle?, by distance?
        //protected float _bCheckForClosest;

        public VisionSense(in PerceptionSystem inPerceptionSystem, in GameObject inGameObject) : base(inPerceptionSystem, inGameObject)
        {
            _visionDistance = _perceptionData.visionSenseData.distance;

            // Convert FOV lcAngle to a dot product threshold (usually done once in Start) - Maybe later I should create a function to change this mid game - Marco
            // Example: A 90-degree total FOV means 45 degrees each side. cos(45) ≈ 0.707
            _convertedFovAngleToDot = Mathf.Cos(_perceptionData.visionSenseData.horizontalFieldOfView * 0.5f * Mathf.Deg2Rad);
            
            if (_enableDebugLog)
            {
                Debug.Log("_OverlapedCollidersBuffer.Length = " + _OverlapedCollidersBuffer.Length);
            }
        }

        public override void Method_Execute()
        {
            base.Method_Execute();
            Method_ExecuteVision();
            Method_ExecuteLoseSenseLoop();
        }

        protected virtual void Method_ExecuteVision()
        {
            int lcFoundCollidersAmmount = Physics.OverlapSphereNonAlloc(_ownerTransform.position, _perceptionData.visionSenseData.distance, results: _OverlapedCollidersBuffer, _perceptionData.visionSenseData.visionLayerMask, QueryTriggerInteraction.Ignore);

            // TO DO...
            //if (lcFoundCollidersAmmount > _OverlapedCollidersBuffer.Length) { }

            // for loop of : check if sensed GameObject has already been sensed...
            for (int i = 0; i < lcFoundCollidersAmmount; i++)
            {
                GameObject lcCurrentGameObject = _OverlapedCollidersBuffer[i].gameObject;

                //need to check if :should i have the block inside this if, or just make a return; if == false?? there is a difrence on performance or on compilation?
                if (Method_CheckIfGameObjectCanBeSensed(lcCurrentGameObject) == true)
                {

                    if (_OverlapedCollidersBuffer[i] != null)
                    {
                        if (_sensedGameObjects.Contains(lcCurrentGameObject) == true)
                        {
                            // means that was already sensed...
                        }
                        else
                        {
                            // means that is the first time that was sensed...
                            Method_OnEnterSense(lcCurrentGameObject);
                        }
                    }
                    else
                    {
                        // what if the slot already has a value?
                        //...
                        Debug.LogError(this + " : [ MARCO ] : Method_ExecuteVision() : Something very strange is hapening !!!!");
                    }
                }
                // if it can not be sensed...
                // i can run the Lose sense logic on this else block insted of run another for loop right?
                // not quite
                else
                {
                    if (_enableDebugLog)
                    {
                        Debug.Log(this + " : [ MARCO ] : Method_ExecuteVision() : Method_CheckIfGameObjectCanBeSensed(lcCurrentGameObject) was FALSE !");
                    }
                }
                // end for loop
            }
        }

        // added on 21 - Apr - 2026
        protected override bool Method_CheckIfGameObjectIsStillCanStillBeSensed(in GameObject inGameObject)
        {
            return Method_CheckIfGameObjectCanBeSensed(inGameObject: inGameObject);
        }

        // overrided on 22 - Apr - 2022
        protected override bool Method_CheckIfGameObjectCanBeSensed(in GameObject inGameObject)
        {
            // check 1 : View Distance
            Vector3 lcOtherPosition = inGameObject.transform.position;
            Vector3 lcSelfPosition = _ownerTransform.position;
            Vector3 lcDistanceVector = lcOtherPosition - lcSelfPosition;
            float lcSqrDistance = lcDistanceVector.sqrMagnitude;
            float lcSqrVisionDistance = _perceptionData.visionSenseData.distance * _perceptionData.visionSenseData.distance;

            bool lc_bIsInViewDistance = (lcSqrDistance <= lcSqrVisionDistance) ? true : false;
            
            // Quick exit, if is not in lcRange the next step is redundant
            if(lc_bIsInViewDistance == false) { return false; }

            // Check 2 : FOV
            Vector3 lcNormalizedDirectionToTarget = lcDistanceVector.normalized;
            float lcDotProduct = Vector3.Dot(_ownerTransform.forward, lcNormalizedDirectionToTarget);

            bool lc_bIsInFovAngle = (lcDotProduct >= _convertedFovAngleToDot) ? true : false;
            if( lc_bIsInFovAngle == false) { return false; }


            // If all its true than return true;
            return true;
        }

        // GIZMOS | GIZMOS | GIZMOS | GIZMOS | GIZMOS | GIZMOS | GIZMOS | GIZMOS | GIZMOS | GIZMOS | GIZMOS | GIZMOS | GIZMOS | GIZMOS | GIZMOS | GIZMOS | GIZMOS | GIZMOS | GIZMOS | GIZMOS | GIZMOS | GIZMOS | GIZMOS | GIZMOS |
        public override void Method_DrawGizmos()
        {
            base.Method_DrawGizmos();

            Gizmos.color = _perceptionData.gizmoColorSightDistance;
            Gizmos.DrawWireSphere(center: _ownerTransform.position, radius: _perceptionData.visionSenseData.distance);

            Color lcMeshColor = _perceptionData.gizmoColorSightDistance;
            lcMeshColor.a = 0.1f;

            Gizmos.color = lcMeshColor;
            Gizmos.DrawSphere(_ownerTransform.position, _perceptionData.visionSenseData.distance);

            // lost sight gizmo
            Gizmos.color = _perceptionData.gizmoCOlorLostSightDistance;
            Gizmos.DrawWireSphere(_ownerTransform.position, _perceptionData.visionSenseData.lostSightDistance);

            // Fov
            float lcAngle = _perceptionData.visionSenseData.horizontalFieldOfView;
            float lcRange = _perceptionData.visionSenseData.distance;
            Vector3 lcPos = _ownerTransform.position;
            Vector3 lcForward = _ownerTransform.forward;
            // Calculate FOV Edges
            // rotate the lcForward vector by half the lcAngle to the left and right - Gemini
            Vector3 lcLeftBoundary = Quaternion.Euler(0, -lcAngle * 0.5f, 0) * lcForward;
            Vector3 lcRightBoundary = Quaternion.Euler(0, lcAngle * 0.5f, 0) * lcForward;
            Gizmos.color = _perceptionData.gizmoColorSightDistance;
            Gizmos.DrawLine(lcPos, lcPos + lcLeftBoundary * lcRange);
            Gizmos.DrawLine(lcPos, lcPos + lcRightBoundary * lcRange);
            
            // sensed game objects
            Gizmos.color = _perceptionData.gizmoColorDetection;
            for (int i = 0; i < _sensedGameObjects.Count; i++)
            {
                Gizmos.DrawWireSphere(_sensedGameObjects[i].transform.position, 1.5f);
                Gizmos.DrawLine(_sensedGameObjects[i].transform.position, _ownerGameObject.transform.position);
            }

            Gizmos.color = _perceptionData.gizmoColor_PerceivedButNotSensedColor;
            // possible to perceived but not sensed
            for(int i2 = 0; i2 < _OverlapedCollidersBuffer.Length; i2++)
            {
                GameObject lcGo = _OverlapedCollidersBuffer[i2].gameObject;
                if (lcGo != null && _sensedGameObjects.Contains(lcGo) == false)
                {

                    Gizmos.DrawLine(_OverlapedCollidersBuffer[i2].transform.position, _ownerGameObject.transform.position);
                    Gizmos.DrawWireSphere(_OverlapedCollidersBuffer[i2].transform.position, 1.5f);
                }
            }
        }
    }
}