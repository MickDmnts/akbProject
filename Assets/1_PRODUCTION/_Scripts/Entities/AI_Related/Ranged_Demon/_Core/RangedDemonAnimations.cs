namespace akb.Entities.AI.Implementations.Ranged_Demon
{
    public class RangedDemonAnimations : AI_EntityAnimations
    {
        public void SetIsFlyingState(bool state)
        {
            ai_animator.SetBool("isFlying", state);
        }

        public void PlayDeathAnimation()
        {
            ai_animator.Play("RangedDemon_Death");
        }

        public void PlayDamagedAnimation()
        {
            ai_animator.Play("RangedDemon_Damage");
        }

        public override void PlayAttackAnimation()
        {
            ai_animator.Play("RangedDemon_Attack");
        }

        public override void PlayChargeAnimation()
        {
            //nop..
        }

        public override void ResetAttackVariables()
        {
            //nop..
        }
    }
}