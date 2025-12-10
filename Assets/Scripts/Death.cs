using System.Collections;
using UnityEngine;

public class Death : MonoBehaviour
{
    public void Die()
    {
        StartCoroutine(CooldownDie());
    }

    private IEnumerator CooldownDie()
    {
        yield return new WaitForSeconds(2f);
        
        Destroy(gameObject);
    }
}
