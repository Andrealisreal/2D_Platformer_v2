using UnityEngine;

namespace Enemies.Movement
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Chaser : MonoBehaviour
    {
        [SerializeField] private float _speed = 3f;

        [Header("Gizmo и Overlap")]
        [SerializeField] private LayerMask _playerLayerMask;
        [SerializeField] private float _radius = 3f;
        [SerializeField] private Vector3 _position;
        [SerializeField] private Color _gizmoColor = Color.green;
        
        private Rigidbody2D _rigidbody;
        private Flipper _flipper;
        private Vector2 _direction;
        private Vector3 _rotatedOffset;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _flipper = new Flipper();
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = _gizmoColor;
            
            _rotatedOffset = transform.rotation * _position;
            
            Gizmos.DrawWireSphere(transform.position + _rotatedOffset, _radius);
        }
        
        public void Chase()
        {
            if (TryPlayer(out var playerPosition) == false)
            {
                _rigidbody.linearVelocity = new Vector2(0, _rigidbody.linearVelocity.y);
                
                return;
            }

            _direction = (playerPosition - transform.position).normalized;
            
            _flipper.Turn(transform, _direction);
            _rigidbody.linearVelocity = new Vector2(_direction.x * _speed, _rigidbody.linearVelocity.y);
        }
        
        public bool TryPlayer(out Vector3 playerPosition)
        {
            _rotatedOffset = transform.rotation * _position;
            var playerCollider = Physics2D.OverlapCircle(transform.position + _rotatedOffset, _radius, _playerLayerMask);
            
            if (playerCollider != null)
            {
                playerPosition = playerCollider.transform.position;
                
                return true;
            }

            playerPosition = Vector2.zero;
            
            return false;
        }
    }
}
