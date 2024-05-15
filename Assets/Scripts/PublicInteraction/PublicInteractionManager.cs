using System.Collections.Generic;
using UnityEngine;

public class PublicInteractionManager : MonoBehaviour
{
    [SerializeField] private List<InteractionBehavior> interactions;

    private void Update()
    {
        foreach (var interaction in interactions)
        {
            interaction.TryInteract();
        }
    }
}
