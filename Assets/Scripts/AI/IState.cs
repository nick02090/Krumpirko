using UnityEngine;

namespace AI
{
    public interface IState 
    {
        void Enter();
        void Update();
        void Exit();
        IState Process();

    }
}