using System.Collections.Generic;
using UnityEngine;
// created at 28 - Apr - 2026
namespace MP_Npc.Perception
{
    public struct StCacheValue
    {
        public int ticksWithout;
    }
    public class StimulusSource
    {
        protected GameObject _ownerGameObject;
        protected Vector3 _soundOrigin;
        protected int _OverlapedCollidersAmmount;
        protected Collider[] _OverlapedCollidersBuffer;
        protected int _cacheMemorySize;

        protected Dictionary<GameObject, StCacheValue> _dictionaryOfSCachedtimuliReceivers;


        // added on 28 - Apr - 2026
        public StimulusSource(in GameObject inOwnerGameObject, int inBufferSize, in int inCacheMemory)
        {
            if(inOwnerGameObject != null)
            {
                _ownerGameObject = inOwnerGameObject;
            }
            else
            {
                Debug.LogError(this + " : [ MARCO ] : public StimulusSource(constructor...) : inOwnerGameObject is null !!!");
            }
            _OverlapedCollidersBuffer = new Collider[inBufferSize];

            _dictionaryOfSCachedtimuliReceivers = new Dictionary<GameObject, StCacheValue>();

            _cacheMemorySize = inCacheMemory;
        }

        // added on 28 - Apr - 2026
        //public virtual void Method_ExecuteSoundStimuli(in Vector3 inSoundOrigin, in float inSoundRange, in float inSoundForce, in LayerMask inSoundLayerMask)
        //{
        //   _OverlapedCollidersAmmount = Physics.OverlapSphereNonAlloc(position: inSoundOrigin, radius: inSoundRange, results: _OverlapedCollidersBuffer, layerMask: inSoundLayerMask, QueryTriggerInteraction.Ignore);

        //    // run a for loop on the _OverlapedCollidersBuffer
        //    for (int i = 0; i <  _OverlapedCollidersAmmount; i++)
        //    {
        //        GameObject lcCurrentGO = _OverlapedCollidersBuffer[i].gameObject;
        //        IHearable lcCurrentHearableInterface;

        //        StCacheValue lcValue;

        //        // new main loop body ... ... ... ... ... ... ... ... ... ... ... ... ... ... ... ... ... ... ... ... ... ... ... ... ... ... ... ... ... ... ... ... ... ... ... ... ... ... ... ... ... ... ... ... ...
        //        if (_dictionaryOfSCachedtimuliReceivers.TryGetValue(key: lcCurrentGO, value: out lcValue))
        //        {
        //            // contruct soundStimuli struct and send it...

        //            // if overlaped reset timer
        //            lcValue.ticksWithout = 0;

        //            // apply the value of the key, cause the lcValue is a copy, cause in C# using out on a struct returns a copy instead of a ref/pointer to the original struct
        //            _dictionaryOfSCachedtimuliReceivers[lcCurrentGO] = lcValue;

        //        }
        //        else
        //        {
        //            if (lcCurrentGO.TryGetComponent<IHearable>(out lcCurrentHearableInterface))
        //            {
        //                lcValue = new StCacheValue()
        //                {
        //                    interfacePtr = lcCurrentHearableInterface,
        //                    ticksWithout = 0
        //                };

        //                _dictionaryOfSCachedtimuliReceivers.Add(key: lcCurrentGO, value: lcValue);
        //                // contruct soundStimuli struct and send it...

        //            }
        //            else
        //            {
        //                continue;
        //            }
        //        }
        //        // ... ... ... ... ... ... ... ... ... ... ... ... ... ... ... ... ... ... ... ... ... ... ... ... ... ... ... ... ... ... ... ... ... ... ... ... ... ... ... ... ... ... ... ... ... ... ... ... ... ... ...
        //        // old loop body
        //        // if it has already was cached

        //        //if (_dictionaryOfSCachedtimuliReceivers.ContainsKey(lcCurrentGO))
        //        //{
        //        //    lcCurrentHearableInterface = _dictionaryOfSCachedtimuliReceivers[lcCurrentGO];

        //        //}
        //        //// else added it to cached
        //        //else
        //        //{

