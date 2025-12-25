using Players.Abilities.Vampirisms;
using UnityEngine;

namespace Players.Abilities
{
    [RequireComponent(typeof(Vampirism))]
    public class PlayerAbility : MonoBehaviour
    {
        private Vampirism _vampirism;
        private Health _health;

        private void Awake()
        {
            _vampirism = GetComponent<Vampirism>();
        }

        public void Initialize(Health health)
        {
            _health = health;
        }

        public void ActivateVampirism()
        {
            _vampirism.Activate(_health);
        }
    }
}