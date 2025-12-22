using System;
using System.Collections;
using UnityEngine;

namespace Players.Abilities.Vampirisms
{
    [RequireComponent(typeof(VampirismView))]
    public class Vampirism : MonoBehaviour
    {
        public event Action Activated;

        private VampirismView _view;

        private bool _isOnActivated = false;

        private void Awake()
        {
            _view = GetComponent<VampirismView>();
        }

        public void Activate(float cooldown, float duration, float amountSteal)
        {
            if (_isOnActivated)
                return;

            StartCoroutine(StealRoutine(duration, cooldown));
        }
        
        

        private IEnumerator StealRoutine(float duration, float cooldown)
        {
            _isOnActivated = true;
            
            var durationSpell = new WaitForSeconds(duration);
            var cooldownSpell = new WaitForSeconds(cooldown);
            
            
            
            yield return durationSpell;
            
            yield return cooldownSpell;
            
            
            
            _isOnActivated = false;
        }
    }
}