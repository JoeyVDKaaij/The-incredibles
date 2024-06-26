using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class TriggerInteraction : MonoBehaviour
{
    [SerializeField, Tooltip("Drag in the trigger collider")] private BoxCollider boxCollider;
    [SerializeField, Tooltip("Drag in the interaction behavior")] private InteractionBehavior interactionBehavior;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            interactionBehavior.TryInteract(false);
        }
    }
}
