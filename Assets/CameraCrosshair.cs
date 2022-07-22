using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace freddynewton
{
    public class CameraCrosshair : Singleton<CameraCrosshair>
    {
        public GameObject CrosshairObject;

        private void Update()
        {
            CrosshairObject.transform.position = GetPointToLook();
        }


        public Vector3 GetPointToLook()
        {
            Ray cameraRay = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
            float rayLength;
            Vector3 pointToLook = new Vector3();

            if (groundPlane.Raycast(cameraRay, out rayLength))
            {
                pointToLook = cameraRay.GetPoint(rayLength);
                pointToLook.y += 0.5f;
                return pointToLook;
            }

            return pointToLook;
        }
    }
}
