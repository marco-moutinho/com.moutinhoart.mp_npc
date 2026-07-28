using UnityEditor;
using UnityEngine;
namespace MP_Npc.PatrolPath
{
    // [ 28 Jul 2026 ] #Created
    public class PatrolPath : MonoBehaviour
    {
        [SerializeField]
        protected GameObject[] _patrolPoints;

        [SerializeField]
        protected float _patrolPointRadius = 0.5f; // use it to determine how close the Agent must be to the patrol point to be considered "reached"

        // DATA HELPER
        public Color PatrolPointColor = Color.magenta;
        public Color PathConectionColor = Color.orange;


        private void OnValidate()
        {
            XFuncInitializePatrolPointsArray();
        }

        private void Start()
        {
            XFuncInitializePatrolPointsArray();
        }

        // [ 28 Jul 2026 ] #Created
        protected void XFuncInitializePatrolPointsArray()
        {
            // 1. Allocate space for the array based on direct children count
            int childCount = gameObject.transform.childCount;
            if(childCount == 0) { return; }
            _patrolPoints = new GameObject[childCount];

            // 2. Populate the array with each direct child
            for (int i = 0; i < childCount; i++)
            {
                _patrolPoints[i] = transform.GetChild(i).gameObject;
            }
        }

        // [ 28 Jul 2026 ] #Created
        public void XFuncGetNextPatrolPoint()
        {

        }

        private void OnDrawGizmos()
        {
            XFuncDraw();
        }

        protected void XFuncDraw()
        {
            if(_patrolPoints.Length == 0) { return; }

            if(XFuncIsPathOrChildBeingSelected() == false) { return; }


            float dst = 0;
            float topDst = dst;

            for (int i = 0; i < _patrolPoints.Length; i++)
            {
                Gizmos.color = PatrolPointColor;

                // draw patrol points
                Gizmos.DrawWireSphere(_patrolPoints[i].transform.position, _patrolPointRadius);

                // calculate distance
                Vector3 a = transform.position;
                Vector3 b = _patrolPoints[i].transform.position;
                dst = (a - b).magnitude;

                // check distance
                if(i == 0) { topDst = dst; }
                if( dst > topDst) {  topDst = dst; }

                // gizmos stuff...
                Gizmos.color = PathConectionColor;
                if(i < _patrolPoints.Length -1)
                {
                    Gizmos.DrawLine(_patrolPoints[i].transform.position, _patrolPoints[i + 1].transform.position);
                }
            }

            //  draw a sphere from center to the furthest patrol point, just so i can easly see the area where where all points are
            // Patrol area center
            Gizmos.DrawWireCube(transform.position, Vector3.one);
           
            Color sphereMeshColor = PatrolPointColor; sphereMeshColor.a = 0.2f;
            Gizmos.color = sphereMeshColor;
            Gizmos.DrawSphere(transform.position, topDst);

            Gizmos.color = PatrolPointColor;
            Gizmos.DrawWireSphere(transform.position, topDst);

        }
#if UNITY_EDITOR
        protected bool XFuncIsPathOrChildBeingSelected()
        {
            GameObject activeGameObject = Selection.activeGameObject;
            if(activeGameObject == null ) { return false; }

            // check if is self
            if(activeGameObject == gameObject) { return true; }

            // check if is a child gameobject
            if (activeGameObject.transform.IsChildOf(gameObject.transform)) {  return true; }

            return false;
        }
#endif
    }
}