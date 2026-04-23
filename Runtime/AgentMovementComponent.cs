using UnityEngine;
using UnityEngine.AI;
// created on 11-Apr-2026
namespace MP_Npc
{

    public class AgentMovementComponent : MonoBehaviour
    {
        protected NavMeshAgent m_NavMeshAgent;

        private Vector3 m_MoveDirection;
        private float m_MoveSpeed;
        private Vector3 _moveOffset;

        private Vector3 m_MoveToPosition;
        private NavMeshHit _navMeshHit;
        private bool _validMovePosition;

        private RaycastHit _raycastHit;
        public LayerMask _layerMask;
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            m_MoveToPosition = transform.position + transform.forward * 2;
            Method_MoveAgent();
        }

        // added on 11-Apr-2026
        public void Method_Initialze(in NavMeshAgent inAgentComponent)
        {
            // safety check
            if(inAgentComponent == null)
            {
                Debug.LogError(this + " : Method_Initialze(...) : inAgentComponent is null !!!");
                return;
            }

            m_NavMeshAgent = inAgentComponent;
        }

        // added on 11-Apr-2026
        public void Method_MoveAgent()
        {
            if(m_NavMeshAgent == null) { Debug.LogError(this + " : Method_MoveAgent() : m_NavMeshAgent == null !!!"); return; }
            
            m_NavMeshAgent.Raycast(m_MoveToPosition, out _navMeshHit);
            
            _validMovePosition = _navMeshHit.hit;

            Physics.SphereCast(origin: transform.position, radius: 0.25f, direction: _navMeshHit.position - transform.position, out _raycastHit, maxDistance: (_navMeshHit.position - transform.position).magnitude, layerMask: _layerMask, QueryTriggerInteraction.Collide);

            m_MoveDirection = transform.forward;
            _moveOffset = m_MoveDirection * m_NavMeshAgent.speed;
            _moveOffset *= Time.deltaTime;
            m_NavMeshAgent.Move(_moveOffset);
        }


        // GIZMOS | GIZMOS | GIZMOS | GIZMOS | GIZMOS | GIZMOS | GIZMOS | GIZMOS | GIZMOS | GIZMOS | GIZMOS | GIZMOS | GIZMOS | GIZMOS | GIZMOS | GIZMOS | GIZMOS | GIZMOS | GIZMOS | GIZMOS | GIZMOS | GIZMOS | GIZMOS |
        public Color color_MovePosition = Color.violetRed;
        public Color color_MoveDirection = Color.softRed;
        public Color color_Forward;
        public Color color_Sideways;
        public Color color_NavMeshHit = Color.red;
        private void OnDrawGizmos()
        {
                Gizmos.color = color_MovePosition;
                Gizmos.DrawWireSphere(m_MoveToPosition, 0.15f);
                Gizmos.DrawLine(transform.position, m_MoveToPosition);


            Gizmos.color = color_NavMeshHit;
            Gizmos.DrawWireSphere(_navMeshHit.position, 0.2f);
            Gizmos.DrawWireSphere(_navMeshHit.position, 0.1f);
            Gizmos.DrawLine(transform.position, _navMeshHit.position);

            Gizmos.color = Color.yellow;
                Gizmos.DrawWireSphere(_raycastHit.point, 0.05f);
                Gizmos.DrawLine(transform.position, _raycastHit.point);
            
        }
    }
}