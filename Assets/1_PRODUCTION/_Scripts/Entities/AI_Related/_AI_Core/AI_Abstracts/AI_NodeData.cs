using UnityEngine;
using UnityEngine.AI;

namespace AKB.Entities.AI
{
    /* [CLASS DOCUMENTATION]
     *
     * The AI_NodeData abstract class will be used and stored inside AI_EntityBTHandlers implementations
     * to store the needed data for each enemy type.
     */
    [System.Serializable]
    public abstract class AI_NodeData : INodeData
    {
        protected AI_Entity ai_entity;
        protected AI_EntityAnimations ai_animations;

        protected NavMeshAgent ai_agent;
        protected Transform target;

        bool isDead = false;
        bool isStunned = false;

        bool canMove = false;
        bool canRotate = false;

        #region MUTATORS
        public void SetNavMeshAgent(NavMeshAgent value) => ai_agent = value;
        public void SetTarget(Transform value) => target = value;

        public void SetIsDead(bool value) => isDead = value;
        public void SetIsStunned(bool value) => isStunned = value;

        public void SetCanMove(bool value) => canMove = value;
        public void SetCanRotate(bool value) => canRotate = value;
        #endregion

        #region ACCESSORS
        public NavMeshAgent GetNavMeshAgent() => ai_agent;
        public Transform GetTarget() => target;
        public Vector3 GetAgentPosition() => ai_agent.transform.position;

        public bool GetIsDead() => isDead;
        public bool GetIsStunned() => isStunned;

        public bool GetCanMove() => canMove;
        public bool GetCanRotate() => canRotate;
        #endregion

    }
}