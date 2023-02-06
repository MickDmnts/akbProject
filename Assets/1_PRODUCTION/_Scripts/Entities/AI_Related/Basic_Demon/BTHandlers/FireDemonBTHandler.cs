namespace AKB.Entities.AI.Implementations.Simple_Demon.Fire_Demon
{
    /* [CLASS DOCUMENTATION]
     *
     * The SimpleDemonBTHandler implementation of AI_EntityBTHandler is responsible for 
     * constructing and updating the FireDemon BehaviourTree.
     */
    public class FireDemonBTHandler : AI_EntityBTHandler
    {
        BehaviourTree entryBT;
        BehaviourTree movePattern;
        BehaviourTree attackPattern;

        public FireDemonBTHandler(SimpleDemon ai_entity, INodeData nodeData)
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
            SimpleDemonNodeData data = ai_nodeData as SimpleDemonNodeData;

            #region CUSTOM_BEHAVIOUR
            #region ATTACK_PATTERN
            //Attack pattern            
            SimpleDemonAttackAction attackAction = new SimpleDemonAttackAction(data);
            SimpleDemonCountdownAction attackCooldown = new SimpleDemonCountdownAction(data, attackAction);

            attackPattern = new BehaviourTree(attackCooldown, ai_nodeData);
            #endregion

            #region MOVE_PATTERN
            //MovePattern
            SimpleDemonNavToTarget navToTarget = new SimpleDemonNavToTarget(data);
            movePattern = new BehaviourTree(navToTarget, ai_nodeData);
            #endregion

            //Parallel
            Selector simpleDemonBrancher = new Selector(data, movePattern, attackPattern);

            //Rotator
            FaceTargetAction faceTargetAction = new FaceTargetAction(data, ai_nodeData.GetTarget());

            //Orded matters - left executes first
            INode[] nodesToParallel = new INode[]
            {
                faceTargetAction, simpleDemonBrancher
            };

            ParallerExecutor parallerExecutor = new ParallerExecutor(nodesToParallel, ai_nodeData);

            IsStunnedSelector isStunnedSelector = new IsStunnedSelector(data, parallerExecutor);
            #endregion

            #region ENTRY_BT

            //Death action
            FireDemonOnDeathAction onDeathAction = new FireDemonOnDeathAction(data, GetParentEntity<SimpleDemon>().FireDemonDeathAction);

            //Activator
            FireDemonCheckIfDead checkIfDead = new FireDemonCheckIfDead(data, isStunnedSelector, onDeathAction);

            //Entry
            entryBT = new BehaviourTree(checkIfDead, ai_nodeData);
            #endregion

            entryNode = entryBT;
        }

        /// <summary>
        /// Call to update the enemy Behaviour tree.
        /// </summary>
        public override void UpdateBT()
        {
            //if ((ai_nodeData as AI_NodeData).GetIsDead()) return;

            if (ai_entryNode != null)
            {
                ai_entryNode.Run();
            }
        }
    }
}
