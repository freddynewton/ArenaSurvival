using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace freddynewton.Ability
{
    [CreateAssetMenu(menuName = "Player/Ability/Basic Shoot")]
    public class BasicShootAbility : Ability
    {
        [SerializeField] protected GameObject projectile;
        [SerializeField] protected GameObject muzzleFlash;
        [SerializeField] protected float pojectileSpeed = 300;
        [SerializeField] protected float arcRange = 1;
        [SerializeField] protected float punchTimeMin = 0.5f;
        [SerializeField] protected float punchTimeMax = 5f;
        [SerializeField] protected float knockbackStrength = 10;
        [SerializeField] protected float bulletLifeTime = 10;

        protected Vector3 pointToLook;
        protected PlayerAbilityManager playerAbilityManager;

        public override void Use(PlayerAbilityManager playerAbilityManager)
        {
            this.playerAbilityManager = playerAbilityManager;

            pointToLook = CameraCrosshair.Instance.GetPointToLook();

            if (pointToLook != Vector3.zero)
            {
                InstantiateProjectile(playerAbilityManager.transform.position + new Vector3(0, 0.5f, 0), pointToLook);
            }
        }

        protected virtual void InstantiateProjectile(Vector3 spawnPosition, Vector3 pointToShoot)
        {
            var muzzleFlash = Instantiate(this.muzzleFlash, spawnPosition, Quaternion.identity, null);
            var projectileObj = Instantiate(projectile, spawnPosition, Quaternion.identity, null);

            projectileObj.layer = LayerMask.NameToLayer("PlayerBullet");
            muzzleFlash.layer = LayerMask.NameToLayer("PlayerBullet");


            muzzleFlash.transform.LookAt(pointToShoot);
            Destroy(muzzleFlash, 1);

            projectileObj.GetComponent<Rigidbody>().velocity = (pointToShoot - spawnPosition).normalized * pojectileSpeed * Time.deltaTime;

            var bullet = projectileObj.GetComponent<Bullet>();
            bullet.StartCoroutine(bullet.DestroyBullet(bulletLifeTime));

            iTween.PunchPosition(projectileObj, new Vector3(Random.Range(-arcRange, arcRange), 0, Random.Range(-arcRange, arcRange)), Random.Range(punchTimeMin, punchTimeMax));
        }
    }
}
