using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class FlockAgent : MonoBehaviour
{
    public Flock AgentFlock { get; private set; }
    public void Initialize(Flock flock)
    {
        AgentFlock = flock;
    }
    public Collider AgentCollider { get; private set; }

    private void Awake()
    {
        AgentCollider = GetComponent<Collider>();
    }

    public void Move(Vector3 velocity)
    {
        transform.forward = velocity;
        transform.position += velocity * Time.deltaTime;
    }
}
