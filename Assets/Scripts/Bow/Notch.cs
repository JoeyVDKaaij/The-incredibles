using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Notch : XRSocketInteractor
{
    protected override void Awake()
    {
        base.Awake();
        PullInteraction.PullActionReleased += OnArrowReleased;
    }

    protected override void OnDestroy()
    {
        PullInteraction.PullActionReleased -= OnArrowReleased;
        base.OnDestroy();
    }

    private void OnArrowReleased(float pullAmount)
    {
        // Force the notch to release the arrow using the new XR Interaction Toolkit methods
        if (hasSelection)
        {
            IXRSelectInteractable selectedArrow = firstInteractableSelected;
            Debug.Log($"The selected arrow : {selectedArrow}");
            if (interactionManager != null && selectedArrow != null)
            {
                interactionManager.SelectExit(this, selectedArrow);
                StartCoroutine(DisableAndEnableSocket());
            }
        }
    }

    private IEnumerator DisableAndEnableSocket()
    {
        socketActive = false;
        yield return new WaitForSeconds(0.2f);
        socketActive = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, socketSnappingRadius);

        if(socketScaleMode == SocketScaleMode.StretchedToFitSize)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(transform.position, targetBoundsSize);
        }
    }
}
