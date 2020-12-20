using UnityEngine;


namespace AI.Enemy
{
    public sealed class EnemyAIParameters {

        public float BaitVisDist = 10.0f;
        public float BaitSpeedMultiplayer = 4.0f;

        public float BurnSpeedMultiplayer = 6.0f;

        public float FearMinTime = 1.0f;
        public float FearMinDist = 8.0f;
        public float FearMaxDist = 10.0f;
        public float FearSpeedMultiplayer = 6.0f;

        public float ChaseVisAngle = 20.0f;
        public float ChaseSpeedMultiplayer = 2.5f;
        public float FromChaseToIdleChance = 0.05f;

        public float MinEatingTime = 0.3f;
        public float EatingMinRingDistance = 4.0f;
        public float EatingMaxRingDistance = 7.0f;
        public float EatingSpeedMultiplayer = 0.5f;

        public float EnteringRange = 8.0f;
        public float MinEnteringTime = 2.5f;
        public float EnteringHearDist = 8.0f;
        public float EnteringRemainingDistance = 0.5f;

        public float IdlingTime = 1.5f;

        public float StarvingSpeedMultiplayer = 5.5f;
        public float StarvationTriggerTime = 6.0f;

        public float MinWanderingTime = 3.0f;
        public float WanderRemainingDis = 1.0f;
        public float WanderMinRingDistance = 5.0f;
        public float WanderMaxRingDistance = 10.0f;
        public float FromWanderToIdleChance = 0.005f;
        
        public float NormalVisDist = 8.0f;
        public float NormalHearDist = 12.0f;
        public float NormalVisAngle = 40.0f;

        public float SLowRotationSpeed = 1.0f;
        public float NormalRotationSpeed = 1.5f;
        public float FastRotationSpeed = 3.5f;

        private static readonly EnemyAIParameters instance = new EnemyAIParameters();

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static EnemyAIParameters() {}

        private EnemyAIParameters() {}

        public static EnemyAIParameters Instance
        {
            get
            {
                return instance;
            }
        }

    }

}