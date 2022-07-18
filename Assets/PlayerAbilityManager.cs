using freddynewton.player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace freddynewton.Ability
{
    public class PlayerAbilityManager : MonoBehaviour
    {
        public Ability mouse0Ability;
        private float currentMouse0AbilityGlobalCD = 1;

        public Ability mouse1Ability;
        private float currentMouse1AbilityGlobalCD = 1;

        public Ability eAbility;
        private float currentEAbilityGlobalCD = 1;

        public Ability qAbility;
        private float currentQAbilityGlobalCD = 1;

        public Ability spaceAbility;
        private float currentSpaceAbilityGlobalCD = 1;

        private PlayerMovement playerMovement;

        public PlayerMovement Playermovement
        {
            get
            {
                if (playerMovement == null)
                {
                    playerMovement = FindObjectOfType<PlayerMovement>();
                }

                return playerMovement;
            }

            set
            {
                playerMovement = value;
            }
        }

        private void Update()
        {
            if (currentSpaceAbilityGlobalCD > 0)
            {
                currentSpaceAbilityGlobalCD -= Time.deltaTime;
            }

            if (currentMouse0AbilityGlobalCD > 0)
            {
                currentMouse0AbilityGlobalCD -= Time.deltaTime;
            }

            if (currentMouse1AbilityGlobalCD > 0)
            {
                currentMouse1AbilityGlobalCD -= Time.deltaTime;
            }

            if (currentEAbilityGlobalCD > 0)
            {
                currentEAbilityGlobalCD -= Time.deltaTime;
            }

            if (currentQAbilityGlobalCD > 0)
            {
                currentQAbilityGlobalCD -= Time.deltaTime;
            }

            CheckMouseButton0Press();
        }

        private void CheckMouseButton0Press()
        {
            if (mouse0Ability == null)
            {
                return;
            }

            if (currentMouse0AbilityGlobalCD <= 0 && mouse0Ability.isButtonPressed)
            {
                currentMouse0AbilityGlobalCD = mouse0Ability.CoolDownTime;
                mouse0Ability.Use(this);
            }
        }

        private void Awake()
        {
            PlayerManager.Instance.PlayerInputActionAsset.FindActionMap("Player").FindAction("Space").started += PlayerAbilityManager_Space_started;
            PlayerManager.Instance.PlayerInputActionAsset.FindActionMap("Player").FindAction("Fire").started += PlayerAbilityManager_Mouse0_Activate;
            PlayerManager.Instance.PlayerInputActionAsset.FindActionMap("Player").FindAction("Fire").canceled += PlayerAbilityManager_Mouse0_Deactivate;
        }

        private void PlayerAbilityManager_Mouse0_Activate(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            if (mouse0Ability == null)
            {
                return;
            }

            mouse0Ability.isButtonPressed = true;
        }

        private void PlayerAbilityManager_Mouse0_Deactivate(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            if (mouse0Ability == null)
            {
                return;
            }

            mouse0Ability.isButtonPressed = false;
        }

        private void PlayerAbilityManager_Space_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            if (spaceAbility == null)
            {
                return;
            }

            if (currentSpaceAbilityGlobalCD <= 0)
            {
                currentSpaceAbilityGlobalCD = spaceAbility.CoolDownTime;
                spaceAbility.Use(this);
            }
        }
    }
}
