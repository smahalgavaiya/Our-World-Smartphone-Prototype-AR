using UnityEngine;

public class ShottingStar : MonoBehaviour
{
    private float _xSpeed;
    private float _ySpeed;
    private Vector3 _move;

    private void Start()
    {
        GetRandom();
    }

    private void GetRandom()
    {
        _xSpeed = Random.Range(8f, 9f);
        _ySpeed = Random.Range(8f, 9f);
        _move = new Vector3(_xSpeed * Random.Range(1f, -1f), _ySpeed * Random.Range(1f, -1f), 0);
    }

    private void Update()
    {
        transform.position += _move * Time.deltaTime;
        if (transform.localPosition.x > 6 || transform.localPosition.x < -6 || transform.localPosition.y < -4f || transform.localPosition.y > 4f)
        {
            DestroyStar();
        }
    }

    private void DestroyStar()
    { 
        Destroy(gameObject);
    }
}
