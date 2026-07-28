using UnityEngine;
namespace MP_Npc.Behavior
{
    // [ 23 - Jun - 2026 ] #Created

    //[CreateAssetMenu(fileName = "AiAction", menuName = "MP_NPC/Ai Action/Base Action")]
    public abstract class AiActionData : ScriptableObject
    {
        /// <summary>
        /// Returns a object class of the corresponding action script
        /// </summary>
        /// <returns></returns>
        //public abstract AiAction MFuncGetAiAction();


        public abstract float Evaluate(in BehaviourBrain inBrain);
    }
}