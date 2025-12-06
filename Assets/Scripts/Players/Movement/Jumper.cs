using UnityEngine;

namespace Players.Movement
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CapsuleCollider2D))]
    public class Jumper : MonoBehaviour
    {
        [SerializeField] private float _force = 10f;
        [SerializeField] private LayerMask _ground;
        
        private Rigidbody2D _rigidbody;
        private CapsuleCollider2D _collider;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _collider = GetComponent<CapsuleCollider2D>();
        }

        public void Jump()
        {
            if(_collider.IsTouchingLayers(_ground))
                _rigidbody.AddForce(Vector2.up * _force, ForceMode2D.Impulse);
        }
    }
}