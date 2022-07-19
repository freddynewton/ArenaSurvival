using freddynewton.player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.InputSystem;
using System;

namespace freddynewton.Ability
{
    public class PlayerAbilityManager : MonoBehaviour
    {
        [SerializeField] public Ability[] Abilities = new Ability[5];

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
            for (int i = 0; i < Abilities.Length; i++)
            {
                if (Abilities[i] != null)
                {
                    if (Abilities[i].currentCooldownTime > 0)
                    {
                        Abilities[i].currentCooldownTime -= Time.deltaTime;
                    }
                }
            }

            CheckMouseButton0Press();
        }

        private Ability GetAbility(AbilityType abilityType)
        {
            return Abilities.ToList().Find(ability => ability.AbilityType == abilityType);
        }

        private void CheckMouseButton0Press()
        {
            var ability = GetAbility(AbilityType.MOUSE0);

            if (ability == null)
            {
                return;
            }

            if (ability.currentCooldownTime <= 0 && ability.isButtonPressed)
            {
                ability.currentCooldownTime = ability.CoolDownTime;
                ability.Use(this);
            }
        }

        private void Awake()
        {
            PlayerManager.Instance.PlayerInputActionAsset.FindActionMap("Player").FindAction("Space").started += PlayerAbilityManager_started;
            PlayerManager.Instance.PlayerInputActionAsset.FindActionMap("Player").FindAction("Fire").started += PlayerAbilityManager_Mouse0_Activate;
            PlayerManager.Instance.PlayerInputActionAsset.FindActionMap("Player").FindAction("Fire").canceled += PlayerAbilityManager_Mouse0_Deactivate;
        }

        private void PlayerAbilityManager_Mouse0_Deactivate(InputAction.CallbackContext obj)
        {
            var ability = GetAbility(AbilityType.MOUSE0);

            if (ability == null)
            {
                return;
            }

            ability.isButtonPressed = false;
        }

        private void PlayerAbilityManager_Mouse0_Activate(InputAction.CallbackContext obj)
        {
            var ability = GetAbility(AbilityType.MOUSE0);

            if (ability == null)
            {
                return;
            }

            ability.isButtonPressed = true;
        }

        private void PlayerAbilityManager_started(InputAction.CallbackContext obj)
        {
            var ability = GetAbility(AbilityType.SPACEBAR);
            Debug.Log("Ability name: " + ability.name);
            if (ability == null)
            {
                return;
            }

            if (ability.currentCooldownTime <= 0)
            {
                ability.currentCooldownTime = ability.CoolDownTime;
                ability.Use(this);
            }
        }
    }
}
