using UnityEngine;

namespace Enemies.Movement
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Patroller : MonoBehaviour
    {
        [Header("Настройка передвижения")]
        [SerializeField] private float _speed = 5f;
        
        [Header("Контрольные точки и дистанция поиска")]
        [SerializeField] private Transform[] _points;
        [SerializeField] private float _reachDistance = 0.2f;
        
        private Rigidbody2D _rigidbody;
        
        private int _currentWaypointIndex;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public void Patrol()
        {
            if (_points == null)
                return;
            
            Vector2 targetPosition = _points[_currentWaypointIndex].position;
            var direction = (targetPosition - _rigidbody.position).normalized;

            _rigidbody.linearVelocity = direction * _speed;

            if (Vector2.Distance(_rigidbody.position, targetPosition) <= _reachDistance)
                _currentWaypointIndex = (_currentWaypointIndex + 1) % _points.Length;
        }
    }
}
