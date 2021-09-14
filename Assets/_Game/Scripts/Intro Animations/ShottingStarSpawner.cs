using UnityEngine;

public class ShottingStarSpawner : MonoBehaviour
{
    public GameObject _shootingStar;

    private float _xPos;
    private float _yPos;

    public float _xMin;
    public float _xMax;
    public float _yMin;
    public float _yMax;
    public float _xDesMin;
    public float _xDesMax;
    public float _yDesMin;
    public float _yDesMax;
    public float _speedMin;
    public float _speedMax;
    public bool _isNotMainMenu;

    private void Start()
    {
        GetRandom();
    }

    private void GetRandom()
    {
        _xPos = Random.Range(_xMin, _xMax);
        _yPos = Random.Range(_yMin, _yMax);
        GameObject star = Instantiate(_shootingStar);
        star.transform.SetParent(transform);
        star.transform.localPosition = new Vector3(_xPos, _yPos, 0);
        star.GetComponent<ShottingStar>().StarInit(_xDesMin, _xDesMax, _yDesMin, _yDesMax, _speedMin, _speedMax);
        if (_isNotMainMenu)
        {
            ParticleSystem.MainModule main = star.GetComponent<ParticleSystem>().main;
            main.startSize = 50;
        }
        star.GetComponent<ParticleSystem>().Play();
        Invoke("GetRandom", Random.Range(5f, 10f));
    }
}
