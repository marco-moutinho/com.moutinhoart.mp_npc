using UnityEngine;
namespace MP_Npc.Behavior
{
    // [ 26 Jun 2026 ] #Created
    // [ 28 Jun 2026 ]
    // [ 28 Jull 2026 ] #Changed

    /// <summary>
    /// Base class to create new behaviors as ScriptableObejcts. Never store any run time data on this class (and subclasses).
    /// <para></para>
    /// During run time "Behavior" class should only be used to call functions from it.
    /// </summary>
    public abstract class Behavior : ScriptableObject
    {
        public string sName = "Default state string name";

        // [ 28 Jun 2026 ] #Added
        public abstract float FuncCalculateScore();

        // [ 10 Jul 2026 ] #Added
        public abstract void MfuncRunBehavior(in BehaviourBrain inBrain, out EBehaviourPhase outBehaviourPhase);
    }
}