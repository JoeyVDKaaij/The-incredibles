using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PullInteraction : XRBaseInteractable
{
    public static event Action<float> PullActionReleased;

    [SerializeField] private Transform start;
    [SerializeField] private GameObject notch;
    [SerializeField] private float oscillationFrequency = 10f; // Controls how fast the spring oscillates
    [SerializeField] private float dampingRatio = 0.6f; // Controls the damping, lower values for more bounciness

    [Space(10)]
    [Header("CHANGE ONLY IF NECESSARY")]
    [SerializeField, Tooltip("This variable controls where the string stops so that it creates a realistic <spring-back> effect. Adjust this value if the bow model is different.")]
    private float stringStopPosition = -0.08f; // It is calculated based on the string's (LineRenderer's) local position

    public float pullAmount { get; private set; } = 0.0f;

    private LineRenderer lineRenderer;
    private IXRSelectInteractor pullingInteractor = null;

    private Vector3 notchInitialPosition;
    private Vector3[] linePositions = new Vector3[3];

    private AudioSource _audioSource;

    protected override void Awake()
    {
        base.Awake();
        lineRenderer = GetComponent<LineRenderer>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        notchInitialPosition = lineRenderer.GetPosition(1);  // Assuming the middle position in the inspector
        ResetString();
    }

    public void SetPullInteractor(SelectEnterEventArgs args)
    {
        pullingInteractor = args.interactorObject;
    }

    public void Release()
    {
        PullActionReleased?.Invoke(pullAmount);
        pullingInteractor = null;
        pullAmount = 0.0f;
        StartCoroutine(SpringBackNotch());
        PlayReleaseSound();
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);
        if (updatePhase == XRInteractionUpdateOrder.UpdatePhase.Dynamic)
        {
            if (isSelected)
            {
                Vector3 pullPosition = pullingInteractor.transform.position;
                pullAmount = CalculatePull(pullPosition);
                UpdateString(pullPosition);
            }
        }
    }

    private float CalculatePull(Vector3 pullPosition)
    {
        Vector3 pullDirection = pullPosition - start.position;
        float pullDistance = pullDirection.magnitude;

        // Calculate pull amount based on distance, no clamping
        return pullDistance;
    }

    private void UpdateString(Vector3 pullPosition)
    {
        lineRenderer.GetPositions(linePositions);
        linePositions[1].z = notchInitialPosition.z + start.InverseTransformPoint(pullPosition).z; // Update middle point with pull position
        if (linePositions[1].z > stringStopPosition) //ADJUST THIS VALUE IN CASE THE BOW MODEL IS DIFFERENT
        {
            linePositions[1].z = stringStopPosition;
        }
        lineRenderer.SetPositions(linePositions);
        notch.transform.localPosition = linePositions[1];
    }

    private void ResetString()
    {
        lineRenderer.GetPositions(linePositions);
        linePositions[1] = notchInitialPosition; // Reset middle position to initial
        lineRenderer.SetPositions(linePositions);
        notch.transform.localPosition = linePositions[1];
    }

    private System.Collections.IEnumerator SpringBackNotch()
    {
        // Initial velocity and position
        Vector3 currentVelocity = Vector3.zero;
        Vector3 currentMiddlePos = lineRenderer.GetPosition(1);
        Vector3 targetPosition = notchInitialPosition;

        // Parameters for spring-damping system
        float stiffness = oscillationFrequency * oscillationFrequency; // Spring stiffness
        float damping = 2.0f * dampingRatio * oscillationFrequency;    // Damping coefficient
        float mass = 1.0f;
        float randomFactor = 0.1f; // Adjust for randomness in the release

        while (Vector3.Distance(currentMiddlePos, targetPosition) > 0.01f || currentVelocity.magnitude > 0.01f)
        {
            float deltaTime = Time.deltaTime;

            // Calculate the spring force
            Vector3 displacement = currentMiddlePos - targetPosition;
            Vector3 springForce = -stiffness * displacement;
            Vector3 dampingForce = -damping * currentVelocity;
            Vector3 force = springForce + dampingForce;

            // Introduce a slight randomness to the force to simulate real-world variations
            force += new Vector3(
                UnityEngine.Random.Range(-randomFactor, randomFactor),
                UnityEngine.Random.Range(-randomFactor, randomFactor),
                UnityEngine.Random.Range(-randomFactor, randomFactor)
            );

            // Update velocity and position using a simple Euler integration
            currentVelocity += force / mass * deltaTime;
            currentMiddlePos += currentVelocity * deltaTime;

            if(currentMiddlePos.z > stringStopPosition) //ADJUST THIS VALUE IN CASE THE BOW MODEL IS DIFFERENT
            {
                currentMiddlePos.z = stringStopPosition;
            }

            // Update the line renderer
            linePositions[1] = currentMiddlePos;
            lineRenderer.SetPositions(linePositions);
            notch.transform.localPosition = linePositions[1];

            yield return null;
        }

        // Ensure the final position is exactly the initial position
        linePositions[1] = notchInitialPosition;
        lineRenderer.SetPositions(linePositions);
        notch.transform.localPosition = linePositions[1];
    }



    private void PlayReleaseSound()
    {
        _audioSource.Play();
    }

    ///DEBUG
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log("Pressed the RELEASE DEBUG KEY");
            PullActionReleased?.Invoke(10f);
        }
    }
}
