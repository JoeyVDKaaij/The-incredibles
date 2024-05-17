using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ResetPlayer : MonoBehaviour
{
    private Transform initialTransform;

    private void Awake()
    {
        GetComponent<Collider>().isTrigger = true;
    }
    private void Start()
    {
        initialTransform = transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("DeathZone"))
        {
            Debug.Log("Player has died");
            transform.position = initialTransform.position;
        }
    }
}
