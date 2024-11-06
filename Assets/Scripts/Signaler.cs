using System.Collections;
using UnityEngine;

public class Signaler : MonoBehaviour
{
    [SerializeField] private AudioSource _soundOfAlarm;
    [SerializeField] private float _speedVolumeChange;

    private bool _isActive = false;
    private Coroutine _onCoroutine;
    private Coroutine _offCoroutine;

    private void OnTriggerEnter()
    {
        if (_isActive)
            _offCoroutine = StartCoroutine(TurnOff());
        else
            _onCoroutine = StartCoroutine(TurnOn());
    }

    private IEnumerator TurnOn()
    {
        var wait = new WaitForSeconds(_speedVolumeChange);
        float maxVolume = 1f;
        float step = 0.1f;

        if (_offCoroutine != null)
            StopCoroutine(_offCoroutine);

        _soundOfAlarm.volume = 0f;
        _soundOfAlarm.Play();
        _isActive = true;

        for (float i = 0; i < maxVolume; i += step)
        {
            _soundOfAlarm.volume = i;

            yield return wait;
        }
    }

    private IEnumerator TurnOff()
    {
        var wait = new WaitForSeconds(_speedVolumeChange);
        float maxVolume = 1f;
        float step = 0.1f;

        if (_onCoroutine != null)
            StopCoroutine(_onCoroutine);

        for (float i = maxVolume; i >= 0; i -= step)
        {
            _soundOfAlarm.volume = i;

            yield return wait;
        }

        _soundOfAlarm.Stop();
        _isActive = false;
    }
}
