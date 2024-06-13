using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(HingeJoint))]
public class AttachPlankToLine : MonoBehaviour
{
    [SerializeField, Tooltip("The attach point of the plank")] private Transform _plankAttachPoint;
    [SerializeField, Tooltip("The point to attach the plank to")] private Transform _attachPoint;
    [SerializeField, Tooltip("The game object to attach to")] private GameObject _attachToObject;

    private HingeJoint _hingeJoint;

    private void Start()
    {
        // Ensure the plank has a Rigidbody component
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.useGravity = true;

        // Ensure the plank has a HingeJoint component
        _hingeJoint = GetComponent<HingeJoint>();

        // Configure the HingeJoint
        _hingeJoint.autoConfigureConnectedAnchor = false;

        // Attach the plank to the specified game object
        if (_attachToObject != null)
        {
            Rigidbody attachToRb = _attachToObject.GetComponent<Rigidbody>();
            if (attachToRb != null)
            {
                _hingeJoint.connectedBody = attachToRb;

                // Set the connected anchor in the local space of the connected body
                _hingeJoint.connectedAnchor = attachToRb.transform.InverseTransformPoint(_attachPoint.position);
            }
            else
            {
                Debug.LogWarning("The specified attachToObject does not have a Rigidbody component.");
            }
        }
        else
        {
            Debug.LogWarning("No attachToObject specified.");
        }

        // Set the anchor of the hinge joint relative to the attach point
        _hingeJoint.anchor = transform.InverseTransformPoint(_plankAttachPoint.position);
    }

    // Remove Update method to prevent continuous updates of the hinge joint's properties
}
