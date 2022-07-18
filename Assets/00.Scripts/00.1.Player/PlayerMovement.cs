using freddynewton.Helper;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace freddynewton.player
{
    public class PlayerMovement : MonoBehaviour
    {
        public Rigidbody playerRigidbody;

        [HideInInspector] public Vector3 playerMovementVector;
        [HideInInspector] public bool canGetPlayerInput = true;

        private void Update()
        {
            if (canGetPlayerInput)
            {
                GetPlayerMovementVector();
            }

            ApplyLookDirection();
            ApplyPlayerMovementVector();
        }

        private void GetPlayerMovementVector()
        {
            var input = PlayerManager.Instance.PlayerInputActionAsset.FindActionMap("Player").FindAction("Move").ReadValue<Vector2>();
            playerMovementVector = new Vector3(input.x, 0, input.y);
        }

        private void ApplyLookDirection()
        {
            Ray cameraRay = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
            float rayLength;

            if (groundPlane.Raycast(cameraRay, out rayLength))
            {
                Vector3 pointToLook = cameraRay.GetPoint(rayLength);
                transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
            }
        }

        private void ApplyPlayerMovementVector()
        {
            if (playerMovementVector.magnitude > 0)
            {
                playerRigidbody.MovePosition(playerMovementVector.ToIso()
                    * PlayerManager.Instance.PlayerStats.MovementSpeed
                    * PlayerManager.Instance.PlayerStats.MovementSpeedMultiplier
                    * Time.deltaTime
                    + transform.position);
            }
        }

        private void Awake()
        {
            PlayerManager.Instance.playerMovement = this;
        }
    }
}
