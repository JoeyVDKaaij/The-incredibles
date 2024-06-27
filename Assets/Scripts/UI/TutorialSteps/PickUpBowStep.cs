using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "PickUpBowStep", menuName = "Tutorial/Steps/PickUpBowStep", order = 1)]
public class PickUpBowStep : TutorialStep
{
    private bool hasPickedUpBow = false;
    public override void Init(GameObject player)
    {
        base.Init(player);
        Quiver.OnBowSpawned -= BowSpawned;
        Quiver.OnBowSpawned += BowSpawned;
    }
    public override bool Condition()
    {
        return hasPickedUpBow;
    }

    public void BowSpawned()
    {
        hasPickedUpBow = true;
        Quiver.OnBowSpawned -= BowSpawned;
    }
}
