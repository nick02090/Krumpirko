namespace Gameplay.Ailments
{
    [System.Serializable]
    public class AilmentImmunity
    {
        public bool BurnAilmentImmunity = false;
        public bool FearAilmentImmunity = false;
        public bool HappyAilmentImmunity = false;
        public bool SlowAilmentImmunity = false;

        public bool GetImmunity(AilmentType ailmentType)
        {
            switch (ailmentType)
            {
                case AilmentType.Burn:
                    return BurnAilmentImmunity;
                case AilmentType.Fear:
                    return FearAilmentImmunity;
                case AilmentType.Happy:
                    return HappyAilmentImmunity;
                case AilmentType.Slow:
                    return SlowAilmentImmunity;
                default:
                    return false;
            }
        }
    }
}
