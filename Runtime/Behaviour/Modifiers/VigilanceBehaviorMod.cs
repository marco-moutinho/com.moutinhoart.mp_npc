using UnityEngine;
namespace MP_Npc.Behavior
{
    // [ 27 Jul 2026 ] #Created

    [CreateAssetMenu(fileName = "VigilanceBehaviorMod", menuName = "[ MP_NPC ]/Behavior Mods/Vigilance Settings")]
    public class VigilanceBehaviorMod : ScriptableObject
    {
        // implement the rules / params of the vigilance behavior...
        // idk really, I was thinking stuff like patrol path settings, like if it loops and how it loops, but this a more specific "action" than vigilance
        //...

        public enum EVigilanceStrategy
        {
            PatrolPath,
            Wander,
        }
    }
}