using UnityEngine;
using UnityEngine.AI;

namespace akb.Entities.AI
{
    /* [CLASS DOCUMENTATION]
     *
     * The AI_Entity abstract class will be used from every AI entity of the game for better managing.
     */
    public abstract class AI_Entity : Entity
    {
        [SerializeField] int deathLayerID;

        protected INodeData entityNodeData;
        protected IBehaviourTreeHandler ai_BTHandler;

        protected AI_EntityAnimations ai_entityAnimations;
        protected NavMeshAgent ai_agent;

        protected bool isDead;

        /// <summary>
        /// Call to create and return an initialized node data instance.
        /// </summary>
        protected abstract T SetupNodeData<T>() where T : class, INodeData, new();

        /// <summary>
        /// Call to create the corresponding Simple demon variant based on demonType passed
        /// and assign it to the ai_BTHandler variable.
        /// </summary>
        protected abstract void CreateAppropriateBTHandler(out IBehaviourTreeHandler handlerVar);

        /// <summary>
        /// Call to subtract the passed value from EntityLife variable.
        /// </summary>
        protected virtual void SubtractHealth(float subtractionValue)
        {
            EntityLife -= subtractionValue;
        }

        /// <param name="healthValue">Entity health value</param>
        /// <returns>True if healthValue smaller or  equal to 0, false otherwise.</returns>
        protected virtual bool CheckIfDead(float healthValue)
        {
            if (healthValue <= 0)
            {
                return true;
            }

            return false;
        }

        protected abstract void UpdateNodeDataIsDead(bool value);

        protected virtual void MoveLayerOnDeath()
        {
            gameObject.layer = deathLayerID;
        }
    }
}