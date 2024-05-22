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
                DestroyImmediate(transform.GetChild(i).gameObject);
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
                if (generateXAxis && generateZAxis)
                    _spawnPoint.x += Mathf.Sqrt(Mathf.Pow(plank.transform.lossyScale.x,2)/3) + positionPadding;
                else
                    _spawnPoint.x += (generateYAxis||generateZAxis?Mathf.Sqrt(Mathf.Pow(plank.transform.lossyScale.x,2)/2):plank.transform.lossyScale.x) + positionPadding;
            }
            if (generateYAxis)
            {
                if (generateXAxis && generateZAxis)
                    _spawnPoint.y += Mathf.Sqrt(Mathf.Pow(plank.transform.lossyScale.x,2)/3) + positionPadding;
                else
                    _spawnPoint.y += (generateXAxis||generateZAxis?Mathf.Sqrt(Mathf.Pow(plank.transform.lossyScale.x,2)/2):plank.transform.lossyScale.x) + positionPadding;
                if (generateXAxis || generateZAxis)
                    rotation.z = generateXAxis?45:-45;
                else
                    rotation.z = 90;
            }
            if (generateZAxis)
            {
                if (generateXAxis && generateZAxis)
                    _spawnPoint.z += Mathf.Sqrt(Mathf.Pow(plank.transform.lossyScale.x,2)/3) + positionPadding;
                else
                    _spawnPoint.z += (generateXAxis||generateYAxis?Mathf.Sqrt(Mathf.Pow(plank.transform.lossyScale.x,2)/2):plank.transform.lossyScale.x) + positionPadding;
                rotation.y = generateXAxis?-45:90;
            }

            plank.transform.rotation = Quaternion.Euler(rotation);
            
            Rigidbody rb = plank.GetComponent<Rigidbody>();
            HingeJoint hj = plank.GetComponent<HingeJoint>();
            
            if (i == 0 || i  == generatePlanksAmount -1)
            {
                if (i == 0) DestroyImmediate(hj);
                rb.useGravity = false;
                rb.isKinematic = true;
            }
            if (i > 0)
            {
                hj.connectedBody = transform.GetChild(i - 1).GetComponent<Rigidbody>();
            }
        }
    }
}
