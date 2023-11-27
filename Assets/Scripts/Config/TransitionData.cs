using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System.IO;
using System.Linq;
using System.Reflection;

[CreateAssetMenu(menuName ="ScriptableObject/Transition")]
public class TransitionData : ScriptableObject
{
    public int Id = 1;
    public string Name = "AvA!";
    public Color color;
    [AssetsOnly, AssetSelector(Paths = "Assets/Sprite/Transitions")]
    public Sprite sprite1;
    [AssetsOnly, AssetSelector(Paths = "Assets/Sprite/Transitions")]
    public Sprite sprite2;
    [AssetsOnly, AssetSelector(Paths = "Assets/Sprite/Transitions")]
    public Sprite sprite3;
    [AssetsOnly, AssetSelector(Paths = "Assets/Sprite/Transitions")]
    public Sprite sprite4;
    [AssetsOnly, AssetSelector(Paths = "Assets/Sprite/Transitions")]
    public Sprite sprite5;

    #region Editor
    #if UNITY_EDITOR
    [OnInspectorGUI]
    private void OnInspectorGUI()
    {
        RenameFile();
    }
    private void RenameFile()
    {
        string[] guids = UnityEditor.Selection.assetGUIDs;
        int i = guids.Length;
        if (i == 1)
        {
            string guid = guids[0];
            string assetPath = UnityEditor.AssetDatabase.GUIDToAssetPath(guid);
            var so = UnityEditor.AssetDatabase.LoadAssetAtPath<TransitionData>(assetPath);
            if (so != this)
            {
                return;
            }
            var fileName = Path.GetFileName(assetPath);
            var newName = $"Transition_{Id}";
            if (fileName != newName)
            {
                UnityEditor.AssetDatabase.RenameAsset(assetPath, newName);
            }
        }
    }
    #endif
    #endregion
}
