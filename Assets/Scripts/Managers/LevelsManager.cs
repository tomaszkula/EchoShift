using System.Collections.Generic;
using UnityEngine;

public class LevelsManager : BaseManager
{
    [Header("Settings")]
    [SerializeField] private List<LevelData> levelsData = new List<LevelData>();

    public bool IsLevelLoaded { get; private set; } = false;

    public async void Load()
    {
        IsLevelLoaded = false;

        LevelData levelData = levelsData[0];

        await Manager.Instance.GetManager<ScenesManager>().LoadMainScene(levelData.GameSceneAsset.name);

        for (int i = 0; i < levelData.LevelSceneAssets.Count; i++)
        {
            await Manager.Instance.GetManager<ScenesManager>().LoadAdditionalScene(levelData.LevelSceneAssets[i].name);
        }

        IsLevelLoaded = true;
    }
}
