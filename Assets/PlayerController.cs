using BehaviorDesigner.Runtime.Tactical;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace freddynewton
{
    public class PlayerController : MonoBehaviour, IDamageable
    {
        public void Damage(float amount)
        {
            Debug.Log($"Get {amount} damage");
        }

        public bool IsAlive()
        {
            return true;
        }

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
