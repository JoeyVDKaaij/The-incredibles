using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlankBreaking))]
public class PlankBreakingEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        serializedObject.Update();
        PlankBreaking script = (PlankBreaking)target;
        
        if (script.triggerColliderTriggerBool == false)
            EditorGUILayout.HelpBox("The collider assigned is not set as trigger!. Please assign the trigger collider!", MessageType.Warning);

        serializedObject.ApplyModifiedProperties();
    }
}
