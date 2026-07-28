using UnityEngine;
using UnityEngine.AI;
using MP_Npc.Behavior;
using MP_Npc.Perception;
//using MP_CoreUtilities.Data; // set NPC layer (self) and sense detection via core utils. "GameplayUtilitiesData"

// created on 02-Apr-2026
/// | 29 Jun 2026 | 001 | add tick rates for Brain and Perception
/// | 30 Jun 2026 | 002 | implement tick "rate"/interval
/// | 09 Jul 2026 | 003 |
namespace MP_Npc
{
    /// <summary>
    /// This MonoBehaviour is the NPC assembler/builder. It organizes the several moving parts of a NPC.
    /// </summary>
    [RequireComponent(typeof(NavMeshAgent))]
    public class NpcComponent : MonoBehaviour
    {
        private NavMeshAgent _navMeshAgent;

        public Transform _goTarget;
        protected AgentMovementComponent _movementComponent;

        protected BehaviourBrain _behaviourBrain;
        protected PerceptionSystem _perceptionSystem;

        protected GlobalPerceptionSystem _globalPerceptionSystem;

        private float _brainTickTimer = 0;
        private float _perceptionTickTimer = 0;
        private float _thisTickTImer = 0;

        //protected NpcBlackboard _npcBlackboard;


        [Header("[ DATA ]")]

        [SerializeField] protected NpcPerceptionData _perceptionData;
        [SerializeField] protected NpcPersonalityData _personalityData;
        [SerializeField] protected BehaviourData _behaviourData;
        [SerializeField] protected UtilityDeciderData _utilityDeciderData;

        private void Awake()
        {
            if(_navMeshAgent == null)
            {
                _navMeshAgent = GetComponent<NavMeshAgent>();

                if(_navMeshAgent == null)
                {
                    Debug.LogError(this + " : [ MARCO ] : if(_navMeshAgent == null)... !");
                }
            }

            // Create and Construct classes
            
            _perceptionSystem = new PerceptionSystem(_perceptionData, this.gameObject);
            _behaviourBrain = new BehaviourBrain(inNpcComponent: this, inPersonalityData: _personalityData, inGameObject: this.gameObject, inPerceptionSystem: _perceptionSystem);


        }
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

            // Agent mover Component
            if (_movementComponent == null)
            {
                _movementComponent = GetComponent<AgentMovementComponent>();
                if(_movementComponent == null)
                {
                    Debug.LogError(this + " : Can't get component <AgentMovementComponent>");
                }
            }

            if(_movementComponent != null)
            {
                _movementComponent.Method_Initialze(_navMeshAgent);
            }

            // set initial blackboard keys ( relative to the owner )
            _behaviourBrain.Method_SetBlackboardKeysOfOwnerReferences();

            _globalPerceptionSystem = FindAnyObjectByType<GlobalPerceptionSystem>();
            if (_globalPerceptionSystem != null) { Debug.Log(this + " : GPS found!"); }

        }

        private void Update()
        {

            _perceptionTickTimer += Time.deltaTime;
            if(_perceptionTickTimer >= _behaviourData.perceptionTickRate)
            {
                _perceptionSystem.Method_ExecutePerceptionSystem();

                _perceptionTickTimer = 0;
            }
            

            Method_HandleSelfOnGrid();


            // [ 30 Jun 2026 ] : Execute Brain ( on a tick interval rate )
            _brainTickTimer += Time.deltaTime;
            if(_brainTickTimer >= _behaviourData.aiBrainTickInterval)
            {
                // [ 29 Jun 2026 ] run brain and behaviour
                _behaviourBrain.ExecuteTick();

                // reset brain tick timer
                _brainTickTimer = 0;
            }

            _thisTickTImer += Time.deltaTime;
            if(_thisTickTImer >= _behaviourData.npcComponentTickInterval)
            {
                // call "tick dependent" functions
                //...
            }
            else
            {
                // call "non tick dependent" functions
                //...
                return;
            }
        }

        protected virtual void Method_SetNavMeshAgentParameters()
        {
            //_navMeshAgent.speed =
        }

