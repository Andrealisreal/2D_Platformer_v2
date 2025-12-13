using UnityEngine;

namespace Enemies.Movement
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Chaser : MonoBehaviour
    {
        [SerializeField] private float _speed = 3f;
        
        private Rigidbody2D _rigidbody;
        private Flipper _flipper;
        private Vector2 _direction;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _flipper = new Flipper();
        }
        
        public void Chase(Transform target)
        {
            _direction = (target.position - transform.position).normalized;
            
            _flipper.Turn(transform, _direction);
            _rigidbody.linearVelocity = new Vector2(_direction.x * _speed, _rigidbody.linearVelocity.y);
        }
    }
}
