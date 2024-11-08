using System.Collections;
using UnityEngine;

public class Signaler : MonoBehaviour
{
    [SerializeField] private AudioSource _soundOfAlarm;
    [SerializeField] private float _speedVolumeChange;
    [SerializeField] private House _house;

    private float targetVolume;

    private void OnEnable()
    {
        _house.Entered += TurnOn;
        _house.Left += TurnOff;
    }

    private void OnDisable()
    {
        _house.Entered -= TurnOn;
        _house.Left -= TurnOff;
    }

    private void TurnOn()
    {
        _soundOfAlarm.volume = 0f;
        targetVolume = 1f;
        _soundOfAlarm.Play();

        StartCoroutine(ChangeVolume());
    }

    private void TurnOff()
    {
        targetVolume = 0f;

        StartCoroutine(ChangeVolume());
    }

    private IEnumerator ChangeVolume()
    {
        var wait = new WaitForSeconds(_speedVolumeChange);
        float step = 0.05f;

        while (targetVolume != _soundOfAlarm.volume)
        {
            _soundOfAlarm.volume = Mathf.MoveTowards( _soundOfAlarm.volume, targetVolume, step);

            yield return wait;
        }

        if (targetVolume == 0f)
            _soundOfAlarm.Stop();
    }
}
