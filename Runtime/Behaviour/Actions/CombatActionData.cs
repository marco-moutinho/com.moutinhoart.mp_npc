using UnityEngine;
namespace MP_Npc.Behavior
{

    [CreateAssetMenu(fileName = "AiAction", menuName = "[ MP_NPC ]/Ai Action/Combat Action")]
    public class CombatActionData : AiActionData
    {
        [Header("[ Combat Subclass ]")]

        [Min(0f)] public float MinOpponentDistance;
        [Min(0f)] public float MaxOpponentDistance;
        [Min(0f)] public float IdealDistance;


        public override float Evaluate(in BehaviourBrain inBrain)
        {
            ///     NOTE:
            ///     Pontos a avaliar para o combate :
            ///         * Distancia
            ///         * Personalidade
            ///         * custo da ação ( ataque ou defesa )
            ///         * + modificadores
            ///         * opponent state ( health , power , toughtness, ... )



            // safety check
            if(inBrain == null) { Debug.LogError(this + " : public override float Evaluate(in BehaviourBrain inBrain) : inBrain is null !!!"); return 0f; }

            NpcBlackboard npcBb = inBrain.GetBlackboard();

            // safety check
            if(npcBb == null) { Debug.LogError(this + " : public override float Evaluate(...) : npcBb is null !!!"); return 0f; }

            float distance = Vector3.Distance(npcBb.bbk_OwnerTransform.position, npcBb.bbKeyTargetGameObject.transform.position);
            
            
            if (distance < MinOpponentDistance || distance > MaxOpponentDistance) { return 0f; }
            else
            {
                //// calculate max ever error, this works cause I will allways positive values
                //float maxError = Mathf.Max(IdealDistance - MinOpponentDistance, MaxOpponentDistance - IdealDistance);

                //// calculate how far it is from the ideal distance
                //float error = Mathf.Abs(distance - IdealDistance);

                //// this is always between 0 and 1 because error is never > than maxError,
                //float score = 1f - (error / maxError);

                //return score;
                float score;
                UtilityFunctionsLibrary.EvaluateDistance(distance, MinOpponentDistance, MaxOpponentDistance, IdealDistance, out score);
                return score;

                // se o erro for > maxError o score vai ser negativo
            }

            //float actionScore = distanceError *
            
        }
    }
}