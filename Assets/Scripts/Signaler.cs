using System.Collections;
using UnityEngine;

public class Signaler : MonoBehaviour
{
    [SerializeField] private AudioSource _soundOfAlarm;
    [SerializeField] private float _speedVolumeChange;

    private bool _isActive = false;

    private void OnTriggerEnter()
    {
        if (_isActive)
            TurnOff();
        else
            TurnOn();
    }

    private void TurnOn()
    {
        _soundOfAlarm.volume = 0f;
        _soundOfAlarm.Play();

        StartCoroutine(ChangeVolume());
    }

    private void TurnOff()
    {
        StartCoroutine(ChangeVolume());
    }

    private IEnumerator ChangeVolume()
    {
        var wait = new WaitForSeconds(_speedVolumeChange);
        int additiveInverseNumber = _isActive ? -1 : 1;
        float maxVolume = 1f;
        float step = 0.05f;

        for (float i = maxVolume; i >= 0; i -= step)
        {
            _soundOfAlarm.volume += step * additiveInverseNumber;

            yield return wait;
        }

        if (_isActive)
        {
            _soundOfAlarm.Stop();
            _isActive = false;
        }
        else
        {
            _isActive = true;
        }
    }
}
