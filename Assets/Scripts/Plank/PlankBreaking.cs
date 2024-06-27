using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class PlankBreaking : MonoBehaviour
{
    [SerializeField, Tooltip("Drag in the trigger collider on this object. If none available, create and assign one")] BoxCollider triggerCollider;
    [SerializeField, Tooltip("Drag in the collider on this object. If none available, create and assign one")] BoxCollider contactCollider;
    public bool triggerColliderTriggerBool { private set; get; }
    Rigidbody[] childrenRigidbody;

    //This variable will be used to determine which plank we are currently on
    private bool isTouchingPlayer = false;

    private void OnValidate()
    {
        triggerColliderTriggerBool = triggerCollider.isTrigger;
    }
    private void Awake()
    {
        BreakPlankBehaviour.OnPlankBreak += BreakPlank;
        childrenRigidbody = new Rigidbody[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            childrenRigidbody[i] = transform.GetChild(i).gameObject.TryGetComponent(out Rigidbody rb) ? rb : null;
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
            for(int i=0;i<childrenRigidbody.Length; ++i)
            {
                childrenRigidbody[i].isKinematic = false;
                childrenRigidbody[i].useGravity = true;
            }
            contactCollider.enabled = false;
            Destroy(gameObject, 5.0f);
        }
    }

    private void OnDestroy()
    {
        BreakPlankBehaviour.OnPlankBreak -= BreakPlank;
    }
}
