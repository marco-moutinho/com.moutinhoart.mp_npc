using UnityEngine;
// created at 13-Apr-2026
namespace MP_Npc.Behavior
{
    [CreateAssetMenu(fileName = "BehaviourData", menuName = "[ MP_NPC ]/BehaviourData")]
    public class BehaviourData : ScriptableObject
    {
        public TaskData[] tasks;

        [SerializeField]
        public MoveAgentTask _moveTask;
    }
}