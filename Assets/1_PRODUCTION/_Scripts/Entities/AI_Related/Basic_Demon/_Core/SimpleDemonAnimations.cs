namespace akb.Entities.AI.Implementations.Simple_Demon
{
    public class SimpleDemonAnimations : AI_EntityAnimations
    {
        public void SetIsRunningState(bool state)
        {
            ai_animator.SetBool("isRunning", state);
        }

        public override void PlayChargeAnimation()
        {
            ai_animator.SetTrigger("chargingAttack");
        }

        public override void PlayAttackAnimation()
        {
            ai_animator.SetBool("executeAttack", true);
        }

        public override void ResetAttackVariables()
        {
            ai_animator.SetBool("executeAttack", false);
        }

        public void PlayDeathAnimation()
        {
            ai_animator.Play("Death");
        }

        public void PlayGotHitAnimation()
        {
            if (IsCurrentlyInAttackTransition()) return;

            ai_animator.Play("GetHit");
        }

        bool IsCurrentlyInAttackTransition()
        {
            if (ai_animator.GetCurrentAnimatorStateInfo(0).IsName("AttackCharge")
                || ai_animator.GetCurrentAnimatorStateInfo(0).IsName("AttackAction"))
                return true;

            return false;
        }
    }
}
