namespace akb.Entities.AI.Implementations.Big_Demon
{
    public class BigDemonBTHandler : AI_EntityBTHandler
    {
        BehaviourTree entryBT;
        BehaviourTree movePattern;
        BehaviourTree attackPattern;

        public BigDemonBTHandler(BigDemon ai_entity, INodeData nodeData)
        {
            this.ai_parentEntity = ai_entity;
            this.ai_nodeData = nodeData;

            CreateBehaviourTree(ref ai_entryNode);
        }

        /// <summary>
        /// Call to create the AI Entity behaviour tree and assign it to the passed ref value.
        /// </summary>
        protected override void CreateBehaviourTree(ref INode entryNode)
        {
            BigDemonNodeData data = GetDemonNodeData<BigDemonNodeData>();

            #region CUSTOM_BEHAVIOUR
            #region ATTACK_PATTERN
            //Create slam branch
            BigDemonSlamAction slamAction = new BigDemonSlamAction(data);
            BigDemonCountdownAction slamCountdown = new BigDemonCountdownAction(data, slamAction, data.SetIsSlamming);

            //Create charge branch
            BigDemonChargeAction chargeAction = new BigDemonChargeAction(data, InitiateDemonRush);
            BigDemonCountdownAction chargeCooldown = new BigDemonCountdownAction(data, chargeAction, data.SetIsRushing);

            INode[] attackModes = new INode[]
            {
                chargeCooldown,
                slamCountdown
            };

            BigDemonAttackSwitch attackSwitch = new BigDemonAttackSwitch(data, attackModes);

            attackPattern = new BehaviourTree(attackSwitch, data);

            #endregion

            #region MOVE_PATTERN
            BigDemonNavToTarget navToTarget = new BigDemonNavToTarget(data);
            movePattern = new BehaviourTree(navToTarget, data);
            #endregion
            #endregion

            #region ENTRY_BEHAVIOUR
            Selector selector = new Selector(data, movePattern, attackPattern);

            FaceTargetAction faceTarget = new FaceTargetAction(data, data.GetTarget());

            INode[] nodesToRun = new INode[]
            {
                faceTarget, selector
            };

            ParallerExecutor parallerExecutor = new ParallerExecutor(nodesToRun, data);

            IsStunnedSelector isStunnedSelector = new IsStunnedSelector(data, parallerExecutor);

            CheckIfDead checkIfDead = new CheckIfDead(data, isStunnedSelector);

            entryBT = new BehaviourTree(checkIfDead, data);

            entryNode = entryBT;
            #endregion
        }

        void InitiateDemonRush()
        {
            GetDemonNodeData<BigDemonNodeData>().GetEnemyEntity().ActivateRushFOV();
        }

        /// <summary>
        /// Call to update the enemy Behaviour tree.
        /// </summary>
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