using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WalkAndLookAroundStep", menuName = "Tutorial/Steps/WalkAndLookAroundStep")]
public class WalkAroundStep : TutorialStep
{
    // Variables to store the initial position of the player and the initialization state
    private Vector3 initialPosition;
    private bool isInitialized = false;

    [SerializeField] private float distanceToMove = 1.0f;
    public override bool Condition()
    {
        if (!isInitialized)
        {
            Initialize();
        }
        return HasPlayerMoved();
    }

    private void Initialize()
    {
        initialPosition = player.transform.position;
        isInitialized = true;
    }

    private bool HasPlayerMoved()
    {
        float distanceMoved = Vector3.Distance(initialPosition, player.transform.position); // Calculate the distance moved by the player
        return distanceMoved >= distanceToMove; // Return true if the player has moved more than the set distance
    }
}
