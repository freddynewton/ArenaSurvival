using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace freddynewton.Ability
{
    [CreateAssetMenu(menuName = "Player/Ability/Lightning Bolt")]
    public class LightningBoltAbility : Ability
    {
        public GameObject Bolt;
        [SerializeField] protected int maxTargets = 3;
        [SerializeField] protected float targetRadius = 10;
        [SerializeField] protected float hitTimer = 1;
        [SerializeField] protected LayerMask targetLayer = default;

        public override void Use(PlayerAbilityManager playerAbilityManager)
        {
            Collider[] possibleTargets = Physics.OverlapSphere(playerAbilityManager.transform.position, targetRadius, targetLayer);

            if (possibleTargets.Length > 0)
            {
                var targets = possibleTargets.Select(collider => collider.transform).Take(maxTargets);
                FireBolts(targets);
            }
        }

        private void InstantiateProjectile(Vector3 vector)
        {
            var projectileObj = Instantiate(Bolt, vector, Quaternion.identity, null);

            projectileObj.layer = LayerMask.NameToLayer("PlayerBullet");

            var spell = projectileObj.GetComponent<Spell>();
            spell.HitTimer = hitTimer;
        }

        private async void FireBolts(IEnumerable<Transform> targetPositions)
        {
            foreach(Transform target in targetPositions)
            {
                InstantiateProjectile(target.position);
                await Task.Delay((int)(CoolDownTime*100));
            }
        }
    }
}
