using UnityEngine;
using System.Collections.Generic;
using FMODUnity;

[RequireComponent(typeof(BoxCollider))]
public class PlankBreaking : MonoBehaviour
{
    [SerializeField, Tooltip("Drag in the trigger collider on this object. If none available, create and assign one")] BoxCollider triggerCollider;
    [SerializeField, Tooltip("Drag in the collider on this object. If none available, create and assign one")] BoxCollider contactCollider;
    public bool triggerColliderTriggerBool { private set; get; }
    [SerializeField] private List<Rigidbody> childrenRigidbody;

    //This variable will be used to determine which plank we are currently on
    private bool isTouchingPlayer = false;

    private EventReference breakPlankEventReference;

    private void OnValidate()
    {
        triggerColliderTriggerBool = triggerCollider.isTrigger;
    }
    private void Awake()
    {
        BreakPlankBehaviour.OnPlankBreak += BreakPlank;
        for (int i = 0; i < childrenRigidbody.Count; i++)
        {
            if (childrenRigidbody[i] != null)
            {
                childrenRigidbody[i].isKinematic = true;
                childrenRigidbody[i].useGravity = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isTouchingPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isTouchingPlayer = false;
        }
    }

    private void BreakPlank()
    {
        if (isTouchingPlayer)
        {
            for (int i = 0; i < childrenRigidbody.Count; ++i)
            {
                childrenRigidbody[i].isKinematic = false;
                childrenRigidbody[i].useGravity = true;
            }
            contactCollider.enabled = false;
            Destroy(gameObject);
            if (!breakPlankEventReference.IsNull)
                RuntimeManager.PlayOneShot(breakPlankEventReference, transform.position);
        }
    }

    private void OnDestroy()
    {
        BreakPlankBehaviour.OnPlankBreak -= BreakPlank;
    }
}
