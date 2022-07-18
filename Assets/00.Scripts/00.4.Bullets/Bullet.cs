using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace freddynewton.Bullet
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
    }
}
