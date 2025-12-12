using UnityEngine;

public static class AnimationParameters
{
    public static class Movement
    {
        public static readonly int Jump = Animator.StringToHash("Jump");
        public static readonly int Run = Animator.StringToHash("Speed");
        public static readonly int Death = Animator.StringToHash("Death");
    }

    public static class Attacks
    {
        public static readonly int AnimAttack = Animator.StringToHash("AnimAttack");
    }
}
