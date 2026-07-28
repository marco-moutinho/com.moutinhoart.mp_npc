using System.Collections.Generic;
using UnityEngine;
namespace MP_Npc.Behavior
{
    // [ 24 Jun 2026 ] #Created

    /// Diary XD
    /// | 001 | [ 29 Jun 2026 ]
    /// | 002 | [ 30 Jun 2026 ] : fix CalculateStatesScore function and continue to work on it + add more comments
    /// | 09 Jul 2026 | 003 
    /// | 23 Jul 2026 | 004

    /// <summary>
    /// 
    /// </summary>
    public class UtilityDecider
    {
        protected NpcBlackboard _blackboard;

        public UtilityDecider(in NpcBlackboard BlackboardSource)
        {
            // safety checks
            if(BlackboardSource == null) { Debug.LogError("[ MARCO ERROR ] : " + this + " : public UtilityDecider(in NpcBlackboard BlackboardSource) : BlackboardSource is null !!!"); }
            else { _blackboard = BlackboardSource; }
        }
        // ............................................
        protected float currentScore;
        protected float previousScore;
        protected float bestScore;
        protected int bestScoreIndex;

        protected Behavior HighestScoreStateData;
        //..............................................

        // [ 28 Jun 2026 ] #Added
        /// <summary>
        /// Calculate the highest score between all possible states by calling "float Behavior.FuncCalculateScore()".
        /// Stores the Behavior ref on a variable called "HighestScoreStateData". Call UtilityDecider."FuncReturnSelectedState" to get the reference/pointer to the calculated state class.
        /// </summary>
        public virtual void CalculateStatesScore()
        {
            // Starter values
            bestScore = 0;
            bestScoreIndex = 0;
            
            for ( int i = 0; i < _blackboard.UtilityDeciderData.StateDataPool.Count; i++)
            {
                // safety check: for null pointers
                if(_blackboard.UtilityDeciderData.StateDataPool[i] == null) { Debug.LogError("[ MARCO ] : " + this + " : public virtual void FuncConsiderAboutStates() : current Behavior is a null pointer !!!"); continue; }

                // score of the current element/object on the loop
                currentScore = _blackboard.UtilityDeciderData.StateDataPool[i].FuncCalculateScore();

                // check if it is bigger than the highst so far, and if so also store its index (to point to the correct element on the list)
                if(currentScore > bestScore) {  bestScore = currentScore; bestScoreIndex = i; }
            }

            // set the highest score data reference
            HighestScoreStateData = _blackboard.UtilityDeciderData.StateDataPool[bestScoreIndex];
        }

        // [ 28 Jun 2026 ] #Added
        ///<summary>Returns the highst scored state data, returns null if none evaluation was done.</summary>
        /// <remarks>Returns a MP_Npc.Behavior.Behavior scriptable object pointer </remarks>
        public virtual bool TryGetHighestScoreState(out Behavior outStateDataPtr)
        {
            if(HighestScoreStateData == null)
            {
                Debug.LogWarning(this + " : [ MARCO WARING ] : public virtual Behavior FuncReturnSelectedState() : HighestScoreStateData is a null pointer");
                outStateDataPtr = null;
                return false;
            }

            outStateDataPtr = HighestScoreStateData;
            return true;
        }
    }
}