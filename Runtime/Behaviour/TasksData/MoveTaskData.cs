using UnityEngine;
// created at 12-Apr-2026
namespace MP_Npc.Behavior
{

    [CreateAssetMenu(fileName = "MoveTaskData", menuName = "[ MP_NPC ]/Tasks Data >/Move TD")]
    public class MoveTaskData : TaskData
    {
        [Header("Move subclass")]
        public float distanceFromTarget;

        public MoveAgentTask _task;
    }
}