namespace Gameplay.Actions
{
    public abstract partial class Action
    {
        public class CoughAction : Action
        {
            /// <summary>
            /// COUGH/KASALJ:
            ///         - affects all enemies that are within the radius of the players current position
            ///         - enemies are inflicted with Fear ailment and they immediately drop all of their pommes
            /// </summary>
            public override void Execute()
            {
                throw new System.NotImplementedException();
            }
        }

    }
}
