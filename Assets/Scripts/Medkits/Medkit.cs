using UnityEngine;

namespace Medkits
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class Medkit : MonoBehaviour
    {
        [SerializeField] private float _countHealth = 20f;
        
        public float CountHealth => _countHealth;
    }
}
