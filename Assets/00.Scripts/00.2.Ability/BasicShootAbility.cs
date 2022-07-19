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

        public override void Use(PlayerAbilityManager playerAbilityManager)
        {
            Debug.Log("Shoot");
            Ray cameraRay = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
            float rayLength;
            Vector3 pointToLook;

            if (groundPlane.Raycast(cameraRay, out rayLength))
            {
                pointToLook = cameraRay.GetPoint(rayLength);
                pointToLook.y += 0.5f;
                InstantiateProjectile(playerAbilityManager.transform.position + new Vector3(0, 0.5f, 0), pointToLook);
            }
        }

        private void InstantiateProjectile(Vector3 spawnPosition, Vector3 pointToShoot)
        {
            var muzzleFlash = Instantiate(this.muzzleFlash, spawnPosition, Quaternion.identity, null);
            var projectileObj = Instantiate(projectile, spawnPosition, Quaternion.identity, null);

            projectileObj.layer = LayerMask.NameToLayer("PlayerBullet");
            muzzleFlash.layer = LayerMask.NameToLayer("PlayerBullet");


            muzzleFlash.transform.LookAt(pointToShoot);
            Destroy(muzzleFlash, 1);

            projectileObj.GetComponent<Rigidbody>().velocity = (pointToShoot - spawnPosition).normalized * pojectileSpeed * Time.deltaTime;

            iTween.PunchPosition(projectileObj, new Vector3(Random.Range(-arcRange, arcRange), 0, Random.Range(-arcRange, arcRange)), Random.Range(punchTimeMin, punchTimeMax));
        }
    }
}
