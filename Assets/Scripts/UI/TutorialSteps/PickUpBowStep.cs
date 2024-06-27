using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "PickUpBowStep", menuName = "ScriptableObjects/Steps/PickUpBowStep", order = 1)]
public class PickUpBowStep : TutorialStep
{
    [SerializeField] private UnityEvent onActionComplete;

    private bool isBowPickedUp = false;

    public override bool Condition()
    {
        throw new System.NotImplementedException();
    }
}
