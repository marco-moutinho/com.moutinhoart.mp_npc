using MP_Npc.Behavior;
using MP_Npc.Perception;
using UnityEngine;
using UnityEngine.AI;
//using MP_CoreUtilities.Data; // set NPC layer (self) and sense detection via core utils. "GameplayUtilitiesData"

// created on 02-Apr-2026
namespace MP_Npc
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class NpcComponent : MonoBehaviour
    {
        private NavMeshAgent _navMeshAgent;

        private Vector3 _goTargetLocation;
        private NavMeshPath _currentPath;

        public Transform _goTarget;
        protected AgentMovementComponent _movementComponent;

        protected BehaviourBrain _behaviourBrain;
        protected PerceptionSystem _perceptionSystem;
        //protected NpcBlackboard _npcBlackboard;

        [Header("[ DATA ]")]

        [SerializeField] private NpcPerceptionData _perceptionData;
        [SerializeField] protected NpcPersonalityData _personalityData;

        private void OnValidate()
        {
            if (_navMeshAgent == null)
            {
                _navMeshAgent = GetComponent<NavMeshAgent>();

                if (_navMeshAgent == null)
                {
                    Debug.LogError(this + " : [ MARCO ] : if(_navMeshAgent == null)... !");
                }
            }
            _currentPath = new NavMeshPath();
        }
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
            _behaviourBrain = new BehaviourBrain(inNpcComponent: this, inPersonalityData: _personalityData, inGameObject: gameObject);
            _perceptionSystem = new PerceptionSystem(_perceptionData, this.gameObject);

           
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
        }
        private void Update()
        {
            Vector3 lvDesiredVelocity = _navMeshAgent.desiredVelocity;

            _perceptionSystem.Method_ExecutePerceptionSystem();
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

        private Vector3 _nextPoint;
        private Vector3 _previousPoint;

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

            if(_currentPath.corners.Length != 0)
            {
                //_tempColor = _gizmoColorPathCorner;
                //float lcAlphaSteps = 1.0f/_currentPath.corners.Length;
                //Debug.Log("1/" + _currentPath.corners.Length + " = " + lcAlphaSteps);

                for (int i = 0; i<_currentPath.corners.Length; i++)
                {
                    //_tempColor.a -= lcAlphaSteps;
                    //Gizmos.color = _tempColor;
                    //Debug.Log(_tempColor);

                    Gizmos.DrawWireSphere(_currentPath.corners[i], _navMeshAgent.radius);

                    // first
                    if (i == 0)
                    {
                        Gizmos.color = Color.blue;
                        Gizmos.DrawWireSphere(_currentPath.corners[i], _navMeshAgent.radius);
                    }
                    // last
                    if (i == _currentPath.corners.Length - 1)
                    {
                        Gizmos.color = Color.darkSeaGreen;
                        Gizmos.DrawWireSphere(_currentPath.corners[i], _navMeshAgent.radius);
                    }
                    if (i != 0 && i != _currentPath.corners.Length - 1)
                    {

                        _nextPoint = _currentPath.corners[i + 1];

                        Gizmos.color = _gizmoColorPathCorner;
                        Gizmos.DrawWireSphere(_nextPoint, _navMeshAgent.radius);
                    }

                }
                Gizmos.color = _gizmoColorPathLines;
                Gizmos.DrawLineStrip(_currentPath.corners, false);

                //Gizmos.color = Color.red;
                //Gizmos.DrawWireSphere(_goTarget.position, 1);

            }
        }
    }
}