using UnityEngine;

[RequireComponent(typeof(Collider))]
public class BreakRope : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object that collided with the rope has a Rigidbody component
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // Break the rope by destroying the HingeJoint component
            HingeJoint hingeJoint = GetComponent<HingeJoint>();
            if (hingeJoint != null)
            {
                Destroy(hingeJoint);
            }
        }
    }
}
