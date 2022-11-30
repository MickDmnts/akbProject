namespace AKB.Entities.AI.Implementations.Status_Demon
{
    public class StatusDemonBTHandler : AI_EntityBTHandler
    {
        BehaviourTree entryBT;
        BehaviourTree movePattern;
        BehaviourTree attackPattern;

        public StatusDemonBTHandler(StatusDemon ai_entity, INodeData nodeData)
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
            StatusDemonNodeData data = ai_nodeData as StatusDemonNodeData;

            #region CUSTOM_BEHAVIOUR
            #region ATTACK_PATTERN
            StatusDemonTeleportAwayAction teleportAwayAction = new StatusDemonTeleportAwayAction(data);
            StatusDemonAOEReset aoeReset = new StatusDemonAOEReset(data);

            StatusDemonAfterAttackSelector afterAttackSelector = new StatusDemonAfterAttackSelector(data, teleportAwayAction, aoeReset);

            StatusDemonAOECountdown countdownToInitiateAOE = new StatusDemonAOECountdown(data, afterAttackSelector);

            attackPattern = new BehaviourTree(countdownToInitiateAOE, data);
            #endregion

            #region MOVE_PATTERN
            StatusDemonMoveset moveset = new StatusDemonMoveset(data);

            movePattern = new BehaviourTree(moveset, data);
            #endregion

            Selector selector = new Selector(data, movePattern, attackPattern);
            #endregion

            #region ENTRY_BEHAVIOUR
            FaceTargetAction faceTarget = new FaceTargetAction(data, data.GetTarget());

            INode[] nodesToRun = new INode[]
            {
                faceTarget, selector,
            };

            ParallerExecutor parallerExecutor = new ParallerExecutor(nodesToRun, data);

            IsStunnedSelector isStunnedSelector = new IsStunnedSelector(data, parallerExecutor);

            CheckIfDead checkIfDead = new CheckIfDead(data, isStunnedSelector);

            entryBT = new BehaviourTree(checkIfDead, data);
            #endregion

            entryNode = entryBT;
        }

        /// <summary>
        /// Call to update the enemy Behaviour tree.
        /// </summary>
        public override void UpdateBT()
        {
            if (ai_entryNode != null)
            {
                ai_entryNode.Run();
            }
        }
    }
}