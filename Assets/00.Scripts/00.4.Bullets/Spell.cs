using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace freddynewton
{
    public class Spell : Bullet
    {
        public float impactRadius = 2f;
        public float HitTimer = 1f;

        private void Start()
        {
            StartCoroutine(HitTargets());
        }

        private IEnumerator HitTargets()
        {
            yield return new WaitForSeconds(HitTimer);
            Collider[] affectedUnits = Physics.OverlapSphere(transform.position, impactRadius, hitLayer);

            foreach (Collider unit in affectedUnits)
            {
                unit.gameObject.GetComponentInParent<Unit>().DoDamage(Damage);
            }

            yield return new WaitForSeconds(2);
            DestroyBullet();
        }
    }
}
