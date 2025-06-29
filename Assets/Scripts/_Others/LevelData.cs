using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "levelData_Default", menuName = "New LevelData", order = 100000)]
public class LevelData : ScriptableObject
{
    private string key => $"LEVEL_DATA_{LevelId}";
    private string starsKey => $"{key}_STARS";

    [SerializeField] private int levelId = -1;
    [SerializeField] private string gameSceneName = "Scene";
    [SerializeField] private List<string> levelSceneNames = new List<string>();
    [SerializeField] private int maxStars = 3;

    public int LevelId => levelId;
    public string GameSceneName => gameSceneName;
    public List<string> LevelSceneNames => levelSceneNames;
    public int MaxStars => maxStars;

    public int Stars
    {
        get => Manager.Instance.GetManager<SaveManager>().GetValue(starsKey, 0);
        set => Manager.Instance.GetManager<SaveManager>().SetValue(starsKey, value);
    }
}
