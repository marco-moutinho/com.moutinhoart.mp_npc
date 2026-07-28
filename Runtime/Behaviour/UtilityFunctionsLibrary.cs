using UnityEngine;
namespace MP_Npc.Behavior
{
    // [ 25 Jun 2026 ] #Created

    /// <summary>
    /// Stores commun functions to calculate scores to use on Utility AI
    /// </summary>
    public static class UtilityFunctionsLibrary
    {
        // [ 25 Jun 2026 ] #Created
        /// <summary>
        /// Returns a score by calculating the AI current distance between him and his target.
        /// </summary>
        /// <param name="inDistance">AI Agente current distance from the target</param>
        /// <param name="inMinDistance"></param>
        /// <param name="inMaxDistance"></param>
        /// <param name="inIdealDistance"></param>
        /// <param name="outScore"></param>
        public static void EvaluateDistance(in float inDistance, in float inMinDistance, in float inMaxDistance, in float inIdealDistance, out float outScore)
        {
            // calculate max ever error, this works cause I will allways positive values
            float maxError = Mathf.Max(inIdealDistance - inMinDistance, inMaxDistance - inIdealDistance);

            // calculate how far it is from the ideal distance
            float error = Mathf.Abs(inDistance - inIdealDistance);

            // this is always between 0 and 1 because error is never > than maxError,
            outScore = 1f - (error / maxError);
        }

        // [ 26 Jun 2026 ] #Added
        public static void TryGetBlackboard(in GameObject inGameObject, out NpcBlackboard outNpcBb)
        {
            if(inGameObject == null) outNpcBb = null;
            else outNpcBb = null; // TEMP
        }

        //// [ 30 Jun 2026 ] #Added
        //public static void EvaluateHealth(in float inMin, in float inMax, in float inCurrent, out float outScore)
        //{
        //    outScore = 0;
        //}
    }
}