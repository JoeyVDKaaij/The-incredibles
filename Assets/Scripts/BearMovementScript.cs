using UnityEditor;
using UnityEngine;

[System.Serializable]
public class Waypoint
{
    [Tooltip("Set the position of the waypoint.")]
    public Vector3 position;
    [Tooltip("Set the time until the object starts moving."), Min(0)]
    public float timeUntilReturnMoving = 0;
    [Tooltip("Set the speed between 0 to 2 that gets multiplied with the current movement speed."), Range(0,2)]
    public float movementSpeedMultiplier = 1;
}

public class BearMovementScript : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField, Tooltip("Set the movement speed in units per second."), Min(0)]
    private float movementSpeed = 1;
    [SerializeField, Tooltip("Set the waypoints.")]
    private Waypoint[] waypoints = null;
    [SerializeField, Tooltip("Set to true if the object should return to the first waypoint after reaching the last.")]
    private bool loopWaypoints;

    private float _startTime, _journeyLength, _timer;
    private int _currentWaypoint;
    private bool stopMoving;

    public Vector3 placedPosition;
    private Vector3 OldPosition;
    
    private void Start()
    {
        placedPosition = transform.position;
        OldPosition = transform.position;
        
        if (waypoints != null && waypoints.Length > 0)
        {
            if (waypoints.Length > 1)
                _journeyLength = Vector3.Distance(waypoints[_currentWaypoint].position, waypoints[_currentWaypoint+1].position);
            
            Debug.Log(waypoints.Length);
        }
        else Debug.LogError("No waypoints are set.");

        _startTime = Time.time;
            
        transform.hasChanged = false;
    }
    
    private void OnDrawGizmos()
    {
        if (waypoints != null)
        {
            if (Application.isPlaying)
            {
                for (int i = 0; i < waypoints.Length; i++)
                {
                    Gizmos.color = Color.yellow;
                    if (i + 1 < waypoints.Length && waypoints.Length > 1)
                        Gizmos.DrawLine(placedPosition + waypoints[i].position,
                            placedPosition + waypoints[i + 1].position);
                    else if (waypoints.Length > 2 && loopWaypoints)
                        Gizmos.DrawLine(placedPosition + waypoints[waypoints.Length - 1].position,
                            placedPosition + waypoints[0].position);
                }
            }
            else
            {
                for (int i = 0; i < waypoints.Length; i++)
                {
                    Gizmos.color = Color.yellow;
                    if (i + 1 < waypoints.Length && waypoints.Length > 1)
                        Gizmos.DrawLine(transform.position + waypoints[i].position,
                            transform.position + waypoints[i + 1].position);
                    else if (waypoints.Length > 2 && loopWaypoints)
                        Gizmos.DrawLine(transform.position + waypoints[waypoints.Length - 1].position,
                            transform.position + waypoints[0].position);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.hasChanged)
        {
            transform.hasChanged = false;
            placedPosition += transform.position - OldPosition;
        }
        
        if (waypoints != null && waypoints.Length > 0)
        {
            if (_currentWaypoint + 1 >= waypoints.Length && !loopWaypoints)
            {
                _startTime = Time.time;
                return;
            }

            if (_timer < waypoints[_currentWaypoint].timeUntilReturnMoving)
            {
                _startTime = Time.time;
                _timer += Time.deltaTime;
                return;
            }
            
            float distCovered = (Time.time - _startTime) * (movementSpeed * waypoints[_currentWaypoint].movementSpeedMultiplier);

            if (_journeyLength == 0)
                _journeyLength = 1;
            float fractionOfJourney = Mathf.Clamp01(distCovered / _journeyLength);

            fractionOfJourney = Mathf.Clamp01(fractionOfJourney);

            if (_currentWaypoint + 1 < waypoints.Length)
                transform.position = Vector3.Lerp(placedPosition + waypoints[_currentWaypoint].position, placedPosition + waypoints[_currentWaypoint + 1].position, fractionOfJourney);
            else
                transform.position = Vector3.Lerp(placedPosition + waypoints[_currentWaypoint].position, placedPosition + waypoints[0].position, fractionOfJourney);

            if (fractionOfJourney >= 1f)
            {
                if (_currentWaypoint + 1 < waypoints.Length)
                {
                    _currentWaypoint++;
                    if (_currentWaypoint + 1 < waypoints.Length)
                    {
                        _journeyLength = Vector3.Distance(waypoints[_currentWaypoint].position,
                            waypoints[_currentWaypoint + 1].position);
                    }
                    _timer = 0;
                    _startTime = Time.time;
                }
                else if (loopWaypoints)
                {
                    _currentWaypoint = 0;
                    _journeyLength = Vector3.Distance(waypoints[_currentWaypoint].position, waypoints[0].position);
                    _timer = 0;
                    _startTime = Time.time;
                }
            }
        }

        OldPosition = transform.position;
        transform.hasChanged = false;
    }

    public Waypoint[] Waypoint
    {
        get { return waypoints; }
    }

    public bool WaypointLoop
    {
        get { return loopWaypoints; }
    }
}
