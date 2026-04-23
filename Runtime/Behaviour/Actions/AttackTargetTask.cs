using UnityEngine;
// created on 16-apr-2026
namespace MP_Npc.Behavior
{
    public class AttackTargetTask : BaseTask
    {
        protected GameObject _targetGameObject;
        protected float _attackDistance;
        protected float _angleTreshold;

        // constructor
        public AttackTargetTask(in TaskData inTaskData, in NpcBlackboard inBlackboard) : base(inTaskData, inBlackboard)
        {
        }

        public override void Method_CheckPreCondition(out bool outCanExecute)
        {
            base.Method_CheckPreCondition(out outCanExecute);

            if(m_NpcBlackboard.bbKeyTargetGameObject != null)
            {
                Vector3 lc_VectorToTarget = m_NpcBlackboard.bbKeyTargetPosition - m_NpcBlackboard.bbKeyOwnerGameObject.transform.position;
                float  lc_DistanceToTarget = lc_VectorToTarget.magnitude;
                float lc_Angle = Vector3.SignedAngle(from: m_NpcBlackboard.bbKeyOwnerGameObject.transform.forward,to: lc_VectorToTarget, axis: Vector3.up);
            }
            else
            {
                outCanExecute = false;
            }
        }

        public override void Method_EndAction()
        {
            base.Method_EndAction();
        }

        public override void Method_ExecuteAction()
        {
            base.Method_ExecuteAction();
        }

        public override void Method_StartAction()
        {
            base.Method_StartAction();
        }
    }
}