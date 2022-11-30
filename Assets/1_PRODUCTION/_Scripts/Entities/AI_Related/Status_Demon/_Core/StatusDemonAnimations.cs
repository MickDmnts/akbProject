namespace AKB.Entities.AI.Implementations.Status_Demon
{
    public class StatusDemonAnimations : AI_EntityAnimations
    {
        public override void PlayAttackAnimation()
        {
            ai_animator.Play("StatusDemon_Attack");
        }

        public void PlayTeleportAnimation()
        {
            ai_animator.Play("StatusDemon_Teleporting");
        }

        public void PlayGotHitAnimation()
        {
            ai_animator.Play("StatusDemon_GetHit");
        }

        public void PlayDeathAnimation()
        {
            ai_animator.Play("StatusDemon_Death");
        }

        public override void PlayChargeAnimation()
        {
            //nop...
        }

        public override void ResetAttackVariables()
        {
            //nop...
        }
    }
}
