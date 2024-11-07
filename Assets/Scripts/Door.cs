using System;
using UnityEngine;

public class Door : MonoBehaviour
{
    public event Action Entered;

    private void OnTriggerEnter()
    {
        Entered?.Invoke();
    }
}
