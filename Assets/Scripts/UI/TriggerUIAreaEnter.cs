using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class TriggerUIAreaEnter : MonoBehaviour
{
    [SerializeField] private UnityEvent triggerEntered;
    [SerializeField] private UnityEvent triggerExited;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            triggerEntered?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            triggerExited?.Invoke();
        }
    }
}
