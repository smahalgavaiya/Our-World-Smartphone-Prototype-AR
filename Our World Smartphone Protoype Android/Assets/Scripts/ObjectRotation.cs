using UnityEngine;

public class ObjectRotation : MonoBehaviour
{
    public float add;
    public float time;
    public Vector3 offset;

    private void FixedUpdate()
    {
        LeanTween.rotateAroundLocal(gameObject, Vector3.up, add, time);
        transform.localPosition = Vector3.zero + offset;
    }
}
