using UnityEngine;


namespace AI.Enemy
{
    public sealed class EnemyAIParameters {
        
        public float normalHearDist = 12.0f;
        public float normalVisDist = 8.0f;
        public float normalStealDist = 1.0f;

        public float normalVisAngle = 40.0f;
        public float chaseVisAngle = 20.0f;

        // public float walkingSpeed = 2.0f;
        public float eatingSpeedMultiplayer = 0.5f;
        public float chaseSpeedMultiplayer = 2.0f;

        public float eatingRange = 2.5f;
        public float enteringRange = 5.0f;
        public float wanderingRange = 5.0f;

        public float idlingTime = 1.5f;
        public float eatingTime = 5.0f;
        public float enteringTime = 2.0f;
        public float wanderingTime = 3.0f;

        public float enteringRemainingDis = 2.0f;
        public float wanderingRemainingDis = 0.5f;

        public float normalRotationSpeed = 1.5f;
        public float chaseRotationSpeed = 3.0f;

        public float fromWanderToIdleChance = 0.01f;
        public float fromChaseToWanderChance = 0.05f;

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