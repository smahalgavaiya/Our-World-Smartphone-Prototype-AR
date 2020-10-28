using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public const float RotationSpeed = 20f;
    //public GameeObject Speed;

    void Update()
    {
        transform.Rotate(Vector3.up, RotationSpeed * Time.deltaTime);
    }
}