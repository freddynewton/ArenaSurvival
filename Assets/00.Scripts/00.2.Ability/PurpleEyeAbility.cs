using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace freddynewton.Ability
{
    [CreateAssetMenu(menuName = "Player/Ability/Purple Eye")]
    public class PurpleEyeAbility : Ability
    {
        public GameObject PurpleEye;
        public float bulletLifeTime;

        public override void Use(PlayerAbilityManager playerAbilityManager)
        {
            var pointToLook = CameraCrosshair.Instance.GetPointToLook();

            if (pointToLook != Vector3.zero)
            {
                InstantiateProjectile(playerAbilityManager.transform.position + new Vector3(0, 0.5f, 0), pointToLook);
            }
        }

        private void InstantiateProjectile(Vector3 vector3, Vector3 pointToLook)
        {
            var projectileObj = Instantiate(PurpleEye, pointToLook, Quaternion.identity, null);

            projectileObj.layer = LayerMask.NameToLayer("PlayerBullet");

            var bullet = projectileObj.GetComponent<Bullet>();
            bullet.StartCoroutine(bullet.DestroyBullet(bulletLifeTime));
        }
    }
}
