using Enemies;
using UnityEngine;

namespace Players.Abilities.Vampirisms
{
    public class VampirismDetector : MonoBehaviour
    {
        [SerializeField] private float _radius;
        [SerializeField] private LayerMask _layerMask;
        
        private Collider2D[] _colliders;
        private ContactFilter2D _contactFilter;
        
        public float Radius => _radius;

        private void Awake()
        {
            _colliders = new Collider2D[32];
            _contactFilter = new ContactFilter2D
            {
                layerMask = _layerMask,
            };
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, _radius);
        }

        public Enemy GetEnemy()
        {
            var countEnemies = Physics2D.OverlapCircle(transform.position, _radius, _contactFilter, _colliders);
            var minDistSqr = float.MaxValue;
            Enemy nearest = null;

            for (var i = 0; i < countEnemies; i++)
            {
                var enemyCollider = _colliders[i];

                if (enemyCollider == null || enemyCollider.TryGetComponent<Enemy>(out var enemy) == false || (enemy.Health.Current > 0) == false) 
                    continue;
                
                var distSqr = (enemy.transform.position - transform.position).sqrMagnitude;
                    
                if ((distSqr < minDistSqr) == false)
                    continue;
                    
                minDistSqr = distSqr;
                nearest = enemy;
            }
            
            return nearest;
        }
    }
}
