using UnityEngine;

public class Death
{
    public void Die(GameObject gameObject)
    {
        Debug.Log("Смерть");
        Object.Destroy(gameObject);
    }    
}
