using UnityEngine;
// created at 13-Apr-2026
// Recreated at 29 Jun 2026
namespace MP_Npc.Behavior
{
    [CreateAssetMenu(fileName = "BehaviourData", menuName = "[ MP_NPC ]/NPC AI Settings")]
    public class BehaviourData : ScriptableObject // <- rename to NPC technical settings or something like it
    {
        [Header("[ AI Brain Settings ]")]
        public float npcComponentTickInterval = 0.01666666666666666666666666666667f;
        public float aiBrainTickInterval = 0.01666666666666666666666666666667f;
        public float perceptionTickRate = 0.01666666666666666666666666666667f;
    }
}