using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BridgeGenerationScript))]
public class BridgeGenerationScriptEditor : Editor
{
    private SerializedProperty _plankGameObjectsProp;
    private SerializedProperty _generatePlanksAmountProp;
    private SerializedProperty _generateXAxisProp;
    private SerializedProperty _generateYAxisProp;
    private SerializedProperty _generateZAxisProp;
    private SerializedProperty _positionPaddingProp;
    
    private void OnEnable()
    {
        _plankGameObjectsProp = serializedObject.FindProperty("plankGameObjects");
        _generatePlanksAmountProp = serializedObject.FindProperty("generatePlanksAmount");
        _generateXAxisProp = serializedObject.FindProperty("generateXAxis");
        _generateYAxisProp = serializedObject.FindProperty("generateYAxis");
        _generateZAxisProp = serializedObject.FindProperty("generateZAxis");
        _positionPaddingProp = serializedObject.FindProperty("positionPadding");
    }

    public override void OnInspectorGUI()
    {
        
        serializedObject.Update();
        
        EditorGUILayout.PropertyField(_plankGameObjectsProp);
        EditorGUILayout.PropertyField(_generatePlanksAmountProp);
        EditorGUILayout.PropertyField(_generateXAxisProp);
        EditorGUILayout.PropertyField(_generateYAxisProp);
        EditorGUILayout.PropertyField(_generateZAxisProp);
        EditorGUILayout.PropertyField(_positionPaddingProp);
        
        serializedObject.ApplyModifiedProperties();
        
        BridgeGenerationScript script = (BridgeGenerationScript)target;
        
        if (Application.isPlaying) 
            if (GUILayout.Button("Regenerate Bridge")) script.GenerateBridge();
    }
}
