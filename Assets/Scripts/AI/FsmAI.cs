using UnityEngine;

namespace AI
{
    public class FsmAI
    {
        IState currentState;

        public FsmAI(IState startingState)
        {   
            currentState = startingState;
        }

        public void Update()
        {
            currentState = currentState.Process();
        }
    }
}