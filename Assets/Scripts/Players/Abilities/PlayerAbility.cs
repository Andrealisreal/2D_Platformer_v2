using Players.Abilities.Vampirisms;
using UnityEngine;

namespace Players.Abilities
{
    [RequireComponent(typeof(Vampirism))]
    public class PlayerAbility : MonoBehaviour
    {
        [Header("Настройки вампиризма")] [SerializeField]
        private float _healthStealCount;

        [SerializeField] private float _cooldownVampirism;
        [SerializeField] private float _durationVampirism;

        private Vampirism _vampirism;

        private void Awake()
        {
            _vampirism = GetComponent<Vampirism>();
        }

        public void ActivateVampirism()
        {
            _vampirism.Activate(_cooldownVampirism, _durationVampirism, _healthStealCount);
        }
    }
}