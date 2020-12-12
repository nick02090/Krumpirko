namespace Gameplay.Characters
{
    // PlayerController and EnemyController should implement ICharacter
    public interface ICharacter
    {
        void SetMovementSpeed(float movementSpeed);
        float GetMovementSpeed();
        void DisableAllActions();
        void EnableAllActions();
        // void DisableAction(ActionType actionType);
        // void EnableAction(ActionType actionType);
    }
}