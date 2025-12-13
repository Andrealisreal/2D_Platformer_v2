using Players;
using UnityEngine;

namespace Enemies
{
    public class EnemyDetector : MonoBehaviour
    {
        [Header("Layer Masks")]
        [SerializeField] private LayerMask _playerLayerMask;
        
        [Header("Overlap и Gizmos")]
        [SerializeField] private float _radius = 3f;
        [SerializeField] private Vector3 _position;
        [SerializeField] private Color _gizmoColor = Color.green;
        
        private Vector2 _direction;
        private Vector3 _rotatedOffset;

        private void OnDrawGizmos()
        {
            _rotatedOffset = transform.rotation * _position;
            
            Gizmos.color = _gizmoColor;
            Gizmos.DrawWireSphere(transform.position + _rotatedOffset, _radius);
        }

        public bool TryDetect(out Player player)
        {
            _rotatedOffset = transform.rotation * _position;
            var playerCollider = Physics2D.OverlapCircle(transform.position + _rotatedOffset, _radius, _playerLayerMask);

            if (playerCollider != null)
            {
                player = playerCollider.GetComponent<Player>();
                
                return true;
            }

            player = null;
            return false;
        }
    }
}
