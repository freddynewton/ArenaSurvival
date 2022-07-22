using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace freddynewton
{
    public class Bullet : MonoBehaviour
    {
        public LayerMask hitLayer;

        public GameObject ImpactVfx;

        private void OnCollisionEnter(Collision collision)
        {
            if (hitLayer == (hitLayer | (1 << collision.gameObject.layer)))
            {
                var impact = Instantiate(ImpactVfx, collision.contacts[0].point, Quaternion.identity) as GameObject;

                Destroy(impact, 2);

                Destroy(gameObject);
            }
        }

        public void DestroyBullet()
        {
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            var impact = Instantiate(ImpactVfx, transform.position, Quaternion.identity) as GameObject;

            Destroy(impact, 2);

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
