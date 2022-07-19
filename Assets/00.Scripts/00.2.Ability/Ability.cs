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
        public AbilityType AbilityType = AbilityType.MOUSE0;

        public float CoolDownTime;
        [HideInInspector] public float currentCooldownTime;
        [HideInInspector] public bool isButtonPressed;

        public abstract void Use(PlayerAbilityManager playerAbilityManager);
    }
}
