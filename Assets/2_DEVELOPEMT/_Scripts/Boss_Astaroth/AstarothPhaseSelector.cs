using UnityEngine;

namespace akb.Entities.AI.Implementations.Astaroth
{
    public class AstarothPhaseSelector : INode
    {
        AstarothNodeData nodeData;
        INode phase1;
        INode phase2;
        INode phase3;

        public AstarothPhaseSelector(AstarothNodeData nodeData, INode phaseOne, INode phaseTwo, INode phaseThree)
        {
            this.nodeData = nodeData;

            this.phase1 = phaseOne;
            this.phase2 = phaseTwo;
            this.phase3 = phaseThree;
        }

        public INodeData GetNodeData()
        {
            return nodeData;
        }

        public bool Run()
        {
            switch (nodeData.GetCurrentPhase())
            {
                case AstarothPhases.Phase1:
                    {
                        Debug.Log("Updating phase 1");
                        return phase1.Run();
                    }

                case AstarothPhases.Phase2:
                    {
                        Debug.Log("Updating phase 2");
                        return phase2.Run();
                    }

                case AstarothPhases.Phase3:
                    {
                        Debug.Log("Updating phase 3");
                        return phase3.Run();
                    }
            }

            return false;
        }
    }
}