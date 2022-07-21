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
        [SerializeField] protected float bulletLifeTime;
        [SerializeField] protected float bulletFlyArc;

        public override void Use(PlayerAbilityManager playerAbilityManager)
        {
            base.Use(playerAbilityManager);
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
                projectileObj.GetComponent<Rigidbody>().velocity = GetPointInShootingArc(pointToShoot - spawnPosition).normalized * pojectileSpeed * Time.deltaTime;

                playerAbilityManager.StartCoroutine(projectileObj.GetComponent<Bullet>().DestroyBullet(bulletLifeTime));

                iTween.PunchPosition(projectileObj, new Vector3(Random.Range(-arcRange, arcRange), 0, Random.Range(-arcRange, arcRange)), Random.Range(punchTimeMin, punchTimeMax));
            }
        }

        protected Vector3 GetPointInShootingArc(Vector3 direction)
        {
            return Quaternion.AngleAxis(Random.Range(-bulletFlyArc, bulletFlyArc), Vector3.up) * direction;
        }
    }
}
