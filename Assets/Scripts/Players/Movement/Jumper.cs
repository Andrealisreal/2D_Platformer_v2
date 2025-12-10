using UnityEngine;

namespace Players.Movement
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CapsuleCollider2D))]
    public class Jumper : MonoBehaviour
    {
        [Header("Настройка прыжка")]
        [SerializeField] private float _force = 6f;
        
        [Header("Gizmo и Overlap")]
        [SerializeField] private LayerMask _ground;
        [SerializeField] private float _radius = 3f;
        [SerializeField] private Vector3 _position;
        [SerializeField] private Color _gizmoColor = Color.green;
        
        private Rigidbody2D _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = _gizmoColor;
            Gizmos.DrawWireSphere(transform.position + _position, _radius);
        }

        public void Jump()
        {
            if(TryJump() == false)
                return;

            _rigidbody.AddForce(Vector2.up * _force, ForceMode2D.Impulse);
        }

        public bool TryJump()
        {
            bool isGrounded = Physics2D.OverlapCircle(transform.position + _position, _radius, _ground);
            
            return isGrounded;
        }
    }
}