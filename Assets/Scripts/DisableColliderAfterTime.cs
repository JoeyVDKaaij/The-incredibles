using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableColliderAfterTime : MonoBehaviour
{
    [SerializeField] private float timeToDisable = 0.5f;
    [SerializeField] private Collider colliderToDisable;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
           Debug.Log("Collision with PLAYER detected!");
           StartCoroutine(DisableCollider());
        }
    }

    private IEnumerator DisableCollider()
    {
        yield return new WaitForSeconds(timeToDisable);
        colliderToDisable.enabled = false;
    }
}
