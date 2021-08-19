using UnityEngine;

public class ShottingStarSpawner : MonoBehaviour
{
    public GameObject _shootingStar;

    private float _xPos;
    private float _yPos;

    private void Start()
    {
        GetRandom();
    }

    private void GetRandom()
    {
        do
        {
            _xPos = Random.Range(-5.6f, 5.6f);
            _yPos = Random.Range(-2.9f, 2.9f);
        } while (_xPos < 0 && _yPos < 1.5f);
        GameObject star = Instantiate(_shootingStar);
        star.transform.SetParent(transform);
        star.transform.localPosition = new Vector3(_xPos, _yPos, 0);
        star.GetComponent<ParticleSystem>().Play();
        Invoke("GetRandom", Random.Range(5f, 10f));
    }
}
