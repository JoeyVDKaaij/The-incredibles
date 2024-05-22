using System.Collections.Generic;
using UnityEngine;

public class PublicInteractionManager : MonoBehaviour
{
    [SerializeField] private List<InteractionBehavior> interactions;
    private Transform player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        foreach (var interaction in interactions)
        {
            interaction.SetPlayer(player);
        }
    }

    private void Update()
    {
        foreach (var interaction in interactions)
        {
            interaction.TryInteract();
        }
    }
}