        // added on 20 - Apr -2026
        public PerceptionSystem Method_ReturnPerceptionSystem()
        {
            if(_perceptionSystem != null)
            {
                return _perceptionSystem;
            }
            else { return null; }
        }

        // added on 20 - Apr -2026
        public NavMeshAgent Method_ReturnNavMeshAgent()
        {
            if(_navMeshAgent != null)
            {
                return _navMeshAgent;
            }
            else { return null; }
        }

        // added on [ 30 - Apr - 2026 ]
        public void Method_SetGPS(in GlobalPerceptionSystem inGPS)
        {
            if (inGPS != null)
            {
                _globalPerceptionSystem = inGPS;
            }
        }

        private Vector3Int _lastCellCoordinate;
        private Vector3Int _currentCellCoordinate;
        // added on [ 30 - Apr - 2026 ]
        protected void Method_HandleSelfOnGrid()
        {

            _globalPerceptionSystem.Method_ReturnGridData(out Vector3Int lc_GridSize, out float lc_CellSize);

            //int x = Mathf.FloorToInt(transform.position.x / lc_CellSize);
            //int y = Mathf.FloorToInt(transform.position.y / lc_CellSize);
            //int z = Mathf.FloorToInt(transform.position.z / lc_CellSize);

            //_currentCellCoordinate = new Vector3Int(x, y, z);

            // v2
            _currentCellCoordinate = _globalPerceptionSystem.Method_WorldToCell(transform.position);
            

            if (_currentCellCoordinate != _lastCellCoordinate)
            {
                _globalPerceptionSystem.Method_UpdateNpcToGridValue(this, _currentCellCoordinate, _lastCellCoordinate);
            }

            _lastCellCoordinate = _currentCellCoordinate;
        }

        // GIZMOS stuff | GIZMOS stuff | GIZMOS stuff | GIZMOS stuff | GIZMOS stuff | GIZMOS stuff | GIZMOS stuff | GIZMOS stuff | GIZMOS stuff | GIZMOS stuff | GIZMOS stuff | GIZMOS stuff | GIZMOS stuff | GIZMOS stuff | GIZMOS stuff |

        [Header("Colors")]
        [SerializeField] private Color _gizmoColorPathCorner = Color.rebeccaPurple;
        [SerializeField] private Color _gizmoColorPathLines = Color.mediumPurple;
        private Color _tempColor;
        private void OnDrawGizmos()
        {
            if (Application.isPlaying)
            {
                _perceptionSystem.Method_DrawPerceptionGizmos();
            }

            //if(_currentPath.corners.Length != 0)
            //{
            //    //_tempColor = _gizmoColorPathCorner;
            //    //float lcAlphaSteps = 1.0f/_currentPath.corners.Length;
            //    //Debug.Log("1/" + _currentPath.corners.Length + " = " + lcAlphaSteps);

            //    for (int i = 0; i<_currentPath.corners.Length; i++)
            //    {
            //        //_tempColor.a -= lcAlphaSteps;
            //        //Gizmos.color = _tempColor;
            //        //Debug.Log(_tempColor);

            //        Gizmos.DrawWireSphere(_currentPath.corners[i], _navMeshAgent.radius);

            //        // first
            //        if (i == 0)
            //        {
            //            Gizmos.color = Color.blue;
            //            Gizmos.DrawWireSphere(_currentPath.corners[i], _navMeshAgent.radius);
            //        }
            //        // last
            //        if (i == _currentPath.corners.Length - 1)
            //        {
            //            Gizmos.color = Color.darkSeaGreen;
            //            Gizmos.DrawWireSphere(_currentPath.corners[i], _navMeshAgent.radius);
            //        }
            //        if (i != 0 && i != _currentPath.corners.Length - 1)
            //        {

            //            _nextPoint = _currentPath.corners[i + 1];

            //            Gizmos.color = _gizmoColorPathCorner;
            //            Gizmos.DrawWireSphere(_nextPoint, _navMeshAgent.radius);
            //        }

            //    }
            //    Gizmos.color = _gizmoColorPathLines;
            //    Gizmos.DrawLineStrip(_currentPath.corners, false);

                //Gizmos.color = Color.red;
                //Gizmos.DrawWireSphere(_goTarget.position, 1);

            //}
        }
    }
}