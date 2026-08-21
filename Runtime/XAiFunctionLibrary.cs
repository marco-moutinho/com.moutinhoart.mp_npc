using MP_Npc.Behavior;
using UnityEngine;
using UnityEngine.AI;
namespace MP_Npc
{
    // [ 28 Jul 2026 ] #Created
    // [ 21 Ago 2026 ] change on HasReachedDestination : rename rawDistance to flatDistance, cause I set both vector3 .y to 0
    public static class XAiFunctionLibrary
    {
        // [ 28 Jul 2026 ] #Created

        /// <summary>
        /// Try's to get a random reachable point on the navmesh
        /// </summary>
        /// <param name="inOrigin"></param>
        /// <param name="inRadius"></param>
        /// <param name="inAgent"></param>
        /// <param name="inNavMeshPath"></param>
        /// <param name="outPosition">represents as worldPosition as in NavMesh point/position</param>
        /// <param name="outNavMeshHit"></param>
        /// <returns></returns>
        public static bool TryGetRandomReachablePoint(Vector3 inOrigin, float inRadius, NavMeshAgent inAgent, NavMeshPath inNavMeshPath, out Vector3 outPosition, out NavMeshHit outNavMeshHit)
        {
            // Pick a raw math point somewhere in the air/world near the agent
            /// 1. Random.insideUnitSphere generates a random point INSIDE a sphere of radius 1 (length between 0.0 and 1.0).
            /// 2. Multiplying by 'inRadius' scales the sphere so points fall anywhere between 0 and 'inRadius' distance away.
            /// 3. Adding 'inOrigin' offsets the center of that sphere to the agent's position, producing a World Space point.
            Vector3 randomPointInWorldSpace = Random.insideUnitSphere * inRadius + inOrigin;

            bool bPathReachDestination;
            bool bHasFoundHitNavMesh;

            // Snap THAT raw point ( randomPointInWorldSpace ) to the nearest floor/NavMesh polygon, that point will be 'outNavMeshHit.position'
            bHasFoundHitNavMesh = NavMesh.SamplePosition(randomPointInWorldSpace, out outNavMeshHit, inRadius, NavMesh.AllAreas);

            // try calculate a path on hited navmesh
            if (bHasFoundHitNavMesh)
            { 
                inAgent.CalculatePath(outNavMeshHit.position, inNavMeshPath);
                bPathReachDestination = inNavMeshPath.status == NavMeshPathStatus.PathComplete;

                if (bPathReachDestination)
                {
                    outPosition = outNavMeshHit.position;
                    return true;
                }
            }


            outPosition = Vector3.zero;
            return false;

            /// NOTES:
            /// You don't need out NavMeshPath outNavMeshPath because the caller's inNavMeshPath instance is updated directly.
        }
        // TO DO make a new version of this function but that has a min and max distance

        // [ 29 Jul 2026 ] #Added | [ 21 Ago 2026 ] #Lastchange
        public static bool HasReachedDestination(BehaviourBrain inBrain, in float inTolerance)
        {
            // safety check
            if(inBrain == null)
            {
                Debug.LogError(" XAiFunctionLibrary.HasReachedDestination(BehaviourBrain inBrain) inBrain is null !!!");
                return false;
            }

            // create var to use on output parameter/argument
            NavigationBbExtension navigationBbExt;

            if (inBrain.GetBlackboard().TryGetExtension<NavigationBbExtension>(out navigationBbExt))
            {
                if(navigationBbExt != null)
                {
                    Vector3 destination = navigationBbExt.bbk_DestinationPosition;
                    Vector3 currentPosition = inBrain.GetBlackboard().bbk_OwnerTransform.position;

                    // Zero the .y /vertical so it does not exist a miss match between agent/NPC and the NavMesh point/ world point
                    destination.y = 0; currentPosition.y = 0;

                    // calculate raw distance between current position and destination
                    float flatDistance = (destination - currentPosition).magnitude;
                    Debug.Log("[ MARCO ] : HasReachedDestination(...) : [ flatDistance = " + flatDistance + " ]");

                    // check against Data
                    if (flatDistance <= inTolerance)
                    {
                        return true;
                    }
                    else {  return false; }
                }

                else
                {
                    Debug.LogError("[ MARCO ] : public static bool HasReachedDestination(...) : navigationBbExt is null !!!");
                    return false;
                }
            }
            Debug.LogError("[ MARCO ] : public static bool HasReachedDestination(...) : TryGetExtension has return FALSE !!!");
            return false;
        }
    }
}