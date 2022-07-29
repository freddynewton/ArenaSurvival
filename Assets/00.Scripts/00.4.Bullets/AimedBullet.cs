using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace freddynewton
{
    public class AimedBullet : Bullet
    {
        public float Speed;

        private IEnumerable<Transform> targets;
        private Transform nextTarget;

        public void SetupTargets(IEnumerable<Transform> targets)
        {
            this.targets = targets;
        }

        private void Update()
        {
            if(nextTarget == null)
            {
                if(targets.Count() == 0)
                {
                    DestroyBullet();
                    return;
                }

                nextTarget = targets.FirstOrDefault();
                targets = targets.Skip(1);
            }

            transform.LookAt(nextTarget.position);
            transform.Translate(Vector3.forward * Time.deltaTime * Speed);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (hitLayer == (hitLayer | (1 << collision.gameObject.layer)))
            {
                var impact = Instantiate(ImpactVfx, collision.contacts[0].point, Quaternion.identity) as GameObject;

                Destroy(impact, 2);

                var unit = collision.gameObject.GetComponent<Unit>();

                unit?.DoDamage(Damage);

                nextTarget = null;
            }
        }
    }
}
