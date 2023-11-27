using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System.IO;
using System.Linq;
using System.Reflection;

[CreateAssetMenu(menuName ="ScriptableObject/Enemy")]
public class EnemyData : ScriptableObject
{
    public int Id = 1;
    public string Name;
    public string Desc;
    public List<SkillData> skills;
    [SerializeField]
    public List<AttrData> attrs;

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
            var so = UnityEditor.AssetDatabase.LoadAssetAtPath<EnemyData>(assetPath);
            if (so != this)
            {
                return;
            }
            var fileName = Path.GetFileName(assetPath);
            var newName = $"Enemy_{Id}";
            if (fileName != newName)
            {
                UnityEditor.AssetDatabase.RenameAsset(assetPath, newName);
            }
        }
    }
    #endif
    #endregion
}

[Serializable]
public class AttrData
{
    public AttrType attrType = AttrType.Hp;
    public float attrValue = 0;
}
