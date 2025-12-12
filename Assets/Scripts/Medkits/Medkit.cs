using System;
using UnityEngine;

namespace Medkits
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class Medkit : MonoBehaviour
    {
        [SerializeField] private float _countHealth = 20f;

        public event Action<Medkit> Collected;
        
        public float CountHealth => _countHealth;
        
        public void Collect()
        {
            Collected?.Invoke(this);
        }
    }
}
