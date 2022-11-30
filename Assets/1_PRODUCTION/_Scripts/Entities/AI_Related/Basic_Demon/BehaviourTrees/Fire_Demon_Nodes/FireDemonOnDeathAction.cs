using System;

namespace AKB.Entities.AI.Implementations.Simple_Demon.Fire_Demon
{
    public sealed class FireDemonOnDeathAction : INode
    {
        INodeData nodeData;

        delegate void OnDeathCallback();
        OnDeathCallback onDeath;

        bool calledAction = false;

        public FireDemonOnDeathAction(INodeData nodeData, Action onDeathMethod)
        {
            this.nodeData = nodeData;

            onDeath = new OnDeathCallback(onDeathMethod);
        }

        public bool Run()
        {
            if (!calledAction)
            {
                calledAction = true;
                onDeath();
                return false;
            }

            return true;
        }

        public INodeData GetNodeData() => nodeData;
    }
}