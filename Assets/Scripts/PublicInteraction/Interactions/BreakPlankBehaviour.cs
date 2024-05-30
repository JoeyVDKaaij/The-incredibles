using System;
using UnityEngine;

[CreateAssetMenu(fileName = "BreakPlankBehaviour", menuName = "Interactions/BreakPlankBehaviour")]
public class BreakPlankBehaviour : InteractionBehavior
{
    public static event Action OnPlankBreak;

    protected override void DoAction()
    {
        OnPlankBreak?.Invoke();
    }
}
