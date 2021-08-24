using UnityEngine;

public class ObjectRotation : MonoBehaviour
{
    [HideInInspector] public bool _rotate = true;

    public float add;
    public float time;
    public enum AxisToRotateIn
    { 
        UP,
        RIGHT,
        FORWARD
    }
    public AxisToRotateIn Axis;

    private Vector3 _axis;
    private Vector3 _originalPos;
    private void Start()
    {
        _originalPos = transform.localPosition;
        SetAxis();
    }

    public void SetAxis()
    {
        if (Axis == AxisToRotateIn.FORWARD)
            _axis = transform.forward;
        else if (Axis == AxisToRotateIn.UP)
            _axis = transform.up;
        else if (Axis == AxisToRotateIn.RIGHT)
            _axis = transform.right;
    }

    private void FixedUpdate()
    {
        transform.localPosition = _originalPos;
        if(_rotate)
            LeanTween.rotateAroundLocal(gameObject, _axis, add, time);
    }
}
