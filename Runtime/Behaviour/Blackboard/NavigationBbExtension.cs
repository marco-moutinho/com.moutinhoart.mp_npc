using UnityEngine;
using UnityEngine.AI;
namespace MP_Npc.Behavior
{
    // [ 29 Jul 2026 ] #Created
    public class NavigationBbExtension : BlackboardExtension
    {
        // NavMesh stuff
        //...

        // I think that I really need to write this new (), cause if not it will throw a null expetion error on editor, cause this var does not really exist/its initialize, only declered
        public NavMeshPath bbk_NavMeshPath = new NavMeshPath();
        
        public NavMeshPathStatus bbk_NavMeshPathStatus;
        
        public NavMeshHit bbk_NavMeshHit;

        public Vector3 bbk_DestinationPosition;

        public bool bbk_HasReachDestination;

        // use this on control flow situation
        public bool bbk_HasADestinationPoint;
    }
}