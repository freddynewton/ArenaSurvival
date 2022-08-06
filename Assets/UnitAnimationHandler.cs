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
        }

        private void Update()
        {
            CheckMovementSpeed();
        }

        private void CheckMovementSpeed()
        {
            animator.SetFloat("WaalkSpeed", animator.velocity.normalized.magnitude);
            animator.SetBool("isMoving", navMeshAgent.velocity != Vector3.zero);
        }
    }
}
