using UnityEngine;
// created at 04-Apr-2026
namespace MP_Npc.Perception
{

    [CreateAssetMenu(fileName = "NewNpcPerceptionSettings", menuName = "[ MP_NPC ]/Perception Settings")]
    public class NpcPerceptionData : ScriptableObject
    {
        [System.Serializable]
        public struct StVisionSense
        {
            [Min(0)] public float distance;
            [Min(0)] public float vertialFieldOfView;
            [Min(0)] public float horizontalFieldOfView;
            [Min(0)] public float lostSightDistance;
            [Min(1)] public int visionBufferSize;
            // really min of 0 ? ahahaha, why!?

            public LayerMask visionLayerMask;
            public LayerMask visionObstructionLayer;

            // current version of C# dont support constructors on struct
            // VS2026 : "Feature 'parameterless struct constructors' is not available in C# 9.0. Please use language version 10.0 or greater."
            
            //public StVisionSense()
            //{
            //    distance = 20;
            //    vertialFieldOfView = 90;
            //    horizontalFieldOfView = 90;
            //    lostSightDistance = 30;
            //    visionBufferSize = 4;

            //    visionLayerMask = LayerMask.NameToLayer("NPC");
            //    visionObstructionLayer = LayerMask.NameToLayer("Obstruction");
            //}
        }

        [System.Serializable]
        public struct StSoundSense
        {
            public float range;
        }

        [Header("[ Senses Settings ]")]
        [Min(1)] public int perceivedBufferSize;
        public StVisionSense visionSenseData;
        public StSoundSense soundSenseData;

        [Header("< GIZMOS >")]
        public Color gizmoColorSightDistance = Color.green;
        public Color gizmoColorSightFieldOfView = Color.green;
        public Color gizmoCOlorLostSightDistance = Color.lightGreen;
        public Color gizmoColorDetection = Color.indianRed;
        public Color gizmoColorVisabilityCheckLine = Color.softGreen;

        public Color gizmoColor_PerceivedButNotSensedColor = Color.ghostWhite;

        private void OnValidate()
        {
            // check mins

            if(perceivedBufferSize <= 0)
            {
                perceivedBufferSize = 1;
            }

            // vision min
            if(visionSenseData.visionBufferSize <= 0)
            {
                visionSenseData.visionBufferSize = 1;
            }

            // lost sight
            if(visionSenseData.lostSightDistance < visionSenseData.distance)
            {
                visionSenseData.lostSightDistance = visionSenseData.distance;
            }
        }
    }
}