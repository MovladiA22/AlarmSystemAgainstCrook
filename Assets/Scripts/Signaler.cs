using System.Collections;
using UnityEngine;

public class Signaler : MonoBehaviour
{
    [SerializeField] private AudioSource _soundOfAlarm;
    [SerializeField] private float _speedVolumeChange;
    [SerializeField] private Door _door;

    private bool _isActive = false;

    private void OnEnable()
    {
        _door.Entered += TurnOnOrOff; 
    }

    private void OnDisable()
    {
        _door.Entered -= TurnOnOrOff;
    }

    private void TurnOnOrOff()
    {
        if (_isActive == false)
        {
            _soundOfAlarm.volume = 0f;
            _soundOfAlarm.Play();
        }

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
