using UnityEngine;
using UnityEngine.XR;

public class CollideOnHeadMovement : MonoBehaviour
{
    [SerializeField] private Transform xrCamera; // Reference to the XR camera
    [SerializeField] private LayerMask collisionLayers; // Layer mask for the layers to collide with

    private Vector3 previousPosition;
    private CharacterController characterController;

    void Start()
    {
        if (xrCamera == null)
        {
            xrCamera = Camera.main.transform;
        }

        // Ensure the CharacterController component is attached
        characterController = GetComponent<CharacterController>();
        if (characterController == null)
        {
            characterController = gameObject.AddComponent<CharacterController>();
        }

        previousPosition = xrCamera.position;
    }

    void Update()
    {
        Vector3 currentPosition = xrCamera.position;
        Vector3 direction = currentPosition - previousPosition;
        float distance = direction.magnitude;

        if (distance > 0)
        {
            RaycastHit hit;
            if (Physics.Raycast(previousPosition, direction, out hit, distance, collisionLayers))
            {
                // If there's a collision with the specified layers, prevent further movement
                Vector3 hitPosition = hit.point - direction.normalized * characterController.radius;
                transform.position = hitPosition;
            }
            else
            {
                // No collision, update previous position
                previousPosition = currentPosition;
            }
        }
    }
}
