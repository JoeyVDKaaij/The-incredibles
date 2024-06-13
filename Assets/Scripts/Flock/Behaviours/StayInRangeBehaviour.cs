using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviours/Stay In Range")]
public class StayInRangeBehaviour : FlockBehavior
{
    [SerializeField] private float radius = 15f;

    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        Vector3 centerOffset = flock.transform.position - agent.transform.position;
        float t = centerOffset.magnitude / radius;

        //if(t < 0.9f)
        //{
        //    return Vector3.zero;
        //}

        return centerOffset * t * t;
    }
}
