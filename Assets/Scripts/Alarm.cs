using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Alarm : MonoBehaviour
{
    [Min(1)][SerializeField] private float _timeToFullVolume;

    private AudioSource _audioSource;
    private Coroutine action;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = 0;
    }

    public void TurnOn()
    {
        _audioSource.Play();
        action = StartCoroutine(IncreaseVolume(_timeToFullVolume));
    }
    public void TurnOff()
    {
        if (action != null)
        {
            StopCoroutine(action);
            action = null;
        }
        _audioSource.volume = 0;
        _audioSource.Stop();
    }
    private IEnumerator IncreaseVolume(float timeToFullVolume)
    {
        float timeFromStart = 0;
        if (timeToFullVolume < 0)
            timeToFullVolume = 1;
        while (timeFromStart < timeToFullVolume)
        {
            _audioSource.volume = Mathf.Lerp(0, 1, timeFromStart / timeToFullVolume);
            timeFromStart += Time.deltaTime;
            yield return null;
        }
    }
}
