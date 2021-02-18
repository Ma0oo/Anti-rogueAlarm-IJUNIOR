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
        _action = StartCoroutine(IncreaseVolume(_timeToFullChangeVolume, 5));
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

        timeToChange = Mathf.Max(1, timeToChange);
        volumeTarget = Mathf.Clamp(volumeTarget, 0, 1);

        while (timeFromStart < timeToChange)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, volumeTarget, Time.deltaTime/timeToChange);
            timeFromStart += Time.deltaTime;
            yield return null;
        }
        _audioSource.volume = volumeTarget;
    }
}
