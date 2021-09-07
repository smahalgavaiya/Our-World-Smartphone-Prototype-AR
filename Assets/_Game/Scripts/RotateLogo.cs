using UnityEngine;

public class RotateLogo : MonoBehaviour
{
    public float speed;
    private LTDescr rotate;
    private void OnEnable() => StartRotate();
    private void OnDisable() => StopRotate();
    private void StopRotate() => LeanTween.cancel(rotate.id);
    private void StartRotate() => rotate = LeanTween.rotateAroundLocal(gameObject, Vector3.up, speed, 50).setOnComplete(() => StartRotate());
}
