using UnityEngine;


public abstract class InteractionBehavior : ScriptableObject
{
    [SerializeField] protected KeyCode interactionKey;

    //Put this in the update loop in a MonoBehavior
    public void TryInteract()
    {
        if(Input.GetKeyDown(interactionKey))
        {
            DoAction();
        }
    }

    protected abstract void DoAction();
}
