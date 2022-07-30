using freddynewton.player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace freddynewton.AI
{
    public class UnitMovement : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField]
        private float DistanceToNewPoint;

        [SerializeField]
        private float stopRange;

        [SerializeField]
        private float movementSpeed;


        [Header("Assigns")]
        [SerializeField]
        private NavMeshObstacle navMeshObstacle;

        [SerializeField]
        private Rigidbody rigidbody;


        public GameObject target;
        private NavMeshPath path;

        private void Awake()
        {
            path = new NavMeshPath();
            NavMesh.avoidancePredictionTime = 0.5f;
        }

        private void Update()
        {
            FollowPath();
        }

        private void FollowPath()
        {
            if (Vector3.Distance(transform.position, target.transform.position) > stopRange)
            {
                CalculatePath();

                if (path.status == NavMeshPathStatus.PathComplete)
                {
                rigidbody.MovePosition(transform.position + (transform.position - path.corners[0]).normalized * Time.deltaTime * movementSpeed);
                }
            }
        }

        private void CalculatePath()
        {
            path.ClearCorners();
            NavMesh.CalculatePath(transform.position, target.transform.position, -1, path);
        }
    }
}
