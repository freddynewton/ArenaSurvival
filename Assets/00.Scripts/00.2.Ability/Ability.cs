using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace freddynewton.Ability
{
    public abstract class Ability : ScriptableObject
    {
        public string Name;
        public string Description;
        public Sprite Icon;
        public AbilityType AbilityType = AbilityType.MOUSE0;

        public float CoolDownTime;
        [HideInInspector] public float currentCooldownTime;
        [HideInInspector] public bool isButtonPressed;

        public abstract void Use(PlayerAbilityManager playerAbilityManager);

        protected void ApplyPlayerBackwardsKnockback(float strength, PlayerAbilityManager playerAbilityManager)
        {
            playerAbilityManager.Playermovement.playerRigidbody.AddForce((playerAbilityManager.transform.position - CameraCrosshair.Instance.GetPointToLook()).normalized * strength * Time.deltaTime, ForceMode.Impulse);
        }
    }
}
