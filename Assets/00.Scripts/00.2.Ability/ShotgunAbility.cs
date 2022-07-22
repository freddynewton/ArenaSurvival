using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace freddynewton.Ability
{

    [CreateAssetMenu(menuName = "Player/Ability/Shotgun Shoot")]
    public class ShotgunAbility : BasicShootAbility
    {
        [Header("Shotgun Stats")]
        [SerializeField] protected int bulletAmount;
        [SerializeField] protected float bulletFlyArc;

        public override void Use(PlayerAbilityManager playerAbilityManager)
        {
            this.playerAbilityManager = playerAbilityManager;

            pointToLook = CameraCrosshair.Instance.GetPointToLook();

            if (pointToLook != Vector3.zero)
            {
                ApplyPlayerBackwardsKnockback(knockbackStrength, playerAbilityManager);
                InstantiateProjectile(playerAbilityManager.transform.position + new Vector3(0, 0.5f, 0), pointToLook);
            }
        }

        protected override void InstantiateProjectile(Vector3 spawnPosition, Vector3 pointToShoot)
        {
            var muzzleFlash = Instantiate(this.muzzleFlash, spawnPosition, Quaternion.identity, null);
            muzzleFlash.transform.LookAt(pointToShoot);
            muzzleFlash.layer = LayerMask.NameToLayer("PlayerBullet");
            Destroy(muzzleFlash, 1);

            for (int i = 0; i < bulletAmount; i++)
            {
                var projectileObj = Instantiate(projectile, spawnPosition, Quaternion.identity, null);
                projectileObj.layer = LayerMask.NameToLayer("PlayerBullet");
                projectileObj.GetComponent<Rigidbody>().velocity = GetPointInShootingArc(pointToShoot - spawnPosition, i).normalized * pojectileSpeed * Time.deltaTime;


                var bullet = projectileObj.GetComponent<Bullet>();
                bullet.StartCoroutine(bullet.DestroyBullet(bulletLifeTime));

                iTween.PunchPosition(projectileObj, new Vector3(Random.Range(-arcRange, arcRange), 0, Random.Range(-arcRange, arcRange)), Random.Range(punchTimeMin, punchTimeMax));
            }
        }

        protected Vector3 GetPointInShootingArc(Vector3 direction, int index)
        {
            var rotation = bulletFlyArc - bulletAmount;

            if (index % 2 != 0)
            {
                rotation = -rotation;
            }

            return Quaternion.AngleAxis(rotation * index, Vector3.up) * direction;
        }
    }
}
