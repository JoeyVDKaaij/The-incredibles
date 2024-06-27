using UnityEngine;
using UnityEngine.XR;

public class CustomPlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController characterController; // Reference to the CharacterController component
    [SerializeField] private Transform vrHead; // Reference to the VR headset transform

    [Header("Settings")]
    [Space(2)]
    [SerializeField] private float collisionPushBackForce = 0.15f; // Force to push back when colliding with an object
    [SerializeField] private float headRadius = 0.2f; // Radius of the spherecast for head collision
    [SerializeField] private LayerMask collisionMask; // Layer mask for objects to collide with


    void FixedUpdate()
    {
        CheckHeadCollision();
    }

    //DEBUG
    private void OnDrawGizmosSelected()
    {
        // Draw a sphere at the VR headset's position
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(vrHead.position, headRadius);

        if (Physics.Raycast(vrHead.position, vrHead.InverseTransformDirection(vrHead.forward), out RaycastHit hit, headRadius, collisionMask))
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(vrHead.position, hit.point);
            Gizmos.DrawWireSphere(hit.point, headRadius);
        }
    }


    void CheckHeadCollision()
    {
        // Perform a spherecast from the VR headset's position
        if (Physics.SphereCast(vrHead.position, headRadius, vrHead.forward, out RaycastHit hit, headRadius, collisionMask))
        {
            if (hit.collider.isTrigger) return;
            Debug.Log("Head collision detected with " + hit.collider.name);

            // Calculate push back direction
            Vector3 pushBackDirection = -vrHead.InverseTransformDirection(vrHead.forward) * collisionPushBackForce;

            // Apply the push back
            transform.Translate(pushBackDirection);
        }
    }
}
