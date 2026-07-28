using System.Collections.Generic;
using UnityEngine;
// creates at [ 29 - Apr - 2026 ]
namespace MP_Npc
{
    public class NpcGlobalManager : MonoBehaviour
    {
        public GameObject[] npcPool;
        protected List<NpcComponent> _npcComponentList;

        public virtual void Method_SpawnNewNpc()
        {
            // will this work 100%?
            NpcComponent lcNpc = Instantiate(npcPool[0], Vector3.zero, Quaternion.identity).GetComponent<NpcComponent>();
        }
    }
}