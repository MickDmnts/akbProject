namespace akb.Entities.AI.Implementations.Big_Demon
{
    public class BigDemonAnimations : AI_EntityAnimations
    {
        #region RUSH_ATTACK
        public override void PlayChargeAnimation()
        {
            ai_animator.SetTrigger("chargingAttack");
        }
        #endregion

        #region SLAM_ATTACK
        public void PlaySlamChargeAnimation()
        {
            ai_animator.SetTrigger("slamCharge");
        }

        public override void PlayAttackAnimation()
        {
            ai_animator.SetBool("slamAttack", true);
        }
        #endregion

        public void PlayGetHitAnimation()
        {
            ai_animator.Play("BigDemon_GetHit");
        }

        public void PlayDeathAnimation()
        {
            ai_animator.Play("BigDemon_Die");
        }

        public override void ResetAttackVariables()
        {
            ai_animator.SetBool("slamAttack", false);
        }
    }
}
