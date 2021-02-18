using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Alarm : MonoBehaviour
{
    [Min(1)][SerializeField] private float _timeToFullChangeVolume;

    private AudioSource _audioSource;
    private Coroutine _action;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = 0;
    }

    public void TurnOn()
    {
        TryDeleteCoroutine();
        _audioSource.Play();
        _action = StartCoroutine(IncreaseVolume(_timeToFullChangeVolume, 1));
    }
    public void TurnOff()
    {
        TryDeleteCoroutine();
        _action = StartCoroutine(IncreaseVolume(_timeToFullChangeVolume, 0));
        Invoke("StopPlaySource", _timeToFullChangeVolume);
    }
    private void TryDeleteCoroutine()
    {
        if (_action != null)
        {
            StopCoroutine(_action);
            _action = null;
        }
    }
    private void StopPlaySource()
    {
        _audioSource.Stop();
    }
    private IEnumerator IncreaseVolume(float timeToChange, float volumeTarget)
    {
        float timeFromStart = 0;

        if (timeToChange < 0)
            timeToChange = 1;

        if (volumeTarget > 1)
            volumeTarget = 1;
        if (volumeTarget < 0)
            volumeTarget = 0;
        while (timeFromStart < timeToChange)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, volumeTarget, Time.deltaTime/timeToChange);
            timeFromStart += Time.deltaTime;
            yield return null;
        }
        _audioSource.volume = volumeTarget;
    }
}
