using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace freddynewton
{
    public class Bullet : MonoBehaviour
    {
        public LayerMask hitLayer;
        public float Damage;
        public GameObject ImpactVfx;

        private void OnCollisionEnter(Collision collision)
        {
            if (hitLayer == (hitLayer | (1 << collision.gameObject.layer)))
            {
                var impact = Instantiate(ImpactVfx, collision.contacts[0].point, Quaternion.identity) as GameObject;

                Destroy(impact, 2);

                var unit = collision.gameObject.GetComponent<Unit>();

                unit?.DoDamage(Damage);

                Destroy(gameObject);
            }
        }

        public void DestroyBullet()
        {
            if(gameObject.TryGetComponent<Rigidbody>(out var rb))
            {
                rb.velocity = Vector3.zero;
            }
            
            if(ImpactVfx != null)
            {
                var impact = Instantiate(ImpactVfx, transform.position, Quaternion.identity) as GameObject;

                Destroy(impact, 2);
            }

            Destroy(gameObject);
        }

        public IEnumerator DestroyBullet(float time)
        {
            yield return new WaitForSeconds(time);

            if (gameObject != null)
            {
                gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                var impact = Instantiate(ImpactVfx, transform.position, Quaternion.identity) as GameObject;

                Destroy(impact, 2);

                Destroy(gameObject);
            }
        }
    }
}
