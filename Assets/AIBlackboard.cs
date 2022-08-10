using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner;
using BehaviorDesigner.Runtime;
using freddynewton.player;

namespace freddynewton.AI
{
    public class AIBlackboard : Singleton<AIBlackboard>
    {
        private void Update()
        {
            GlobalVariables.Instance.SetVariableValue("Player", PlayerManager.Instance.playerMovement.gameObject);
        }
    }
}
