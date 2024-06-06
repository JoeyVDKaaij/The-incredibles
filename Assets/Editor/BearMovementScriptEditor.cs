using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BearMovementScript))]
public class BearMovementScriptEditor : Editor
{
    // Custom in-scene UI for when ExampleScript
    // component is selected.
    public void OnSceneGUI()
    {
        var t = target as BearMovementScript;
        for (int i = 0; i < t.Waypoint.Length; i++)
        {
            GUI.contentColor = Color.black;
            
            Handles.color = Color.gray;

            Handles.color = Color.black;
            Handles.Label(t.transform.localPosition + t.Waypoint[i].position, (i).ToString());
        }
    }
}
