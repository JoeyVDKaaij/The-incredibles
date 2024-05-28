using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class PlankBreaking : MonoBehaviour
{
    [SerializeField, Tooltip("Drag in the trigger collider on this object. If none available, create and assign one")] BoxCollider triggerCollider;
    [SerializeField, Tooltip("Drag in the collider on this object. If none available, create and assign one")] BoxCollider contactCollider;
    public bool triggerColliderTriggerBool { private set; get; }
    Rigidbody[] childrenRB;

    //This variable will be used to determine which plank we are currently on
    private bool isCurrentPlank = false;

    private void OnValidate()
    {
        triggerColliderTriggerBool = triggerCollider.isTrigger;
    }
    private void Awake()
    {
        BreakPlankBehaviour.OnPlankBreak += BreakPlank;
        childrenRB = new Rigidbody[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            childrenRB[i] = transform.GetChild(i).gameObject.GetComponent<Rigidbody>();
            if (childrenRB[i] == null)
            {
                Debug.LogError("Rigidbody not found on child object. Please make sure to add a RigidBody on all children of this object!");
            }
            else
            {
                childrenRB[i].isKinematic = true;
                childrenRB[i].useGravity = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isCurrentPlank = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isCurrentPlank = false;
        }
    }

    private void BreakPlank()
    {
        if (isCurrentPlank)
        {
            for(int i=0;i<childrenRB.Length; i++)
            {
                childrenRB[i].isKinematic = false;
                childrenRB[i].useGravity = true;
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
