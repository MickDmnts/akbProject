namespace akb.Entities.AI.Implementations.Astaroth
{
    public class AstarothBTHandler : AI_EntityBTHandler
    {
        BehaviourTree entryBT;
        BehaviourTree phase1;
        BehaviourTree phase2;
        BehaviourTree phase3;
        BehaviourTree attackPattern;

        public AstarothBTHandler(BossAstaroth ai_entity, INodeData nodeData)
        {
            this.ai_parentEntity = ai_entity;
            this.ai_nodeData = nodeData;
        }

        protected override void CreateBehaviourTree(ref INode entryNode)
        {
            AstarothNodeData data = ai_nodeData as AstarothNodeData;

            AstarothSelector astarothBrancher = new AstarothSelector(data,phase1,phase2,phase3);
            //Rotator
            FaceTargetAction faceTargetAction = new FaceTargetAction(data, ai_nodeData.GetTarget());
            //Orded matters - left executes first
            INode[] nodesToParallel = new INode[]
            {
                faceTargetAction, 
            };
            ParallerExecutor parallerExecutor = new ParallerExecutor(nodesToParallel, ai_nodeData);
            //Activator
            AstarothCheckIfDead checkIfDead = new AstarothCheckIfDead(data,phase3);
            //Entry
            entryBT = new BehaviourTree(checkIfDead, ai_nodeData);
            entryNode = entryBT;
        }


        public override void UpdateBT()
        {
            if ((ai_nodeData as AI_NodeData).GetIsDead()) return;

            if(ai_entryNode != null)
            {
                ai_entryNode.Run();
            }
        }
    }
}
