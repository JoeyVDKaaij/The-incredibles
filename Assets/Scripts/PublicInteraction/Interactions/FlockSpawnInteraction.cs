using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FlockSpawnInteraction", menuName = "Interactions/FlockSpawnInteraction")]
public class FlockSpawnInteraction : InteractionBehavior
{
    [SerializeField] private GameObject flockPrefab;
    [SerializeField] private float timeAlive = 5f;
    [SerializeField] private Vector3 offSet = new Vector3(0, 3, 0);

    protected override void DoAction()
    {
        Debug.Log($"Spawning Flock");
        GameObject flock = Instantiate(flockPrefab, player.position + offSet, Quaternion.identity);
        flock.transform.SetParent(player);
        Destroy(flock, timeAlive);
    }
}
