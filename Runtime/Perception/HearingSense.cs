using System.Collections.Generic;
using UnityEngine;
// created on 28 - Apr - 2026
namespace MP_Npc.Perception
{
    public class HearingSense : NpcSense
    {
        public HearingSense(in PerceptionSystem inPerceptionSystem, in GameObject inOwnerGameObject) : base(inPerceptionSystem, inOwnerGameObject)
        {
        }

        protected override bool Method_CheckIfGameObjectCanBeSensed(in GameObject inGameObject)
        {
            throw new System.NotImplementedException();
        }

        protected override bool Method_CheckIfGameObjectIsStillCanStillBeSensed(in GameObject inGameObject)
        {
            throw new System.NotImplementedException();
        }

        // Hearing Sense subclass methods | Hearing Sense subclass methods | Hearing Sense subclass methods | Hearing Sense subclass methods | Hearing Sense subclass methods | Hearing Sense subclass methods |
        // added at 29-Apr-2026
        public virtual void Method_ReceiveSoundStimuli(in StSoundStimuli inSoundStimuli)
        {
            // filter rules
        }
    }
}