using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    [Header("Player")]
    [SerializeField, Tooltip("Please drag in the reference to the player camera")] private GameObject PlayerCamera;

    [Space(5)]
    [SerializeField] private Canvas worldCanvas;
    [SerializeField] private TextMeshProUGUI instructionText;
    [SerializeField] private Image instructionImage;
    [SerializeField] private List<TutorialStep> tutorialSteps;

    private Queue<TutorialStep> stepsQueue;

    void Start()
    {
        if (PlayerCamera == null)
        {
            Debug.LogError("Player camera reference not set in TutorialManager");
        }
        foreach (TutorialStep step in tutorialSteps) // Initialize all steps adding the player reference
        {
            step.Init(PlayerCamera);
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
        //EndTutorial();
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
        if (step.StarterStep)
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

    public void FadeInUINonCoroutine()
    {
        float elapsedTime = 0f;
        float fadeDuration = 1f;
        CanvasGroup canvasGroup = worldCanvas.GetComponent<CanvasGroup>();
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
        }
    }

    public void FadeOutUINonCoroutine()
    {
        float elapsedTime = 0f;
        float fadeDuration = 1f;
        CanvasGroup canvasGroup = worldCanvas.GetComponent<CanvasGroup>();
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = 1 - Mathf.Clamp01(elapsedTime / fadeDuration);
        }
    }

    void EndTutorial()
    {
        // Handle end of tutorial
    }
}
