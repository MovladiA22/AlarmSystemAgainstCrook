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
        _door.Entered += ToggleAlarm; 
    }

    private void OnDisable()
    {
        _door.Entered -= ToggleAlarm;
    }

    private void ToggleAlarm()
    {
        float targetVolume = 0f;

        if (_isActive == false)
        {
            targetVolume = 1f;
            _soundOfAlarm.volume = 0f;
            _soundOfAlarm.Play();
        }

        StartCoroutine(ChangeVolume(targetVolume));
    }

    private IEnumerator ChangeVolume(float targetVolume)
    {
        var wait = new WaitForSeconds(_speedVolumeChange);
        float step = 0.05f;

        while (targetVolume != _soundOfAlarm.volume)
        {
            _soundOfAlarm.volume = Mathf.MoveTowards( _soundOfAlarm.volume, targetVolume, step);

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
