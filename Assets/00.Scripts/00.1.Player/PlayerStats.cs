using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace freddynewton.player
{
    [CreateAssetMenu(menuName = "Player/Player Stats")]
    public class PlayerStats : ScriptableObject
    {
        public float HealthMax;
        public float HealthCurrent;

        public float MovementSpeed;
        public float MovementSpeedMultiplier;
        public float RotationSpeed;
    }
}