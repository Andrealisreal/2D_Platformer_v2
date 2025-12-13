using System.Collections;
using Enemies;
using Players;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    [SerializeField] private float _amountDamage = 10f;
    [SerializeField] private float _delay = 0.5f;
    [SerializeField] private float _range = 3f;
    
    private WaitForSeconds _wait;
    private WaitForFixedUpdate _waitFixed;
    private Health _targetHealth;

    private float _sqrRadius;
    
    private bool _isAttacking;
    
    private void Awake()
    {
        gameObject.SetActive(false);
        
        _wait = new WaitForSeconds(_delay);
        _waitFixed = new WaitForFixedUpdate();
        
        _sqrRadius = _range * _range;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Enemy enemy))
            _targetHealth = enemy.Health;
        else if(other.TryGetComponent(out Player player))
            _targetHealth = player.Health;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
         if (other.TryGetComponent(out Enemy enemy) || other.TryGetComponent(out Player player))
            _targetHealth = null;
    }

    public void Attack()
    {
        if (_isAttacking)
            return;
        
        gameObject.SetActive(true);
        StartCoroutine(CooldownAttack());
    }

    public bool IsInRange(Player player)
    {
        var direction = player.transform.position - transform.position;
        var distanceSqr = direction.sqrMagnitude;
        
        return (distanceSqr < _sqrRadius); 
    }

    private IEnumerator CooldownAttack()
    {
        _isAttacking = true;

        yield return _waitFixed;
        
        if (_targetHealth != null)
            _targetHealth.TakeDamage(_amountDamage);

        yield return _wait;
        
        gameObject.SetActive(false);
        _isAttacking = false;
    }
}
