using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NoiseScareInteraction", menuName = "Interactions/NoiseScareInteraction")]
public class NoiseScareInteraction : InteractionBehavior
{
    protected override void DoAction()
    {
        //Play the sound here
        AudioManager.Instance.PlayScareSound();
        Debug.Log("NOISE SCARE!");
    }
}
