using freddynewton.Ability;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace freddynewton.Ability
{
    [CreateAssetMenu(menuName = "Player/Ability/Aimed Shoot")]
    public class AimedShootAbility : Ability
    {
        [SerializeField] protected GameObject projectile;
        [SerializeField] protected GameObject muzzleFlash;
        [SerializeField] protected float pojectileSpeed = 300;
        [SerializeField] protected int maxTargets = 3;
        [SerializeField] protected float targetRadius = 10;
        [SerializeField] protected LayerMask targetLayer = default;

        protected PlayerAbilityManager playerAbilityManager;

        public override void Use(PlayerAbilityManager playerAbilityManager)
        {
            this.playerAbilityManager = playerAbilityManager;

            Collider[] possibleTargets = Physics.OverlapSphere(playerAbilityManager.transform.position, targetRadius, targetLayer);

            if (possibleTargets.Length > 0)
            {
                var targets = possibleTargets.Select(collider => collider.transform).Take(maxTargets);
                InstantiateProjectile(playerAbilityManager.transform.position + new Vector3(0, 0.5f, 0), targets);
            }
        }

        protected virtual void InstantiateProjectile(Vector3 spawnPosition, IEnumerable<Transform> targets)
        {
            var muzzleFlash = Instantiate(this.muzzleFlash, spawnPosition, Quaternion.identity, null);
            var projectileObj = Instantiate(projectile, spawnPosition, Quaternion.identity, null);

            projectileObj.layer = LayerMask.NameToLayer("PlayerBullet");
            muzzleFlash.layer = LayerMask.NameToLayer("PlayerBullet");

            Destroy(muzzleFlash, 1);

            var bullet = projectileObj.GetComponent<AimedBullet>();
            bullet.SetupTargets(targets);
            bullet.Speed = pojectileSpeed;
        }
    }
}
