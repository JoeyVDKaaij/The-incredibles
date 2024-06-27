using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using FMODUnity;

public class Quiver : XRBaseInteractable
{
    [SerializeField] private GameObject arrowPrefab = null;
    [SerializeField] private GameObject bowPrefab = null;
    private bool bowSpawned = false;

    private EventReference pickUpEventReference;
    
    protected override void OnEnable()
    {
        base.OnEnable();
        selectEntered.AddListener(CreateAndSelectArrow);
        selectEntered.AddListener(CreateAndSelectBow);
        DespawnBow.OnBowDespawned += SetBowSpawnedFalse;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        selectEntered.RemoveListener(CreateAndSelectArrow);
        selectEntered.RemoveListener(CreateAndSelectBow);
        DespawnBow.OnBowDespawned -= SetBowSpawnedFalse;
    }

    private void SetBowSpawnedFalse()
    {
        bowSpawned = false;
    }

    private void CreateAndSelectBow(SelectEnterEventArgs args)
    {
        // Create bow, force into interacting hand
        if(bowSpawned)
        {
            return;
        }
        Bow bow = Instantiate(bowPrefab, args.interactorObject.transform.position, args.interactorObject.transform.rotation).GetComponent<Bow>();
        interactionManager.SelectEnter(args.interactorObject, bow);
        bowSpawned = true;
        if (!pickUpEventReference.IsNull)
            RuntimeManager.PlayOneShot(pickUpEventReference, transform.position);
    }

    private void CreateAndSelectArrow(SelectEnterEventArgs args)
    {
        if(!bowSpawned)
        {
            return;
        }
        // Create arrow, force into interacting hand
        Arrow arrow = CreateArrow(args.interactorObject.transform);
        interactionManager.SelectEnter(args.interactorObject, arrow);
        if (!pickUpEventReference.IsNull)
            RuntimeManager.PlayOneShot(pickUpEventReference, transform.position);
    }

    private Arrow CreateArrow(Transform orientation)
    {
        // Create arrow, and get arrow component
        GameObject arrowObject = Instantiate(arrowPrefab, orientation.position, orientation.rotation);
        return arrowObject.GetComponent<Arrow>();
    }
}