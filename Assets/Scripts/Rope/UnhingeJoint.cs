using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HingeJoint))]
public class UnhingeJoint : MonoBehaviour
{
    private HingeJoint _hingeJoint;

    private void Awake()
    {
        _hingeJoint = GetComponent<HingeJoint>();
    }

    private void OnEnable()
    {
        Arrow.OnArrowHitRope += Unhinge;
    }

    private void OnDisable()
    {
        Arrow.OnArrowHitRope -= Unhinge;
    }

    private void Unhinge()
    {
        _hingeJoint.breakForce = 0f;
    }
}
