using UnityEngine;

namespace akb.Entities.AI.Implementations.Simple_Demon
{
    /* [CLASS DOCUMENTATION]
     *
     * Same as base type.
     * 
     * [Action node]
     * This node is responsible for checking the distance between the agent and the target set in the passed node data.
     * 
     *  Returns true when not close to target 
     *          Else
     *  Returns false.
     * 
     * [Convertion]
     * Converts the nodeData passed from AI_NodeData to SimpleDemonNodeData and assigns it to the class variable.
     */
    public class SimpleDemonNavToTarget : NavToTarget
    {
        public SimpleDemonNavToTarget(SimpleDemonNodeData nodeData) : base(nodeData)
        {
            this.nodeData = nodeData;
            previousRun = false;
        }

        /// <summary>
        /// Sets the destination of the agent to the SimpleDemonNodeData target.
        /// <para>Call to check the distance between the agent and the set target.</para>
        /// </summary>
        /// <returns>True when not close to target
        /// Else false.</returns>
        public override bool Run()
        {
            if (!(nodeData as SimpleDemonNodeData).GetCanMove()) return false;

            bool currentRun = true;

            //Movement
            nodeData.GetNavMeshAgent().SetDestination(nodeData.GetTarget().position);

            Vector3 agentPos = agent.transform.position;
            Vector3 targetPos = target.position;

            if ((agentPos - targetPos).magnitude <= (nodeData as SimpleDemonNodeData).GetAttackRange())
            {
                agent.isStopped = true;
                currentRun = false;
            }

            EvaluateRunState(currentRun);

            return currentRun;
        }

        protected override void OnNodeEntry()
        {
            (nodeData as SimpleDemonNodeData).GetDemonAnimations().SetIsRunningState(true);

            agent.isStopped = false;
            previousRun = true;
        }

        protected override void OnNodeExit()
        {
            (nodeData as SimpleDemonNodeData).GetDemonAnimations().SetIsRunningState(false);

            agent.isStopped = false;
            previousRun = false;
        }
    }
}