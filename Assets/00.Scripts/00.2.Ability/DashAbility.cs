using freddynewton.player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace freddynewton.Ability
{
    [CreateAssetMenu(menuName = "Player/Ability/Dash")]
    public class DashAbility : Ability
    {
        [Header("Dash Settings")]
        public float DashTime;
        public float DashSpeed;
        public Material GlowMaterial;

        private PlayerAbilityManager PlayerAbilityManager;
        private MeshTrailEffect meshTrailEffect;

        public override void Use(PlayerAbilityManager playerAbilityManager)
        {
            if (meshTrailEffect == null)
            {
                meshTrailEffect = new MeshTrailEffect(PlayerManager.Instance.playerMovement.GetComponentsInChildren<SkinnedMeshRenderer>(), GlowMaterial);
            }

            this.PlayerAbilityManager = playerAbilityManager;

            playerAbilityManager.StartCoroutine(StopMoving());
        }

        private IEnumerator StopMoving()
        {
            PlayerAbilityManager.Playermovement.canGetPlayerInput = false;
            PlayerManager.Instance.PlayerStats.MovementSpeedMultiplier = DashSpeed;
            PlayerAbilityManager.StartCoroutine(meshTrailEffect.ActivateTrail(DashTime, DashTime * 2, meshRefreshRate: 0.01f));

            yield return new WaitForSecondsRealtime(DashTime);
            PlayerAbilityManager.Playermovement.canGetPlayerInput = true;
            PlayerManager.Instance.PlayerStats.MovementSpeedMultiplier = 1;
        }
    }
}
