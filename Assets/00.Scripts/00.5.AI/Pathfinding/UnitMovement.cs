using freddynewton.Helper;
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

        [Header("Debug")]
        [SerializeField]
        private bool drawPath;

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
                rigidbody.MovePosition((path.corners[1] - transform.position).normalized * Time.deltaTime * movementSpeed + transform.position);
                }
            }
        }

        private void CalculatePath()
        {
            if (path == null)
            {
                path = new NavMeshPath();
            }

            path.ClearCorners();
            NavMesh.CalculatePath(transform.position, target.transform.position, -1, path);
        }

        private void OnDrawGizmos()
        {
            if (drawPath)
            {
                CalculatePath();

                if (path.status == NavMeshPathStatus.PathComplete)
                {
                    Gizmos.color = Color.cyan;

                    for (int i = 0; i < path.corners.Length - 1; i++)
                    {
                        Gizmos.DrawLine(path.corners[i], path.corners[i + 1]);
                    }
                }
            }
        }
    }
}
