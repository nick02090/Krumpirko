using Gameplay.Actions;
using Gameplay.Ailments;
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
        void SpillHotSauce();
        bool CanBaitWithHotSauce();
        int BaitWithHotSauce();
        void DisableAction(ActionType actionType);
        void EnableAction(ActionType actionType);
        void InflictWith(Ailment ailment);
        void DropAllPommes();
    }
}