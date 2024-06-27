using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[CreateAssetMenu(fileName = "PickUpItemStep", menuName = "Tutorial/Steps/PickUpItemStep")]
public class PickUpItemStep : TutorialStep
{
    private XRInteractionManager interactionManager;
    public override bool Condition()
    {
        //return HasPickedUp();
        throw new System.NotImplementedException();
    }

    public override void Init(GameObject player)
    {
        base.Init(player);
        interactionManager = player.GetComponent<XRInteractionManager>();
    }

    private void Initialize()
    {

    }

    //private bool HasPickedUp()
    //{
    //    //return 
    //}
}
