using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "levelData_Default", menuName = "New LevelData", order = 100000)]
public class LevelData : ScriptableObject
{
    public string LevelName;
    public SceneAsset GameSceneAsset = null;
    public List<SceneAsset> LevelSceneAssets = new List<SceneAsset>();
}
