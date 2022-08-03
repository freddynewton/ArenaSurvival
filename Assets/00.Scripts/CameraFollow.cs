using freddynewton.player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace freddynewton
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField]
        private Vector3 offset;




        // Update is called once per frame
        void Update()
        {
            transform.position = PlayerManager.Instance.playerMovement.transform.position + offset;
        }
    }
}
