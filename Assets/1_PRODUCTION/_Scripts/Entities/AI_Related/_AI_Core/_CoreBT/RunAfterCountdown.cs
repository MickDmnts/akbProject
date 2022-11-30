namespace AKB.Entities.AI.Implementations
{
    public abstract class RunAfterCountdown : INode
    {
        protected INodeData nodeData;
        protected INode nodeToRun;

        protected float initialTime;
        protected float currentTimer;

        public RunAfterCountdown(INodeData nodeData, INode nodeToRun)
        {
            this.nodeData = nodeData;
            this.nodeToRun = nodeToRun;
        }

        protected virtual void RefreshTimers(float initialTime = 0)
        {
            this.initialTime = initialTime;
            this.currentTimer = this.initialTime;
        }

        public INodeData GetNodeData() => nodeData;

        public abstract bool Run();
    }
}