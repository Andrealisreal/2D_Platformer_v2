using UnityEngine;

namespace Players.Animation
{
    public class PlayerAnimator
    {
        private readonly Animator _animator;

        public PlayerAnimator(Animator animator)
        {
            _animator = animator;
        }

        public void PlayRun(float direction)
        {
            _animator.SetFloat(AnimationParameters.Movement.Run, Mathf.Abs(direction));
        }

        public void PlayJump()
        {
            _animator.SetTrigger(AnimationParameters.Movement.Jump);
        }
        
        public void PlayDeath()
        {
            _animator.SetTrigger(AnimationParameters.Movement.Death);
        }
    }
}
