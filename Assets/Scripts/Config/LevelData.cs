using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System.IO;
using System.Linq;
using System.Reflection;

[CreateAssetMenu(menuName ="ScriptableObject/Level")]
public class LevelData : ScriptableObject
{
    // 是否是禅模式
    public bool isSingle = false;

    public int Id = 1;
    // 敌人的id
    public int enemyId;
    // 玩家的id
    public int playerId;
    // 难度系数-按倍率增加属性
    // todo  考虑拆分成单个属性的额外系数，比如攻击加倍等
    public float difficultyFactor = 1f;

    // 界面——三个模式的界面可能有所 不同
    public Dialog dialog;

    // todo 对话List 想办法支持多语言，暂时先不用支持选择

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
            var so = UnityEditor.AssetDatabase.LoadAssetAtPath<LevelData>(assetPath);
            if (so != this)
            {
                return;
            }
            var fileName = Path.GetFileName(assetPath);
            var newName = $"Level_{Id}";
            if (fileName != newName)
            {
                UnityEditor.AssetDatabase.RenameAsset(assetPath, newName);
            }
        }
    }
    #endif
    #endregion
}
