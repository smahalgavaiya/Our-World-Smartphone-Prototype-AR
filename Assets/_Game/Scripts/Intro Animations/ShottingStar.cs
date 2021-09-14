using UnityEngine;

public class ShottingStar : MonoBehaviour
{
    private float _xSpeed;
    private float _ySpeed;
    private Vector3 _move;
    private bool _canMove;
    private float _xDesMin;
    private float _xDesMax;
    private float _yDesMin;
    private float _yDesMax;

    public void StarInit(float xMin, float xMax, float yMin, float yMax, float _speedMin, float _speedMax)
    {
        _xDesMin = xMin;
        _xDesMax = xMax;
        _yDesMin = yMin;
        _yDesMax = yMax;
        _xSpeed = Random.Range(_speedMin, _speedMax);
        _ySpeed = Random.Range(_speedMin, _speedMax);
        _move = new Vector3(_xSpeed * Random.Range(1f, -1f), _ySpeed * Random.Range(1f, -1f), 0);
        _canMove = true;
    }

    private void Update()
    {
        if (_canMove)
        {
            transform.position += _move * Time.deltaTime;
            if (transform.localPosition.x > _xDesMax || transform.localPosition.x < _xDesMin || transform.localPosition.y < _yDesMin || transform.localPosition.y > _yDesMax)
            {
                DestroyStar();
            }
        }
    }

    private void DestroyStar()
    {
        Destroy(gameObject);
    }
}
