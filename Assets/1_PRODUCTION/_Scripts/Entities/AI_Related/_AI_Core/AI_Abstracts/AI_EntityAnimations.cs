using UnityEngine;

namespace akb.Entities.AI
{
    public abstract class AI_EntityAnimations : MonoBehaviour
    {
        protected Animator ai_animator;

        private void Awake()
        {
            ai_animator = GetComponent<Animator>();
        }

        public abstract void PlayAttackAnimation();

        public abstract void PlayChargeAnimation();

        public abstract void ResetAttackVariables();
    }
}
