using System.Collections;
using UnityEngine;

public class Death : MonoBehaviour
{
    [SerializeField] private float _delay = 2f;

    private WaitForSeconds _wait;

    private void Awake()
    {
        _wait = new WaitForSeconds(_delay);    
    }
    
    public void Die()
    {
        StartCoroutine(CooldownDie());
    }

    private IEnumerator CooldownDie()
    {
        yield return _wait;
        
        Destroy(gameObject);
    }
}
