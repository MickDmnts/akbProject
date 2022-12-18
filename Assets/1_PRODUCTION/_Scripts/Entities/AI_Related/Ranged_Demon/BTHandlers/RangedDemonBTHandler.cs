namespace akb.Entities.AI.Implementations.Ranged_Demon
{
    public class RangedDemonBTHandler : AI_EntityBTHandler
    {
        BehaviourTree entryBT;
        BehaviourTree movePattern;
        BehaviourTree attackPattern;

        public RangedDemonBTHandler(RangedDemon ai_entity, INodeData nodeData)
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
            RangedDemonNodeData data = GetDemonNodeData<RangedDemonNodeData>();

            #region CUSTOM_BEHAVIOUR
            #region ATTACK_PATTERN
            RangedDemonAttackAction attackAction = new RangedDemonAttackAction(data);

            RangedDemonAttackCooldown attackCooldown = new RangedDemonAttackCooldown(data, attackAction);

            RangedDemonActivator attackWhileActive = new RangedDemonActivator(data, attackCooldown);

            attackPattern = new BehaviourTree(attackWhileActive, data);
            #endregion

            #region MOVE_PATTERN
            RangedDemonMoveset moveset = new RangedDemonMoveset(data);

            RangedDemonActivator idleWhileSearching = new RangedDemonActivator(data, moveset);

            movePattern = new BehaviourTree(idleWhileSearching, data);
            #endregion

            INode[] attackMoveNodes = new INode[]
            {
                movePattern, attackPattern,
            };

            ParallerExecutor attackMoveParallel = new ParallerExecutor(attackMoveNodes, data);
            #endregion

            #region ENTRY_BEHAVIOUR
            FaceTargetAction faceTarget = new FaceTargetAction(data, data.GetTarget());

            INode[] nodesToRun = new INode[]
            {
                faceTarget, attackMoveParallel,
            };

            ParallerExecutor parallerExecutor = new ParallerExecutor(nodesToRun, data);

            IsStunnedSelector isStunnedSelector = new IsStunnedSelector(data, parallerExecutor);

            CheckIfDead checkIfDead = new CheckIfDead(data, isStunnedSelector);

            entryBT = new BehaviourTree(checkIfDead, data);

            entryNode = entryBT;
            #endregion
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