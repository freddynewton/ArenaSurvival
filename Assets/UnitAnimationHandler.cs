using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace freddynewton.AI
{
    public class UnitAnimationHandler : MonoBehaviour
    {
        [SerializeField]
        private NavMeshAgent navMeshAgent;

        [SerializeField]
        private Animator animator;

        private void Awake()
        {
            navMeshAgent = navMeshAgent ?? GetComponent<NavMeshAgent>();
            animator = animator ?? GetComponent<Animator>();
            navMeshAgent.updatePosition = false;
        }

        private void Update()
        {
            CheckMovementSpeed();
            OnAnimatorMove();
        }

        private void CheckMovementSpeed()
        {
            animator.SetFloat("speedMagnitude", navMeshAgent.velocity.normalized.magnitude);
            animator.SetBool("isMoving", navMeshAgent.velocity != Vector3.zero);
        }

       private void OnAnimatorMove()
        {
            Vector3 position = animator.rootPosition;
            position.y = navMeshAgent.nextPosition.y;
            transform.position = position;
            navMeshAgent.nextPosition = transform.position;

            var localVelocity = transform.InverseTransformDirection(navMeshAgent.velocity);
            //animator.SetFloat("Forward", localVelocity.z);
            //animator.SetFloat("Sideways", localVelocity.x);
        }
    }
}
