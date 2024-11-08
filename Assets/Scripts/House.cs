using System;
using UnityEngine;

public class House : MonoBehaviour
{
    public event Action Entered;
    public event Action Left;

    private void OnTriggerEnter()
    {
        Entered?.Invoke();
    }

    private void OnTriggerExit()
    {
        Left?.Invoke();
    }
}
