using UnityEngine;

namespace Gameplay.Characters
{
    // PlayerController and EnemyController should implement ICharacter
    public interface ICharacter
    {
        void SetMovementSpeed(float movementSpeed);
        float GetMovementSpeed();
        void DisableAllActions();
        void EnableAllActions();
        Transform GetTransform();
        // void DisableAction(ActionType actionType);
        // void EnableAction(ActionType actionType);
    }
}