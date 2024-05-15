using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FlockSpawnInteraction", menuName = "Interactions/FlockSpawnInteraction")]
public class FlockSpawnInteraction : InteractionBehavior
{
    [SerializeField] private Flock flockPrefab;
    [SerializeField] private float flockTimeAlive = 5f;
    protected override void DoAction()
    {
        Debug.Log($"Spawning Flock");
        Instantiate(flockPrefab);
        Destroy(flockPrefab, flockTimeAlive);
    }
}
