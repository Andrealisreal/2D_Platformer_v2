using Coins;
using Medkits;
using Players.Animation;
using Players.Inventory;
using UnityEngine;

namespace Players
{
    public class Trigger
    {
        private readonly Wallet _wallet;
        private readonly Health _health;
        private readonly PlayerAnimator _animator;
        private readonly Death _death;
        
        public Trigger(Wallet wallet, Health health, PlayerAnimator animator, Death death)
        {
            _wallet = wallet;
            _health = health;
            _animator = animator;
            _death = death;
        }
        
        public void OnEnter(Collider2D other)
        {
            if (TryCollectedCoin(other)) return;

            if (TryCollectedMedkit(other)) return;

            EnterDeathZone(other);
        }

        private void EnterDeathZone(Collider2D other)
        {
            if (other.TryGetComponent(out DeathZone _) == false)
                return;
            
            _animator.PlayDeath();
            _death.Die();
        }

        private bool TryCollectedMedkit(Collider2D other)
        {
            if (other.TryGetComponent(out Medkit medkit) == false)
                return false;
            
            _health.Heal(medkit.CountHealth);

            return true;
        }

        private bool TryCollectedCoin(Collider2D other)
        {
            if (other.TryGetComponent(out Coin coin) == false)
                return false;

            coin.Collect();
            _wallet.AddCoin();
                
            return true;
        }
    }
}
