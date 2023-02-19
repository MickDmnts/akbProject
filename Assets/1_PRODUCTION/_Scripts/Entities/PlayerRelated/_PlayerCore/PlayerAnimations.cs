using UnityEngine;
using akb.Core.Managing;
using akb.Core.Sounds;

namespace akb.Entities.Player
{
    [DefaultExecutionOrder(410)]
    public class PlayerAnimations : MonoBehaviour
    {
        [Header("Set animator layers")]
        [SerializeField] int animatorMovementLayer;
        [SerializeField] int animatorSpearLayer;
        [SerializeField] AudioClip[] audioClips;
        AudioSource audio;
        Animator playerAnimator;

        private void Start()
        {
            audio = GetComponent<AudioSource>();
            playerAnimator = GetComponentInChildren<Animator>();
        }

        public void SetRunningBlendValue(float input)
        {
            playerAnimator.SetFloat("MoveInput", input);
        }

        public void SetSpearChargeState(bool state)
        {
            playerAnimator.SetBool("IsCharging", state);

            if (state == true)
            {
                playerAnimator.Play("SpearCharging", animatorSpearLayer);
            }
        }

        public void PlayThrowAnimation()
        {
            playerAnimator.Play("SpearThrow", animatorSpearLayer);
        }

        public void PlayChargeCancelAnim()
        {
            playerAnimator.Play("ChargeCancel", animatorSpearLayer);
        }

        public void PlayFallingAfterTPAnim()
        {
            playerAnimator.Play("Falling", animatorMovementLayer);
        }

        public void SetSpearPullAnimationState(bool state)
        {
            playerAnimator.SetBool("hasSpear", state);

            if (state == false)
            {
                playerAnimator.Play("PullSpear", animatorSpearLayer);
            }
        }

        public void PlayDodgeRollAnimation()
        {
            playerAnimator.Play("DodgeRoll", animatorMovementLayer);
        }

        public void PlayAttackAnimation()
        {
            playerAnimator.SetTrigger("Attack");
            for(int i =0; i < audioClips.Length; i++)
            {
                audio.PlayOneShot(audioClips[i]);
            }
        }

        public Animator GetPlayerAnimator()
        {
            return playerAnimator;
        }

        public bool IsInTransition(int layer)
        {
            return playerAnimator.IsInTransition(layer);
        }

        public bool IsAnimationRunning(int layer, string name)
        {
            return playerAnimator.GetCurrentAnimatorStateInfo(layer).IsName(name);
        }
    }
}