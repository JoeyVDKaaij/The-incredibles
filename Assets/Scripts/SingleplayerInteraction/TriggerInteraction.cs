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
            Debug.Log($"Player entered trigger for the TriggerInteraction script on the object: {gameObject.name}");
            interactionBehavior.TryInteract(false);
        }
    }
}
