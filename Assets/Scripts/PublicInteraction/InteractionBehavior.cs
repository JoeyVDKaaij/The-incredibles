using UnityEngine;


public abstract class InteractionBehavior : ScriptableObject
{
    protected Transform player;
    [SerializeField] protected KeyCode interactionKey;


    //Put this in the update loop in a MonoBehavior
    public void TryInteract(bool needsToPressInteract = true)
    {
        if(needsToPressInteract && Input.GetKeyDown(interactionKey))
        {
            DoAction();
        }else if(!needsToPressInteract)
        {
            DoAction();
        }
    }

    public void SetPlayer(Transform player)
    {
        this.player = player;
    }


    protected abstract void DoAction();
}
