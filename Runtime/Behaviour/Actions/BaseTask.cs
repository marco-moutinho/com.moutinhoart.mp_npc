using UnityEngine;
// created at 12-Apr-2026
namespace MP_Npc.Behavior
{
    [System.Serializable]
    public abstract class BaseTask
    {
        protected TaskData m_TaskData;
        protected NpcBlackboard m_NpcBlackboard;
        protected bool writeDebugMsg;

        // added on 12-Apr-2026
        public BaseTask(in TaskData inTaskData, in NpcBlackboard inBlackboard)
        {
            if(inTaskData != null) { m_TaskData = inTaskData; }
            else { Debug.LogError(this + " : [ MARCO ] : BaseTask(...) : inTaskData is null !!!"); }

            if(inBlackboard != null) { m_NpcBlackboard = inBlackboard; }
            else { Debug.LogError(this + " : [ MARCO ] : BaseTask(...) : inBlackboard is null !!!"); }

            if(m_NpcBlackboard == null || m_TaskData == null)
            {
                Debug.LogError(this + " : [ MARCO ] : BaseTask(...) : constructor badly initialized !!!");
                return;
            }
        }

        // added on 16-Apr-2026
        public virtual void Method_CheckPreCondition(out bool outCanExecute)
        {
            if (writeDebugMsg) { Debug.Log(this + " : [ MARCO ] : Method_CheckPreCondition(...);"); }
            outCanExecute = true;
        }

        // added on 12-Apr-2026
        public virtual void Method_StartAction()
        {
            if(writeDebugMsg == true) { Debug.Log(this + " : [ MARCO ] : Method_StartAction()...;"); }
        }

        // added on 12-Apr-2026
        public virtual void Method_EndAction()
        {
            if (writeDebugMsg == true) { Debug.Log(this + " : [ MARCO ] : Method_EndAction()...;"); }
        }

        // added on 12-Apr-2026
        public virtual void Method_ExecuteAction()
        {
            if (writeDebugMsg == true) { Debug.Log(this + " : [ MARCO ] : Method_ExecuteAction()...;"); }
        }
    }
}