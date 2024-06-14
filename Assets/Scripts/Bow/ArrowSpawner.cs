using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ArrowSpawner : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private GameObject notch;

    [Space(2)]
    [Header("Options")]
    [SerializeField, Tooltip("The time it takes for the arrow to get spawned on the bow")] private float arrowTimeSpawn = 1f;


    private XRGrabInteractable _bow;
    private bool _arrowNotched = false;
    private GameObject _currentArrow = null;

    private void Start()
    {
        _bow = GetComponent<XRGrabInteractable>();
        PullInteraction.PullActionReleased += NotchEmpty;
    }

    private void OnDestroy()
    {
        PullInteraction.PullActionReleased -= NotchEmpty;
    }

    private void Update()
    {
        if(_bow.isSelected && _arrowNotched == false)
        {
            _arrowNotched = true;
            StartCoroutine(DelayedSpawn());
        }
        if(!_bow.isSelected && _currentArrow != null)
        {
            _arrowNotched = false;
            Destroy(_currentArrow);
        }
    }

    private void NotchEmpty(float value)
    {
        _arrowNotched = false;
        _currentArrow = null;
    }

    private IEnumerator DelayedSpawn()
    {
        yield return new WaitForSeconds(arrowTimeSpawn);
        _currentArrow = Instantiate(arrowPrefab, notch.transform);
    }
}
