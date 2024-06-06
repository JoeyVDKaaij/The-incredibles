using UnityEngine;

[RequireComponent(typeof(Transform))]
public class FluctuateSinWave : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField, Tooltip("The amplitude of the sin wave.")] private float amplitude = 1f;
    private Transform objectTransform;

    private void Awake()
    {
        objectTransform = GetComponent<Transform>();
    }

    private void Fluctuate()
    {
        objectTransform.position = new Vector3(objectTransform.position.x, objectTransform.position.y + Mathf.Sin(Time.time) * amplitude * Time.deltaTime, objectTransform.position.z);
    }

    private void Update()
    {
        Fluctuate();
    }
}
