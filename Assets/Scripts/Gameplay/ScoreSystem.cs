namespace Gameplay
{
    [System.Serializable]
    public class ScoreSystem
    {
        public int EatenPommesScore = 200;
        public int ActionExecutedScore = 500;
        public int SecondOfLifeScore = 100;
        public int DeathAvoidedScore = 300;

        public float ScoreMultiplier = 1.0f;

        public int GetScore(ScoreType scoreType)
        {
            switch (scoreType)
            {
                case ScoreType.EatenPommes:
                    return (int)(EatenPommesScore * ScoreMultiplier);
                case ScoreType.ActionExecuted:
                    return (int)(ActionExecutedScore * ScoreMultiplier);
                case ScoreType.SecondOfLife:
                    return (int)(SecondOfLifeScore * ScoreMultiplier);
                case ScoreType.DeathAvoided:
                default:
                    return 0;
            }
        }
    }
}
