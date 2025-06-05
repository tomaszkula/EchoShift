using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "levelData_Default", menuName = "New LevelData", order = 100000)]
public class LevelData : ScriptableObject
{
    [SerializeField] private string levelName = "Level";
    [SerializeField] private string gameSceneName = "Scene";
    [SerializeField] private List<string> levelSceneNames = new List<string>();

    public string LevelName => levelName;
    public string GameSceneName => gameSceneName;
    public List<string> LevelSceneNames => levelSceneNames;
}