        //        //    //lcCurrentHearableInterface = lcCurrentGO.GetComponent<IHearable>();
        //        //    if(lcCurrentGO.TryGetComponent<IHearable>(out lcCurrentHearableInterface))
        //        //    {
        //        //        // store it on a temp memory "buffer"
        //        //        _dictionaryOfSCachedtimuliReceivers.Add(lcCurrentGO, lcCurrentHearableInterface);
        //        //    }
        //        //    else
        //        //    {
        //        //        // if is not cached and has not the correct interface...
        //        //        // then do nothing
        //        //        continue;
        //        //    }
        //        //}


        //        // create sound stimuli struct to send
        //        StSoundStimuliSource lcSoundSource = new StSoundStimuliSource
        //        {
        //            stimuliSource = this,
        //            soundOriginPosition = inSoundOrigin,
        //            soundForce = inSoundForce,
        //        };

        //        // send stimuli
        //        lcValue.interfacePtr.IMethod_SenseSound(lcSoundSource);
        //    }

        //    Method_ClearCachle_V2();
        //}

        // added on 28 - Apr - 2026
        //public virtual void Method_ClearCache()
        //{
        //    List<GameObject> lcKeysToRemove = new List<GameObject>();


        //    List<GameObject> lcOverlapedGameObjects = new List<GameObject>();
        //    //List<GameObject> lcNotOverlapedGameObjects = new List<GameObject>();

        //    for (int i = 0; i < _OverlapedCollidersAmmount; i++)
        //    {
        //        lcOverlapedGameObjects.Add(_OverlapedCollidersBuffer[i].gameObject);
        //    }

        //        // remove null refs
        //    foreach (var item in _dictionaryOfSCachedtimuliReceivers)
        //    {
        //        GameObject lcGo = item.Key;
        //        IHearable lcInterface = item.Value.interfacePtr;

        //        if(lcGo == null || lcInterface == null)
        //        {
        //            lcKeysToRemove.Add(lcGo);
        //        }

        //        // if the overlaped list does not contain a cached gameobject that means that was not overlaped this tick, so iterate on cache memory
        //        if(lcOverlapedGameObjects.Contains(lcGo) == false)
        //        {
        //            //lcNotOverlapedGameObjects.Add(lcGo);
        //            StCacheValue lcTempValue = item.Value;
        //            lcTempValue.ticksWithout ++;
        //            _dictionaryOfSCachedtimuliReceivers[lcGo] = lcTempValue;
        //        }
        //        else
        //        {
        //            StCacheValue lcTempValue = item.Value;
        //            lcTempValue.ticksWithout = 0;
        //            _dictionaryOfSCachedtimuliReceivers[lcGo] = lcTempValue;
        //        }

        //        if(item.Value.ticksWithout >= _cacheMemorySize)
        //        {
        //            lcKeysToRemove.Add(lcGo);
        //        }
        //    }

        //    for(int i = 0; i < lcKeysToRemove.Count; i++)
        //    {
        //        _dictionaryOfSCachedtimuliReceivers.Remove(lcKeysToRemove[i]);
        //    }
        //}

        // added on 28 - Apr - 2026
        public void Method_ClearCachle_V2()
        {
            List<GameObject> lcKeysToRemove = new List<GameObject>();

            foreach (var item in _dictionaryOfSCachedtimuliReceivers)
            {
                /// automaticly increase the number of iterations that this GameObject was not overlaped, this value increases even if it overlaps
                /// cause in that it is not needed to compare Lists<> and create new ones to compare what GameObjects were overlaped and not
                /// a way of avoiding looping and alloc more Lists<> is just by incrase this, and on next "tick" overlap reset it to 0 the ones that were indeed overlaped

                var lcValue = item.Value;
                lcValue.ticksWithout++;
                _dictionaryOfSCachedtimuliReceivers[item.Key] = lcValue;

                if (item.Value.ticksWithout >= _cacheMemorySize)
                {
                    lcKeysToRemove.Add(item.Key);
                }
            }


            for(int i = 0;i < lcKeysToRemove.Count; i++)
            {
                _dictionaryOfSCachedtimuliReceivers.Remove(lcKeysToRemove[i]);
            }
        }
    }
}