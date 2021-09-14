using UnityEngine;

public class StarsLensFlare : MonoBehaviour
{
    private ParticleSystem _particleSystem;
    private float _xPos;
    private float _yPos;
    private float _duration;

    private void Start()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        GetRandom();
    }

    private void GetRandom()
    {
        _particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        do
        {
            _xPos = Random.Range(-5.6f, 5.6f);
            _yPos = Random.Range(-2.9f, 2.9f);
        } while (_xPos < 0 && _yPos < 1.5f);
        _duration = Random.Range(5.0f, 20.0f);
        gameObject.transform.localPosition = new Vector3(_xPos, _yPos, -10);
        var main = _particleSystem.main;
        main.duration = _duration;
        _particleSystem.Play();
    }

    private void OnParticleSystemStopped()
    {
        GetRandom();
    }
}
