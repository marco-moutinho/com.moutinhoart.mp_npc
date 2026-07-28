using UnityEngine;
using UnityEngine.AI;
// created in [ 29 - Apr - 2026 ]
namespace MP_Npc
{
    public class NpcSpawner : MonoBehaviour
    {
        [SerializeField] private float _sampleRadius;
        [SerializeField] private GameObject _Npc;
        [SerializeField] private LayerMask _layerMask;
        private Collider[] _collidersBuffer;

        private Vector3 _storedPosition;
        private bool _hasFoundValidPosition;

        private int _CollidersFoundInt;
        private bool _hasObstruction;

        private void Start()
        {
            Method_SpawnNpc(_Npc);
        }

        private void Update()
        {

        }

        [ContextMenu(itemName: "GetNavMeshPos()")]
        protected void Method_TestSpawn()
        {
            //if(Method_IsAreaObstructionFree() == false) { return; }
            Method_TryGetNavMeshPosition(transform.position, out _storedPosition);
        }
        
        // added in [ 30 - Apr - 2026 ]
        public virtual void Method_SpawnNpc(GameObject inGameObject)
        {
            // safety check
            if(inGameObject == null)
            {
                Debug.LogError(this + " : Method_SpawnNpc(GameObject inGameObject) : inGameObject is null !!!");
                return;
            }

            if(Method_TryGetNavMeshPosition(transform.position, out _storedPosition))
            {
                //GameObject lcSpawnedGameObject = Instantiate(original:  inGameObject, position: _storedPosition, rotation: transform.rotation);
                
                // intead of doing this, instantiate on NpcGlobalManager, via this, this should only handle if can and when, basicaly conditions
                // ALSO should hold the possible spawn GameObjects/Npc ?

                // register NPC on global perception system
                //...
            }
            
        }

        // added in [ 29 - Apr - 2026 ]
        protected virtual bool Method_TryGetNavMeshPosition(in Vector3 inWorldPosition, out  Vector3 outNavMeshPosition)
        {
            NavMeshHit lcNavMeshHit;
            if(NavMesh.SamplePosition(sourcePosition: inWorldPosition, hit: out lcNavMeshHit, maxDistance: _sampleRadius, NavMesh.AllAreas))
            {
                //_storedPosition = outNavMeshPosition;

                outNavMeshPosition = lcNavMeshHit.position;
                _hasFoundValidPosition = true;
                return true;
            }
            else { outNavMeshPosition = Vector3.zero; _hasFoundValidPosition = false; return false;  }
        }

        // added in [ 30 - Apr - 2026 ]
        protected bool Method_IsAreaObstructionFree()
        {
            _CollidersFoundInt = Physics.OverlapSphereNonAlloc(transform.position, _sampleRadius, _collidersBuffer, _layerMask, QueryTriggerInteraction.Ignore);
            if(_CollidersFoundInt == 0)
            {
                return true;
            }
            else
            {
                _hasObstruction = true;
                return false;
            }

        }

        private void OnDrawGizmosSelected()
        {
            
            if (_hasFoundValidPosition)
            {
                Gizmos.color = Color.softRed;
                Gizmos.DrawLine(transform.position, _storedPosition);
                Gizmos.DrawWireSphere(_storedPosition, 0.25f);
            }

            Gizmos.color = Color.softGreen;
            Gizmos.DrawWireSphere(transform.position, _sampleRadius);
            Vector3 lcVectorTo = transform.position + (transform.forward * _sampleRadius);
            Gizmos.DrawLine(transform.position, lcVectorTo);
            Gizmos.DrawWireSphere(lcVectorTo, 0.15f);

            if (_hasObstruction)
            {
                Gizmos.color = Color.orangeRed;
                for (int i = 0; i < _CollidersFoundInt; i++)
                {
                    Gizmos.DrawLine(transform.position, _collidersBuffer[i].transform.position);
                }
            }
        }
    }
}