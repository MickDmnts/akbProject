namespace akb.Entities.AI.Implementations.Astaroth
{
    public class AstarothBTHandler : AI_EntityBTHandler
    {
        BehaviourTree entryBT;
        BehaviourTree attackPattern;
        BehaviourTree phase1;
        BehaviourTree phase2;
        BehaviourTree phase3;

        public AstarothBTHandler(BossAstaroth ai_entity, INodeData nodeData)
        {
            this.ai_parentEntity = ai_entity;
            this.ai_nodeData = nodeData;

            CreateBehaviourTree(ref ai_entryNode);
        }

        protected override void CreateBehaviourTree(ref INode entryNode)
        {
            AstarothNodeData data = ai_nodeData as AstarothNodeData;

            #region PHASE_3

            #endregion

            #region PHASE_2

            #endregion

            #region PHASE_1
            //Attack Pattern phase 1
            AstarothAttackAction attackAction = new AstarothAttackAction(data);
            AstarothCountDownAction attackCooldown = new AstarothCountDownAction(data,attackAction);
            
            attackPattern = new BehaviourTree(attackCooldown, ai_nodeData);
            #endregion

            AstarothPhaseSelector astarothBrancher = new AstarothPhaseSelector(data, phase1, phase2, phase3);

            //Rotator
            FaceTargetAction faceTargetAction = new FaceTargetAction(data, ai_nodeData.GetTarget());

            //Order matters - first executes first
            INode[] nodesToParallel = new INode[]
            {
                faceTargetAction,
                astarothBrancher,
            };

            ParallerExecutor parallerExecutor = new ParallerExecutor(nodesToParallel, ai_nodeData);

            //Activator
            CheckIfDead checkIfDead = new CheckIfDead(data, parallerExecutor);

            //Entry
            entryBT = new BehaviourTree(checkIfDead, ai_nodeData);

            entryNode = entryBT;
        }


        public override void UpdateBT()
        {
            if ((ai_nodeData as AI_NodeData).GetIsDead()) return;

            if (ai_entryNode != null)
            {
                ai_entryNode.Run();
            }
        }
    }
}
