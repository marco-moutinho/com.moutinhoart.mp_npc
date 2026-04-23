using UnityEngine;
// created at 20-Apr-2026
namespace MP_Npc.Perception
{
    /// <summary>
    /// This component is responsible to emit senses of a character like smell, sound and touch
    /// </summary>
    public class StimuliEmitter : MonoBehaviour
    {
        protected int _menaceValue;
        protected PerceptionSystem _stimuliReceiver;

        // added on 20-Apr-2026
        public virtual void Method_GetMenaceValue(out int outValue)
        {
            outValue = _menaceValue;
        }

        // added on 20-Apr-2026
        public virtual void Method_SetMenaceValue(in int inValue)
        {
            _menaceValue = inValue;
        }
    }
}