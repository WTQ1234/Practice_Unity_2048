using UnityEditor;
using UnityEditor.UI;
using UnityEngine;

[CustomEditor(typeof(ShapeImage), true)]
public class ShapeImageEditor : ImageEditor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        ShapeImage image = (ShapeImage)target;
        SerializedProperty sp = serializedObject.FindProperty("offset");
        EditorGUILayout.PropertyField(sp, new GUIContent("offset 倾斜偏移"));
        serializedObject.ApplyModifiedProperties();
    }
}
