using akb.Core.Managing;

namespace akb.Entities.AI.Implementations.Astaroth
{
    public class BossAstarothAnimations : AI_EntityAnimations
    {
        private void Start()
        {
            ManagerHUB.GetManager.GameEventsHandler.onAstarothSecondPhase += PlayShieldedAnimation;
            ManagerHUB.GetManager.GameEventsHandler.onAllRocksBroken += StopShieldedAnimation;
            ManagerHUB.GetManager.GameEventsHandler.onProjectilePulse += PlayProjectilePulseAnimation;
        }

        public override void PlayAttackAnimation()
        {
            //nop..
        }

        public override void PlayChargeAnimation()
        {
            //nop...
        }

        public override void ResetAttackVariables()
        {
            //nop...
        }

        public void PlayGotHitAnimation()
        {
            ai_animator.Play("GetHit", 0);
        }

        public void PlayDeathAnimation()
        {
            ai_animator.Play("Death", 0);
        }

        public void PlayShieldedAnimation()
        {
            ai_animator.Play("IdleSleep");
            ai_animator.SetBool("hasShield", true);
        }

        public void StopShieldedAnimation()
        {
            ai_animator.Play("WakeUp");
            ai_animator.SetBool("hasShield", false);
        }

        public void PlayProjectilePulseAnimation()
        {
            ai_animator.Play("ProjectilePulse");
        }

        private void OnDestroy()
        {
            ManagerHUB.GetManager.GameEventsHandler.onAstarothSecondPhase -= PlayShieldedAnimation;
            ManagerHUB.GetManager.GameEventsHandler.onAllRocksBroken -= StopShieldedAnimation;
            ManagerHUB.GetManager.GameEventsHandler.onProjectilePulse -= PlayProjectilePulseAnimation;
        }
    }
}