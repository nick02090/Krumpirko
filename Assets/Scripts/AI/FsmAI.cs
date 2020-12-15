using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI.Enemy;

namespace AI
{
    public class FsmAI
    {
        EnemyState currentState;

        public FsmAI(EnemyState startingState)
        {
            currentState = startingState;
        }

        public void Update()
        {
            currentState = currentState.Process();
        }
    }
}