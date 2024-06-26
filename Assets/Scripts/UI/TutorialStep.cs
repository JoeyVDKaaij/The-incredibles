using System;
using UnityEngine;

public abstract class TutorialStep : ScriptableObject
{
    [SerializeField] private string instruction;
    [SerializeField] private Sprite instructionImage;

    //Public getters for encapsulation
    public string Instruction { get { return instruction; } }
    public Sprite InstructionImage { get { return instructionImage; } }

    protected GameObject player;
    public void Init(GameObject player)
    {
        this.player = player;
    }
    public abstract bool Condition();
}
