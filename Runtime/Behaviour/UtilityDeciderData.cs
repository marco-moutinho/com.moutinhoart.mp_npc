using System.Collections.Generic;
using UnityEngine;
// [ 2? Jun 2026 ] #created
// [ 28 Jun 2026 ] #changed

namespace MP_Npc.Behavior
{
    [CreateAssetMenu(fileName = "UtilityDeciderData", menuName = "[ MP_NPC ]/Utility Decider Data")]
    public class UtilityDeciderData : ScriptableObject
    {
        [Header("[ State Base Class ]")]
        public string uName = "Placeholder Name";

        [SerializeField]
        public Behavior RootState;

        [SerializeField, Tooltip("[ MARCO ] :\nAdd State Data (scriptable objcects) that add possible states to AI NPC.")]
        public List<Behavior> StateDataPool;

        [Header("[ Blackboard Extensions ]")]
        public bool bNavigationExtension = false;
    }
}