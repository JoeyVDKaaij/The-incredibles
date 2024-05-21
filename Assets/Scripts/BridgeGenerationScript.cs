using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class BridgeGenerationScript : MonoBehaviour
{
    [Header("Bridge Generation Settings")]
    [SerializeField, Tooltip("Set the planks game objects that spawn on the bridge.")]
    private GameObject[] plankGameObjects = null;
    [SerializeField, Tooltip("Set how many planks should be generated."), Min(1)]
    private int generatePlanksAmount = 1;
    [SerializeField, Tooltip("Set to true if the planks should be generated along the x Axis.")]
    private bool generateXAxis = true;
    [SerializeField, Tooltip("Set to true if the planks should be generated along the y Axis.")]
    private bool generateYAxis = false;
    [SerializeField, Tooltip("Set to true if the planks should be generated along the z Axis.")]
    private bool generateZAxis = false;
    [SerializeField, Tooltip("Set the padding between planks."), Min(0)]
    private float positionPadding = 1f;
    [SerializeField, Tooltip("Set the padding between planks.")]
    private float lowestPossiblePosition = -5f;

    private bool[] _oldGenerateBools = new [] { true, false, false };
    
    private Vector3 _spawnPoint;
    
    private void Start()
    {
        GenerateBridge();
    }

    private void OnValidate()
    {
        if (!generateXAxis && !generateYAxis && !generateZAxis)
        {
            generateXAxis = _oldGenerateBools[0];
            generateYAxis = _oldGenerateBools[1];
            generateZAxis = _oldGenerateBools[2];
        }
        
        _oldGenerateBools[0] = generateXAxis;
        _oldGenerateBools[1] = generateYAxis;
        _oldGenerateBools[2] = generateZAxis;
    }

    public void GenerateBridge()
    {
        if (transform.childCount > 0)
        {
            for (int i = transform.childCount-1; i >= 0; i--)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }
        
        _spawnPoint = transform.position;
        for(int i = 0; i < generatePlanksAmount; i++)
        {
            GameObject plank = Instantiate(plankGameObjects[Random.Range(0, plankGameObjects.Length)], transform);

            plank.transform.position = _spawnPoint;
            
            Vector3 rotation = Vector3.zero;
            
            if (generateXAxis)
            {
                _spawnPoint.x += plank.transform.lossyScale.x + positionPadding;
            }
            if (generateYAxis)
            {
                _spawnPoint.y += plank.transform.lossyScale.x + positionPadding;
                if (generateXAxis || generateZAxis)
                    rotation.z = generateXAxis?45:-45;
                else
                    rotation.z = 90;
            }
            if (generateZAxis)
            {
                _spawnPoint.z += plank.transform.lossyScale.x + positionPadding;
                rotation.y = generateXAxis?-45:90;
            }
            
            
            float plankComparedToLowestPlank = (float)i / (generatePlanksAmount-1);
            
            plank.transform.localPosition = new Vector3(plank.transform.position.x, Mathf.Sin(plankComparedToLowestPlank * Mathf.PI) * lowestPossiblePosition);

            plank.transform.localRotation = Quaternion.Euler(rotation);
        }

        int childCount = transform.childCount;

        GameObject middleChild = transform.GetChild(Mathf.Abs(childCount/2)-1).gameObject;
        
        for (int i = 0; i < childCount; i++)
        {
            Vector3 rotation = transform.GetChild(i).rotation.eulerAngles;

            float degrees = Mathf.Atan2(middleChild.transform.localPosition.y, middleChild.transform.localPosition.x);
            degrees = -Mathf.Rad2Deg;
            Debug.Log(degrees);
            
            rotation.z += Mathf.Cos((float)i / (childCount-1) * Mathf.PI) * degrees;
            
            transform.GetChild(i).transform.localRotation = Quaternion.Euler(rotation);
        }
    }
}