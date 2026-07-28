using UnityEngine;
// created at 12-Apr-2026
namespace MP_Npc.Behavior
{
    // [ 17 - May - 2026 ] #Created
    public enum ETaskState { Initializing, CheckingCondition, Executing, Completed, Aborted,}

    public abstract class AiAction
    {
        protected NpcBlackboard m_NpcBlackboard;
        protected bool writeDebugMsg;
        protected ETaskState m_TaskState;

        // added on 12-Apr-2026 | [ 17 - May - 2026 ] #Changed
        public AiAction(in BehaviourBrain inBrain)
        {
            if(inBrain == null) { Debug.LogError(this + " : public AiAction(in BehaviourBrain inBrain) : inBrain is null !!!"); }
        }

        // [ 18 - May - 2026 ] #Added
        public virtual void InitializeValues(in NpcBlackboard inBlackboard)
        {
            m_TaskState = ETaskState.Initializing;

            if (inBlackboard != null) { m_NpcBlackboard = inBlackboard; }
            else { Debug.LogError(this + " : [ MARCO ] : InitializeValues(...) : inBlackboard is null !!!"); }
        }

        // added on 16-Apr-2026
        public virtual bool Method_CheckPreCondition()
        {
            m_TaskState = ETaskState.CheckingCondition;
            
            if (writeDebugMsg) { Debug.Log(this + " : [ MARCO ] : Method_CheckPreCondition(...);"); }
            
            return true;
        }

        // added on 12-Apr-2026
        public virtual void Method_StartAction()
        {
            m_TaskState = ETaskState.Executing;

            if(writeDebugMsg == true) { Debug.Log(this + " : [ MARCO ] : Method_StartAction()...;"); }
        }

        // added on 12-Apr-2026
        public virtual void Method_EndAction(in bool inWasSucessufully)
        {
            if (inWasSucessufully) { m_TaskState = ETaskState.Completed; }
            else { m_TaskState = ETaskState.Aborted; }

            if (writeDebugMsg == true) { Debug.Log(this + " : [ MARCO ] : Method_EndAction()...;"); }
        }

        // added on 12-Apr-2026
        public virtual void Method_ExecuteAction()
        {
            if (writeDebugMsg == true) { Debug.Log(this + " : [ MARCO ] : Method_ExecuteAction()...;"); }
        }
        
        // [ 17 - May - 2026 ] #Created
        public virtual ETaskState Method_ReturnTaskState()
        {
            return m_TaskState;
        }
    }
}