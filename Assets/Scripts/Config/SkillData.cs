using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName ="ScriptableObject/Skill")]
public class SkillData : ScriptableObject
{
    public int Id = 1;
    public string skillName;
    [PreviewField,  AssetSelector(Paths = "Assets/Sprite/Skill")]
    public Sprite icon;

    [SerializeField]
    public List<ActionData> actions;

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
            var so = UnityEditor.AssetDatabase.LoadAssetAtPath<SkillData>(assetPath);
            if (so != this)
            {
                return;
            }
            var fileName = Path.GetFileName(assetPath);
            var newName = $"Skill_{Id}";
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
public class ActionData
{
    // 使用此object 的 name 拿子类的Type 进而实例化指定Action
    [AssetsOnly, AssetSelector(Paths = "Assets/Scripts/SkillAction")]
    public UnityEngine.Object scriptInfo;
    public float offsetTime = 0f;
    public AttrType attrType = AttrType.Hp;
    public int numerical1 = 10;   // 伤害、Buff数值等
}
