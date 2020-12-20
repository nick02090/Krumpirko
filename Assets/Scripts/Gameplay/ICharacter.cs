using Gameplay.Ailments;
using Gameplay.SceneObjects;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Characters
{
    public interface ICharacter
    {
        void SetMovementSpeed(float movementSpeed);
        float GetMovementSpeed();
        float GetRotationSpeed();
        Transform GetTransform();
        void SpillHotSauce();
        bool CanBaitWithHotSauce();
        int BaitWithHotSauce();
        void InflictWith(Ailment ailment);
        bool IsImmuneTo(AilmentType ailmentType);
        float GetEatingRate();
        void SetEatingRate(float eatingRate);
        float GetDeathClock();
        void SetDeathClock(float deathClock);
        int GetLeftPommesCapacity();
        int GetMaxPommesCapacity();
        void SetMaxPommesCapacity(int maxCapacity);
        void AddPommes(int numberOfPommes, int numberOfHotPommes);
        List<Ailment> GetAilments();
        void AddGum();
        bool HasTag(string tag);
        void AddBait(GameObject bait);
        void RemoveBait(GameObject bait);
        List<GameObject> GetBaits();
        int GetCurrentScore();
        int GetScore(ScoreType scoreType);
        int GetPommesCapacity();
        int GetPommesEatenCapacity();
        int GetGumCapacity();
        int GetHotSauceCapacity();
    }
}