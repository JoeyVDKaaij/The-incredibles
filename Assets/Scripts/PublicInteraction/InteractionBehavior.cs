using UnityEngine;


public abstract class InteractionBehavior : ScriptableObject
{
    protected Transform player;
    [SerializeField] protected KeyCode interactionKey;

    //Put this in the update loop in a MonoBehavior
    public void TryInteract()
    {
        if(Input.GetKeyDown(interactionKey))
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
