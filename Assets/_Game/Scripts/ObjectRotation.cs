using System;
using UnityEngine;

public class ObjectRotation : MonoBehaviour
{
    LTDescr rotate;

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
        StartRotation();
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

    public void StartRotation()
    {
        rotate = LeanTween.rotateAroundLocal(gameObject, _axis, add, time).setOnComplete(() => {
            StartRotation();
        });
    }

    public void StopRotation()
    {
        LeanTween.cancel(rotate.id);
    }
}
