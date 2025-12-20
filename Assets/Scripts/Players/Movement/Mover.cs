using Players.Input;
using UnityEngine;

namespace Players.Movement
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Mover : MonoBehaviour
    {
        [SerializeField] private float _speed = 5f;
        [SerializeField] private Transform _transformView;
        
        private PlayerInput _input;
        private Rigidbody2D _rigidbody;
        private Flipper _flipper;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _flipper = new Flipper();
        }

        public void Move(Vector2 direction)
        {
            _rigidbody.linearVelocity = new Vector2(direction.x * _speed, _rigidbody.linearVelocity.y);
            _flipper.Turn(_transformView, direction);
        }
    }
}
