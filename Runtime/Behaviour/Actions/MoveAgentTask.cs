using UnityEngine;
using UnityEngine.AI;
// created on 12-Apr-2026
namespace MP_Npc.Behavior
{
    public class MoveAgentTask : BaseTask
    {
        protected NavMeshAgent m_NavMeshAgent;
        protected Vector3 m_MoveDirection;
        protected Vector3 m_MoveOffset;

        [SerializeField] public MoveAgentTask _task;

        [SerializeField] protected int _intParam;

        public MoveAgentTask(in TaskData inTaskData, in NpcBlackboard inBlackboard) : base(inTaskData, inBlackboard)
        {
        }

        public override void Method_CheckPreCondition(out bool outCanExecute)
        {
            base.Method_CheckPreCondition(out outCanExecute);
        }

        public override void Method_EndAction()
        {
            base.Method_EndAction();
        }

        public override void Method_ExecuteAction()
        {
            base.Method_ExecuteAction();

            m_MoveOffset = m_MoveDirection * Time.deltaTime;
            m_NavMeshAgent.Move(m_MoveOffset);
        }

        public override void Method_StartAction()
        {
            base.Method_StartAction();
        }
    }
}