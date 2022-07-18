using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace freddynewton.Ability
{
    public abstract class Ability : ScriptableObject
    {
        public string Name;
        public string Description;
        public Sprite Icon;

        public float CoolDownTime;
        [HideInInspector] public bool isButtonPressed;

        public abstract void Use(PlayerAbilityManager playerAbilityManager);
    }
}
