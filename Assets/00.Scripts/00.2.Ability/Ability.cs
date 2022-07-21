using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace freddynewton.Ability
{
    public abstract class Ability : ScriptableObject
    {
        public string Name;
        public string Description;
        public Sprite Icon;
        public AbilityType AbilityType = AbilityType.MOUSE0;

        public float CoolDownTime;
        [HideInInspector] public float currentCooldownTime;
        [HideInInspector] public bool isButtonPressed;

        public abstract void Use(PlayerAbilityManager playerAbilityManager);

        protected Vector3 GetPointToLook ()
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
