using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

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

    private void Start()
    {
        if (waypoints != null && waypoints.Length > 0)
        {
            transform.position = waypoints[0].position;
            if (waypoints.Length > 1)
                _journeyLength = Vector3.Distance(waypoints[_currentWaypoint].position, waypoints[_currentWaypoint+1].position);
            
            Debug.Log(waypoints.Length);
        }
        else Debug.LogError("No waypoints are set.");

        _startTime = Time.time;
    }
    
    private void OnDrawGizmos()
    {
        for (int i = 0; i < waypoints.Length; i++)
        {
            Color previousContentColor = GUI.contentColor;
            GUI.contentColor = Color.black;

            Handles.color = Color.gray;

            Handles.color = Color.black;
            Handles.Label(transform.localPosition + waypoints[i].position, (i).ToString());

            GUI.contentColor = previousContentColor;

            Gizmos.color = Color.yellow;
            if (i + 1 < waypoints.Length && waypoints.Length > 1)
                Gizmos.DrawLine(transform.localPosition + waypoints[i].position, transform.localPosition + waypoints[i + 1].position);
            else if (waypoints.Length > 2 && loopWaypoints)
                Gizmos.DrawLine(transform.localPosition + waypoints[waypoints.Length - 1].position, transform.localPosition + waypoints[0].position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (waypoints != null && waypoints.Length > 0)
        {
            if (_currentWaypoint + 1 >= waypoints.Length && !loopWaypoints)
                return;

            if (_timer < waypoints[_currentWaypoint].timeUntilReturnMoving)
            {
                _timer += Time.deltaTime;
                return;
            }
            
            float distCovered = (Time.time - _startTime) * (movementSpeed * waypoints[_currentWaypoint].movementSpeedMultiplier);

            if (_journeyLength == 0)
                _journeyLength = 1;
            float fractionOfJourney = Mathf.Clamp01(distCovered / _journeyLength);

            fractionOfJourney = Mathf.Clamp01(fractionOfJourney);

            if (_currentWaypoint + 1 < waypoints.Length)
                transform.position = Vector3.Lerp(waypoints[_currentWaypoint].position, waypoints[_currentWaypoint + 1].position, fractionOfJourney);
            else
                transform.position = Vector3.Lerp(waypoints[_currentWaypoint].position, waypoints[0].position, fractionOfJourney);

            if (fractionOfJourney >= 1f)
            {
                _startTime = Time.time;
                
                if (_currentWaypoint + 1 < waypoints.Length)
                {
                    _currentWaypoint++;
                    if (_currentWaypoint + 1 < waypoints.Length)
                        _journeyLength = Vector3.Distance(waypoints[_currentWaypoint].position, waypoints[_currentWaypoint + 1].position);
                    else 
                        _journeyLength = Vector3.Distance(waypoints[_currentWaypoint].position, waypoints[0].position);
                    _timer = 0;
                }
                else if (loopWaypoints)
                {
                    _currentWaypoint = 0;
                    _journeyLength = Vector3.Distance(waypoints[_currentWaypoint].position, waypoints[_currentWaypoint+1].position);
                    _timer = 0;
                }
            }
        }
    }
}
