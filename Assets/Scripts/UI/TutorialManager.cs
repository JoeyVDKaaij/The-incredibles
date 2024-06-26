using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private Canvas worldCanvas;
    [SerializeField] private Text instructionText;
    [SerializeField] private Image instructionImage;
    [SerializeField] private List<TutorialStep> tutorialSteps;

    private Queue<TutorialStep> stepsQueue;
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if(player == null)
        {
            Debug.LogError("Player not found");
            return;
        }
        foreach(TutorialStep step in tutorialSteps) // Initialize all steps adding the player reference
        {
            step.Init(player);
        }
        stepsQueue = new Queue<TutorialStep>(tutorialSteps);
        StartCoroutine(RunTutorial());
    }

    IEnumerator RunTutorial()
    {
        while (stepsQueue.Count > 0)
        {
            TutorialStep currentStep = stepsQueue.Dequeue();
            ShowInstruction(currentStep);
            yield return new WaitUntil(currentStep.Condition);
            //..............................//
            Debug.Log($"Step {currentStep.name} completed"); // Debug log for sanity's sake
            //..............................//
            yield return StartCoroutine(FadeOutUI());
        }
        EndTutorial();
    }

    void ShowInstruction(TutorialStep step)
    {
        instructionText.text = step.Instruction;
        if (step.InstructionImage != null)
        {
            instructionImage.sprite = step.InstructionImage;
            instructionImage.enabled = true;
        }
        else
        {
            instructionImage.enabled = false;
        }
        StartCoroutine(FadeInUI());
    }

    IEnumerator FadeInUI()
    {
        float elapsedTime = 0f;
        float fadeDuration = 1f;
        CanvasGroup canvasGroup = worldCanvas.GetComponent<CanvasGroup>();
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            yield return null;
        }
    }

    IEnumerator FadeOutUI()
    {
        float elapsedTime = 0f;
        float fadeDuration = 1f;
        CanvasGroup canvasGroup = worldCanvas.GetComponent<CanvasGroup>();
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = 1 - Mathf.Clamp01(elapsedTime / fadeDuration);
            yield return null;
        }
    }

    void EndTutorial()
    {
        // Handle end of tutorial
    }
}
