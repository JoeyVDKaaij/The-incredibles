using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NoiseScareInteraction", menuName = "Interactions/NoiseScareInteraction")]
public class NoiseScareInteraction : InteractionBehavior
{
    //Reference to sound

    protected override void DoAction()
    {
        //Play the sound here

        Debug.Log("NOISE SCARE!");
    }
}
