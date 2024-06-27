using System;
using UnityEngine;

public abstract class TutorialStep : ScriptableObject
{
    [SerializeField] private string instruction;
    [SerializeField] private Sprite instructionImage;
    [SerializeField] private bool starterStep = false;

    //Public getters for encapsulation
    public string Instruction { get { return instruction; } }
    public Sprite InstructionImage { get { return instructionImage; } }
    public bool StarterStep { get { return starterStep; } }

    protected GameObject player;
    public virtual void Init(GameObject player)
    {
        this.player = player;
    }
    public abstract bool Condition();
}
