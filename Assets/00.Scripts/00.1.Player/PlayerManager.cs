using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace freddynewton.player
{
    public class PlayerManager : Singleton<PlayerManager>
    {
        public PlayerStats PlayerStats;
        public InputActionAsset PlayerInputActionAsset;
        public PlayerMovement playerMovement;
    }
}
