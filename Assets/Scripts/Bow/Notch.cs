using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Notch : XRSocketInteractor
{
    //protected override void Awake()
    //{
    //    base.Awake();
    //    GetComponent<SphereCollider>().radius = socketSnappingRadius;
    //}
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, socketSnappingRadius);
    }

}
