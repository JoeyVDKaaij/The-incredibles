using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlankBreaking : MonoBehaviour
{
    Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.isKinematic = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DestructionBall"))
        {
            rb.isKinematic = false;
            rb.useGravity = true;
            Destroy(gameObject, 5.0f);
        }
    }
}
