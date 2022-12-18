namespace akb.Entities.AI.Implementations.Simple_Demon.Fire_Demon
{
    public class FireDemonCheckIfDead : CheckIfDead
    {
        protected INode deathAction;

        public FireDemonCheckIfDead(AI_NodeData nodeData, INode child, INode deathAction) : base(nodeData, child)
        {
            this.deathAction = deathAction;
        }

        public override bool Run()
        {
            if ((nodeData as AI_NodeData).GetIsDead())
            {
                return deathAction.Run();
            }

            return base.Run();
        }
    }
}