using UnityEditor;
using UnityEditor.UI;
using UnityEngine;

[CustomEditor(typeof(ShapeRawImage), true)]
public class ShapeRawImageEditor : RawImageEditor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        ShapeRawImage image = (ShapeRawImage)target;
        SerializedProperty sp = serializedObject.FindProperty("offset");
        EditorGUILayout.PropertyField(sp, new GUIContent("offset 倾斜偏移"));
        serializedObject.ApplyModifiedProperties();
    }
}
