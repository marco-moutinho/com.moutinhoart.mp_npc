using UnityEngine;
// created at 29 - Apr - 2026
namespace MP_Npc.Perception
{
    public struct StSoundStimuli
    {
        public Vector3 soundPosition;
        public float soundForce;

        /// <summary>
        /// used to query the enviorment for obstacles that may prevent sound to reach target
        /// </summary>
        public LayerMask soundLayerMask;

        public GameObject soundGoEmitter;
        public NpcComponent soundNpcEmitter;
    }

    public interface IStimulusReceiver
    {
        public abstract void IMethod_ReceiveSoundStimuli(in StSoundStimuli inSountStimuli);
    }
}