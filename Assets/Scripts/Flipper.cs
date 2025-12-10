using UnityEngine;

public class Flipper
{
    private readonly Quaternion _lookRight = Quaternion.Euler(0, 0, 0);
    private readonly Quaternion _lookLeft = Quaternion.Euler(0, 180, 0);

    public void Turn(Transform transform, Vector2 direction)
    {
        if (direction.x > 0)
            transform.rotation = _lookRight;
        else if (direction.x < 0)
            transform.rotation = _lookLeft;
    }
}
