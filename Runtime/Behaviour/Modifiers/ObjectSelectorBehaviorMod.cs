using UnityEngine;
namespace MP_Npc.Behavior
{
    // [ 23 Jul 2026 ] Created

    /// <summary>
    /// Base class to create behavior parameters
    /// </summary>
    public abstract class ObjectSelectorBehaviorMod : ScriptableObject
    {
        public string _name = "Default Selector Name";

        public abstract GameObject XFuncGetSelectedGameObject(); // [ 23 Jul 2026 ] #Added
    }
}