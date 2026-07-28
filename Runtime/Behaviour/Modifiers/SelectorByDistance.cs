using MP_Npc.Behavior;
using UnityEngine;
namespace MP_Npc
{
    // [ 23 Jul 2026 ] Created

    /// <summary>
    /// 
    /// </summary>
    [CreateAssetMenu(fileName = "SelectorByDistance", menuName = "[ MP_NPC ]/Behavior Mods/Object Selector/By Distance")]
    public class SelectorByDistance : ObjectSelectorBehaviorMod
    {
        [Header("[Subclass Params ]")]
        public float idealDistance;

        public enum EDistance
        {
            Closest,
            ClosestToIdealDistance,
            Fardest,
        }
        public EDistance targetDistance;

        public override GameObject XFuncGetSelectedGameObject() // 23 Jul 2026
        {
            throw new System.NotImplementedException();
        }
    }
}